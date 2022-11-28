using Skipper.Helpers;
using System.Security.Cryptography;

namespace Skipper.Services
{
    public class UserService : IUserService
    {
        private readonly DataContext _dataContext;
        private readonly IConfiguration _configuration;

        public UserService(DataContext dataContext, IConfiguration configuration)
        {
            _dataContext = dataContext;
            _configuration = configuration;
        }

        public AuthenticateResponse Authenticate(AuthenticateRequest request)
        {
            var user = _dataContext.Users.FirstOrDefault(u => u.Email == request.Email);
            if(user == null)
            {
                // todo: need to add logger
                return null;
            }
            if (!VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
            {
                // todo: need to add logger
                return null;
            }

            var token = _configuration.GenerateJwt(user);
            return new AuthenticateResponse(user, token);
        }

        public async Task<AuthenticateResponse> Register(AuthenticateRequest request)
        {
            var user = new User();
            if (_dataContext.Users.Any(u => u.Email == request.Email))
            {
                // todo: need to add logger
                return null;
            }
            CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

            user.Email = request.Email;
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            user.Role = "Unveryfied";

            _dataContext.Users.Add(user);
            await _dataContext.SaveChangesAsync();
            var response = Authenticate(new AuthenticateRequest
            {
                Email = request.Email,
                Password = request.Password
            });

            return response;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }
    }
}
