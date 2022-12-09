using System.ComponentModel.DataAnnotations;

namespace Skipper.Models
{
    public class UserSettings
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<CommunicationWay> CommunicationWays { get; set; }
        public virtual ICollection<NotificationSettings> NotificationSettings { get; set; }
        public string AvatarURL { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string Patronymic { get; set; } = string.Empty;
        public DateTime? DateOfBirth { get; set; }
        public string TimeZoneId { get; set; } = string.Empty;
        public string AboutMe { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public bool BrowserNotifications { get; set; } = false;
    }
}
