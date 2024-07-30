using medic_api.Data;
using medic_api.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace medic_api.Endpoints.Users.GetById
{

    [Route("users")]
    public class UserGetByIdEndpoint:MyBaseEndpoint<int, UserGetByIdResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public UserGetByIdEndpoint(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        [HttpGet("details/{Id}")]
        public override async Task<ActionResult<UserGetByIdResponse>> Obradi([FromRoute]int Id, CancellationToken cancellation = default)
        {
            var user = await _applicationDbContext.UserProfile.FindAsync(Id);
            if (user == null)
            {
                return BadRequest("Nije pronaden user sa tim Id-om");
            }
            return Ok(user);
        }
    }
}
