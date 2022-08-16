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
        IEnumerable<User> GetAll();
        User GetById(int id);
        Task<AuthCreateResponseDto> Create(AuthCreateRequestDto dto);
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

        public async Task<AuthenticateResponseDto> Authenticate(AuthenticateRequestDto model)
        {
            
            var user = await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Username.Equals(model.Username) && u.Password.Equals(model.Password));

          
            // return null if user not found
            if (user == null) return null;

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

        public IEnumerable<User> GetAll()
        {
            return _users;
        }

        public User GetById(int id)
        {
            return _users.FirstOrDefault(x => x.Id == id);
        }

        private string generateJwtToken(User user)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
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
    }
}
