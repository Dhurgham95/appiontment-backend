using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AppiontmentBackEnd.Models
{
    public class Status
    {
      
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }

        public string? Description { get; set; }
        [JsonIgnore]
        public ICollection<Appionetment> Appionetment { get; set; }


    }
}
