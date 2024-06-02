using System.ComponentModel.DataAnnotations;

namespace Cargo.Data.Models
{
    public class Office
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50,MinimumLength = 3)]
        public string Name { get; set; }

        [Required]
        public int CityId { get; set; }

        [Required]
        public City City { get; set; }
        public IEnumerable<Automobile> Automobiles { get; set; }
    }
}
