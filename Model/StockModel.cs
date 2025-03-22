using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace drakek.Model
{
    [Table("Stock")]
    public class Stock
    {
        [Key, Column(Order = 1)]
        public string storage { get; set; }
        [Key, Column(Order = 0)]
        public string product { get; set; }
        public int quantity { get; set; }
        public DateTime? expiredDate { get; set; }
        public DateTime createdDate { get; set; }
    }
}