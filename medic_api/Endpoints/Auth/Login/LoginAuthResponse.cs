using medic_api.Data.Models;

namespace medic_api.Endpoints.Auth.Login
{
    public class LoginAuthResponse
    {
        public AutentifikacijaToken? AuthToken { get; set; }
        public bool IsLogiran { get; set; }

        public LoginAuthResponse(AutentifikacijaToken? token, bool isLogiran)
        {
            AuthToken = token;
            IsLogiran = isLogiran;
        }

    }
}
