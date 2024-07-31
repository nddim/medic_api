namespace medic_api.Endpoints.Users.Register
{
    public class RegisterUserRequest
    {
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int Orders { get; set; }
        public string? SlikaUrl { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
