using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Domain.Entity;
using System.Reflection;



namespace Repository.RepositoryPattern
{
    public class Repository<T> : IRepository<T>  where T : BaseEntity
    {
        readonly ApplicationDbContext _applicationDbContext;
        DbSet<T> entities;


        public Repository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
            entities = _applicationDbContext.Set<T>();

        }

        public async Task DeleteAsync(T entity)
        {
            _ = entity ?? throw new ArgumentNullException(nameof(entity));
            (await entities.FindAsync(entity)).IsActive = false;
            await _applicationDbContext.SaveChangesAsync();
        }

        public async Task DeleteByPredicateAsync(Func<T, bool> predicate) 
        {
            var list = GetByPredicate(predicate);

            foreach (var entity in list)
                entity.IsActive = false;

            await _applicationDbContext.SaveChangesAsync();
        }

        public  IEnumerable<T> GetAll() => entities.AsEnumerable();

        public  IEnumerable<T> GetByPredicate(Func<T, bool> predicate) 
            =>  entities.Where(predicate);
        
       


        public async Task InsertAsync(T entity) 
        {
            _ = entity ?? throw new ArgumentNullException(nameof(entity));
            
            await entities.AddAsync(entity);
            
            await _applicationDbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _ = entity ?? throw new ArgumentNullException(nameof(entity));
            entities.Update(entity);
            await _applicationDbContext.SaveChangesAsync();
                
        }


        #region IDisposable implementation

        async ValueTask IAsyncDisposable.DisposeAsync()
        {
            await _applicationDbContext.DisposeAsync();
        }

        #endregion
    }
}
