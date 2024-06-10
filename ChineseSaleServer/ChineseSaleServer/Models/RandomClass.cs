using System.Text.Json.Serialization;

namespace ChineseSaleServer.Models
{
    public class RandomClass
    {
        public int Id { get; set; }
        public int UserID { get; set; }
        [JsonIgnore]
        public User? User { get; set; }
       
        public int GiftId { get; set; }
        [JsonIgnore]
        public Gift? Gift { get; set; }

    }
}
