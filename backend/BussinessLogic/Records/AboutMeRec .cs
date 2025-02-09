namespace BussinessLogic.Records
{
    public record AboutMeRec
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Job { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public NpgsqlTypes.NpgsqlPoint Coordinates { get; set; }
        public string Description { get; set; }
        public string MainImagePath { get; set; } 
        public List<string> AdditionalPhotosPath { get; set; } 
    }
}
