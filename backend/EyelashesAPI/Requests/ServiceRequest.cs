namespace EyelashesAPI.Requests
{
    public class ServiceRequest
    {
        public string Title { get; set; }
        public string MainImageUrl { get; set; }
        public decimal? Price { get; set; }
        public decimal? NewPrice { get; set; }
        public decimal? OldPrice { get; set; }
        public List<string> ImageUrls { get; set; }
    }
}
