using Microsoft.AspNetCore.Mvc;
using Wemuda_book_app.Data;
using Wemuda_book_app.Model;

namespace Wemuda_book_app.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : Controller
    {
        private readonly ApplicationDBContext _db;

        public BookController(ApplicationDBContext db)
        {
            _db = db;
        }

        //Test
        [Route("test")]
        public string test()
        {
            return "Test";
        }

        //Read
        [Route("read")]
        public IEnumerable<Book> Read() 
        {
            IEnumerable<Book> books = _db.Books.ToList();
            return books;
        }

        //Create
        [HttpPost]
        //[AutoValidateAntiforgeryToken]
        [Route("create")]

        public Book Create(Book book)
        {
            if (ModelState.IsValid)
            {
                _db.Books.Add(book);
                _db.SaveChanges();
            }

            return book;
        }

        //ReadOne
        [Route("readone")]
        public Book ReadOne(int? id)
        {
            Book bookFromDB = _db.Books.Find(id);

            return bookFromDB;
        }

        //Edit
        [Route("edit")]
        public Book Edit(int? id)
        {
            //if (ModelState.IsValid)
            //{
            //    _db.Books.Update();
            //    _db.SaveChanges();
            //}

            return ;
        }









        //Delete
        

    
        //public IActionResult Index()
        //{
        //    return View();
        //}
    }
}
