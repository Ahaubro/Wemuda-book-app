using System.Text.Json.Serialization;

namespace Wemuda_book_app.Model
{
    public class User
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        [JsonIgnore]
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string Email { get; set; }
        public int MinutesRead { get; set; }
        public int BooksRead { get; set; }
        public int BooksGoal { get; set; }
        public string? ResetPasswordToken { get; set; }
        public DateTime? ResetPasswordTokenExpire { get; set; }
        public string ConfirmEmailToken { get; set; }
        public bool EmailConfirmed { get; set; }
    }
}
