using Goods.Models;
using System.Data.Entity;

namespace Goods.Dao
{
    public interface IGoodsContext
    {
        DbSet<Provider> Providers { get; set; }
        DbSet<Stock> Stocks { get; set; }
        DbSet<Delivery> Deliveries { get; set; }
        Database Database { get; }

        void Dispose();

        int SaveChanges();
    }
}
