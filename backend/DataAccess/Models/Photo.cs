namespace DataAccess.Models
{
    public class Photo
    {
        public int Id { get; set; }
        public string Url { get; set; } 
        public int AboutMeId { get; set; } 
        public AboutMe AboutMe { get; set; }
    }
}
