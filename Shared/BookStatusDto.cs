namespace Wemuda_book_app.Shared
{
    public class CreateBookStatusRequestDto
    {
        public int UserId { get; set; }
        public int BookId { get; set; }
        public string Status { get; set; }
    }
    public class CreateBookStatusResponseDto
    {
        public int UserId { get; set; }
        public int BookId { get; set; }
        public string Status { get; set; }
        public string StatusText { get; set; }
    }

    public class UpdateBookStatusRequestDto
    {
        public int UserId { get; set; }
        public int BookId { get; set; }
        public string Status { get; set; }
    }

    public class UpdateBookStatusResponseDto
    {
        public int UserId { get; set; }
        public int BookId { get; set; }
        public string Status { get; set; }
        public string StatusText { get; set; }
    }
}
