using medic_api.Data;
using medic_api.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace medic_api.Endpoints.Users.Edit
{
    public class EditUserEndpoint:MyBaseEndpoint<EditUserRequest, NoResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public EditUserEndpoint(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        [HttpPost("edit")]
        public override async Task<ActionResult<NoResponse>> Obradi([FromBody]EditUserRequest request, CancellationToken cancellation = default)
        {
            var user = await _applicationDbContext.UserProfile.FindAsync(request.Id, cancellation);
            if (user == null)
            {
                return BadRequest($"Nema usera sa Id {request.Id} ");
            }

            if (request.Name == null || request.Username == null || request.DateOfBirth == null ||
                request.Status == null || request.Orders == 0)
            {
                return BadRequest("Nisu unijeti podaci");
            }
            
            user.Name = request.Name;
            user.Username = request.Username;
            user.DateOfBirth = request.DateOfBirth;
            user.Status = request.Status;
            user.Orders = request.Orders;

            await _applicationDbContext.SaveChangesAsync(cancellation);

            return Ok();
        }
}
}
