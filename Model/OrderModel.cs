namespace drakek.Model
{
    public class Order
    {
        public string id { get; set; }
        public string products { get; set; }
        public string people { get; set; }
        public string customer { get; set; }
        public string? coupon { get; set; }
        public string orderType { get; set; }
        public string? description { get; set; }
        public int? paid { get; set; }
        public int? discount { get; set; }
        public DateTime createdDate { get; set; }
        public int totalPrice { get; set; }
    }

    public class OrderProduct{
        public string product { get; set; }
        public string storage { get; set; }
        public int quantity { get; set; }
        public int price { get; set; }
        public DateTime? expiredDate { get; set; }
    }

    public class ProductOnStock{
        public string id { get; set; }
        public string name { get; set; }
    }
}