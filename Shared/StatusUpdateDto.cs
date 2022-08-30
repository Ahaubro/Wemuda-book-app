namespace Wemuda_book_app.Shared
{
    public class StatusUpdateDto
    {
        
    } 

    public class BookStatusUpdateDto
    {
        public int Id { get; set; }
        public DateTime TimeOfUpdate { get; set; }
        public int MinutesRead { get; set; }
        public int CurrentPage { get; set; }
        public bool FinishedBook { get; set; }
    }

    public class UserStatusUpdateDto
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public DateTime TimeOfUpdate { get; set; }
        public int MinutesRead { get; set; }
        public int CurrentPage { get; set; }
        public bool FinishedBook { get; set; }
    }

    public class CreateStatusUpdateRequestDto
    {
        public int MinutesAdded { get; set; }
        public int CurrentPage { get; set; }
        public bool FinishedBook { get; set; }
    }

    public class CreateStatusUpdateResponseDto
    {
        public string StatusText { get; set; }
    }
    
    public class GetBookStatusUpdatesResponseDto
    {
        public IEnumerable<BookStatusUpdateDto> StatusUpdates { get; set; }
    }

    public class GetUserStatusUpdatesResponseDto
    {
        public IEnumerable<UserStatusUpdateDto> StatusUpdates { get; set; }
    }

}
