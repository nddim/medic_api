using medic_api.Data;
using medic_api.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace medic_api.Helpers.Auth
{
    public class MyAuthService
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IHttpContextAccessor _htttContextAccessor;

        public MyAuthService(ApplicationDbContext applicationDbContext, IHttpContextAccessor httpContextAccessor)
        {
            _applicationDbContext = applicationDbContext;
            _htttContextAccessor = httpContextAccessor;
        }

        public bool IsLogiran()
        {
            return GetAuthInfo().IsLogiran;
        }

        public MyAuthInfo GetAuthInfo()
        {
            string? authToken = _htttContextAccessor.HttpContext!.Request.Headers["my-auth-token"];

            AutentifikacijaToken? autentifikacijaToken = _applicationDbContext.AutentifikacijaToken
                .Include(x => x.UserProfile)
                .SingleOrDefault(x => x.Value == authToken);

            return new MyAuthInfo(autentifikacijaToken);
        }

        public async Task<bool> IsAdmin()
        {
            var userProfile = GetAuthInfo().UserProfile;
            if (userProfile == null)
            {
                return false;
            }

            var admin = await _applicationDbContext.UserRole
                .Where(x => x.UserProfileId == userProfile.Id && x.RolesId == 1).ToListAsync();

            return admin.Count > 0;
        }
    }

    public class MyAuthInfo
    {
        public AutentifikacijaToken? AutentifikacijaToken { get; set; }

        public MyAuthInfo(AutentifikacijaToken? autentifikacijaToken)
        {
            AutentifikacijaToken = autentifikacijaToken;
        }

        public UserProfile? UserProfile => AutentifikacijaToken?.UserProfile;
        public bool IsLogiran => UserProfile !=null;
    }
}
