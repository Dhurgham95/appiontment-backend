using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AppiontmentBackEnd.Models
{
    public class User
    {
      
        public int Id { get; set; }
        [Required]
        public string? UserName { get; set; }
        [Required]
        public string? FullName { get; set; }
        [Required]
        public string? PhoneNumber { get; set; }
        [Required]
        public string? Address { get; set; }
        [Required]
        public string? Gender { get; set; }
        public string? Email { get; set; }
        [Required]
        public bool? IsActive { get; set; }
        [Required]
        public string? Password { get; set; }
        public string? CreatedAt { get; set; }
        public int AdderId { get; set; } = 0;
        public string? Specialization { get; set; } 
        [JsonIgnore]
        public ICollection<UserRole> UserRoles { get; set; }
        [JsonIgnore]

        public ICollection<UserAppionetment> UserAppionetments { get; set; }


    }
}
