using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services;
using Moq;
using System.Text.RegularExpressions;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Application.Controllers;
using Application.Logger;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using Domain.Exceptions;
using Domain.APIModels;

namespace ApplicationTest.Controllers
{
    public class CarControllerTests
    {
        //readonly Mock<IClientService> _mockClientService = new();
        readonly IMapper _mapper = RealMapperFactory.Create();
        //readonly Mock<ILoggerAdapter<ClientController>> _mockLogger = new();

        readonly Client _sampleValidClient = new()
        { Address = "some", IDNumber = "11112222333", DateOfBirth = DateTime.Now, Email = "w@g.c", Name = "data", PhoneNumber = "111222333", Surname = "rostiashvili" };
        readonly Car _sampleValidCar = new()
        { Brand = "Honda", Model = "CR-V", ProductionYear = 2001, SellingPrice = 2500.00m, StateNumber = "ZR397RZ", VIN = "11112222333344445" };

        #region RegisterCarForClientAsync

        [Fact]
        public async Task RegisterCarForClientAsync__should_return_ok_for_valid_input()
        {
            var mockCarService = Mock.Of<ICarService>();
            Mock.Get(mockCarService).Setup(service => service.RegisterCarForClientAsync(It.IsAny<string>(), It.IsAny<Domain.DTOs.Car>()))
                .Returns(Task.CompletedTask);

            Mock<ILoggerAdapter<CarController>> mockLogger = new();
            mockLogger.Setup(logger => logger.LogInformation(It.IsAny<string>()));


            var clientController = new CarController(_mapper,
                Mock.Get(mockCarService).Object,
                mockLogger.Object);



            var res = await clientController.RegisterCarForClientAsync(It.IsAny<string>(), _sampleValidCar);

            mockLogger.Verify(logger => logger.LogInformation(It.IsAny<string>()), Times.Once());

            Assert.IsType<OkObjectResult>(res.Result);

        }

        [Fact]
        public async Task RegisterCarForClientAsync__should_return_bad_request_for_already_registered_car_for_given_client()
        {
            var mockCarService = Mock.Of<ICarService>();
            Mock.Get(mockCarService).Setup(service => service.RegisterCarForClientAsync(It.IsAny<string>(), It.IsAny<Domain.DTOs.Car>()))
                .Throws<CarAlreadyRegisteredForClientException>();

            Mock<ILoggerAdapter<CarController>> mockLogger = new();
            mockLogger.Setup(logger => logger.LogError(It.IsAny<Exception>(), It.IsAny<string>()));


            var clientController = new CarController(_mapper,
                Mock.Get(mockCarService).Object,
                mockLogger.Object);



            var res = await clientController.RegisterCarForClientAsync(It.IsAny<string>(), _sampleValidCar);

            mockLogger.Verify(logger => logger.LogError(It.IsAny<Exception>(), It.IsAny<string>()), Times.Once());

            Assert.IsType<BadRequestObjectResult>(res.Result);

        }

        [Fact]
        public async Task RegisterCarForClientAsync__should_return_bad_request_for_nonexistent_client()
        {
            var mockCarService = Mock.Of<ICarService>();
            Mock.Get(mockCarService).Setup(service => service.RegisterCarForClientAsync(It.IsAny<string>(), It.IsAny<Domain.DTOs.Car>()))
                .Throws<ClientDoesntExistsException>();

            Mock<ILoggerAdapter<CarController>> mockLogger = new();
            mockLogger.Setup(logger => logger.LogError(It.IsAny<Exception>(), It.IsAny<string>()));


            var clientController = new CarController(_mapper,
                Mock.Get(mockCarService).Object,
                mockLogger.Object);



            var res = await clientController.RegisterCarForClientAsync(It.IsAny<string>(), _sampleValidCar);

            mockLogger.Verify(logger => logger.LogError(It.IsAny<Exception>(), It.IsAny<string>()), Times.Once());

            Assert.IsType<BadRequestObjectResult>(res.Result);
        }

        #endregion

        #region BuyCarForClientAsync
        [Fact]
        public async Task BuyCarForClientAsync__should_return_ok_for_valid_input()
        {
            var mockCarService = Mock.Of<ICarService>();
            Mock.Get(mockCarService).Setup(service => service.BuyCarForClientAsync(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(Task.CompletedTask);

            Mock<ILoggerAdapter<CarController>> mockLogger = new();
            mockLogger.Setup(logger => logger.LogInformation(It.IsAny<string>()));


            var clientController = new CarController(_mapper,
                Mock.Get(mockCarService).Object,
                mockLogger.Object);



            var res = await clientController.BuyCarForClientAsync(It.IsAny<string>(), It.IsAny<string>());

            mockLogger.Verify(logger => logger.LogInformation(It.IsAny<string>()), Times.Once());

            Assert.IsType<OkResult>(res);

        }

        [Fact]
        public async Task BuyCarForClientAsync__should_return_bad_request_for_already_registered_car_for_given_client()
        {
            var mockCarService = Mock.Of<ICarService>();
            Mock.Get(mockCarService).Setup(service => service.BuyCarForClientAsync(It.IsAny<string>(), It.IsAny<string>()))
                .Throws<CarAlreadyRegisteredForClientException>();

            Mock<ILoggerAdapter<CarController>> mockLogger = new();
            mockLogger.Setup(logger => logger.LogError(It.IsAny<Exception>(), It.IsAny<string>()));


            var clientController = new CarController(_mapper,
                Mock.Get(mockCarService).Object,
                mockLogger.Object);



            var res = await clientController.BuyCarForClientAsync(It.IsAny<string>(), It.IsAny<string>());

            mockLogger.Verify(logger => logger.LogError(It.IsAny<Exception>(), It.IsAny<string>()), Times.Once());

            Assert.IsType<BadRequestObjectResult>(res);

        }


        [Fact]
        public async Task BuyCarForClientAsync__should_return_bad_request_for_nonexistent_client()
        {
            var mockCarService = Mock.Of<ICarService>();
            Mock.Get(mockCarService).Setup(service => service.BuyCarForClientAsync(It.IsAny<string>(), It.IsAny<string>()))
                .Throws<ClientDoesntExistsException>();

            Mock<ILoggerAdapter<CarController>> mockLogger = new();
            mockLogger.Setup(logger => logger.LogError(It.IsAny<Exception>(), It.IsAny<string>()));


            var clientController = new CarController(_mapper,
                Mock.Get(mockCarService).Object,
                mockLogger.Object);



            var res = await clientController.BuyCarForClientAsync(It.IsAny<string>(), It.IsAny<string>());

            mockLogger.Verify(logger => logger.LogError(It.IsAny<Exception>(), It.IsAny<string>()), Times.Once());

            Assert.IsType<BadRequestObjectResult>(res);

        }

        [Fact]
        public async Task BuyCarForClientAsync__should_return_bad_request_for_nonexistent_car()
        {
            var mockCarService = Mock.Of<ICarService>();
            Mock.Get(mockCarService).Setup(service => service.BuyCarForClientAsync(It.IsAny<string>(), It.IsAny<string>()))
                .Throws<CarDoesntExistsException>();

            Mock<ILoggerAdapter<CarController>> mockLogger = new();
            mockLogger.Setup(logger => logger.LogError(It.IsAny<Exception>(), It.IsAny<string>()));


            var clientController = new CarController(_mapper,
                Mock.Get(mockCarService).Object,
                mockLogger.Object);



            var res = await clientController.BuyCarForClientAsync(It.IsAny<string>(), It.IsAny<string>());

            mockLogger.Verify(logger => logger.LogError(It.IsAny<Exception>(), It.IsAny<string>()), Times.Once());

            Assert.IsType<BadRequestObjectResult>(res);

        }
        #endregion

        #region DeleteCarForClientAsync

        [Fact]
        public async Task DeleteCarForClientAsync__should_return_ok_for_valid_input()
        {
            var mockCarService = Mock.Of<ICarService>();
            Mock.Get(mockCarService).Setup(service => service.DeleteCarForClientAsync(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(Task.CompletedTask);

            Mock<ILoggerAdapter<CarController>> mockLogger = new();
            mockLogger.Setup(logger => logger.LogInformation(It.IsAny<string>()));


            var clientController = new CarController(_mapper,
                Mock.Get(mockCarService).Object,
                mockLogger.Object);



            var res = await clientController.DeleteCarForClientAsync(It.IsAny<string>(), It.IsAny<string>());

            mockLogger.Verify(logger => logger.LogInformation(It.IsAny<string>()), Times.Once());

            Assert.IsType<OkResult>(res);

        }

        [Fact]
        public async Task DeleteCarForClientAsync__should_return_bad_request_for_nonexistent_car()
        {
            var mockCarService = Mock.Of<ICarService>();
            Mock.Get(mockCarService).Setup(service => service.DeleteCarForClientAsync(It.IsAny<string>(), It.IsAny<string>()))
                .Throws<CarDoesntExistsException>();

            Mock<ILoggerAdapter<CarController>> mockLogger = new();
            mockLogger.Setup(logger => logger.LogError(It.IsAny<Exception>(), It.IsAny<string>()));


            var clientController = new CarController(_mapper,
                Mock.Get(mockCarService).Object,
                mockLogger.Object);



            var res = await clientController.DeleteCarForClientAsync(It.IsAny<string>(), It.IsAny<string>());

            mockLogger.Verify(logger => logger.LogError(It.IsAny<Exception>(), It.IsAny<string>()), Times.Once());

            Assert.IsType<BadRequestObjectResult>(res);

        }

        #endregion

        #region GetCarsForSale

        [Fact]
        public async Task GetCarsForSale__should_return_ok_for_valid_input()
        {
            var mockCarService = Mock.Of<ICarService>();
            Mock.Get(mockCarService).Setup(service => service.GetCarsForSale(It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                .Returns(It.IsAny<IEnumerable<Domain.DTOs.Car>>);

            Mock<ILoggerAdapter<CarController>> mockLogger = new();
            mockLogger.Setup(logger => logger.LogInformation(It.IsAny<string>()));


            var clientController = new CarController(_mapper,
                Mock.Get(mockCarService).Object,
                mockLogger.Object);



            var res = await clientController.RegisterCarForClientAsync(It.IsAny<string>(), _sampleValidCar);

            mockLogger.Verify(logger => logger.LogInformation(It.IsAny<string>()), Times.Once());

            Assert.IsType<OkObjectResult>(res.Result);

        }
        #endregion
    }
}
