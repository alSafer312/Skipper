namespace Skipper.Core.Repositories
{
    public class NotificationSettingsRepository : GenericRepository<NotificationSettings>, 
                                                                    INotificationSettingsRepository
    {
        public NotificationSettingsRepository(DataContext context) : base(context)
        {
        }
    }
}
