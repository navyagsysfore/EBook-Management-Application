using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interf
{
   public interface IAuthorService
    {
       
            string AddAuthor(AuthorDTO authors);

            string UpdateAuthor(Author author);

            string DeleteAuthor(Guid ID);

            List<Author> GetAllAuthors();
      
    }
}
