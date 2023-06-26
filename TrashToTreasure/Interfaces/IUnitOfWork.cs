using TrashToTreasure.Models.Models;

namespace TrashToTreasure.Interfaces
{
    public interface IUnitOfWork:IDisposable
    {
        int Save();
        void Dispose(bool disposing);
        void Dispose();
        public IGenericRepository<Advertisement> AvertisementRepository { get; }

    }
}
