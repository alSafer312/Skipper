using AutoMapper;
using Skipper.Helpers;
using Skipper.Models.DTOs.Incomig;
using Skipper.Models.DTOs.Outgoing;
using Skipper.Extensions;
using Skipper.Core;
using System.Security.Claims;
using Skipper.Enums;
using System.IO;

namespace Skipper.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public UserService(IUnitOfWork unitOfWork,
                           IMapper mapper,
                           IConfiguration configuration,
                           IHttpContextAccessor httpContextAccessor,
                           IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _configuration = configuration;
            _contextAccessor = httpContextAccessor;
            _webHostEnvironment = webHostEnvironment;
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
            if (user == null)
            {
                //todo: logger
                return null;
            }
            if (!request.Password.TrustTo(user.PasswordHash, user.PasswordSalt))
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
                                                        .HttpContext
                                                        .User
                                                        .FindFirstValue(ClaimTypes.NameIdentifier)));
            if (userSettings == null)
            {
                //todo: logger
                return null;
            }
            var response = _mapper.Map<UserSettingsResponse>(userSettings);
            return response;
        }

        public async Task<UserSettingsResponse> SetUpSettings(UserSettingsRequest request)
        {
            if (_contextAccessor.HttpContext == null)
            {
                //todo: logger
                return null;
            }

            var userId = _contextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userSettings = await _unitOfWork.UserSettings.GetByUserIdAsync(Guid.Parse(userId));
            if (userSettings == null)
            {
                //todo: logger
                return null;
            }

            _mapper.Map<UserSettingsRequest, UserSettings>(request, userSettings);
            await _unitOfWork.CompleteAsync();

            var response = GetUpSettings();

            return response;
        }

        public async Task<bool> UserDelete()
        {
            if (_contextAccessor.HttpContext == null)
            {
                //todo: logger
                return false;
            }
            var UserSettings = _unitOfWork.UserSettings.GetByUserId(Guid.Parse(
                                                        _contextAccessor
                                                        .HttpContext
                                                        .User
                                                        .FindFirstValue(ClaimTypes.NameIdentifier)));

            if (System.IO.File.Exists(Path.Combine(_webHostEnvironment.WebRootPath,
                                                    "Images/Avatars/",
                                                    Path.GetFileName(UserSettings.AvatarURL))))
            {
                System.IO.File.Delete(Path.Combine(_webHostEnvironment.WebRootPath,
                                                    "Images/Avatars/",
                                                    Path.GetFileName(UserSettings.AvatarURL)));
            }                

            var result = await _unitOfWork.Users.Delete(UserSettings.User);
            await _unitOfWork.CompleteAsync();
            return result;
        }

        public async Task<string> UploadAvatar(IFormFile file)
        {
            if (_contextAccessor.HttpContext == null)
            {
                //todo: logger
                return null;
            }
            var userSettings = _unitOfWork.UserSettings.GetByUserId(Guid.Parse(
                                                        _contextAccessor
                                                        .HttpContext
                                                        .User
                                                        .FindFirstValue(ClaimTypes.NameIdentifier)));
            try
            {
                userSettings.AvatarURL = await file.UploadAvatarAsync(_webHostEnvironment, userSettings.User.Id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            await _unitOfWork.CompleteAsync();
            return "Avatar uploaded successfully";
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

        public Dictionary<byte, string> GetCommynicationTypes()
        {
            var commynicationTypes = new Dictionary<byte, string>
            {
                [(byte)CommynicationTypeEnum.Skype] = "Skype",
                [(byte)CommynicationTypeEnum.GoogleHangouts] = "Google Hangouts",
                [(byte)CommynicationTypeEnum.Telegram] = "Telegram",
                [(byte)CommynicationTypeEnum.Zoom] = "Zoom",
                [(byte)CommynicationTypeEnum.Discord] = "Discord",
                [(byte)CommynicationTypeEnum.VK] = "VK",
                [(byte)CommynicationTypeEnum.WhatsApp] = "WhatsApp"
            };
            return commynicationTypes;
        }
        public Dictionary<string, string> GetTimeZones()
        {
            var TimeZones = new Dictionary<string, string>();
            foreach (var item in TimeZoneInfo.GetSystemTimeZones())
            {
                TimeZones.Add(item.Id, item.DisplayName);
            }            
            return TimeZones;
        }
    }
}
