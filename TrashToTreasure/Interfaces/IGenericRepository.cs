using System.Linq.Expressions;

namespace TrashToTreasure.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAllAsync(
            Expression<Func<TEntity, bool>>? filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            string includeProperties = "");
        Task<TEntity?> GetByIdAsync(object id);
        Task AddAsync(TEntity entity);
        void Delete(TEntity entity);
        void Update(TEntity entity);
    }
}
