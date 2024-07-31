using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace medic_api.Data.Models
{
    public class UserProfile
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        [JsonIgnore]
        public string Password { get; set; }
        public int Orders { get; set; }
        public DateTime LastLoginDate { get; set; }
        public string? SlikaUrl { get; set; }
        public string Status { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
