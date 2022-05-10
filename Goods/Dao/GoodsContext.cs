using Goods.Models;
using System.Data.Entity;

namespace Goods.Dao
{
    public class GoodsContext : DbContext, IGoodsContext
    {
        public DbSet<Provider> Providers { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Delivery> Deliveries { get; set; }

        public GoodsContext() : base(nameOrConnectionString: "GoodsDbConnection") { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("goods");
            base.OnModelCreating(modelBuilder);
        }
    }
}
