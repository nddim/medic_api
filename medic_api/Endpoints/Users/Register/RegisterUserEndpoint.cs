using medic_api.Data;
using medic_api.Data.Models;
using medic_api.Helpers;
using medic_api.Helpers.Auth;
using medic_api.Helpers.Image;
using medic_api.Helpers.PasswordHash;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace medic_api.Endpoints.Users.Register
{
    [MyAuthHandler("admin")]
    public class RegisterUserEndpoint:MyBaseEndpoint<RegisterUserRequest, NoResponse>
    {

        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IWebHostEnvironment _environment;
        public RegisterUserEndpoint(ApplicationDbContext applicationDbContext, IPasswordHasher passwordHasher, IWebHostEnvironment environment)
        {
            _applicationDbContext = applicationDbContext;
            _passwordHasher = passwordHasher;
            _environment = environment;
        }
        [HttpPost("register")]
        public override async Task<ActionResult<NoResponse>> Obradi([FromBody]RegisterUserRequest request, CancellationToken cancellation = default)
        {
            if (request.Name == null || request.Username == null || request.Password == null ||
                request.Orders == null || request.DateOfBirth == null)
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
            if (request.SlikaUrl != null)
            {
                byte[] byteArray = Convert.FromBase64String(request.SlikaUrl.Split(',')[1]);

                string fileName = Guid.NewGuid().ToString() + "." + FileHelper.GetImageTypeFromBase64(request.SlikaUrl);
                string envFile = _environment.WebRootPath + "\\Uploads\\Images" + fileName;

                await System.IO.File.WriteAllBytesAsync(envFile, byteArray);

                noviUser.SlikaUrl = fileName;
            }

            _applicationDbContext.UserProfile.Add(noviUser);
            await _applicationDbContext.SaveChangesAsync(cancellation);

            var rola = new UserRole()
            {
                UserProfileId = noviUser.Id,
                RolesId = 2 // employee
            };

            _applicationDbContext.UserRole.Add(rola);
            await _applicationDbContext.SaveChangesAsync(cancellation);

            return Ok();
        }
       
    }
}
