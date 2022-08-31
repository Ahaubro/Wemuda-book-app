using Microsoft.EntityFrameworkCore;
using Wemuda_book_app.Data;
using Wemuda_book_app.Model;
using Wemuda_book_app.Shared;

namespace Wemuda_book_app.Service
{
    public interface IBookStatusService
    {
        Task<CreateBookStatusResponseDto> Create(CreateBookStatusRequestDto dto);
        Task<UpdateBookStatusResponseDto> Update(UpdateBookStatusRequestDto dto);
    }

    public class BookStatusService : IBookStatusService
    {
        private readonly ApplicationDBContext _context;

        public BookStatusService(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<CreateBookStatusResponseDto> Create(CreateBookStatusRequestDto dto)
        {
            var entity = _context.BookStatuses.Add(new BookStatus
            {
                UserId = dto.UserId,
                BookId = dto.BookId,
                Status = dto.Status
            });

            await _context.SaveChangesAsync();

            return new CreateBookStatusResponseDto
            {
                UserId = entity.Entity.UserId,
                BookId = entity.Entity.BookId,
                Status = entity.Entity.Status,
                StatusText = "BookStatusCreated"
            };
        }

        public async Task<UpdateBookStatusResponseDto> Update(UpdateBookStatusRequestDto dto)
        {
            var bookStatus = await _context.BookStatuses.FirstOrDefaultAsync(b => b.UserId == dto.UserId && b.BookId == dto.BookId);
            
            if(bookStatus == null) return new UpdateBookStatusResponseDto
            {
                StatusText = "BookStatusDoesNotExist"
            };

            bookStatus.Status = dto.Status;

            var entity = _context.BookStatuses.Update(bookStatus);

            await _context.SaveChangesAsync();

            return new UpdateBookStatusResponseDto
            {
                Status = entity.Entity.Status,
                StatusText = "BookStatusUpdated"
            };
        }
    }
}
