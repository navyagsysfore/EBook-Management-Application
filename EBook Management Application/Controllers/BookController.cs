using Ebook_Management_Application.DataBase;
using Microsoft.AspNetCore.Mvc;
using Models;
using Service.Interf;
using Service.Interface;



namespace Ebook_Management_Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService bookService;
        private readonly IAuthorService authorService;

        public BookController(IBookService bookService, IAuthorService authorService)
        {
            //bookService = new DataBaseManager();
            //authorService = new AuthorDataBaseM();
            this.bookService = bookService;
           this.authorService = authorService;
        }
       
        [HttpGet]
        [Route("/GetAllBook")]
        public ActionResult GetAllBook()
        {
            return Ok(bookService.GetAllBook());
        }

        [HttpPost]
        [Route("/AddBook")]
        public ActionResult AddBook([FromBody] BookDTO book)
        {
            // string msg = bookService.AddBook(book);


            return Ok(bookService.AddBook(book));
        }

        [HttpDelete]
        [Route("/DeleteBook/{id}")]
        public ActionResult DeleteBook(Guid id)
        {
            return Ok(bookService.DeleteBook(id));
        }



        [HttpPatch]
        [Route("/UpdateBook")]
        public ActionResult UpdateBook([FromBody] Book e)
        {
            return Ok(bookService.UpdateBook(e));
        }




        [HttpGet]
        [Route("/GetAllAuthors")]
        public ActionResult GetAllAuthors()
        {
            return Ok(authorService.GetAllAuthors());
        }

        [HttpPost]
        [Route("/AddAuthor")]
        public ActionResult AddAuthor([FromBody] AuthorDTO author)
        {
            string msg = authorService.AddAuthor(author);

            return Ok("Author added successfully");
        }

        [HttpDelete]
        [Route("/DeleteAuthor")]
        public ActionResult DeleteAuthor([FromBody] Guid ID)
        {
            return Ok(authorService.DeleteAuthor(ID));
        }

        [HttpPatch]
        [Route("/UpdateAuthor")]
        public ActionResult UpdateAuthor([FromBody] Author author)
        {
            return Ok(authorService.UpdateAuthor(author));
        }
        
         [HttpGet]
         [Route("/SearchBooksByTitle")]
         public ActionResult SearchBooksByTitle(string title)
         {
             return Ok(bookService.SearchBooksByTitle(title));
         }

        [HttpGet]
        [Route("/GetBooksByGenre/{genreID}")]
        public ActionResult GetBooksByGenre(int genreID)
        {
            return Ok(bookService.GetBooksByGenre(genreID));
        }

        [HttpGet]
        [Route("/GetBooksByAuthor/{authorID}")]
        public ActionResult GetBooksByAuthor(Guid authorID)
        {
            return Ok(bookService.GetBooksByAuthor(authorID));
        }

    }
    }
