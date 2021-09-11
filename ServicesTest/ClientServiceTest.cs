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
    public class ClientServiceTest
    {
        readonly IMapper _mapper = RealMapperFactory.Create();
        readonly Mock<IRepository<Domain.Entity.ClientEntity>> _clientRepository 
            = MockRepositoryFactory.Create<Domain.Entity.ClientEntity>();

        readonly ClientEntity _sampleValidEntityClient =  new ClientEntity
             {
                 IsActive = true,
                 Name = "Data",
                 Surname = "Rostiashvili",
                 IDNumber = "11112222333",
                 DateOfBirth = DateTime.Now,
                 ClientContactInfo = new ClientContactInfoEntity { IsActive = true, Address = "misamarti", Email = "s@g.c", PhoneNumber = "555666777" }
             };
        readonly ClientDto _sampleValidClientDto;

        public ClientServiceTest()
        {
            _sampleValidClientDto = _mapper.Map<ClientDto>(_sampleValidEntityClient);
        }

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
            var clientRep = MockRepositoryFactory.Create<Domain.Entity.ClientEntity>();

            clientRep.Setup(rep => rep.GetByPredicate(It.IsAny<Func<ClientEntity, bool>>()))
            .Returns(new[] { new ClientEntity() });


            var clientService = new ClientService(clientRep.Object, _mapper);

            var res = clientService.GetClient(It.IsAny<string>());

            clientRep.Verify(clientRep => clientRep.GetByPredicate(It.IsAny<Func<ClientEntity, bool>>()), Times.Once());
            Assert.IsType<ClientDto>(res);


        }
        #endregion

        #region InsertClientAsync
        [Fact]
        public async Task InsertClientAsync__should_insert_client()
        {
            var clientRep = MockRepositoryFactory.Create<Domain.Entity.ClientEntity>();

            var clientService = new ClientService(clientRep.Object, _mapper);

            await clientService.InsertClientAsync(new ClientDto { IDNumber = string.Empty });
            
            clientRep.Verify(rep => rep.InsertAsync(It.IsAny<ClientEntity>()), Times.Once());
        }
        #endregion

        #region UpdateClientAsync

        [Fact]
        public async Task UpdateClientAsync__should_throw_for_nonexistent_client()
        {
            var clientRep = MockRepositoryFactory.Create<ClientEntity>();


            var clientService = new ClientService(clientRep.Object, _mapper);

            await Assert.ThrowsAsync<ClientDoesntExistsException>(async () =>  await clientService.UpdateClientAsync(_sampleValidClientDto));

            clientRep.Verify(rep => rep.UpdateAsync(It.IsAny<ClientEntity>()), Times.Never());
        }


        #endregion


        #region DeleteClientAsync

        [Fact]
        public async Task DeleteClientAsync__should_delete_client()
        {
            var clientRep = MockRepositoryFactory.Create<Domain.Entity.ClientEntity>();
            clientRep.Setup(rep => rep.GetByPredicate(It.IsAny<Func<ClientEntity, bool>>()))
            .Returns(new[] { new ClientEntity() });

            var clientService = new ClientService(clientRep.Object, _mapper);

            await clientService.DeleteClientAsync(It.IsAny<string>());

            clientRep.Verify(rep => rep.DeleteByPredicateAsync(It.IsAny<Func<ClientEntity, bool>>()), Times.Once());
        }


        [Fact]
        public async Task DeleteClientAsync__should_throw_for_nonexistent_client()
        {
            var clientRep = MockRepositoryFactory.Create<Domain.Entity.ClientEntity>();
            

            var clientService = new ClientService(clientRep.Object, _mapper);

            await Assert.ThrowsAsync<ClientDoesntExistsException>(async () => await clientService.DeleteClientAsync(It.IsAny<string>()));

            clientRep.Verify(rep => rep.DeleteByPredicateAsync(It.IsAny<Func<ClientEntity, bool>>()), Times.Never());
        }

        #endregion
    }
}
