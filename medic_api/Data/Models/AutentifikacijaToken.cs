using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace medic_api.Data.Models
{
    public class AutentifikacijaToken
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey(nameof(UserProfile))]
        public int UserProfileId { get; set; }
        public UserProfile UserProfile { get; set; }
        public string Value { get; set; }
        public DateTime TimeLogged { get; set; }

    }
}
