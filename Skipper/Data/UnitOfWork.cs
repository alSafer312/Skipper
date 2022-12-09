using Skipper.Core;
using Skipper.Core.Repositories;

namespace Skipper.Data
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly DataContext _context;
        public IUserRepository Users { get; private set; }
        public IUserSettingsRepository UserSettings { get; private set; }
        public INotificationSettingsRepository NotificationSettings { get; private set; }
        public ICommunicationWayRepository CommunicationWays { get; private set; }


        public UnitOfWork(DataContext context)
        {
            _context = context;

            Users = new UserRepository( _context );
            UserSettings = new UserSettingsRepository( _context );
            NotificationSettings = new NotificationSettingsRepository( _context );
            CommunicationWays = new CommunicationWayRepository( _context );
        }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
