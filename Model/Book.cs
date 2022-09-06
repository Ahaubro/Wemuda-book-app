using System.ComponentModel.DataAnnotations;

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
        //public string Authors { get; set; }
        public string? Thumbnail { get; set; }
        public double? AverageRating { get; set; } 
        public int? RatingCount { get; set; }
    }
}