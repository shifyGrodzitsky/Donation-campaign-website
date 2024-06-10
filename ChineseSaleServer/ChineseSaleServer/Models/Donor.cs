using System.ComponentModel.DataAnnotations;

namespace ChineseSaleServer.Models
{
    public class Donor
    {
        public int Id { get; set; }
        [MaxLength(30)]
        public string FirstName { get; set; }
        [MaxLength(30)]
        public string LastName { get; set; }
        public string Address { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Phone]
        public string Phone { get; set; }
        //ICollection<Gift> Gifts { get; set; } = new List<Gift>();

    }
}