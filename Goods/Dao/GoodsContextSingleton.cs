using System.Linq;

namespace Goods.Dao
{
    public sealed class GoodsContextSingleton
    {
        private static IGoodsContext goodsContext;
        private static readonly object padlock = new object();

        private GoodsContextSingleton() { }

        public static IGoodsContext GetContext()
        {
            if (goodsContext == null)
            {
                lock (padlock)
                {
                    if (goodsContext == null)
                    {
                        goodsContext = new GoodsContext();
                        if (goodsContext.Database != null)
                        {
                            goodsContext.Database.SqlQuery<int>("select 1").First();
                        }
                    }
                }
            }

            return goodsContext;
        }

        public static void DestroyContext()
        {
            goodsContext.Dispose();
        }
    }
}
