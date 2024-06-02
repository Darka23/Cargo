using System.ComponentModel.DataAnnotations;

namespace Cargo.Data.Models
{
    public class Client
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 3)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 3)]
        public string LastName { get; set; }
        
        [Required]
        public char Gender { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        [Required]
        public string CreditCardNumber { get; set; }

        [Required]
        public DateTime CreditCardExpirationDate { get; set; }
        
        [Required]
        [EmailAddress]
        public string Email { get; set; }

    }
}
