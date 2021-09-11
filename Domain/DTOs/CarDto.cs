using System;

namespace Domain.DTOs
{
    public class CarDto : BaseDto
    {

        public string Brand { get; set; }
        public string Model { get; set; }
        public string StateNumber { get; set; }
        public int ProductionYear { get; set; }
        public string VIN { get; set; }
        public decimal SellingPrice { get; set; }
        public DateTime SellingStartDate { get; set; }
        public DateTime SellingEndDate { get; set; }
        public bool IsSold { get; set; }

    }
}
