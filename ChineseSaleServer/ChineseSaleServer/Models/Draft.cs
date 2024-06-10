using System.Text.Json.Serialization;

namespace ChineseSaleServer.Models
{
    public class Draft
    {
        public int Id { get; set; }
        public int Quentity { get; set; }
        public int CartId { get; set; }
        [JsonIgnore]
        public Cart? Cart { get; set; }
        public int GiftId { get; set; }
        [JsonIgnore]
        public Gift? Gift { get; set; }

    }
}

