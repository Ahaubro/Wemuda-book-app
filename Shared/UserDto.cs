using System.ComponentModel.DataAnnotations;
using Wemuda_book_app.Model;

namespace Wemuda_book_app.Shared
{
    public class AuthDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
    }

    public class UserDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public int BooksGoal { get; set;}
    }


    public class AuthenticateRequestDto
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }

    public class AuthenticateResponseDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        //public UserDto User { get; set; }
        public string Token { get; set; }

    }

    // CREATE REQUEST
    public class CreateUserRequestDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

    }

    // CREATE RESPONSE
    public class CreateUserResponseDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string StatusText { get; set; }
    }

    // GETALLRESPONSE
    public class GetAllUsersResponseDto
    {
        public IEnumerable<AuthDTO> Users { get; set; }
    }

    // GET BY ID RESPONSE
    public class GetUserByIdResponseDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public int BooksGoal { get; set; }
        public string StatusText { get; set; }
    }

    // DELETE RESPONSE
    public class DeleteUserResponseDto
    {
        public string StatusText { get; set; }
    }

    public class ChangePasswordRequestDto
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public string NewPassword { get; set; }
    }

    public class ChangePasswordResponseDto
    {
        public string StatusText { get; set; }
    }

    public class SetBookGoalRequestDto
    {
        public int BooksGoal { get; set; }
    }

    public class SetBookGoalResponseDto
    {
        public string StatusText { get; set; }
    }

}
