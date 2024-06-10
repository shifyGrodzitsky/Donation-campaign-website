using System.Text.Json.Serialization;

namespace ChineseSaleServer.Models
{
    public class Gift
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public float TicketPrice { get; set; }
        
        public int NumOfPurchases { get; set; } = 0;
        public int DonorId { get; set; }
       [JsonIgnore]
        public Donor? Donor { get; set; }

    }
}
