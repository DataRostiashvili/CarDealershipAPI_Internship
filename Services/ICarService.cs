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
        Task RegisterCarForClientAsync(string clientIDNumber, Car car);
        Task DeleteCarForClientAsync(string clientIDNumber, string carVINCode);
        IEnumerable<Car> GetCarsForClient(string clientIDNumber);
    }
}
