using System.Text.RegularExpressions;

namespace medic_api.Helpers.Image
{
    public class ImageHelper
    {
        public static string GetFilePath(string productCode, IWebHostEnvironment env)
        {
            return env.WebRootPath + "\\Uploads\\Images\\" + productCode;
        }

        public static string GetImageTypeFromBase64(string base64String)
        {
            //regex for extracting image type from base64 str
            var regex = new Regex(@"^data:image/(?<type>[a-zA-Z]+);base64,", RegexOptions.Compiled);

            // match with source
            var match = regex.Match(base64String);

            // if there is match, get value of group "type"
            if (match.Success)
            {
                return match.Groups["type"].Value.ToLower();
            }

            // if there is no match return null
            return null;
        }

    }
}
