using AutoMapper.Configuration.Annotations;
using Skipper.Enums;
using System.Text.Json.Serialization;

namespace Skipper.Models
{
    public class CommunicationWay
    {
        [Ignore]
        [JsonIgnore]
        public Guid Id { get; set; }
        [Ignore]
        [JsonIgnore]
        public Guid UserSettingsId { get; set; }
        public CommynicationTypeEnum CommunicationType { get; set; }
        public string CommunicationAddress { get; set; } = string.Empty;
        [JsonIgnore]
        public virtual UserSettings UserSettings { get; set; }
    }
}
