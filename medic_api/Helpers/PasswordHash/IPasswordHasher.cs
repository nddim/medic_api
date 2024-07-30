namespace medic_api.Helpers.PasswordHash
{
    public interface IPasswordHasher
    {
        Task<string> Hash(string password);
        Task<bool> Verify(string passwordHash, string inputPassword);
    }
}
