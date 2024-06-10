using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ChineseSaleServer.Models
{
    // public enum Role
    //{
    //    admin,
    //    user
    //}
    public class User
    {

        public int Id { get; set; }
        [MaxLength(30)]
        public string FirstName { get; set; }
        [MaxLength(30)]
        public string LastName { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        [Phone]
        public string Phone { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [DefaultValue("user")]
        public string Role { get; set; }

       // public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
