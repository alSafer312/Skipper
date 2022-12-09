using Skipper.Enums;

namespace Skipper.Models
{
    public class CommunicationWay
    {
        public Guid Id { get; set; }
        public Guid UserSettingsId { get; set; }
        public CommynicationTypeEnum CommunicationType { get; set; }
        public string CommunicationAddress { get; set; } = string.Empty;
        public virtual UserSettings UserSettings { get; set; }
    }
}
