using Skipper.Models.DTOs.Incomig;
using Skipper.Models.DTOs.Outgoing;

namespace Skipper.Services
{
    public interface IUserService
    {
        AuthenticateResponse Authenticate(AuthenticateRequest model);
        Task<AuthenticateResponse> Register(AuthenticateRequest model);
        UserSettingsResponse GetUpSettings();
        Task<UserSettingsResponse> SetUpSettings(UserSettingsRequest request);
        Task<string> Verify(string token);
    }
}
