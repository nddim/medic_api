using medic_api.Data;
using medic_api.Helpers;
using medic_api.Helpers.Auth;
using Microsoft.AspNetCore.Mvc;

namespace medic_api.Endpoints.Users.BlockById
{
    [MyAuthHandler("admin")]
    public class BlockByIdEndpoint:MyBaseEndpoint<int, BlockByIdResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public BlockByIdEndpoint(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        [HttpPost("block/{Id}")]
        public override async Task<ActionResult<BlockByIdResponse>> Obradi([FromRoute]int Id, CancellationToken cancellationToken = default)
        {
            var user = await _applicationDbContext.UserProfile.FindAsync(Id);
            if (user == null)
            {
                return BadRequest("Nije pronaden user sa tim Id-om");
            }

            user.Status = "Blocked";
            await _applicationDbContext.SaveChangesAsync(cancellationToken);
            return Ok();
        }
    }
}
