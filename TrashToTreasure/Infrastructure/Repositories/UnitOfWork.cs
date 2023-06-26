using TrashToTreasure.Interfaces;
using TrashToTreasure.Models.Models;
using TrashToTreasure.Models.Repositories;

namespace TrashToTreasure.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContextClass _dbContext;
        private bool disposed = false;
        public IGenericRepository<Advertisement> AvertisementRepository { get; }
        public UnitOfWork(DbContextClass dbContext, IGenericRepository<Advertisement> advertisementRepository)
        {
            _dbContext = dbContext;
            AvertisementRepository = advertisementRepository;
        }

        
        public void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public int Save()
        {
            return _dbContext.SaveChanges();
        }
    }
}
