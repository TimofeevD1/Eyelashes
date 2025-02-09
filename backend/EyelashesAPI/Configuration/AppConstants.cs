namespace EyelashesAPI.Configuration
{
    public static class AppConstants
    {

        public static readonly string ImagesDirectory = Path.Combine("C:", "UploadedImages");

        public static void Init() 
        {
            if (!Directory.Exists(ImagesDirectory))
            {
                Directory.CreateDirectory(ImagesDirectory);
            }
        }
    }
}
