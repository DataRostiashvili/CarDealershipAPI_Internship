using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.DTOs;
using Repository.RepositoryPattern;
using AutoMapper;
using Domain.APIModels;
using Domain.Exceptions;

namespace Services
{
    public class ReportService : IReportService
    {
        readonly ICarService _carService;
        readonly IClientService _clientService;
        readonly IMapper _mapper;

        public ReportService(ICarService carService,
           IClientService clientService,
           IMapper mapper)
        {
            _carService = carService;
            _clientService = clientService;
            _mapper = mapper;
        }


        public async Task<ReportResponse> GetReport()
        {
            var groupedCars = _carService.GetAllCars()
                 .Where(car => car.IsSold == true)
                 .GroupBy(car => car.SellingEndDate.Date);

            var monthlyReports = new List<MonthlyReport>();
            foreach (var carGroup in groupedCars)
            {
                monthlyReports.Add(new MonthlyReport 
                { 
                    DateTime = carGroup.Key,
                    TotalCarSold = (uint)carGroup.Count(),
                    TotalSumOfPrices = carGroup.Sum(car=> car.SellingPrice)

                });
            }

            return new ReportResponse { monthlyReports = monthlyReports };

        }

    }
}
