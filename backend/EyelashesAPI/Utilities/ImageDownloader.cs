using EyelashesAPI.Configuration;

namespace EyelashesAPI.Utilities
{
    public static class ImageDownloader
    {
        public static async Task<string> DownloadImageAsync(HttpClient httpClient, string imageUrl, CancellationToken cancellationToken)
        {
            var fileName = Guid.NewGuid().ToString();
            var filePath = Path.Combine("", fileName);

            try
            {
                using (var response = await httpClient.GetAsync(imageUrl, cancellationToken))
                {
                    if (!response.IsSuccessStatusCode)
                    {
                        throw new Exception($"Error downloading image from {imageUrl}. Status code: {response.StatusCode}");
                    }

                    var contentType = response.Content.Headers.ContentType.ToString();

                    var fileExtension = GetFileExtensionFromContentType(contentType);

                    filePath += fileExtension;

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await response.Content.CopyToAsync(stream);
                    }
                }

                return filePath;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to download image from URL: {imageUrl}", ex);
            }
        }

        private static string GetFileExtensionFromContentType(string contentType)
        {
            return contentType.ToLower() switch
            {
                "image/jpeg" => ".jpg",
                "image/png" => ".png",
                "image/gif" => ".gif",
                "image/bmp" => ".bmp",
                "image/webp" => ".webp",
                _ => string.Empty
            };
        }
    }
}
