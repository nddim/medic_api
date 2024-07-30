using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace medic_api.Data.Models
{
    public class UserRole
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey(nameof(Userprofile))]
        public int UserProfileId { get; set; }
        public UserProfile Userprofile { get; set; }
        [ForeignKey(nameof(Roles))]
        public int RolesId { get; set; }
        public Roles Roles { get; set; }

    }
}
