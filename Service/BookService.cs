using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System.Diagnostics.Metrics;
using System.Net;
using Wemuda_book_app.Data;
using Wemuda_book_app.Migrations;
using Wemuda_book_app.Model;
using Wemuda_book_app.Shared;

namespace Wemuda_book_app.Service
{

    public interface IBookService
    {
        Task<BookCreateResponseDto> Create(BookCreateRequestDto dto);
        Task<BookUpdateResponseDto> Update(BookUpdateRequestDto dto, int id, string bookId);
        Task<BookDeleteResponseDto> Delete(int id);
        Task<BookGetResponseDto> GetById(int id);
        Task<BookGetAllResponseDto> GetAll();
        Task<BookGetByUseridResponseDto> GetByUserid(int userId);

        Task<BookGetByBookidResponseDto> GetByBookId(string bookId);
        
        Task<BookEditStatusResponseDto> EditStatus(BookEditStatusRequestDto dto);
        //Task<BookAddToUserResponseDto> AddToUser(BookAddToUserRequestDto dto);
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
            var check = _context.Books.FirstOrDefault(d => d.BookId == dto.BookId && d.UserId == dto.UserId);

            if (check == null) 
            {
                var entity = _context.Books.Add(new Book
                {
                    UserId = dto.UserId,
                    BookId = dto.BookId,
                    Title = dto.Title,
                    //Authors = dto.Authors,
                    Description = dto.Description,
                    Thumbnail = dto.Thumbnail,
                    AverageRating = dto.AverageRating,
                    RatingCount = dto.RatingCount,
                    BookStatus = dto.BookStatus

                    //Genre = dto.Genre,
                    //ReleaseDate = dto.ReleaseDate
                });

                await _context.SaveChangesAsync();

                return new BookCreateResponseDto
                {
                    StatusText = "BookCreated",
                    Title = entity.Entity.Title,
                };
            }

            return null;
            
            
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
        public async Task<BookUpdateResponseDto> Update(BookUpdateRequestDto dto, int userId, string bookId)
        {
            var book = await _context.Books.FirstOrDefaultAsync(d => d.BookId == bookId && d.UserId == userId);

            if (book != null)
            {
                book.BookStatus = dto.BookStatus;

                var entity = _context.Books.Update(book);

                await _context.SaveChangesAsync();

                return new BookUpdateResponseDto
                {
                    StatusText = "BookUpdated",
                    Title = entity.Entity.Title
                };
            }
            else 
            {
                var entity = _context.Books.Add(new Book
                {
                    UserId = dto.UserId,
                    BookId = dto.BookId,
                    Title = dto.Title,
                    Thumbnail = dto.Thumbnail,
                    BookStatus = dto.BookStatus
                });

                await _context.SaveChangesAsync();

            }

            return null;

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
                //Authors = book.Authors,
                //Genre = book.Genre,
                //ReleaseDate = book.ReleaseDate

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
                    UserId = b.UserId,
                    BookId = b.BookId,
                    Title = b.Title,
                    Description = b.Description,
                    Thumbnail = b.Thumbnail,
                    AverageRating = b.AverageRating,
                    RatingCount = b.RatingCount,
                    BookStatus = b.BookStatus,
                    //Genre = b.Genre,
                    //ReleaseDate = b.ReleaseDate
                })
            };
        }

        /*
        public async Task<BookAddToUserResponseDto> AddToUser(BookAddToUserRequestDto dto)
        {
            
            return new BookAddToUserResponseDto
            {
                StatusText = "Nothing Happens Yet" //"BookAddedToUser"
            };
        }
        */


        //GET BY USER ID
        public async Task<BookGetByUseridResponseDto> GetByUserid(int userId)
        {
            var books = _context.Books.AsNoTracking().Where(b => b.UserId == userId);

            //INSERT EXCEPTION

            return new BookGetByUseridResponseDto
            {
                Books = books.Select(b => new BookDto
                {
                    Id = b.Id,
                    UserId = b.UserId,
                    BookId = b.BookId,
                    Title = b.Title,
                    Description = b.Description,
                    Thumbnail = b.Thumbnail,
                    AverageRating = b.AverageRating,
                    RatingCount = b.RatingCount,
                    BookStatus = b.BookStatus
                    //Genre = b.Genre,
                    //ReleaseDate = b.ReleaseDate
                })
            };
        }


        //GET ONE BY BOOKID
        public async Task<BookGetByBookidResponseDto> GetByBookId(string bookId)
        {
            var book = await _context.Books.AsNoTracking().FirstOrDefaultAsync(b => b.BookId.Equals(bookId));

            return new BookGetByBookidResponseDto
            {
                Id = book.Id,
                Title = book.Title,
                BookStatus = book.BookStatus,

            };
        }

        //EDIT BOOK STATUS
        public async Task<BookEditStatusResponseDto> EditStatus(BookEditStatusRequestDto dto)
        {
            var book = await _context.Books.FirstOrDefaultAsync(b => b.BookId == dto.BookId && b.UserId == dto.UserId);

            if (book != null)
            {
                book.BookStatus = dto.BookStatus;

                var entity = _context.Books.Update(book);

                await _context.SaveChangesAsync();

                return new BookEditStatusResponseDto
                {
                    StatusText = "BookStatusUpdated"
                };
            }
            else
            {
                return new BookEditStatusResponseDto
                {
                    StatusText = "BookNotFound"
                };

            }
        }

    }
}
