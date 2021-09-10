using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.RepositoryPattern;
using Moq;

namespace TestsShared
{
    public static class MockRepositoryFactory
    {
        public static void Create<T>() where T : Domain.Entity.BaseEntity
        {
            var mockRepository = new Mock<IRepository<T>>();

           // mockRepository.Setups(
        }
    }
}
