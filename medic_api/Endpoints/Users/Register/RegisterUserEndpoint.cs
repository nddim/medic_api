using medic_api.Data;
using medic_api.Data.Models;
using medic_api.Helpers;
using medic_api.Helpers.PasswordHash;
using Microsoft.AspNetCore.Mvc;

namespace medic_api.Endpoints.Users.Register
{
    public class RegisterUserEndpoint:MyBaseEndpoint<RegisterUserRequest, NoResponse>
    {

        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IPasswordHasher _passwordHasher;
        public RegisterUserEndpoint(ApplicationDbContext applicationDbContext, IPasswordHasher passwordHasher)
        {
            _applicationDbContext = applicationDbContext;
            _passwordHasher = passwordHasher;
        }
        [HttpPost("register")]
        public override async Task<ActionResult<NoResponse>> Obradi([FromBody]RegisterUserRequest request, CancellationToken cancellation = default)
        {
            if (request.Name == null || request.Username == null || request.Password == null ||
                request.Orders == null || request.SlikaUrl == null || request.DateOfBirth == null)
            {
                return BadRequest("Nisu uneseni svi podaci");
            }

            var hash = await _passwordHasher.Hash(request.Password);

            var noviUser = new UserProfile()
            {
                Name = request.Name,
                Username = request.Username,
                Password = hash,
                Orders = request.Orders,
                DateOfBirth = request.DateOfBirth,
                LastLoginDate = DateTime.Now,
                SlikaUrl = request.SlikaUrl,
                Status = "Active"
            };

            _applicationDbContext.UserProfile.Add(noviUser);
            await _applicationDbContext.SaveChangesAsync(cancellation);

            return Ok();
        }
    }
}
