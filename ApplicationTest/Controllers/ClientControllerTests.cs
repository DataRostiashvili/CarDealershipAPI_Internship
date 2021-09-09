using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Services;
using Moq;
using System.Text.RegularExpressions;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Application.Controllers;

namespace ApplicationTest.Controllers
{
    [TestClass]
    public class ClientControllerTests
    {
        readonly Mock<IClientService> _mockClientService = new();
        readonly IMapper mapper = MockMapperFactory.Create();
        readonly Mock<ILogger<ClientController>> _mockLogger = new();

        ClientController _clientController;

        [ClassInitialize]
        public static void Init(TestContext context)
        {
            
        }

        public ClientControllerTests()
        {
            _mockClientService.Setup(service => service.DeleteClientAsync(It.Is<string>(str => str.Length == 11)))
                .Returns(Task.CompletedTask);

            _mockClientService.Setup(service => service.InsertClientAsync(It.IsAny<Domain.DTOs.Client>()))
                .Returns(Task.CompletedTask);


            //_mockLogger.Setup(logger => logger.LogInformation(It.IsAny<string>(), It.IsAny<object[]>()));
        }
        [TestInitialize]
        public void TestInit()
        {
            _clientController = new ClientController(_mockClientService.Object,
               mapper, _mockLogger.Object);
        }

        [TestMethod]
        public async Task RegisterClientAsync_returns_ok_for_valid_input() 
        {
            var res = await _clientController.RegisterClientAsync(It.IsAny<Domain.APIModels.Client>());

            _mockLogger.Verify(logger => logger.LogInformation(It.IsAny<string>()), Times.Once());

            Assert.AreEqual(res, It.IsAny<Domain.APIModels.Client>());

        }
    }
}
