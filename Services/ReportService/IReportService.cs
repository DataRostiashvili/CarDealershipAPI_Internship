using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.APIModels;

namespace Services
{
    public interface IReportService
    {
        Task<ReportResponse> GetReport();
    }
}
