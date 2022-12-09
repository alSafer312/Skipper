namespace Skipper.Core.Repositories
{
    public class UserSettingsRepository : GenericRepository<UserSettings>, IUserSettingsRepository
    {
        public UserSettingsRepository(DataContext context) : base(context)
        {
        }

        public async Task<UserSettings?> GetByUserId(Guid id)
        {
            try
            {
                return await _context.UsersSettings
                    .FirstOrDefaultAsync(x => x.Id == id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }
    }
}
