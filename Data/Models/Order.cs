using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cargo.Data.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ClientId { get; set; }

        [Required]
        public Client Client { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }

        [Required]
        public int CityId { get; set; }

        [Required]
        public City City { get; set; }

        [Required]
        public int OfficeId { get; set; }

        [Required]
        public Office Office { get; set; }

        [Required]
        public int AutomobileId { get; set; }

        [Required]
        public Automobile Automobile { get; set; }

        [Required]
        [Column(TypeName = "money")]
        public decimal TotalCost { get; set; }

        [Required]
        public int TravelledKilometers { get; set; }

    }
}
