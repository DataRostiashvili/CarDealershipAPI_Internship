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
        private IRepository<Domain.Entity.Client> _repository;
        private IMapper _mapper;

        public ClientService(IRepository<Domain.Entity.Client> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public Client GetClient(string idNumber) 
        {

            var result = _mapper.Map<Client>(_repository
                .GetByPredicate(entityClient => entityClient.IDNumber == idNumber && entityClient.IsActive).FirstOrDefault());

            return result;
        }

        public async Task InsertClientAsync(Client client) 
        {
            if (_repository.GetByPredicate(cl => cl.IDNumber == client.IDNumber && !cl.IsActive).Any())
            {
                client.IsActive = true;
                await UpdateClientAsync(client);
                return;
            }
            else if (_repository.GetByPredicate(cl => cl.IDNumber == client.IDNumber && cl.IsActive).Any()) 
            {
                throw new UserAlreadyExistsException(nameof(client));
            }

            await _repository.InsertAsync(_mapper.Map<Domain.Entity.Client>(client));

        }

        public async Task UpdateClientAsync(Client client) 
        {

            var clientEntity = _repository
                .GetByPredicate(entityClient => entityClient.IDNumber == client.IDNumber).FirstOrDefault();

            clientEntity.Name = client.Name;
            clientEntity.Surname = client.Surname;
            clientEntity.DateOfBirth = client.DateOfBirth;
            
            await _repository.UpdateAsync(clientEntity);

        }


        public async Task DeleteClientAsync(string idNumber)
        {
            if (ClientExistsAndIsActive(idNumber))
                await _repository.DeleteByPredicateAsync(client => client.IDNumber.Equals(idNumber));

        }

        #region Private methods

        private bool ClientExistsAndIsActive(string idNumber) =>
            GetClient(idNumber) != null;
        #endregion
    }
}
