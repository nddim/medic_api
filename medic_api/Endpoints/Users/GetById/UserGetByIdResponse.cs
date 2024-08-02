using System.Text.Json.Serialization;

namespace medic_api.Endpoints.Users.GetById
{
    public class UserGetByIdResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public int Orders { get; set; }
        public DateTime LastLoginDate { get; set; }
        public string? SlikaUrl { get; set; }
        public string Status { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
