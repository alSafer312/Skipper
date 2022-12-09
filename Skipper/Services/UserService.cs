using AutoMapper;
using Skipper.Helpers;
using Skipper.Models.DTOs.Incomig;
using Skipper.Models.DTOs.Outgoing;
using System.Security.Cryptography;
using Skipper.Extensions;
using Skipper.Core;
using System.Security.Claims;

namespace Skipper.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _contextAccessor;

        public UserService(IUnitOfWork unitOfWork,
                           IMapper mapper, 
                           IConfiguration configuration, 
                           IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _configuration = configuration;
            _contextAccessor = httpContextAccessor;
        }

        public async Task<AuthenticateResponse> Register(AuthenticateRequest request)
        {
            if (await _unitOfWork.Users.Any(request.Email))
            {
                // todo: need to add logger
                return null;
            }
            var user = _mapper.Map<User>(request);
            user.Role = "Unverified";
            user.VeryficationToken = _configuration.GenerateRandomToken();

            await _unitOfWork.Users.Add(user);
            await _unitOfWork.CompleteAsync();

            var response = Authenticate(new AuthenticateRequest
            {
                Email = request.Email,
                Password = request.Password
            });

            return response;
        }

        public AuthenticateResponse Authenticate(AuthenticateRequest request)
        {
            var user = _unitOfWork.Users.GetByEmail(request.Email);
            if(user == null)
            {
                //todo: logger
                return null;
            }
            if(!request.Password.TrustTo(user.PasswordHash, user.PasswordSalt))
            {
                //todo: logger
                return null;
            }
            var response = _mapper.Map<AuthenticateResponse>(user);
            response.AccesToken = _configuration.GenerateJwt(user);
            return response;
        }

        public UserSettingsResponse GetUpSettings()
        {
            var userSettings = _unitOfWork.UserSettings.GetByUserId(Guid.Parse(
                                                        _contextAccessor
                                                        .HttpContext.User                                            
                                                        .FindFirstValue(ClaimTypes.NameIdentifier)));
            if (userSettings == null)
            {
                //todo: logger
                return null;
            }
                return _mapper.Map<UserSettingsResponse>(userSettings);
        }

        public async Task<UserSettingsResponse> SetUpSettings(UserSettingsRequest request)
        {
            if (_contextAccessor.HttpContext == null)
            {
                //todo: logger
                return null;
            }
            var userId = _contextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userSettings = await _unitOfWork.UserSettings.GetByUserId(Guid.Parse(userId));
            if(userSettings == null)
            {
                //todo: logger
                return null;
            }
            userSettings = _mapper.Map<UserSettings>(request);

            //await _unitOfWork.UserSettings.Update(userSettings);
            await _unitOfWork.CompleteAsync();

            var response = GetUpSettings();

            return response;
        }

        public async Task<string> Verify(string token)
        {
            var user = await _unitOfWork.Users.GetByVerificationToken(token);
            if (user == null)
            {
                // todo: need to add logger
                return null;
            }
            user.Role = "Menty";
            await _unitOfWork.CompleteAsync();

            return "User verified!";
        }

    }
}
