using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq.Expressions;
using TrashToTreasure.Infrastructure;
using TrashToTreasure.Interfaces;

namespace TrashToTreasure.Models.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        protected readonly DbContextClass _dbContext;
        internal DbSet<TEntity> _dbSet;
        public GenericRepository(DbContextClass dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<TEntity>();
        }
        public async virtual Task AddAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async virtual void Delete(object id)
        {
            TEntity? entityToDelete = await _dbSet.FindAsync(id);
            if(entityToDelete != null)
                Delete(entityToDelete);
            throw new InvalidOperationException($"Object with id {id} of class {typeof(TEntity)}");
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            if (_dbContext.Entry(entityToDelete).State == EntityState.Detached)
            {
                _dbSet.Attach(entityToDelete);
            }
            _dbSet.Remove(entityToDelete);
        }

        public async virtual Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, string includeProperties = "")
        {
            IQueryable<TEntity> query = _dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return await orderBy(query).ToListAsync();
            }
            else
            {
                return await query.ToListAsync();
            }
        }

        public async virtual Task<TEntity?> GetByIdAsync(object id)
        {
            return await _dbSet.FindAsync(id);
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            _dbSet.Attach(entityToUpdate);
            _dbContext.Entry(entityToUpdate).State = EntityState.Modified;
        }
    }
}
