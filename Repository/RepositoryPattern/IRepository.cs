using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entity;

namespace Repository.RepositoryPattern
{
    public interface IRepository<T> : IAsyncDisposable where T : BaseEntity
    {
        IEnumerable<T> GetAll();
        IEnumerable<T> GetByPredicate(Func<T, bool> predicate);
        Task DeleteByPredicateAsync(Func<T, bool> predicate);
        Task InsertAsync(T entity);
        Task UpdateAsync(T entity);
        
    }
}
