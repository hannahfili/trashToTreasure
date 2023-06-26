using Microsoft.EntityFrameworkCore;
using TrashToTreasure.Models.Models;

namespace TrashToTreasure.Infrastructure
{
    public class DbContextClass:DbContext
    {
        public DbContextClass(DbContextOptions<DbContextClass> contextOptions): base(contextOptions)
        {

        }
        public DbSet<Advertisement> Advertisements { get; set; }
    }
}
