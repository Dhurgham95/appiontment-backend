using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AppiontmentBackEnd.Models
{
    public class UserAppionetment
    {
      
        public int UserId { get; set; }
        [JsonIgnore]
        public User User { get; set; }
      
        public int AppionetmentId { get; set; }
        [JsonIgnore]
        public Appionetment Appionetment { get; set; }
    }
}
