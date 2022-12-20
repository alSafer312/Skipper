using Microsoft.EntityFrameworkCore;

namespace Skipper.Core.Repositories
{
    public class UserSettingsRepository : GenericRepository<UserSettings>, IUserSettingsRepository
    {
        public UserSettingsRepository(DataContext context) : base(context)
        {
        }

        public async Task<UserSettings> GetByUserIdAsync(Guid id)
        {
            try
            {
                return await _context.UsersSettings
                    .Include(x => x.NotificationSettings)
                    .Include(x => x.CommunicationWays)
                    .FirstOrDefaultAsync(x => x.UserId == id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }
        public UserSettings GetByUserId(Guid id)
        {
            try
            {
                return _context.UsersSettings
                    .Include(x => x.User)
                    .Include(x => x.NotificationSettings)
                    .Include(x => x.CommunicationWays)
                    .FirstOrDefault(x => x.UserId == id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }
    }
}
