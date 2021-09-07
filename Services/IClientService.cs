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
        Client GetClient(string idNumber);
        Task InsertClientAsync(Client customer);
        Task UpdateClientAsync(Client customer);
        Task DeleteClientAsync(string idNumber);
    }
}
