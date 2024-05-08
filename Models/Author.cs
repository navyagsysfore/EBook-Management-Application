using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public  class Author
    {
        DateTime current = DateTime.Now;

        public Author()

        {

        }
        public Author(AuthorDTO author)
        {
            
            ID = Guid.NewGuid();
            Created_At = current;
            this.FirstName = author.FirstName;
            this.LastName = author.LastName;
            this.Biography = author.Biography;
            this.BirthDate = author.BirthDate;
            this.Country = author.Country;
            Updated_At = DateTime.Now;
            
        }
        

        public Guid ID { get; set; }
        public string  FirstName { get; set; }
        public string LastName { get; set; }
        public string Biography { get; set; }
        public DateTime BirthDate {  get; set; }
        public string Country { get; set; }
        public DateTime Created_At { get; set; }
        public DateTime Updated_At { get;set; }

        public DateTime getCreated_At()
        {
            return this.Created_At;
        }
    }
}
