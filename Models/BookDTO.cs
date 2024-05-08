using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class BookDTO
    {
      
        public string Title { get; set; }

        public string Description { get; set; }
        public int ISBN { get; set; }
        public DateTime PublicationDate { get; set; }
        public int Price { get; set; }
        public string Language {  get; set; }
        public string Publisher { get; set; }

        public int PageCount { get; set; }
        public double? AverageRating { get; set; }
        public int TotalRating { get; set; }

        public int GenreID { get; set; }

        public List<Guid> ID { get; set; }
    }
}
