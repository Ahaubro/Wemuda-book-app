using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NuGet.Common;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Wemuda_book_app.Data;
using Wemuda_book_app.Helpers;
using Wemuda_book_app.Model;
using Wemuda_book_app.Shared;

namespace Wemuda_book_app.Service
{
    public interface IUserService
    {
        Task<AuthenticateResponseDto> Authenticate(AuthenticateRequestDto model);
        Task<AuthGetAllResponseDto> GetAll();
        Task<AuthGetByIdResponseDto> GetById(int id);
        Task<AuthCreateResponseDto> Create(AuthCreateRequestDto dto);
        Task<AuthDeleteResponseDto> Delete(int id);
    }
    public class UserService : IUserService
    {

        private readonly AppSettings _appSettings;
        private readonly ApplicationDBContext _context;
        private List<User> _users;

        public UserService(IOptions<AppSettings> appSettings, ApplicationDBContext context)
        {
            _appSettings = appSettings.Value;
            _context = context;

        }


        //AUTHENTICATE
        public async Task<AuthenticateResponseDto> Authenticate(AuthenticateRequestDto model)
        {

            var user = await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Username.Equals(model.Username) && u.Password.Equals(model.Password));


            // return null if user not found
            if (user == null) return new AuthenticateResponseDto { };

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
        public async Task<AuthGetAllResponseDto> GetAll()
        {
            var allUsers = _context.Users.ToList();

            return new AuthGetAllResponseDto
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
        public async Task<AuthGetByIdResponseDto> GetById(int id)
        {
            var user = await _context.Users.AsNoTracking().FirstOrDefaultAsync(b => b.Id == id);


            return new AuthGetByIdResponseDto
            { 
                FirstName = user.FirstName,
                LastName = user.LastName,
                Username = user.Username,
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
        public async Task<AuthCreateResponseDto> Create(AuthCreateRequestDto dto)
        {
            var entity = _context.Users.Add(new User
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Username = dto.Username,
                Password = dto.Password
            });

            await _context.SaveChangesAsync();

            return new AuthCreateResponseDto
            {
                StatusText = "UserCreated",
                FirstName = entity.Entity.FirstName,
                LastName = entity.Entity.LastName,
                Username = entity.Entity.Username,
            };
        }


        // DELETE
        public async Task<AuthDeleteResponseDto> Delete(int id)
        {

            var user = await _context.Users.FirstOrDefaultAsync(d => d.Id == id);

            //INSERT EXCEPTION


            _context.Users.Remove(user);

            await _context.SaveChangesAsync();

            return new AuthDeleteResponseDto
            {
                StatusText = "UserDeleted"
            };

        }
    }
}

