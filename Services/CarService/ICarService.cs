using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.DTOs;
namespace Services
{
    public interface ICarService
    {
        Task RegisterCarForClientAsync(string clientIDNumber, CarDto car);
        Task DeleteCarForClientAsync(string clientIDNumber, string carVINCode);
        IEnumerable<CarDto> GetCarsForClient(string clientIDNumber);
        IEnumerable<CarDto> GetCarsForSale(DateTime from, DateTime to);
        Task BuyCarForClientAsync(string clientIDNumber, string carVINCode);
        IEnumerable<CarDto> GetAllCars();
    }
}
