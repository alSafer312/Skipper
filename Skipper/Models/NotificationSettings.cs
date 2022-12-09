using Skipper.Enums;

namespace Skipper.Models
{
    public class NotificationSettings
    {
        public Guid Id { get; set; }
        public Guid UserSettingsId { get; set; }
        public bool LessonStart { get; set; } = false;
        public bool Payment { get; set; } = false;
        public bool NewFeedback { get; set; } = false;
        public NotificationTypeEnum NotificationType { get; set; }
        public virtual UserSettings UserSettings { get; set; }
    }
}
