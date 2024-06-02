using System.ComponentModel.DataAnnotations;

namespace Cargo.Data.Models
{
    public class City
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50,MinimumLength = 3)]
        public string Name { get; set; }

    }
}
