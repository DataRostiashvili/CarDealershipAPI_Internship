using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.RepositoryPattern;
using Domain.DTOs;
using AutoMapper;
using Domain.Exceptions;

namespace Services
{
    public class ClientService : IClientService
    {
        private readonly IRepository<Domain.Entity.ClientEntity> _repository;
        private readonly IMapper _mapper;

        public ClientService(IRepository<Domain.Entity.ClientEntity> repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public ClientDto GetClient(string idNumber, bool includeInactiveClients = false) 
        {

            var result = _mapper.Map<ClientDto>(_repository
                .GetByPredicate(entityClient => entityClient.IDNumber == idNumber && (includeInactiveClients ? true : entityClient.IsActive ))
                .FirstOrDefault());

            if (result is null)
                throw new ClientDoesntExistsException($"client (ID: {idNumber}) doesn't exists");

            return result;
        }

        public async Task InsertClientAsync(ClientDto client) 
        {
            if (ClientExistsAndIsNotActive(client.IDNumber))
            {
                client.IsActive = true;
                await UpdateClientAsync(client);
                return;
            }
            else if (ClientExistsAndIsActive(client.IDNumber)) 
            {
                throw new ClientAlreadyExistsException(nameof(client));
            }

            await _repository.InsertAsync(_mapper.Map<Domain.Entity.ClientEntity>(client));

        }

        public async Task UpdateClientAsync(ClientDto client) 
        {

            var clientEntity = _repository
                .GetByPredicate(entityClient => entityClient.IDNumber == client.IDNumber).FirstOrDefault();

            if(clientEntity is null || ClientExistsAndIsNotActive(client.IDNumber))
                throw new ClientDoesntExistsException($"client (ID: {client.IDNumber}) doesn't exists");

            clientEntity.Name = client.Name;
            clientEntity.Surname = client.Surname;
            clientEntity.DateOfBirth = client.DateOfBirth;
            
            await _repository.UpdateAsync(clientEntity);

        }


        public async Task DeleteClientAsync(string idNumber)
        {
            if (ClientExistsAndIsActive(idNumber))
                await _repository.DeleteByPredicateAsync(client => client.IDNumber.Equals(idNumber));
            else
                throw new ClientDoesntExistsException($"client (ID: {idNumber}) doesn't exists");

        }

        #region Private methods
        private bool ClientExistsAndIsActive(string idNumber) 
        {
            try
            {
                return GetClient(idNumber) != null;

            }
            catch (ClientDoesntExistsException)
            {
                return false;
            }

        }

        private bool ClientExistsAndIsNotActive(string idNumber)
        {
            try
            {
                return GetClient(idNumber, includeInactiveClients: true) != null;

            }
            catch (ClientDoesntExistsException)
            {
                return false;
            }
        }
        #endregion
    }
}
