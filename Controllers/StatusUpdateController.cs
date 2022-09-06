using Microsoft.AspNetCore.Mvc;
using Wemuda_book_app.Service;
using Wemuda_book_app.Shared;

namespace Wemuda_book_app.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusUpdateController : Controller
    {
        private readonly IStatusUpdateService _statusUpdateService;

        public StatusUpdateController(IStatusUpdateService statusUpdateService)
        {
            _statusUpdateService = statusUpdateService;
        }

        [Produces("application/json")]
        [HttpPost("{userId:int}")]
        public async Task<CreateStatusUpdateResponseDto> Create([FromBody] CreateStatusUpdateRequestDto dto, int userId)
        {
            return await _statusUpdateService.Create(dto, userId);
        }

        [Produces("application/json")]
        [HttpGet("{userId:int}/{bookId}")]
        public async Task<GetBookStatusUpdatesResponseDto> GetByUserAndBook(int userId, string bookId)
        {
            return await _statusUpdateService.GetByUserAndBook(userId, bookId);
        }

        [Produces("application/json")]
        [HttpGet("{userId:int}")]
        public async Task<GetUserStatusUpdatesResponseDto> GetByUser(int userId)
        {
            return await _statusUpdateService.GetByUser(userId);
        }

    }
}
