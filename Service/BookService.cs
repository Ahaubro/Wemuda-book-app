using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System.Diagnostics.Metrics;
using Wemuda_book_app.Data;
using Wemuda_book_app.Model;
using Wemuda_book_app.Shared;

namespace Wemuda_book_app.Service
{

    public interface IBookService
    {
        Task<BookCreateResponseDto> Create(BookCreateRequestDto dto);
        Task<BookUpdateResponseDto> Update(BookUpdateRequestDto dto, int id);
        Task<BookDeleteResponseDto> Delete(int id);
        Task<BookGetResponseDto> GetById(int id);
        Task<BookGetAllResponseDto> GetAll();

        Task<BookAddToUserResponseDto> AddToUser(BookAddToUserRequestDto dto);

    }

    public class BookService : IBookService
    {

        private readonly ApplicationDBContext _context;

        public BookService(ApplicationDBContext context)
        {
            _context = context;
        }


        // CREATE
        public async Task<BookCreateResponseDto> Create(BookCreateRequestDto dto)
        {
            var entity = _context.Books.Add(new Book
            {
                Title = dto.Title,
                Author = dto.Author,
                Genre = dto.Genre,
                ReleaseDate = dto.ReleaseDate
            });

            await _context.SaveChangesAsync();

            return new BookCreateResponseDto
            {
                StatusText = "BookCreated",
                Title = entity.Entity.Title,
                Author = entity.Entity.Author,
                Genre = entity.Entity.Genre,
                ReleaseDate = entity.Entity.ReleaseDate

            };
        }

        // DELETE
        public async Task<BookDeleteResponseDto> Delete(int id)
        {

            var book = await _context.Books.FirstOrDefaultAsync(d => d.Id == id);

            //INSERT EXCEPTION


            _context.Books.Remove(book);

            await _context.SaveChangesAsync();

            return new BookDeleteResponseDto
            {
                StatusText = "BookDeleted"
            };
        }

        // UPDATE
        public async Task<BookUpdateResponseDto> Update(BookUpdateRequestDto dto, int id)
        {
            var book = await _context.Books.FirstOrDefaultAsync(d => d.Id == id);

            //INSERT EXCEPTION

            book.Title = dto.Title;
            book.Author = dto.Author;
            book.Genre = dto.Genre;
            book.ReleaseDate = dto.ReleaseDate;

            var entity = _context.Books.Update(book);

            await _context.SaveChangesAsync();

            return new BookUpdateResponseDto
            {
                StatusText = "BookUpdated",
                Title = entity.Entity.Title
            };

        }

        //GET ONE
        public async Task<BookGetResponseDto> GetById(int id)
        {
            var book = await _context.Books.AsNoTracking().FirstOrDefaultAsync(b => b.Id == id);

            //INSERT EXCEPTION

            return new BookGetResponseDto
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
                Genre = book.Genre,
                ReleaseDate = book.ReleaseDate

            };
        }


        //GET ALL
        public async Task<BookGetAllResponseDto> GetAll()
        {
      
            var allBooks = _context.Books.ToList();

            return new BookGetAllResponseDto
            {
                Books = allBooks.Select(b => new BookDto
                {
                    Id = b.Id,
                    Title = b.Title,
                    Author = b.Author,
                    Genre = b.Genre,
                    ReleaseDate = b.ReleaseDate
                })
            };
        }

        public async Task<BookAddToUserResponseDto> AddToUser(BookAddToUserRequestDto dto)
        {
            
            return new BookAddToUserResponseDto
            {
                StatusText = "Nothing Happens Yet" //"BookAddedToUser"
            };
        }

    }
}
