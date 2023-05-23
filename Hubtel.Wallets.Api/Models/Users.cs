using System.ComponentModel.DataAnnotations;

namespace Hubtel.Wallets.Api.Models
{
    public class Users
    {
        public int Id { get; set; }
        [Required] 
        public string Name { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Number { get; set; }
    }
}
