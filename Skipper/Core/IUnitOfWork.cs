namespace Skipper.Core
{
    public interface IUnitOfWork
    {
        IUserRepository Users { get; }
        IUserSettingsRepository UserSettings { get; }
        ICommunicationWayRepository CommunicationWays { get; }
        INotificationSettingsRepository NotificationSettings { get; }

        Task CompleteAsync();
    }
}
