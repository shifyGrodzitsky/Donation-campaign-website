using System.ComponentModel.DataAnnotations;

namespace ChineseSaleServer.Models
{
    public class LoginModel
    {
        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
    }
}

















