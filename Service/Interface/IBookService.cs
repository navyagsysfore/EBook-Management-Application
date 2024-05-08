using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IBookService
    {
         public string AddBook(BookDTO book);
        public string UpdateBook(Book e);

        public string DeleteBook(Guid id);

        public List<Book> SearchBooksByTitle(string title);

        public List<Book> GetBooksByAuthor(Guid authorID);
        List<Book> GetBooksByGenre(int genreID);

        public List<Book> GetAllBook();
        //List<Genere> sample();

    }
}
