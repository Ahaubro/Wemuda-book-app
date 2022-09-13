using Microsoft.AspNetCore.Mvc;
using Wemuda_book_app.Service;
using Wemuda_book_app.Shared;

namespace Wemuda_book_app.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : Controller
    {
        private readonly IReviewService _reviewService;

        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }


        // CREATE REVIEW
        [Produces("application/json")]
        [HttpPost]
        public async Task<ReviewCreateResponseDto> Create([FromBody] ReviewCreateRequestDto dto)
        {
            return await _reviewService.Create(dto);
        }

        // GET ALL REVIEWS
        [Produces("application/json")]
        [HttpGet]
        public async Task<ReviewGetAllResponseDto> GetAll()
        {
            return await _reviewService.GetAll();
        }


        //GET REVIEWS BY BOOKID
        [Produces("application/json")]
        [HttpGet("{bookId}")]
        public async Task<GetReviewsByBookIdResponseDto> GetReviewsByBookId(string bookId)
        {
            return await _reviewService.GetReviewsByBookId(bookId);
        }

    }
}
