namespace DataAccess.Models
{
    public class AboutMe
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Job { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public NpgsqlTypes.NpgsqlPoint Coordinates { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }

        public List<Photo> Photos { get; set; } = new List<Photo>();
    }
}
