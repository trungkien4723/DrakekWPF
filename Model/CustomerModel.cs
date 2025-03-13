namespace drakek.Model
{
    public class Customer
    {
        public string id { get; set; }
        public string name { get; set; }
        public string phone { get; set; }
        public string? address { get; set; }
        public string? city { get; set; }
        public string? district { get; set; }
        public string? ward { get; set; }
        public DateTime createdDate { get; set; }
    }
}