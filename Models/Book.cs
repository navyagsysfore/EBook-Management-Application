using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Book
    {

        DateTime current = DateTime.Now;

       public Book()

       {

          }

        public Book(BookDTO book)
        {
            BookID = Guid.NewGuid();
            Created_At = current;
            this.Title = book.Title;
            this.Description = book.Description;
            this.ISBN = book.ISBN;
            this.PublicationDate = book.PublicationDate;
            this.Price = book.Price;
            this.Language = book.Language;
            this.Publisher = book.Publisher;
            this.PageCount = book.PageCount;
            this.AverageRating = book.AverageRating;
            this.GenreID = book.GenreID;
            Updated_At = DateTime.Now;
        }
        public Guid BookID { get; set; }

       
        public string Title { get; set; }
        public string Description { get; set; }
        public int ISBN { get; set; }

        public DateTime PublicationDate { get; set; }

        public int Price { get; set; }

        public string Language { get; set; }
        public  string Publisher { get; set; }

        public int PageCount { get; set; }
        public double? AverageRating {  get; set; }
        public DateTime Created_At { get; set; }
        public DateTime Updated_At { get; set; }
         
        public int GenreID { get; set; }
        public List<Guid> ID { get; set; }

        public DateTime getCreated_At()
        {
            return this.Created_At;
        }
    }

    
}
