using System.ComponentModel.DataAnnotations;

namespace Wemuda_book_app.Model;

public class Review
{
    [Key]
    public int Id { get; set; }
    public string? Title { get; set; }
    public string Content { get; set; }
    public int Rating { get; set; }
    public string? BookId { get; set; }
    public int? UserId { get; set; }
}