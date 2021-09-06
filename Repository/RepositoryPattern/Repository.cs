using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Domain.Entity;


namespace Repository.RepositoryPattern
{
    public abstract class Repository<T> : IRepository<T> where T : BaseEntity
    {
        readonly ApplicationDbContext _applicationDbContext;
        DbSet<T> entities;


        public Repository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
            entities = _applicationDbContext.Set<T>();

        }

        public async Task Delete(T entity)
        {
            _ = entity ?? throw new ArgumentNullException(nameof(entity));
            (await entities.FindAsync(entity)).IsActive = false;
            await _applicationDbContext.SaveChangesAsync();
        }

        public  IEnumerable<T> GetAll() => entities.AsEnumerable();

        public async Task Insert(T entity) 
        {
            _ = entity ?? throw new ArgumentNullException(nameof(entity));
            if ((await entities.FindAsync(entity)) is not null)
            {
                entity.IsActive = true;
                return;
            }
            else
            {
                await entities.AddAsync(entity);
            }

            await _applicationDbContext.SaveChangesAsync();
        }

        public async Task Update(T entity)
        {
            _ = entity ?? throw new ArgumentNullException(nameof(entity));
            
        }
    }
}
