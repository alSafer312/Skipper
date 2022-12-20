using AutoMapper.Configuration.Annotations;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Skipper.Models
{
    public class UserSettings
    {
        public UserSettings()
        {
            CommunicationWays = new HashSet<CommunicationWay>();
            NotificationSettings = new HashSet<NotificationSettings>();
        }
        [Ignore]
        public Guid Id { get; set; }
        [Ignore]
        public Guid UserId { get; set; }
        [JsonIgnore]
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
