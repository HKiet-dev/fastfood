using CloudinaryDotNet.Actions;
using CloudinaryDotNet;

namespace FrontEnd.Helper
{
    public class CloudinaryServices
    {
        private readonly Cloudinary _cloudinary;
        public CloudinaryServices()
        {
            var account = new Account(
                "dlfvbe9bi",
                "132196749226247",
                "QVy6ptdQ34UnKtjpx1weqvmOlj4"
            );
            _cloudinary = new Cloudinary(account);
        }

        public async Task<string> UploadImageAsync(Stream imageStream, string fileName)
        {
            var UploadParam = new ImageUploadParams
            {
                File = new FileDescription(fileName, imageStream),
                Overwrite = true
            };
            var result = await _cloudinary.UploadAsync(UploadParam);

            return result?.SecureUri.AbsoluteUri;
        }
    }
}
