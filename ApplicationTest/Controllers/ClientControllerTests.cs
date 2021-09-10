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
using TestsShared;

namespace ApplicationTest.Controllers
{
    public class ClientControllerTests
    {
        //readonly Mock<IClientService> _mockClientService = new();
        readonly IMapper _mapper = RealMapperFactory.Create();
        //readonly Mock<ILoggerAdapter<ClientController>> _mockLogger = new();

        readonly Domain.APIModels.ClientRequestResponse _sampleValidClient = new()
        { Address = "some", IDNumber = "11112222333", DateOfBirth = DateTime.Now, Email = "w@g.c", Name = "data", PhoneNumber = "111222333", Surname = "rostiashvili" };
        readonly Domain.APIModels.ClientRequestResponse _sampleInvalidClient = It.IsAny<Domain.APIModels.ClientRequestResponse>();




        #region RegisterClientAsync

        [Fact]
        public async Task RegisterClientAsync__should_return_ok_for_valid_input()
        {
            var mockClientService = Mock.Of<IClientService>();
            Mock.Get(mockClientService).Setup(service => service.InsertClientAsync(_mapper.Map<Domain.DTOs.ClientDto>(_sampleValidClient)))
                .Returns(Task.CompletedTask);

            Mock<ILoggerAdapter<ClientController>> mockLogger = new();
            mockLogger.Setup(logger => logger.LogInformation(It.IsAny<string>()));


            var clientController = new ClientController(Mock.Get(mockClientService).Object,
               _mapper, mockLogger.Object);



            var res = await clientController.RegisterClientAsync(_sampleValidClient);

            mockLogger.Verify(logger => logger.LogInformation(It.IsAny<string>()), Times.Once());

            Assert.IsType<CreatedAtActionResult>(res.Result);

        }

        [Fact]
        public async Task RegisterClientAsync__should_return_bad_request_result_object_for_registered_user()
        {
            var mockClientService = Mock.Of<IClientService>();
            Mock.Get(mockClientService).Setup(service => service.InsertClientAsync(It.IsAny<Domain.DTOs.ClientDto>()))
                .Throws<ClientAlreadyExistsException>();


            Mock<ILoggerAdapter<ClientController>> mockLogger = new();
            mockLogger.Setup(logger => logger.LogError(It.IsAny<Exception>(), It.IsAny<string>()));



            var clientController = new ClientController(Mock.Get(mockClientService).Object,
               _mapper, mockLogger.Object);

            var res = await clientController.RegisterClientAsync(_sampleValidClient);

            mockLogger.Verify(logger => logger.LogError(It.IsAny<Exception>(), It.IsAny<string>()), Times.Once());

            Assert.IsType<BadRequestObjectResult>(res.Result);

        }
        #endregion


        #region GetClient
        [Fact]
        public void GetClient__should_return_bad_request_for_nonexistent_client()
        {
            var mockClientService = Mock.Of<IClientService>();
            Mock.Get(mockClientService).Setup(service => service.GetClient(It.IsAny<string>()))
                .Throws<ClientDoesntExistsException>();


            Mock<ILoggerAdapter<ClientController>> mockLogger = new();
            mockLogger.Setup(logger => logger.LogError(It.IsAny<Exception>(), It.IsAny<string>()));



            var clientController = new ClientController(Mock.Get(mockClientService).Object,
               _mapper, mockLogger.Object);

            var res = clientController.GetClient(It.IsAny<string>());

            mockLogger.Verify(logger => logger.LogError(It.IsAny<Exception>(), It.IsAny<string>()), Times.Once());

            Assert.IsType<BadRequestObjectResult>(res.Result);
        }

        [Fact]
        public void GetClient__should_return_ok_for_exsiting_client()
        {
            var mockClientService = Mock.Of<IClientService>();
            Mock.Get(mockClientService).Setup(service => service.GetClient(It.IsAny<string>()))
                .Returns(It.IsAny<Domain.DTOs.ClientDto>());


            Mock<ILoggerAdapter<ClientController>> mockLogger = new();
            mockLogger.Setup(logger => logger.LogInformation( It.IsAny<string>()));



            var clientController = new ClientController(Mock.Get(mockClientService).Object,
               _mapper, mockLogger.Object);

            var res = clientController.GetClient(It.IsAny<string>());


            Assert.IsType<OkObjectResult>(res.Result);
        }

        #endregion

        #region UpdateClient

        [Fact]  
        public async Task UpdateClient__should_return_bad_request_result_object_for_nonexistent_user()
        {
            var mockClientService = Mock.Of<IClientService>();
            Mock.Get(mockClientService).Setup(service => service.UpdateClientAsync(It.IsAny<Domain.DTOs.ClientDto>()))
                .Throws<ClientDoesntExistsException>();


            Mock<ILoggerAdapter<ClientController>> mockLogger = new();
            mockLogger.Setup(logger => logger.LogError(It.IsAny<Exception>(), It.IsAny<string>()));



            var clientController = new ClientController(Mock.Get(mockClientService).Object,
               _mapper, mockLogger.Object);

            var res = await clientController.UpdateClientAsync(_sampleValidClient);

            mockLogger.Verify(logger => logger.LogError(It.IsAny<Exception>(), It.IsAny<string>()), Times.Once());

            Assert.IsType<BadRequestObjectResult>(res);

        }

        [Fact]
        async Task UpdateClient__should_return_ok_result_object_for_valid_user()
        {
            var mockClientService = Mock.Of<IClientService>();
            Mock.Get(mockClientService).Setup(service => service.UpdateClientAsync(It.IsAny<Domain.DTOs.ClientDto>()))
                .Returns(Task.CompletedTask);


            Mock<ILoggerAdapter<ClientController>> mockLogger = new();
            mockLogger.Setup(logger => logger.LogInformation(It.IsAny<string>()));



            var clientController = new ClientController(Mock.Get(mockClientService).Object,
               _mapper, mockLogger.Object);

            var res = await clientController.UpdateClientAsync(_sampleValidClient);
            mockLogger.Verify(logger=> logger.LogInformation(It.IsAny<string>()), Times.Once());

            Assert.IsType<OkResult>(res);
        }

        #endregion


        #region DeleteClient
        [Fact]
        public async Task DeleteClientAsync__should_return_bad_request_for_nonexistent_user()
        {
            var mockClientService = Mock.Of<IClientService>();
            Mock.Get(mockClientService).Setup(service => service.DeleteClientAsync(It.IsAny<string>()))
                .Throws<ClientDoesntExistsException>();


            Mock<ILoggerAdapter<ClientController>> mockLogger = new();
            mockLogger.Setup(logger => logger.LogError(It.IsAny<Exception>(), It.IsAny<string>()));



            var clientController = new ClientController(Mock.Get(mockClientService).Object,
               _mapper, mockLogger.Object);

            var res = await clientController.DeleteClientAsync(It.IsAny<string>());

            mockLogger.Verify(logger => logger.LogError(It.IsAny<Exception>(), It.IsAny<string>()), Times.Once());

            Assert.IsType<BadRequestObjectResult>(res);

        }

        [Fact]
        public async Task DeleteClientAsync__should_return_ok_for_valid_user()
        {
            var mockClientService = Mock.Of<IClientService>();
            Mock.Get(mockClientService).Setup(service => service.DeleteClientAsync(It.IsAny<string>()))
                .Returns(Task.CompletedTask);


            Mock<ILoggerAdapter<ClientController>> mockLogger = new();
            mockLogger.Setup(logger => logger.LogInformation(It.IsAny<string>()));



            var clientController = new ClientController(Mock.Get(mockClientService).Object,
               _mapper, mockLogger.Object);

            var res = await clientController.DeleteClientAsync(It.IsAny<string>());
            mockLogger.Verify(logger => logger.LogInformation(It.IsAny<string>()), Times.Once());

            Assert.IsType<OkResult>(res);
        }
        #endregion

    }
}
