namespace BookSale.Management.UI.Helpers
{
    public static class ImageHelper
    {
        public static string GetImageUrl(string imageUrl, HttpContext httpContext)
        {
            if (!string.IsNullOrEmpty(imageUrl))
            {
                if (imageUrl.StartsWith("https:"))
                {
                    // Nếu urlImg bắt đầu bằng "https:", sử dụng urlImg trực tiếp
                    return imageUrl;
                }
                else
                {
                    // Ngược lại, kết hợp urlImg với đường dẫn gốc của ứng dụng web
                    return $"{httpContext.Request.PathBase}/images/books/{imageUrl}";
                }
            }
            else
            {
                // Nếu không có urlImg, sử dụng hình ảnh mặc định
                return $"{httpContext.Request.PathBase}/images/image.png";
            }
        }
    }
}
