using Microsoft.Data.SqlClient;
using Models;
using Service.Interf;
using System.Data;

namespace Ebook_Management_Application.DataBase
{
    public class AuthorDataBaseM : IAuthorService
    {
        private readonly string _connectionString = "Data Source=BLRSFLT283;Initial Catalog=Ebookk;Integrated Security=True; Trusted_Connection=true;encrypt=false";

        public List<Author> GetAllAuthors()
        {
            List<Author> authors = new List<Author>();

            using SqlConnection connection = new SqlConnection(_connectionString);
            try
            {
                connection.Open();
                var storedProcedure = "GetAllAuthors";
                using SqlCommand command = new SqlCommand(storedProcedure, connection);
                command.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Author author = MapReaderToAuthor(reader);
                    authors.Add(author);
                }

                return authors;
            }
            catch (Exception ex) 
            {
                return null; 
            }
        }
        private Author MapReaderToAuthor(SqlDataReader reader)
        {
            var author = new Author();
            author.ID = reader.GetGuid(0);
            author.FirstName = reader.GetString(1);
            author.LastName = reader.GetString(2); 
            author.Biography = reader.GetString(3);
            author.BirthDate = reader.GetDateTime(4);
            author.Country = reader.GetString(5);

            return author;
        }

        public Author GetAuthorById(Guid ID)
        {
            Author author = null;

            using SqlConnection connection = new SqlConnection(_connectionString);
            try
            {
                connection.Open();
                var storedProcedure = "GetAuthorById";
                using SqlCommand command = new SqlCommand(storedProcedure, connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Id", ID);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    author = MapReaderToAuthor(reader);
                }

                return author;
            }
            catch (Exception ex)
            {
                return null; 
            }
        }

        public string AddAuthor(AuthorDTO authors)
        {
            int rowsAffected;

            try
            {
                Author aa = new Author(authors);
                using SqlConnection connection = new SqlConnection(_connectionString);
                connection.Open();
                var storedProcedure = "AddAuthor";
                SqlCommand command = new SqlCommand(storedProcedure, connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("ID", SqlDbType.UniqueIdentifier).Value = aa.ID;
                command.Parameters.Add("FirstName", SqlDbType.VarChar).Value = aa.FirstName;
                command.Parameters.Add("LastName", SqlDbType.VarChar).Value = aa.LastName;
                command.Parameters.Add("Biography", SqlDbType.VarChar).Value = aa.Biography;
                command.Parameters.Add("BirthDate", SqlDbType.DateTime).Value = aa.BirthDate;
                command.Parameters.Add("Country", SqlDbType.VarChar).Value = aa.Country;

                command.Parameters.Add("Created_At", SqlDbType.DateTime).Value = aa.getCreated_At();
                command.Parameters.Add("Updated_At", SqlDbType.DateTime).Value = aa.Updated_At;


                rowsAffected = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                return "Author Add Unsuccessful";
            }

            return "Author added successfully";
        }

        public string UpdateAuthor(Author author)
        {
            DateTime current = DateTime.Now;
            try
            {
                using SqlConnection connection = new SqlConnection(_connectionString);
                connection.Open();

                string UpdateQuery = "UpdateAuthor";
                SqlCommand command = new SqlCommand(UpdateQuery, connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@Id", SqlDbType.UniqueIdentifier).Value = author.ID;
                command.Parameters.Add("@FirstName", SqlDbType.VarChar).Value = author.FirstName;
                command.Parameters.Add("@LastName", SqlDbType.VarChar).Value = author.LastName;
                command.Parameters.Add("@Biography", SqlDbType.VarChar).Value = author.Biography;
                command.Parameters.Add("@BirthDate", SqlDbType.DateTime).Value = author.BirthDate;
                command.Parameters.Add("@Country", SqlDbType.VarChar).Value = author.Country;
                command.Parameters.Add("@Updated_At", SqlDbType.VarChar).Value = author.Updated_At;
                command.ExecuteNonQuery();
            }
            catch
            {
                return "Update Unsuccessful";
            }
            return "Author update successful";
        }

        public string DeleteAuthor(Guid ID)
        {
            try
            {
                using SqlConnection connection = new SqlConnection(_connectionString);
                connection.Open();

                string deleteQuery2 = "DeleteAuthor";
                SqlCommand command = new SqlCommand(deleteQuery2, connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ID", ID);

                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                return "Author Not found";
            }

            return "Author Delete success";
        }

       
    }

}

