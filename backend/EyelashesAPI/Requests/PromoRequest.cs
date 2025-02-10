namespace EyelashesAPI.Requests
{
    public class PromoRequest
    {
        public string Title { get; set; }
        public string OldPrice { get; set; }
        public string NewPrice { get; set; }
        public string DiscountDescription { get; set; }
        public List<string> Benefits { get; set; }
    }
}
