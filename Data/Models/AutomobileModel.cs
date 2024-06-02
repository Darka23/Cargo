using System.ComponentModel.DataAnnotations;

namespace Cargo.Data.Models
{
    public class AutomobileModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Manufacturer { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string ModelName { get; set; }

        [Required]
        public DateTime ReleaseYear { get; set; }

        [Required]
        public int MaxLoad { get; set; }

        [Required]
        public int FuelConsumption { get; set; }
    }
}
