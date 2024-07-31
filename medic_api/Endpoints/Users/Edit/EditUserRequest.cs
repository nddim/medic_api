namespace medic_api.Endpoints.Users.Edit
{
    public class EditUserRequest
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public int Orders { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Status { get; set; }

    }
}
