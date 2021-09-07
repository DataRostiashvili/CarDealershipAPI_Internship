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
            IEnumerable<Client> result;
            result = _mapper.Map<IEnumerable<Client>>(_repository.GetByPredicate(entityClient => entityClient.IDNumber == idNumber && entityClient.IsActive));

            return result.FirstOrDefault();
        }

        public async Task InsertClientAsync(Client client) 
        {
            var mapped = _mapper.Map<Domain.Entity.Client>(client);
            if (_repository.GetByPredicate(cl => cl.IDNumber == client.IDNumber && !cl.IsActive).Any())
            {
                mapped.IsActive = true;
            }
            else if (_repository.GetByPredicate(cl => cl.IDNumber == client.IDNumber && cl.IsActive).Any()) 
            {
                throw new UserAlreadyExistsException(nameof(client));
            }

            await _repository.InsertAsync(_mapper.Map<Domain.Entity.Client>(client));

        }

        public async Task UpdateClientAsync(Client client) => 
            await _repository.UpdateAsync(_mapper.Map<Domain.Entity.Client>(client));


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
