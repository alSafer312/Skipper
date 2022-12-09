using System;

namespace Skipper.Core
{
    public interface IUserSettingsRepository : IGenericRepository<UserSettings>
    {
        Task<UserSettings> GetByUserId(Guid userId);
    }
}
