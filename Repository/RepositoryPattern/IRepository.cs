using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entity;

namespace Repository.RepositoryPattern
{
    public interface IRepository<T> where T : BaseEntity
    {
        IEnumerable<T> GetAll();
        Task Delete(T entity);
        Task Insert(T entity);
        Task Update(T entity);
        
    }
}
