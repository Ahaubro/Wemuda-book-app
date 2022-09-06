namespace Wemuda_book_app.Shared
{
    public class StatusUpdateDto
    {
        
    } 

    public class BookStatusUpdateDto
    {
        public int UserId { get; set; }
        public DateTime TimeOfUpdate { get; set; }
        public int MinutesRead { get; set; }
    }

    public class UserStatusUpdateDto
    {
        public int UserId { get; set; }
        public DateTime TimeOfUpdate { get; set; }
        public int MinutesRead { get; set; }
    }

    public class CreateStatusUpdateRequestDto
    {
        public int UserId { get; set; }
        public int MinutesAdded { get; set; }
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
