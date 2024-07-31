using medic_api.Data;
using medic_api.Data.Models;
using medic_api.Helpers;
using medic_api.Helpers.PasswordHash;
using Microsoft.AspNetCore.Mvc;

namespace medic_api.Endpoints.Auth.Generate
{
    public class GenerateAuthEndpoint:MyBaseEndpoint<GenerateAuthRequest,NoResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IPasswordHasher _passwordHasher;
        public GenerateAuthEndpoint(ApplicationDbContext applicationDbContext, IPasswordHasher passwordHasher)
        {
            _applicationDbContext = applicationDbContext;
            _passwordHasher = passwordHasher;
        }
        [HttpPost("generate-admin")]
        public override async Task<ActionResult<NoResponse>> Obradi([FromBody]GenerateAuthRequest request, CancellationToken cancellationToken = default)
        {
            if (request.Username == null || request.Name == null || request.Password == null)
            {
                return BadRequest("Pogresno unesene vrijednosti");
            }

            var hash = await _passwordHasher.Hash(request.Password);

            var user = new UserProfile()
            {
                Name = request.Name,
                Username = request.Username,
                DateOfBirth = DateTime.Now,
                LastLoginDate = DateTime.Now,
                Password = hash,
                Orders = 0,
                Status = "Active"
            };

           
            _applicationDbContext.UserProfile.Add(user);
            await _applicationDbContext.SaveChangesAsync(cancellationToken);

            var role = new UserRole()
            {
                RolesId = 1,
                UserProfileId = user.Id
            };

            _applicationDbContext.UserRole.Add(role);
            await _applicationDbContext.SaveChangesAsync(cancellationToken);

            return Ok();
        }
    }
}
