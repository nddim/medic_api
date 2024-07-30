using medic_api.Data;
using medic_api.Data.Models;
using medic_api.Helpers;
using medic_api.Helpers.Auth;
using medic_api.Helpers.PasswordHash;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace medic_api.Endpoints.Auth.Login
{
    public class LoginAuthEndpoint:MyBaseEndpoint<LoginAuthRequest, MyAuthInfo>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IPasswordHasher _passwordHasher;

        public LoginAuthEndpoint(ApplicationDbContext applicationDbContext, IPasswordHasher passwordHasher)
        {
            _applicationDbContext = applicationDbContext;
            _passwordHasher = passwordHasher;
        }
        [HttpPost("login")]
        public override async Task<ActionResult<MyAuthInfo>> Obradi([FromBody]LoginAuthRequest request, CancellationToken cancellationToken = default)
        {
            UserProfile? loginUserProfile =
                await _applicationDbContext.UserProfile.FirstOrDefaultAsync(x => x.Username == request.Username,
                    cancellationToken);

            if (loginUserProfile == null) // pogresan username
            {
                return BadRequest(new MyAuthInfo(null));
            }

            var hashPassword = await _passwordHasher.Verify(loginUserProfile.Password, request.Password);

            if (!hashPassword)
            {
                return Unauthorized(new MyAuthInfo(null));
            }

            var roleIds = await _applicationDbContext.UserRole.Where(x => x.UserProfileId == loginUserProfile.Id)
                .Select(x => x.RolesId).ToListAsync(cancellationToken);

            var roleNames = await _applicationDbContext.Roles.Where(x => roleIds.Contains(x.Id))
                .Select(x => x.Name.ToLower()).ToListAsync(cancellationToken);

            if (!roleNames.Contains("admin"))
            {
                return NotFound("Nemate privilegije admina");
            }

            string randomString = TokenGenerator.Generate(10);

            var newToken = new AutentifikacijaToken()
            {
                TimeLogged = DateTime.Now,
                UserProfile = loginUserProfile,
                Value = randomString,
            };

            _applicationDbContext.Add(newToken);
            await _applicationDbContext.SaveChangesAsync(cancellationToken);

            return Ok(new MyAuthInfo(newToken));
        }
    }
}
