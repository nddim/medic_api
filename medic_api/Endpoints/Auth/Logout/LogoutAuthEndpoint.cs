using medic_api.Data;
using medic_api.Data.Models;
using medic_api.Helpers;
using medic_api.Helpers.Auth;
using Microsoft.AspNetCore.Mvc;

namespace medic_api.Endpoints.Auth.Logout
{

    public class LogoutAuthEndpoint:MyBaseEndpoint<NoRequest,NoResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly MyAuthService _authService;

        public LogoutAuthEndpoint(ApplicationDbContext applicationDbContext, MyAuthService myAuthService)
        {
            _applicationDbContext = applicationDbContext;
            _authService = myAuthService;
        }

        [HttpPost("logout")]
        public override async Task<ActionResult<NoResponse>> Obradi([FromBody] NoRequest request, CancellationToken cancellationToken = default)
        {
            AutentifikacijaToken? autentifikacijaToken = _authService.GetAuthInfo().AutentifikacijaToken;

            if (autentifikacijaToken == null)
            {
                return NotFound();
            }

            _applicationDbContext.Remove(autentifikacijaToken);
            await _applicationDbContext.SaveChangesAsync(cancellationToken);

            return Ok();
        }
    }
}
