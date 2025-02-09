namespace BussinessLogic.Records
{
    public record ServiceRec
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string MainImagePath { get; set; }
        public decimal Price { get; set; }
        public decimal? NewPrice { get; set; }
        public decimal? OldPrice { get; set; }
        public List<string> ImagePaths { get; set; }
    }
}
