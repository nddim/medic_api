using System.Text.RegularExpressions;

namespace medic_api.Helpers.Image
{
    public class FileHelper
    {
        public static string GetImageByUser(string path, IWebHostEnvironment env)
        {
            string imageUrl = string.Empty;
            string hostUrl = "https://localhost:7110";
            string filePath = GetFilePath(path, env);
            if (!System.IO.File.Exists(filePath))
            {
                imageUrl = hostUrl + "/Uploads/Images/" + "NoImage.png";
            }
            else
            {
                imageUrl = hostUrl + "/Uploads/Images/" + path;
            }

            return imageUrl;
        }
        public static string GetFilePath(string userCode, IWebHostEnvironment env, string putanja = "\\Uploads\\Images\\")
        {
            return env.WebRootPath + putanja + userCode;
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

            // if there is no match return png format
            return "png";
        }

    }
}
