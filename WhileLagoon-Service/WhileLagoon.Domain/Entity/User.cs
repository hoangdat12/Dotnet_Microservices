using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using WhileLagoon.Domain.Common;
using WhileLagoon.Domain.Enum;

namespace WhileLagoon.Domain.Entity
{
    public class User: BaseEntity
    {
        [Required]
        [MaxLength(150)]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        [MaxLength(150)]
        public string LastName { get; set; } = string.Empty;
        [Required]
        [MaxLength(100)]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required]
        [JsonIgnore]
        public string Password { get; set; } = string.Empty;
        [JsonIgnore]
        public UserStatus Status { get; set; } = UserStatus.UN_ACTIVE;
        public Role Role { get; set; } = Role.USER;
        [JsonIgnore]
        public bool IsLocked { get; set; } = false;
        public string PhoneNumber { get; set; } = string.Empty;
        public Cart Cart {get; set;} 
    }
}
