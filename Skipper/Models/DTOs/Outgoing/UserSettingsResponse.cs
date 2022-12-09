namespace Skipper.Models.DTOs.Outgoing
{
    public class UserSettingsResponse
    {
        public string AvatarURL { get; set; }
        public string LastName { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string Patronymic { get; set; } = string.Empty;
        public DateTime? DateOfBirth { get; set; }
        public string TimeZoneId { get; set; } = string.Empty;
        public string AboutMe { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public bool BrowserNotifications { get; set; } = false;
        public ICollection<NotificationSettings> NotificationSettings { get; set; }
        public ICollection<CommunicationWay> Links { get; set; }
    }
}
