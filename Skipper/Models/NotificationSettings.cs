using AutoMapper.Configuration.Annotations;
using Skipper.Enums;
using System.Text.Json.Serialization;

namespace Skipper.Models
{
    public class NotificationSettings
    {
        [Ignore]
        [JsonIgnore]
        public Guid Id { get; set; }
        [Ignore]
        [JsonIgnore]
        public Guid UserSettingsId { get; set; }
        public bool LessonStart { get; set; } = false;
        public bool Payment { get; set; } = false;
        public bool NewFeedback { get; set; } = false;
        public NotificationTypeEnum NotificationType { get; set; }
        [JsonIgnore]
        public virtual UserSettings UserSettings { get; set; }
    }
}
