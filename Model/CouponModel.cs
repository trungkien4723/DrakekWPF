namespace drakek.Model
{
    public class Coupon
    {
        public string id { get; set; }
        public string name { get; set; }
        public int value { get; set; }
        public string valueType { get; set; }
        public string? description { get; set; }
        public DateTime? startDate { get; set; }
        public DateTime? endDate { get; set; }
        public DateTime createdDate { get; set; }
    }
}