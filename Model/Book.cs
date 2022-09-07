using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Wemuda_book_app.Model
{
    public class Book
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public string? BookId { get; set; }
        public string Title { get; set; }
        public string? BookStatus { get; set; }
        public string? Description { get; set; }
        public string? Author { get; set; }
        public string? Thumbnail { get; set; }
        public double? AverageRating { get; set; } 
        public int? RatingCount { get; set; }
    }
}