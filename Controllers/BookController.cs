﻿using Microsoft.AspNetCore.Authorization;
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
        [HttpPatch("{id:int}")]
        public async Task<BookUpdateResponseDto> Update([FromBody] BookUpdateRequestDto dto, int id)
        {
            return await _bookService.Update(dto, id);
        }


        //DELETE BOOK
        [Produces("application/json")]
        [HttpDelete("{id:int}")]
        public async Task<BookDeleteResponseDto> Delete(int id)
        { 
            return await _bookService.Delete(id);
        }


        //GET BOOK BY ID
        [Produces("application/json")]
        [HttpGet("{id:int}")]
        public async Task<BookGetResponseDto> GetById(int id)
        { 
            return await _bookService.GetById(id);
        }


        // GET ALL BOOKS
        [Produces("application/json")]
        [HttpGet]
        public async Task<BookGetAllResponseDto> GetAll()
        {
            return await _bookService.GetAll();
        }

    }
}