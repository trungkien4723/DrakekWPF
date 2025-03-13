using Microsoft.EntityFrameworkCore;

namespace drakek.Model
{
    [Keyless]
    public class Stock
    {
        public string storage { get; set; }
        public string product { get; set; }
        public int quantity { get; set; }
        public DateTime expiredDate { get; set; }
        public DateTime createdDate { get; set; }
    }
}