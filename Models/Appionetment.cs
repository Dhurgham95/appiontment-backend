using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AppiontmentBackEnd.Models
{
    public class Appionetment
    {
       
       
        public int Id { get; set; }
        [Required]
        public string? DateOfAppiontment { get; set; }
        [JsonIgnore]
        public ICollection<UserAppionetment> UserAppionetments { get; set; }
        [Required]
        public string? Description { get; set; }
        [Required]
        public string? BodyParts { get; set; }
        [Required]
        public int StatusId { get; set; }
        public Status Status { get; set; }



    }
}
