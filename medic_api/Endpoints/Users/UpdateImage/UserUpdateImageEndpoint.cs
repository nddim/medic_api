using medic_api.Data;
using medic_api.Helpers;
using medic_api.Helpers.Image;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using medic_api.Helpers.Auth;

namespace medic_api.Endpoints.Users.UpdateImage
{
    [MyAuthHandler("admin")]
    public class UserUpdateImageEndpoint:MyBaseEndpoint<UserUpdateImageRequest, NoResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public UserUpdateImageEndpoint(ApplicationDbContext applicationDbContext, IWebHostEnvironment webHostEnvironment)
        {
            _applicationDbContext = applicationDbContext;
            _webHostEnvironment = webHostEnvironment;
        }
        [HttpPost("update-image")]
        public override async Task<ActionResult<NoResponse>> Obradi(UserUpdateImageRequest request, CancellationToken cancellation = default)
        {
            var user = await _applicationDbContext.UserProfile.FindAsync(request.Id, cancellation);
            if (user == null)
            {
                return BadRequest($"Nema usera sa ID = {request.Id}");
            }
            var url = user.SlikaUrl;

            string imageUrl = string.Empty;
            string filepath = ImageHelper.GetFilePath(url, _webHostEnvironment);
            if (!filepath.ToLower().Contains("noimage") && System.IO.File.Exists(filepath))
            {
                System.IO.File.Delete(filepath);
            }

            byte[] byteArray = Convert.FromBase64String(request.Slika.Split(',')[1]);


            string fileName = Guid.NewGuid().ToString() + "." + ImageHelper.GetImageTypeFromBase64(request.Slika);
            string envFile = _webHostEnvironment.WebRootPath + "\\Uploads\\Images\\" + fileName;

            await System.IO.File.WriteAllBytesAsync(envFile, byteArray);
            user.SlikaUrl = fileName;

            await _applicationDbContext.SaveChangesAsync(cancellation);

            return Ok();
        }
    }
}
