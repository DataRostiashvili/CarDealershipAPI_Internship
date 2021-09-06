using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entity
{
    public class Car
    {
        public string Brand { get; set; }
        public string Model { get; set; }

        [RegularExpression("@^[A-Z]{2}[0-9]{3}[A-Z]{2}$")]
        public string StateNumber { get; set; }

        public int ProductionYear { get; set; }

        [MaxLength(17)]
        public string VIN { get; set; }

        [DataType(DataType.Currency)]
        public decimal SellingPrice { get; set; }

        [DataType(DataType.Date)]
        public DateTime SellingStartDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime SellingEndDate { get; set; }
    }
}
