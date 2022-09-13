﻿using Wemuda_book_app.Data;
using Wemuda_book_app.Model;
using Wemuda_book_app.Shared;

namespace Wemuda_book_app.Service
{

    public interface IReviewService
    {
        Task<ReviewCreateResponseDto> Create(ReviewCreateRequestDto dto);
        Task<ReviewGetAllResponseDto> GetAll();

    }
    public class ReviewService : IReviewService
    {

        private readonly ApplicationDBContext _context;

        public ReviewService(ApplicationDBContext context)
        {
            _context = context;
        }


        // CREATE
        public async Task<ReviewCreateResponseDto> Create(ReviewCreateRequestDto dto)
        {
            var entity = _context.Reviews.Add(new Review
            {
                Id = dto.Id,
                Content = dto.Content,
                Rating = dto.Rating,
                
            });

            await _context.SaveChangesAsync();

            return new ReviewCreateResponseDto
            {
                StatusText = "Review created",
            };
        }


        //GET ALL
        public async Task<ReviewGetAllResponseDto> GetAll()
        {

            var allReviews = _context.Reviews.ToList();

            return new ReviewGetAllResponseDto
            {
                Reviews = allReviews.Select(b => new ReviewDto
                {
                    Id = b.Id,
                    Content = b.Content,
                    Rating = b.Rating
                })
            };
        }





    }
}
