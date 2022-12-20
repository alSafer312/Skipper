using Skipper.Enums;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Skipper.Models
{
    public class User
    {
        public Guid Id { get; set; }
        //public Guid UserSettingsId { get; set; }
        public string Email { get; set; } = string.Empty;

        //public UserRoleEnum Role { get; set; }

        public string Role { get; set; } = string.Empty;
        public byte[] PasswordHash { get; set; } =  new byte[32];
        public byte[] PasswordSalt { get; set; } = new byte[32];
        public string VeryficationToken { get; set; } = string.Empty;
        [JsonIgnore]
        public virtual UserSettings UserSettings { get; set; }
    }
}
