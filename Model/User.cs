using System.Text.Json.Serialization;

namespace Wemuda_book_app.Model
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }

        [JsonIgnore]
        public string Password { get; set; }
        public string Email { get; set; }
        public int MinutesRead { get; set; }
        public int BooksRead { get; set; }
        public int BooksGoal { get; set; }

    }
}
