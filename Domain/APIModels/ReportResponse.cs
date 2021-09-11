using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.APIModels
{
    public class ReportResponse
    {
        public IEnumerable<MonthlyReport> monthlyReports { get; set; } = new List<MonthlyReport>();
    }

    public class MonthlyReport
    {
        public DateTime DateTime { get; set; }
        public uint TotalCarSold { get; set; }
        public decimal TotalSumOfPrices { get; set; }
        public decimal AveragePriceOfCar => TotalSumOfPrices / TotalCarSold;


    }
}
