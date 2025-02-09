namespace BussinessLogic.Records
{
    public record OrderRec
    {
        public int Id { get; set; }
        public DateTime DesiredDate { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Service { get; set; }
        public int Status { get; set; }
    }
}
