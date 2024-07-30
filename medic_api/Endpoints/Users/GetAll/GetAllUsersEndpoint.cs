using medic_api.Data;
using medic_api.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace medic_api.Endpoints.Users.GetAll
{
    public class GetAllUsersEndpoint:MyBaseEndpoint<NoRequest, GetAllUsersResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public GetAllUsersEndpoint(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        [HttpGet("users")]
        public override async Task<ActionResult<GetAllUsersResponse>> Obradi([FromQuery]NoRequest request, CancellationToken cancellation = default)
        {
            var user = await _applicationDbContext.UserProfile.ToListAsync(cancellation);
            if (user == null)
            {
                return BadRequest("Nije pronaden user sa tim Id-om");
            }
            return Ok(user);
        }
    }
}
