namespace EyelashesAPI.Requests
{
    public class AboutMeRequest
    {
        public string? FullName { get; set; }
        public string? Job { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public string? Description { get; set; }
        public string? MainImageUrl { get; set; }
        public List<string>? AdditionalPhotoUrls { get; set; }
    }

}
