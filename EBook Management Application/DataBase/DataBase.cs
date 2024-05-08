using Microsoft.Data.SqlClient;
using Models;
using Service.Interface;
using System.Data;
using Ebook_Management_Application.DataBase;
using System.Net;

namespace Ebook_Management_Application.DataBase
{
    public class DataBaseManager : IBookService

    {
        //DataBaseManager dataBaseManager = new DataBaseManager();
        private readonly string _connectionString = "Data Source= BLRSFLT283;Initial Catalog=Ebookk;Integrated Security = True; Trusted_Connection=true; encrypt=false";



        public List<Book> GetAllBook()
        {
            List<Book> _booksDatabase = new List<Book>();

            using SqlConnection connection = new SqlConnection(_connectionString);
            try
            {
                connection.Open();
                var storedproc = "GetAllBook";
                using SqlCommand command = new SqlCommand(storedproc, connection);

                command.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Book bok = MapReadertoBook(reader);


                    _booksDatabase.Add(bok);
                }

                return _booksDatabase;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while fetching books: {ex.Message}");
                return _booksDatabase;
            }



        }

        public Book MapReadertoBook(SqlDataReader reader)
        {
            var bok = new Book();
            bok.BookID = reader.GetGuid(0);
            bok.Title = reader.GetString(1);
            bok.Description = reader.GetString(2);
            bok.ISBN = reader.GetInt32(3);
            bok.PublicationDate = reader.GetDateTime(4);
            bok.Price = reader.GetInt32(5);
            bok.Language = reader.GetString(6);
            bok.Publisher = reader.GetString(7);
            bok.PageCount = reader.GetInt32(8);
            bok.AverageRating = reader.GetDouble(9);
            bok.GenreID = reader.GetInt32(12);

            return bok;

        }

        public Book GetBookById(Guid ID)
        {
            Book book = null;

            using SqlConnection connection = new SqlConnection(_connectionString);
            try
            {
                connection.Open();
                var storedProcedure = "GetBookById";
                using SqlCommand command = new SqlCommand(storedProcedure, connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Id", ID);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    book = MapReadertoBook(reader);
                }

                return book;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public string AddBook(BookDTO book)
        {
            int rowsAffected;

            try {
                Book ee = new Book(book);

                using SqlConnection connection = new SqlConnection(_connectionString);
                connection.Open();
                var storedProcName = "InsertBook";

                DataTable authorTable = new DataTable();
                authorTable.Columns.Add("ID", typeof(Guid));
                foreach (var ID in book.ID)
                {
                    authorTable.Rows.Add(ID);
                }
                SqlCommand command = new SqlCommand(storedProcName, connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@BookID", SqlDbType.UniqueIdentifier).Value = ee.BookID;
                command.Parameters.Add("@Title", SqlDbType.VarChar).Value = ee.Title;
                command.Parameters.Add("@Description", SqlDbType.VarChar).Value = ee.Description;
                command.Parameters.Add("@ISBN", SqlDbType.Int).Value = ee.ISBN;
                command.Parameters.Add("@PublicationDate", SqlDbType.DateTime).Value = ee.PublicationDate;
                command.Parameters.Add("@Price", SqlDbType.Int).Value = ee.Price;
                command.Parameters.Add("@Language", SqlDbType.VarChar).Value = ee.Language;
                command.Parameters.Add("@Publisher", SqlDbType.VarChar).Value = ee.Publisher;
                command.Parameters.Add("@PageCount", SqlDbType.Int).Value = (int)ee.PageCount;
                command.Parameters.Add("@AverageRating", SqlDbType.Float).Value = (float)ee.AverageRating;


                command.Parameters.Add("@Created_At", SqlDbType.DateTime).Value = ee.getCreated_At();

                command.Parameters.AddWithValue("@updated_At", ee.getCreated_At());
                command.Parameters.Add("@GenreID", SqlDbType.Int).Value = ee.GenreID;

                SqlParameter IDsParameter = command.Parameters.AddWithValue("@IDs", authorTable);
                IDsParameter.SqlDbType = SqlDbType.Structured;
                IDsParameter.TypeName = "dbo.IDList";


                command.ExecuteNonQuery();
                return "Book added successfully";
            }
            catch (Exception ex)
            {
                return "Book Add Unsucessfull";
            }



        }

        public string UpdateBook(Book e)
        {
            DateTime current = DateTime.Now;
            try
            {
                Book ff = new Book();
                using SqlConnection connection = new SqlConnection(_connectionString);
                connection.Open();

                string UpdateQuery = "UpdateBook";

                DataTable authorTable = new DataTable();
                authorTable.Columns.Add("ID", typeof(Guid));
                foreach (var ID in e.ID)
                {
                    authorTable.Rows.Add(ID);
                }
                SqlCommand command = new SqlCommand(UpdateQuery, connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("@BookID", SqlDbType.UniqueIdentifier).Value = e.BookID;
                command.Parameters.Add("@Title", SqlDbType.VarChar).Value = e.Title;
                command.Parameters.Add("@Description", SqlDbType.VarChar).Value = e.Description;
                command.Parameters.Add("@ISBN", SqlDbType.Int).Value = e.ISBN;
                command.Parameters.Add("@PublicationDate", SqlDbType.DateTime).Value = e.PublicationDate;
                command.Parameters.Add("@Price", SqlDbType.Int).Value = e.Price;
                command.Parameters.Add("@Language", SqlDbType.VarChar).Value = e.Language;
                command.Parameters.Add("@Publisher", SqlDbType.VarChar).Value = e.Publisher;
                command.Parameters.Add("@PageCount", SqlDbType.Int).Value = (int)e.PageCount;
                command.Parameters.Add("@AverageRating", SqlDbType.Float).Value = (float)e.AverageRating;
                command.Parameters.Add("@Updated_At", SqlDbType.DateTime).Value = e.Updated_At;
                command.Parameters.Add("@GenreID", SqlDbType.Int).Value = e.GenreID;

                SqlParameter IDsParameter = command.Parameters.AddWithValue("@IDs", authorTable);
                IDsParameter.SqlDbType = SqlDbType.Structured;
                IDsParameter.TypeName = "dbo.IDList";


                command.ExecuteNonQuery();
            }
            
            catch
            {

                return "Update Unsuccesfull";
            }
            return "Book update successfull";
        }

        public List<Book> SearchBooksByTitle(string title)
        {
            List<Book> books = new List<Book>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    var storedProcedure = "SearchBooksByTitle";
                    using (SqlCommand command = new SqlCommand(storedProcedure, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Title", title);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Book book = MapReadertoBook(reader);
                                books.Add(book);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred while searching books by title: {ex.Message}");
                }
            }

            return books;
        }


        /*  public List<Book> SearchBooksByTitle(string title)
          {

              List<Book> books = new List<Book>();

              List<Book> searchResults = DataBaseManager.SearchBooksByTitle("Harry Potter");
              foreach (Book book in searchResults)
              {
                  Console.WriteLine($"Title: {book.Title}, ISBN: {book.ISBN}");
              }

              using (SqlConnection connection = new SqlConnection(_connectionString))
              {
                  try
                  {
                      connection.Open();
                      string storedProcedure = "SearchBooksByTitle";
                      using (SqlCommand command = new SqlCommand(storedProcedure, connection))
                      {
                          command.CommandType = CommandType.StoredProcedure;
                          command.Parameters.AddWithValue("@Title", title);

                          using (SqlDataReader reader = command.ExecuteReader())
                          {
                              while (reader.Read())
                              {
                                  Book book = MapReadertoBook(reader);
                                  books.Add(book);
                              }
                          }
                      }
                  }
                  catch (Exception ex)
                  {
                      Console.WriteLine($"An error occurred while searching books by title: {ex.Message}");
                  }
              }

              return books;
          }*/

        public string DeleteBook(Guid Id)
            {

                try
                {
                    using SqlConnection connection = new SqlConnection(_connectionString);
                    connection.Open();

                    string deleteQuery2 = "DeleteBook";
                    SqlCommand command = new SqlCommand(deleteQuery2, connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ID", Id);


                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    return "Book Not found";
                }

                return "Book Delete success";

            }

        public List<Book> GetBooksByGenre(int ID)
        {
            List<Book> books = new List<Book>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    var storedProcedure = "GetBooksByGenre"; 
                    using (SqlCommand command = new SqlCommand(storedProcedure, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@GenreID", ID);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Book book = MapReadertoBook(reader);
                                books.Add(book);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred while retrieving books by genre: {ex.Message}");
                }
            }

            return books;
        }


        public List<Book> GetBooksByAuthor(Guid authorId)
        {
            List<Book> books = new List<Book>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    var storedProcedure = "GetBooksByAuthor";
                    using (SqlCommand command = new SqlCommand(storedProcedure, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@AuthorId", authorId);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Book book = MapReadertoBook(reader);
                                books.Add(book);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred while retrieving books by author: {ex.Message}");
                }
            }

            return books;
        }

        //@AuthorIDs AuthorL Readonly
    }

}
