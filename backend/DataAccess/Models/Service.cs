namespace DataAccess.Models
{
    public class Service
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string MainImage { get; set; }
        public decimal Price { get; set; }
        public decimal? NewPrice { get; set; }
        public decimal? OldPrice { get; set; }
        public List<ServiceImage> Images { get; set; }
    }
}
