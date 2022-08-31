using Microsoft.AspNetCore.Mvc;
using Wemuda_book_app.Service;
using Wemuda_book_app.Shared;

namespace Wemuda_book_app.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookStatusController : Controller
    {
        private IBookStatusService _bookStatusService;

        public BookStatusController(IBookStatusService bookStatusService)
        {
            _bookStatusService = bookStatusService;
        }

        [Produces("application/json")]
        [HttpPost]
        public async Task<CreateBookStatusResponseDto> Create([FromBody] CreateBookStatusRequestDto dto)
        {
            return await _bookStatusService.Create(dto);
        }

        [Produces("application/json")]
        [HttpPatch]
        public async Task<UpdateBookStatusResponseDto> Update([FromBody] UpdateBookStatusRequestDto dto)
        {
            return await _bookStatusService.Update(dto);
        }
    }
}