using System.Text.Json.Serialization;

namespace ChineseSaleServer.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public float Total { get; set; }
        public int UserID { get; set; }
        [JsonIgnore]
        public User? User { get; set; }
    }
}
