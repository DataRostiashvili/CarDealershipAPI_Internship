using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.DTOs;

namespace Services
{
    public  interface IClientService
    {
        // IEnumerable<Client> GetAllClients();
        ClientDto GetClient(string idNumber);
        Task InsertClientAsync(ClientDto customer);
        Task UpdateClientAsync(ClientDto customer);
        Task DeleteClientAsync(string idNumber);
    }
}
