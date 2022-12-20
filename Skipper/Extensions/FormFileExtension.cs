namespace Skipper.Extensions
{
    public static class FormFileExtension
    {
        public static async Task<string> UploadAvatarAsync(this IFormFile file, IWebHostEnvironment webHostEnvironment, Guid userId)
        {
            string folder = "Images/Avatars/";
            string filename = $"{userId}{Path.GetExtension(file.FileName)}";
            string path = Path.Combine(webHostEnvironment.WebRootPath, folder, filename);

            if(!Directory.Exists(Path.Combine(webHostEnvironment.WebRootPath, folder)))
                Directory.CreateDirectory(Path.Combine(webHostEnvironment.WebRootPath, folder));

            if (File.Exists(path))
            {
                File.Delete(path);
            }

            string AvatarURL = "https://192.168.0.101:7107/" + folder + filename;

            using (FileStream fs = File.Create(path))
            {
                await file.OpenReadStream().CopyToAsync(fs);
            }

            return AvatarURL;
        }
    }
}
