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

namespace ApplicationTest.Controllers
{
    public class ClientControllerTests
    {
        //readonly Mock<IClientService> _mockClientService = new();
        readonly IMapper _mapper = MockMapperFactory.Create();
        //readonly Mock<ILoggerAdapter<ClientController>> _mockLogger = new();

        readonly Domain.APIModels.Client _sampleValidClient = new()
        { Address = "some", IDNumber = "11112222333", DateOfBirth = DateTime.Now, Email = "w@g.c", Name = "data", PhoneNumber = "111222333", Surname = "rostiashvili" };
        readonly Domain.APIModels.Client _sampleInvalidClient = It.IsAny<Domain.APIModels.Client>();






        [Fact]
        public async Task RegisterClientAsync__should_return_ok_for_valid_input()
        {
            var mockClientService = Mock.Of<IClientService>();
            Mock.Get(mockClientService).Setup(service => service.InsertClientAsync(_mapper.Map<Domain.DTOs.Client>(_sampleValidClient)))
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
            Mock.Get(mockClientService).Setup(service => service.InsertClientAsync(It.IsAny<Domain.DTOs.Client>()))
                .Throws<ClientAlreadyExistsException>();


            Mock<ILoggerAdapter<ClientController>> mockLogger = new();
            mockLogger.Setup(logger => logger.LogInformation(It.IsAny<string>()));



            var clientController = new ClientController(Mock.Get(mockClientService).Object,
               _mapper, mockLogger.Object);

            var res = await clientController.RegisterClientAsync(_sampleValidClient);

            mockLogger.Verify(logger => logger.LogInformation(It.IsAny<string>()), Times.Once());

            Assert.IsType<BadRequestObjectResult>(res.Result);

        }

        [Fact]
        public async Task RegisterClientAsync__should_not_validate_invalid_and_validate_valid_client_input()
        {
            var mockClientService = Mock.Of<IClientService>();
            Mock.Get(mockClientService).Setup(service => service.InsertClientAsync(It.IsAny<Domain.DTOs.Client>()))
                .Returns(Task.CompletedTask);


            Mock<ILoggerAdapter<ClientController>> mockLogger = new();
            mockLogger.Setup(logger => logger.LogError(It.IsAny<Exception>(), It.IsAny<string>()));



            var clientController = new ClientController(Mock.Get(mockClientService).Object,
               _mapper, mockLogger.Object);

            await clientController.RegisterClientAsync(_sampleInvalidClient);
        }
    }
}
