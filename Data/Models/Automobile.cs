using System.ComponentModel.DataAnnotations;

namespace Cargo.Data.Models
{
    public class Automobile
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int AutomobileModelId { get; set; }

        [Required]
        public AutomobileModel AutomobileModel { get; set; }

        [Required]
        public int OfficeId { get; set; }

        [Required]
        public Office Office { get; set; }

        [Required]
        public int Speedometer { get; set; }
    }
}
