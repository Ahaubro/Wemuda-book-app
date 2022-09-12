using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NuGet.Common;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Wemuda_book_app.Configuration;
using Wemuda_book_app.Data;
using Wemuda_book_app.Helpers;
using Wemuda_book_app.Model;
using Wemuda_book_app.Shared;

namespace Wemuda_book_app.Service
{
    public interface IUserService
    {
        Task<AuthenticateResponseDto> Authenticate(AuthenticateRequestDto model);
        Task<GetAllUsersResponseDto> GetAll();
        Task<GetUserByIdResponseDto> GetById(int id);
        Task<CreateUserResponseDto> Create(CreateUserRequestDto dto);
        Task<DeleteUserResponseDto> Delete(int id);
        Task<ChangePasswordResponseDto> ChangePassword(ChangePasswordRequestDto dto);
        Task<SetBookGoalResponseDto> SetBooksGoal(SetBookGoalRequestDto dto, int userId);
        Task<ResetBooksReadResponeDto> ResetBooksRead(int userId);
    }
    public class UserService : IUserService
    {

        private readonly AppSettings _appSettings;
        private readonly ApplicationDBContext _context;
        private readonly IMailService _mailService;

        public UserService(IOptions<AppSettings> appSettings, ApplicationDBContext context, IOptions<MailSettings> mailSettings)
        {
            _appSettings = appSettings.Value;
            _context = context;
            _mailService = new MailService(mailSettings);
        }


        //AUTHENTICATE
        public async Task<AuthenticateResponseDto> Authenticate(AuthenticateRequestDto model)
        {

            Console.WriteLine(model.Username, model.Password);

            var user = await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Username.Equals(model.Username) && u.Password.Equals(model.Password));


            // return null if user not found
            if (user == null) return new AuthenticateResponseDto {  };

            // authentication successful so generate jwt token
            var token = generateJwtToken(user);


            return new AuthenticateResponseDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Username = user.Username,
                Token = token,
            };
        }


        // GET ALL
        public async Task<GetAllUsersResponseDto> GetAll()
        {
            var allUsers = _context.Users.ToList();

            return new GetAllUsersResponseDto
            {
                Users = allUsers.Select(b => new AuthDTO
                {
                    Id = b.Id,
                    FirstName = b.FirstName,
                    LastName = b.LastName,
                    Username = b.Username
                })
            };
        }


        // GET BY ID
        public async Task<GetUserByIdResponseDto> GetById(int id)
        {
            var user = await _context.Users.AsNoTracking().FirstOrDefaultAsync(b => b.Id == id);

            if (user == null) return new GetUserByIdResponseDto { StatusText = "UserNotFound" };

            Console.WriteLine("UserService GetById: " + user.Username + ", " + user.BooksGoal);

            return new GetUserByIdResponseDto
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Username = user.Username,
                BooksRead = user.BooksRead,
                BooksGoal = user.BooksGoal,
                StatusText = "UserFound"
            };
        }


        // GENERATES TOKEN FOR AUTH
        private string generateJwtToken(User user)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()), new Claim("name", user.FirstName), new Claim("username", user.Username) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }


        // CREATE
        public async Task<CreateUserResponseDto> Create(CreateUserRequestDto dto)
        {
            var emailReciever = dto.Email;
            Console.WriteLine(emailReciever);
            var emailRecievers = new List<string> { emailReciever };
            var emailSubject = "Welcome to Wemuda Books";
            var emailBody = "Hello " + dto.FirstName + " " + dto.LastName + "\nWelcome to Wemuda Books\nHope you enjoy";

            var email = new Email(emailRecievers, emailSubject, emailBody);

            var emailSent = await _mailService.SendAsync(email, new CancellationToken());

            if (!emailSent)
            {
                return new CreateUserResponseDto
                {
                    StatusText = "Invalid Email",
                    FirstName = "",
                    LastName = "",
                    Username = ""
                };
            }

            var entity = _context.Users.Add(new User
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Username = dto.Username,
                Email = dto.Email,
                Password = dto.Password
            });

            await _context.SaveChangesAsync();

            return new CreateUserResponseDto
            {
                StatusText = "User Created",
                FirstName = entity.Entity.FirstName,
                LastName = entity.Entity.LastName,
                Username = entity.Entity.Username,
            };
        
        }


        // DELETE
        public async Task<DeleteUserResponseDto> Delete(int id)
        {

            var user = await _context.Users.FirstOrDefaultAsync(d => d.Id == id);

            //INSERT EXCEPTION


            _context.Users.Remove(user);

            await _context.SaveChangesAsync();

            return new DeleteUserResponseDto
            {
                StatusText = "UserDeleted"
            };

        }

        
        public async Task<ChangePasswordResponseDto> ChangePassword(ChangePasswordRequestDto dto)
        {
            User user = await _context.Users.FirstOrDefaultAsync(d => d.Id == dto.UserId);

            user.Password = dto.NewPassword;

            _context.Users.Update(user);

            return new ChangePasswordResponseDto
            {
                StatusText = "PasswordChanged"
            };
        }

        public async Task<SetBookGoalResponseDto> SetBooksGoal(SetBookGoalRequestDto dto, int userId)
        {
            User user = await _context.Users.FirstOrDefaultAsync(d => d.Id == userId);

            user.BooksGoal = dto.BooksGoal;

            Console.WriteLine("UserService SetBooksGoal: " + user.BooksGoal);

            _context.Users.Update(user);

            await _context.SaveChangesAsync();

            return new SetBookGoalResponseDto
            {
                StatusText = "BookGoalSet"
            };
        }

        public async Task<ResetBooksReadResponeDto> ResetBooksRead(int userId)
        {
            User user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);

            user.BooksRead = 0;

            _context.Users.Update(user);

            await _context.SaveChangesAsync();

            return new ResetBooksReadResponeDto
            {
                StatusText = "ResetBooksRead"
            };
        }

    }
}

