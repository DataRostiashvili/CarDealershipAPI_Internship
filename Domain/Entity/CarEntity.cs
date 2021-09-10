using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entity
{
    public class CarEntity  : BaseEntity
    {
        public int CarId { get; set; }


        [MaxLength(128)]
        [Required]
        public string Brand { get; set; }
        [MaxLength(128)]
        [Required]
        public string Model { get; set; }

        [RegularExpression(@"^[A-Z]{2}[0-9]{3}[A-Z]{2}$")]
        [Required]
        public string StateNumber { get; set; }

        [RegularExpression(@"^\d{4}$")]
        [Required]
        public int ProductionYear { get; set; }

        [MaxLength(17)]
        [Required]
        public string VIN { get; set; }

        [DataType(DataType.Currency)]
        [Required]
        public decimal SellingPrice { get; set; }

        [DataType(DataType.Date)]
        [Required]
        public DateTime SellingStartDate { get; set; }
        [DataType(DataType.Date)]
        [Required]
        public DateTime SellingEndDate { get; set; }

        public bool IsSold { get; set; }

        public ClientEntity Client { get; set; }
        public int ClientId { get; set; }
    }
}
