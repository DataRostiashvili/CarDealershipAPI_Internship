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
        public static Mock<IRepository<T>> Create<T>() where T : Domain.Entity.BaseEntity
        {
            var mockRepository = new Mock<IRepository<T>>();

            mockRepository
                .Setup(rep => rep.DeleteByPredicateAsync(It.IsAny<Func<T, bool>>()))
                .Returns(Task.CompletedTask);
            mockRepository
                .Setup(rep => rep.GetAll())
                .Returns(Array.Empty<T>());
            mockRepository
                .Setup(rep => rep.GetByPredicate(It.IsAny<Func<T, bool>>()))
                .Returns(Array.Empty<T>());
            mockRepository
                .Setup(rep => rep.InsertAsync(It.IsAny<T>()))
                .Returns(Task.CompletedTask);
            mockRepository
                .Setup(rep => rep.UpdateAsync(It.IsAny<T>()))
                .Returns(Task.CompletedTask);

            return mockRepository;
        }
    }
}
