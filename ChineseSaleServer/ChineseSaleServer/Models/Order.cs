using System.Text.Json.Serialization;

namespace ChineseSaleServer.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public float Total { get; set; }
        public int UserID { get; set; }
        [JsonIgnore]
        public User? User { get; set; }
    }
    // ICollection<OrderDetails> OrderDetails { get; set; }


}


