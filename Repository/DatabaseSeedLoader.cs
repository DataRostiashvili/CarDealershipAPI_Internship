using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entity;

namespace Repository
{
    public static class DatabaseSeedLoader
    {
        static List<Client> clients = new()
        {
            // new Client { Name = "Data", Surname = "Rostiashvili", DateOfBirth = DateTime.Now, IsActiveAccount}
        };
        static List<Car> cars = new();

        public static void LoadData()
        {
            using var context = new ApplicationDbContext();
        }
    }
}
