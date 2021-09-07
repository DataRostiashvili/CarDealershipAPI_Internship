using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.DTOs;
using Repository.RepositoryPattern;
using AutoMapper;
using Domain.Exceptions;

namespace Repository.DatabaseSeedLoader
{
    public  class DatabaseSeedLoader : IDatabaseSeedLoader
    {
         readonly IMapper _mapper;
         readonly IRepository<Domain.Entity.Client> _clientRepository;

         List<Client> clients = new()
        {

             new Client { Name = "Data", Surname = "Rostiashvili", IDNumber = "11112222333", DateOfBirth = DateTime.Now },
             new Client { Name = "Giorgi", Surname = "Edisherashvili", IDNumber = "11112222533", DateOfBirth = DateTime.Now },
             new Client { Name = "Vasil", Surname = "Wantiani", IDNumber = "11412222333", DateOfBirth = DateTime.Now },
             new Client { Name = "Toma", Surname = "axalaia", IDNumber = "11112222733", DateOfBirth = DateTime.Now },
             new Client { Name = "Vano", Surname = "Grdzelishvili", IDNumber = "11112222313", DateOfBirth = DateTime.Now }


        };
         List<Car> cars = new();

        public DatabaseSeedLoader(IRepository<Domain.Entity.Client> clientRepository,
            IMapper mapper) {

            _clientRepository = clientRepository;
            _mapper = mapper;
        }
        public async Task SeedData()
        {
            if (_clientRepository.GetAll().Any())
                throw new DatabaseAlreadySeededException("Database is already seeded");

            foreach (var client in clients)
                await _clientRepository.InsertAsync(_mapper.Map<Domain.Entity.Client>(client));
            

        }
    }
}
