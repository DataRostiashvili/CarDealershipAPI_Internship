using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using TestsShared;
using AutoMapper;
using Repository.RepositoryPattern;
using Moq;
using Services;
using Domain.Exceptions;
using Domain.Entity;
using Domain.DTOs;

namespace ServicesTest
{
    public class CarServiceTests
    {
        readonly IMapper _mapper = RealMapperFactory.Create();

        readonly ClientEntity _sampleValidClientEntity = new ClientEntity
        {
            IsActive = true,
            Name = "Data",
            Surname = "Rostiashvili",
            IDNumber = "11112222333",
            DateOfBirth = DateTime.Now,
            ClientContactInfoEntity = new ClientContactInfoEntity { IsActive = true, Address = "misamarti", Email = "s@g.c", PhoneNumber = "555666777" }
        };

        readonly CarEntity _sampleValidCarEntity = new CarEntity { IsActive = true, Brand = "Honda", Model = "CR-V", ProductionYear = 2001, SellingPrice = 2500.00m, StateNumber = "ZR397RZ", VIN = "11112222333344445" };


        readonly CarDto _sampleValidCarDto;
        readonly ClientDto _sampleValidClientDto;

        public CarServiceTests()
        {
            _sampleValidClientDto = _mapper.Map<ClientDto>(_sampleValidClientEntity);
            _sampleValidCarDto = _mapper.Map<CarDto>(_sampleValidCarEntity);
        }

        #region RegisterCarForClient

        [Fact]
        public async Task RegisterCarForClientAsync__should_throw_for_nonexistent_client()
        {
            var carRepository = MockRepositoryFactory.Create<CarEntity>();
            var clientRepository = MockRepositoryFactory.Create<ClientEntity>();

            var carService = new CarService(carRepository.Object, clientRepository.Object, _mapper);

            await Assert.ThrowsAsync<ClientDoesntExistsException>(async ()=> await carService.RegisterCarForClientAsync(It.IsAny<string>(), _sampleValidCarDto));

            clientRepository.Verify(rep => rep.GetByPredicate(It.IsAny<Func<ClientEntity, bool>>()), Times.Once());


        }

        [Fact]
        public async Task RegisterCarForClientAsync__should_throw_for_already_registered_car_for_client()
        {
            var carRepository = MockRepositoryFactory.Create<CarEntity>();

            carRepository.Setup(rep => rep.GetByPredicate(It.IsAny<Func<CarEntity, bool>>()))
                .Returns(new[] { new CarEntity { VIN = "11112222333344445" } });

            var clientRepository = MockRepositoryFactory.Create<ClientEntity>();

            clientRepository.Setup(rep => rep.GetByPredicate(It.IsAny<Func<ClientEntity, bool>>()))
                .Returns(new[] { _sampleValidClientEntity });

            var carService = new CarService(carRepository.Object, clientRepository.Object, _mapper);

            await Assert.ThrowsAsync<CarAlreadyRegisteredForClientException>(async () => await carService.RegisterCarForClientAsync(It.IsAny<string>(), _sampleValidCarDto ));

            clientRepository.Verify(rep => rep.GetByPredicate(It.IsAny<Func<ClientEntity, bool>>()), Times.Once());
            carRepository.Verify(rep => rep.GetByPredicate(It.IsAny<Func<CarEntity, bool>>()), Times.Once());



        }

        [Fact]
        public async Task RegisterCarForClientAsync__should_register_car_for_valid_inputs()
        {
            //Arrange
            var carRepository = MockRepositoryFactory.Create<CarEntity>();

            carRepository.Setup(rep => rep.GetByPredicate(It.IsAny<Func<CarEntity, bool>>()))
                .Returns(new[] { new CarEntity { VIN = "11112222333399995" } });

            var clientRepository = MockRepositoryFactory.Create<ClientEntity>();

            clientRepository.Setup(rep => rep.GetByPredicate(It.IsAny<Func<ClientEntity, bool>>()))
                .Returns(new[] { _sampleValidClientEntity });

            var carService = new CarService(carRepository.Object, clientRepository.Object, _mapper);

            //Act
            await carService.RegisterCarForClientAsync(It.IsAny<string>(), _sampleValidCarDto);

            //Assert
            clientRepository.Verify(rep => rep.GetByPredicate(It.IsAny<Func<ClientEntity, bool>>()), Times.Once());
            clientRepository.Verify(rep => rep.UpdateAsync(It.IsAny<ClientEntity>()), Times.Once());

            carRepository.Verify(rep => rep.GetByPredicate(It.IsAny<Func<CarEntity, bool>>()), Times.Once());



        }
        #endregion

        #region BuyCarForClientAsync

        [Fact]
        public async Task BuyCarForClientAsync__should_buy_car_for_valid_inputs()
        {
            //Arrange
            var carRepository = MockRepositoryFactory.Create<CarEntity>();

            carRepository.Setup(rep => rep.GetByPredicate(It.IsAny<Func<CarEntity, bool>>()))
                .Returns(new[] { new CarEntity { VIN = "11112222333399995" } });

            var clientRepository = MockRepositoryFactory.Create<ClientEntity>();

            clientRepository.Setup(rep => rep.GetByPredicate(It.IsAny<Func<ClientEntity, bool>>()))
                .Returns(new[] { _sampleValidClientEntity });

            var carService = new CarService(carRepository.Object, clientRepository.Object, _mapper);

            //Act
            await carService.BuyCarForClientAsync(It.IsAny<string>(), It.IsAny<string>());

            //Assert
            clientRepository.Verify(rep => rep.GetByPredicate(It.IsAny<Func<ClientEntity, bool>>()), Times.Once());

            carRepository.Verify(rep => rep.UpdateAsync(It.IsAny<CarEntity>()), Times.Once());
            carRepository.Verify(rep => rep.GetByPredicate(It.IsAny<Func<CarEntity, bool>>()), Times.Exactly(2));


        }

        [Fact]
        public async Task BuyCarForClientAsync__should_throw_for_nonexistent_car()
        {
            //Arrange
            var carRepository = MockRepositoryFactory.Create<CarEntity>();

            carRepository.Setup(rep => rep.GetByPredicate(It.IsAny<Func<CarEntity, bool>>()))
                .Returns(It.IsAny<IEnumerable<CarEntity>>());

            var clientRepository = MockRepositoryFactory.Create<ClientEntity>();

            clientRepository.Setup(rep => rep.GetByPredicate(It.IsAny<Func<ClientEntity, bool>>()))
                .Returns(new[] { _sampleValidClientEntity });

            var carService = new CarService(carRepository.Object, clientRepository.Object, _mapper);

            
            //Act and Assert

            await Assert.ThrowsAsync<CarDoesntExistsException>
                (async () => await carService.BuyCarForClientAsync(It.IsAny<string>(), It.IsAny<string>()));

            clientRepository.Verify(rep => rep.GetByPredicate(It.IsAny<Func<ClientEntity, bool>>()), Times.Once());

            carRepository.Verify(rep => rep.UpdateAsync(It.IsAny<CarEntity>()), Times.Never());
            carRepository.Verify(rep => rep.GetByPredicate(It.IsAny<Func<CarEntity, bool>>()), Times.Once());


        }

        [Fact]
        public async Task BuyCarForClientAsync__should_throw_for_nonexistent_client()
        {
            //Arrange
            var carRepository = MockRepositoryFactory.Create<CarEntity>();

            var clientRepository = MockRepositoryFactory.Create<ClientEntity>();

            clientRepository.Setup(rep => rep.GetByPredicate(It.IsAny<Func<ClientEntity, bool>>()))
                .Returns(It.IsAny<IEnumerable<ClientEntity>>());

            var carService = new CarService(carRepository.Object, clientRepository.Object, _mapper);


            //Act and Assert

            await Assert.ThrowsAsync<ClientDoesntExistsException>
                (async () => await carService.BuyCarForClientAsync(It.IsAny<string>(), It.IsAny<string>()));

            clientRepository.Verify(rep => rep.GetByPredicate(It.IsAny<Func<ClientEntity, bool>>()), Times.Once());

            carRepository.Verify(rep => rep.UpdateAsync(It.IsAny<CarEntity>()), Times.Never());
            carRepository.Verify(rep => rep.GetByPredicate(It.IsAny<Func<CarEntity, bool>>()), Times.Never());


        }

        [Fact]
        public async Task BuyCarForClientAsync__should_throw_for_already_registered_car()
        {
            //Arrange
            var carRepository = MockRepositoryFactory.Create<CarEntity>();

            carRepository.Setup(rep => rep.GetByPredicate(It.IsAny<Func<CarEntity, bool>>()))
                .Returns(new[] { new CarEntity { VIN = _sampleValidCarEntity.VIN } });

            var clientRepository = MockRepositoryFactory.Create<ClientEntity>();

            clientRepository.Setup(rep => rep.GetByPredicate(It.IsAny<Func<ClientEntity, bool>>()))
                .Returns(new[] { _sampleValidClientEntity });

            var carService = new CarService(carRepository.Object, clientRepository.Object, _mapper);


            //Act and Assert

            await Assert.ThrowsAsync<CarAlreadyRegisteredForClientException>
                (async () => await carService.BuyCarForClientAsync(It.IsAny<string>(), carVINCode: _sampleValidCarEntity.VIN));

            clientRepository.Verify(rep => rep.GetByPredicate(It.IsAny<Func<ClientEntity, bool>>()), Times.Once());

            carRepository.Verify(rep => rep.UpdateAsync(It.IsAny<CarEntity>()), Times.Never());
            carRepository.Verify(rep => rep.GetByPredicate(It.IsAny<Func<CarEntity, bool>>()), Times.Exactly(2));


        }

        #endregion


        #region DeleteCarForClientAsync

        [Fact]
        public async Task DeleteCarForClientAsync__should_throw_for_nonexistent_car()
        {
            //Arrange
            var carRepository = MockRepositoryFactory.Create<CarEntity>();

            var clientRepository = MockRepositoryFactory.Create<ClientEntity>();

            var carService = new CarService(carRepository.Object, clientRepository.Object, _mapper);


            //Act and Assert

            await Assert.ThrowsAsync<CarDoesntExistsException>
                (async () => await carService.DeleteCarForClientAsync(It.IsAny<string>(), It.IsAny<string>()));

            carRepository.Verify(rep => rep.UpdateAsync(It.IsAny<CarEntity>()), Times.Never());
            carRepository.Verify(rep => rep.GetByPredicate(It.IsAny<Func<CarEntity, bool>>()), Times.Once());


        }

        [Fact]
        public async Task DeleteCarForClientAsync__should_delete_car_for_valid_inputs()
        {
            //Arrange
            var carRepository = MockRepositoryFactory.Create<CarEntity>();

            carRepository.Setup(rep => rep.GetByPredicate(It.IsAny<Func<CarEntity, bool>>()))
             .Returns(new[] { new CarEntity { VIN = _sampleValidCarEntity.VIN } });

            var clientRepository = MockRepositoryFactory.Create<ClientEntity>();

            clientRepository.Setup(rep => rep.GetByPredicate(It.IsAny<Func<ClientEntity, bool>>()))
              .Returns(new[] { _sampleValidClientEntity });

            var carService = new CarService(carRepository.Object, clientRepository.Object, _mapper);


            //Act and Assert

           await carService.DeleteCarForClientAsync(It.IsAny<string>(), _sampleValidCarEntity.VIN);

            carRepository.Verify(rep => rep.DeleteByPredicateAsync(It.IsAny<Func<CarEntity, bool>>()), Times.Once());
            carRepository.Verify(rep => rep.GetByPredicate(It.IsAny<Func<CarEntity, bool>>()), Times.Once());


        }
        #endregion


        #region GetCarsForClient

        [Fact]
        public async Task DeleteCarForClientAsync__should_get_cars_for_client()
        {
            //Arrange
            var carRepository = MockRepositoryFactory.Create<CarEntity>();

            carRepository.Setup(rep => rep.GetByPredicate(It.IsAny<Func<CarEntity, bool>>()))
             .Returns(new[] { new CarEntity { VIN = _sampleValidCarEntity.VIN } });

            var clientRepository = MockRepositoryFactory.Create<ClientEntity>();

            var carService = new CarService(carRepository.Object, clientRepository.Object, _mapper);


            //Act and Assert

            carService.GetCarsForClient(It.IsAny<string>());

          
            carRepository.Verify(rep => rep.GetByPredicate(It.IsAny<Func<CarEntity, bool>>()), Times.Once());


        }

        #endregion
    }
}
