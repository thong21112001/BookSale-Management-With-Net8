using BookSale.Management.Application.Abstracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;


namespace BookSale.Management.Application.Services
{
	//Class dùng để lưu ảnh
	public class ImageService : IImageService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ImageService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<bool> SaveImage(List<IFormFile> images, string path, string? defaultName)
        {
            try
            {
                if (images?.Count == 0 || string.IsNullOrEmpty(path))
                {
                    return default;
                }

                string pathImage = Path.Combine(_webHostEnvironment.WebRootPath, path);//wwwroot/images/user

                if (!Directory.Exists(pathImage))
                {
                    Directory.CreateDirectory(pathImage);
                }

                foreach (var image in images)
                {
                    if (image is not null)
                    {
                        string originPath = Path.Combine(pathImage, !string.IsNullOrEmpty(defaultName) ? defaultName : image.Name);

                        using (var fileStream = new FileStream(originPath, FileMode.Create))
                        {
                            await image.CopyToAsync(fileStream);
                        }
                    }
                }
            }
            catch (Exception)
            {
                return default;
            }

            return true;
        }

        //wwwroot/images/..
        public string UrlSaveImg()
        {
            string pathImage = Path.Combine(_webHostEnvironment.WebRootPath);

            return pathImage;
        }
    }
}
