using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
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
        Task<UserForgotPasswordResponseDto> ForgotPassword(UserForgotPasswordRequestDto dto);
        Task<UserResetPasswordResponseDto> ResetPassword(UserResetPasswordRequestDto dto, string token);
        Task<ConfirmEmailResponseDto> ConfirmEmail(string token);
    }
    public class UserService : IUserService
    {

        private readonly AppSettings _appSettings;
        private readonly ApplicationDBContext _context;
        private readonly IMailService _mailService;
        private readonly IPasswordHelper _passwordHelper;

        public UserService(IOptions<AppSettings> appSettings, ApplicationDBContext context, IOptions<MailSettings> mailSettings, IPasswordHelper passwordHelper)
        {
            _appSettings = appSettings.Value;
            _context = context;
            _mailService = new MailService(mailSettings);
            _passwordHelper = passwordHelper;
        }


        //AUTHENTICATE
        public async Task<AuthenticateResponseDto> Authenticate(AuthenticateRequestDto model)
        {

            var user = await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Email.Equals(model.Email));

            // return null if user not found
            if (user == null) return new AuthenticateResponseDto { };

            if (!user.EmailConfirmed) return new AuthenticateResponseDto { };

            if (!_passwordHelper.VerifyPassword(model.Password, user.PasswordHash, user.PasswordSalt))
            {
                return new AuthenticateResponseDto { };
            }

            // authentication successful so generate jwt token
            var token = generateJwtToken(user);


            return new AuthenticateResponseDto
            {
                Id = user.Id,
                FullName = user.FullName,
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
                    FullName = b.FullName
                })
            };
        }


        // GET BY ID
        public async Task<GetUserByIdResponseDto> GetById(int id)
        {
            var user = await _context.Users.AsNoTracking().FirstOrDefaultAsync(b => b.Id == id);

            if (user == null) return new GetUserByIdResponseDto { StatusText = "UserNotFound" };

            return new GetUserByIdResponseDto
            {
                FullName = user.FullName,
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
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()), new Claim("name", user.FullName), new Claim("username", user.FullName) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }


        // CREATE
        public async Task<CreateUserResponseDto> Create(CreateUserRequestDto dto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(d => d.Email == dto.Email);

            if(user != null)
            {
                return new CreateUserResponseDto
                {
                    StatusText = "EmailAlreadyInUse",
                    FullName = ""
                };
            }

            var confirmEmailToken = _passwordHelper.GenerateRandomString(40);

            var emailReciever = dto.Email;
            var emailRecievers = new List<string> { emailReciever };
            var emailSubject = "Welcome to Wemuda Books";
            var emailBody = "Hello " + dto.FullName + "<br>Welcome to Wemuda Books<br>Confirm your email here:<br>" +
                $"http://localhost:3000/confirmEmail?token={confirmEmailToken}";

            var email = new Email(emailRecievers, emailSubject, emailBody);

            var emailSent = await _mailService.SendAsync(email, new CancellationToken());

            if (!emailSent)
            {
                return new CreateUserResponseDto
                {
                    StatusText = "Invalid Email",
                    FullName = ""
                };
            }

            var (passwordHash, passwordSalt) = _passwordHelper.CreateHash(dto.Password);

            var entity = _context.Users.Add(new User
            {
                FullName = dto.FullName,
                Email = dto.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                ConfirmEmailToken = confirmEmailToken,
                EmailConfirmed = false
            });

            await _context.SaveChangesAsync();

            return new CreateUserResponseDto
            {
                StatusText = "User Created",
                FullName = entity.Entity.FullName
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

            if (user == null) return new ChangePasswordResponseDto { StatusText = "UserNotFound" };

            if (!_passwordHelper.VerifyPassword(dto.Password, user.PasswordHash, user.PasswordSalt))
            {
                return new ChangePasswordResponseDto { StatusText = "IncorrectPassword" };
            }

            var (passwordHash, passwordSalt) = _passwordHelper.CreateHash(dto.NewPassword);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            _context.Users.Update(user);
            
            await _context.SaveChangesAsync();

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

        public async Task<UserForgotPasswordResponseDto> ForgotPassword(UserForgotPasswordRequestDto dto)
        {
            var user = await _context.Users.Where(u => u.Email == dto.Email).FirstOrDefaultAsync();

            if (user == null)
            {
                return new UserForgotPasswordResponseDto { StatusText = "UserNotFoundException" };
            }

            var passwordResetToken = _passwordHelper.GenerateRandomString(40);

            user.ResetPasswordToken = passwordResetToken;
            user.ResetPasswordTokenExpire = DateTime.Now.AddDays(2);

            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            var emailBody = $"http://localhost:3000/reset?token={passwordResetToken}";
            var email = new Email(new List<string> { dto.Email }, "Reset password here", emailBody);


            var isMailSent = await _mailService.SendAsync(email, new CancellationToken());

            if (isMailSent == false)
            {
                return new UserForgotPasswordResponseDto { StatusText = "MailNotSentException" };
            }

            return new UserForgotPasswordResponseDto { StatusText = "MailSent" };
        }

        public async Task<UserResetPasswordResponseDto> ResetPassword(UserResetPasswordRequestDto dto, string token)
        {
            var user = await _context.Users.Where(u => u.ResetPasswordToken == token).FirstOrDefaultAsync();
            if (user == null)
            {
                return new UserResetPasswordResponseDto { StatusText = "InvalidToken" };
            }

            if (user.ResetPasswordTokenExpire < DateTime.Now)
            {
                return new UserResetPasswordResponseDto { StatusText = "TokenExpired" };
            }

            var (passwordHash, passwordSalt) = _passwordHelper.CreateHash(dto.NewPassword);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            user.ResetPasswordToken = null;
            _context.Update(user);
            await _context.SaveChangesAsync();

            return new UserResetPasswordResponseDto { StatusText = "PasswordChanged" };
        }

        public async Task<ConfirmEmailResponseDto> ConfirmEmail(string token)
        {
            var user = await _context.Users.Where(u => u.ConfirmEmailToken == token).FirstOrDefaultAsync();

            if (user == null) return new ConfirmEmailResponseDto { StatusText = "InvalidToken" };

            if (user.EmailConfirmed) return new ConfirmEmailResponseDto { StatusText = "EmailAlreadyConfirmed" };

            user.EmailConfirmed = true;

            _context.Update(user);
            await _context.SaveChangesAsync();

            return new ConfirmEmailResponseDto { StatusText = "EmailConfirmed" };
        }

    }
}

