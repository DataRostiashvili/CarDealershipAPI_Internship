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

namespace ServicesTest
{
    public class ClientServiceTest
    {
        readonly IMapper _mapper = RealMapperFactory.Create();
        readonly Mock<IRepository<Domain.Entity.ClientEntity>> _clientRepository 
            = MockRepositoryFactory.Create<Domain.Entity.ClientEntity>();



        #region GetClient

        [Fact]
        public void GetClient__should_throw_for_nonexistent_client()
        {
            var clientService = new ClientService(_clientRepository.Object, _mapper);

            Assert.Throws<ClientDoesntExistsException>(() => clientService.GetClient(It.IsAny<string>()));
        }

        [Fact]
        public void GetClient__should_return_client_for_valid_idNumber_input()
        {
            var clientService = new ClientService(_clientRepository.Object, _mapper);
            var clientRep = MockRepositoryFactory.Create<Domain.Entity.ClientEntity>();
        }
        #endregion
    }
}
