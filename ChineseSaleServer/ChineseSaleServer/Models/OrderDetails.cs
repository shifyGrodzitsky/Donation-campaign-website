using System.Text.Json.Serialization;

namespace ChineseSaleServer.Models
{
    public class OrderDetails
    {
        public int Id { get; set; }
        public int Quentity { get; set; }
        public int OrderId { get; set; }
        [JsonIgnore]
        public Order? Order { get; set; }
        public int GiftId { get; set; }
       [JsonIgnore]
        public Gift? Gift { get; set; }
        
    }
}