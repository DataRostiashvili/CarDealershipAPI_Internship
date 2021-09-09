using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.DTOs;
using Repository.RepositoryPattern;
using AutoMapper;
using Domain.Exceptions;

namespace Services
{
    public class CarService : ICarService
    {
        readonly IRepository<Domain.Entity.Car> _carRepository;
        readonly IRepository<Domain.Entity.Client> _clientRepository;
        readonly IMapper _mapper;


        public CarService(IRepository<Domain.Entity.Car> carRepository,
            IRepository<Domain.Entity.Client> clientRepository,
            IMapper mapper)
        {
            _carRepository = carRepository;
            _clientRepository = clientRepository;
            _mapper = mapper;
        }

        public async Task RegisterCarForClientAsync(string clientIDNumber, Car car)
        {
            var clientEntity = _clientRepository
                .GetByPredicate(entityClient => entityClient.IDNumber == clientIDNumber)
                .FirstOrDefault();

            if (clientEntity == null)
            {
                throw new ClientDoesntExistsException("client with the given IDNumber doesn't exists");
            }

            foreach (var carOfClient in GetCarsForClient(clientIDNumber))
            {
                if (carOfClient.VIN == car.VIN)
                {
                    var msg = $"car (VIN: {car.VIN}) for the client (ID: {clientIDNumber}) already exists in database";
                    throw new CarAlreadyRegisteredForClientException(msg);
                }
            }

            clientEntity.Cars ??= new List<Domain.Entity.Car>();
            clientEntity.Cars.Add(_mapper.Map<Domain.Entity.Car>(car));

            await _clientRepository.UpdateAsync(clientEntity);
        }
        public async Task BuyCarForClientAsync(string clientIDNumber, string carVINCode)
        {
            var clientEntity = _clientRepository
                .GetByPredicate(entityClient => entityClient.IDNumber == clientIDNumber)
                .FirstOrDefault();

            
              _ = clientEntity ??  throw new ClientDoesntExistsException("client with the given IDNumber doesn't exists");
            

            var carEntity = _carRepository
                .GetByPredicate(car => car.VIN == carVINCode)
                .FirstOrDefault();

            _ = clientEntity ?? throw new CarDoesntExistsException($"car (VIN: {carVINCode} doesn't exists");


            foreach (var carOfClient in GetCarsForClient(clientIDNumber))
            {
                if (carOfClient.VIN == carVINCode)
                {
                    var msg = $"car (VIN: {carVINCode}) for the client (ID: {clientIDNumber}) already exists in database";
                    throw new CarAlreadyRegisteredForClientException(msg);
                }
            }


            carEntity.Client = clientEntity;
            await _carRepository.UpdateAsync(carEntity);
            

        }
        public async Task DeleteCarForClientAsync(string clientIDNumber, string carVINCode)
        {
            var carToDelete = GetCarsForClient(clientIDNumber)
                .Where(carEntity => carEntity.VIN == carVINCode).FirstOrDefault();

            if (carToDelete is null)
            {
                var errMsg = $"either client (ID: {clientIDNumber}) or car (VIN: {carVINCode} for the given client doesn't exists";
                throw new InvalidRequestException(errMsg);
            }

            await _carRepository.DeleteByPredicateAsync(carEntity => carEntity.VIN == carVINCode);
        }
        public IEnumerable<Car> GetCarsForSale(DateTime from, DateTime to)
        {
            return GetAllCars().Where(car => car.SellingStartDate > from && car.SellingEndDate < to);
        }
        
        public IEnumerable<Car> GetCarsForClient(string clientIDNumber)
        {
            var entityList = _carRepository
                .GetByPredicate(entityCar => entityCar.Client.IDNumber == clientIDNumber);

            return _mapper.Map<Domain.DTOs.Car[]>(entityList);
        }


        #region private methods
        private IEnumerable<Car> GetAllCars() =>
            _mapper.Map<Domain.DTOs.Car[]>(_carRepository.GetAll());

        #endregion

    }
}
