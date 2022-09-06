using Wemuda_book_app.Data;
using Wemuda_book_app.Model;
using Wemuda_book_app.Shared;

namespace Wemuda_book_app.Service
{
    public interface IStatusUpdateService
    {
        Task<CreateStatusUpdateResponseDto> Create(CreateStatusUpdateRequestDto dto, int userId);
        Task<GetBookStatusUpdatesResponseDto> GetByUserAndBook(int userId, string bookId);
        Task<GetUserStatusUpdatesResponseDto> GetByUser(int userId);
    }
    public class StatusUpdateService : IStatusUpdateService
    {
        private readonly ApplicationDBContext _context;

        public StatusUpdateService(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<CreateStatusUpdateResponseDto> Create(CreateStatusUpdateRequestDto dto, int userId)
        {
            var entity = _context.StatusUpdates.Add(new StatusUpdate
            {
                UserId = userId,
                MinutesRead = dto.MinutesAdded,
                TimeOfUpdate = DateTime.Now
            });

            await _context.SaveChangesAsync();

            return new CreateStatusUpdateResponseDto
            {
                StatusText = "StatusUpdated"
            };
        }

        public async Task<GetBookStatusUpdatesResponseDto> GetByUserAndBook(int userId, string bookId)
        {
            var bookStatusUpdates = _context.StatusUpdates.Where(s => s.UserId == userId).ToList();

            return new GetBookStatusUpdatesResponseDto
            {
                StatusUpdates = bookStatusUpdates.Select(statusUpdate => new BookStatusUpdateDto
                {
                    UserId = userId,
                    MinutesRead = statusUpdate.MinutesRead,
                    TimeOfUpdate = statusUpdate.TimeOfUpdate
                })
            };
        }

        public async Task<GetUserStatusUpdatesResponseDto> GetByUser(int userId)
        {
            var userStatusUpdates = _context.StatusUpdates.Where(s => s.UserId == userId).ToList();

            return new GetUserStatusUpdatesResponseDto
            {
                StatusUpdates = userStatusUpdates.Select(statusUpdate => new UserStatusUpdateDto
                {
                    MinutesRead = statusUpdate.MinutesRead,
                    TimeOfUpdate = statusUpdate.TimeOfUpdate
                })
            };
        }

    }
}
