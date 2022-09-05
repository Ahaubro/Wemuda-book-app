using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Wemuda_book_app.Data;
using Wemuda_book_app.Model;
using Wemuda_book_app.Service;
using Wemuda_book_app.Shared;

namespace Wemuda_book_app.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : Controller
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }


        // CREATE BOOK
        [Produces("application/json")]
        [HttpPost]
        public async Task<BookCreateResponseDto> Create([FromBody] BookCreateRequestDto dto)
        {
            return await _bookService.Create(dto);
        }


        //UPDATE BOOK
        [Produces("application/json")]
        [HttpPatch("{userId:int}/{bookId}")]
        public async Task<BookUpdateResponseDto> Update([FromBody] BookUpdateRequestDto dto, int userId, string bookId)
        {
            return await _bookService.Update(dto, userId, bookId);
        }


        //DELETE BOOK
        [Produces("application/json")]
        [HttpDelete("{id:int}")]
        public async Task<BookDeleteResponseDto> Delete(int id)
        { 
            return await _bookService.Delete(id);
        }


        ////GET BOOK BY ID
        //[Produces("application/json")]
        //[HttpGet("{id:int}")]
        //public async Task<BookGetResponseDto> GetById(int id)
        //{ 
        //    return await _bookService.GetById(id);
        //}


        // GET ALL BOOKS
        [Produces("application/json")]
        [HttpGet]
        public async Task<BookGetAllResponseDto> GetAll()
        {
            return await _bookService.GetAll();
        }

        // BOOK QUERY FOR SAFETY

        //[Produces("application/json")]
        //[HttpGet("search/{query:string}")]
        //public async Task<BookGetAllResponseDto> BookSearch()
        //{
        //    return await ;
        //}


        //GET BOOKs BY USERID
        [Produces("application/json")]
        [HttpGet("{userId:int}")]
        public async Task<BookGetByUseridResponseDto> GetByUserId(int userId)
        {
            return await _bookService.GetByUserid(userId);
        }


        //GET BOOK BY BOOKID
        [Produces("application/json")]
        [HttpGet("getByBookId/{bookId}")]
        public async Task<BookGetByBookidResponseDto> GetByBookId(string bookId)
        {
            return await _bookService.GetByBookId(bookId);
        }

        //EDIT BOOK STATUS
        [Produces("application/json")]
        [HttpPatch]
        public async Task<BookEditStatusResponseDto> EditStatus([FromBody]BookEditStatusRequestDto dto)
        {
            return await _bookService.EditStatus(dto);
        }
    }
}
