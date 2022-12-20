using System;

namespace Skipper.Core
{
    public interface IUserSettingsRepository : IGenericRepository<UserSettings>
    {
        Task<UserSettings> GetByUserIdAsync(Guid userId);
        UserSettings GetByUserId(Guid userId);
    }
}
