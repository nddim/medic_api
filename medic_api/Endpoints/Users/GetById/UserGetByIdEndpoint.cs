using medic_api.Data;
using medic_api.Helpers;
using medic_api.Helpers.Auth;
using medic_api.Helpers.Image;
using Microsoft.AspNetCore.Mvc;

namespace medic_api.Endpoints.Users.GetById
{
    [MyAuthHandler("admin")]
    [Route("users")]
    public class UserGetByIdEndpoint:MyBaseEndpoint<int, UserGetByIdResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private IWebHostEnvironment _environment;
        public UserGetByIdEndpoint(ApplicationDbContext applicationDbContext, IWebHostEnvironment environment)
        {
            _applicationDbContext = applicationDbContext;
            _environment = environment;
        }
        [HttpGet("details/{Id}")]
        public override async Task<ActionResult<UserGetByIdResponse>> Obradi([FromRoute]int Id, CancellationToken cancellation = default)
        {
            var user = await _applicationDbContext.UserProfile.FindAsync(Id);
            if (user == null)
            {
                return BadRequest("Nije pronaden user sa tim Id-om");
            }
            return Ok(new UserGetByIdResponse()
            {
                Id =  user.Id,
                Name = user.Name,
                Username = user.Username,
                Status = user.Status,
                Orders = user.Orders,
                SlikaUrl = FileHelper.GetImageByUser(user.SlikaUrl, _environment),
                LastLoginDate = user.LastLoginDate,
                DateOfBirth = user.DateOfBirth
            });
        }
    }
}
