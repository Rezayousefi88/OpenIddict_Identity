using EntityCode;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;


namespace Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly DataBaseContext dbContext;
        private DbSet<T> entities;


        public GenericRepository(DataBaseContext dbContext)
        {
            this.dbContext = dbContext;
            entities = dbContext.Set<T>();
        }
        public IQueryable<T> GetAllAsQueryable()
        {
            IQueryable<T> query = entities;
            return query;
        }

        public T Get(int id)
        { 
            return entities.Find(id);
        }
        public IQueryable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            IQueryable<T> query = entities.Where(predicate);
            return query;
        }
  
        public IEnumerable<T> GetAll()
        {
            return entities.AsEnumerable<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await entities.ToListAsync();
        }


        public async Task<T> GetAsync(int id)
        {
            return await entities.FindAsync(id);
        }

        public void Insert(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("Entity");
            }
            entities.Add(entity);
        }



        public void Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("Entity");
            }
            dbContext.Entry<T>(entity).State = EntityState.Modified;             
        }

        public void Delete(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("Entity");
            }
            entities.Remove(entity);
        }

    }

}