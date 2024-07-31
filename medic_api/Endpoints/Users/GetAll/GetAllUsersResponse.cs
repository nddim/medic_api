namespace medic_api.Endpoints.Users.GetAll
{
    public class GetAllUsersResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public DateTime LastLoginDate { get; set; }
    }
}
