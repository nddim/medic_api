using System.ComponentModel.DataAnnotations;

namespace medic_api.Data.Models
{
    public class Roles
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

    }
}
