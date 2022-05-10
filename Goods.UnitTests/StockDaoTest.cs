using Goods.Dao;
using Goods.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Goods.UnitTests
{
    [TestClass]
    public class StockDaoTest
    {
        private IStockDao stockDao;

        [ClassInitialize]
        public static void InitStocks(TestContext testContext)
        {
            Mock<IGoodsContext> goodsContext = new Mock<IGoodsContext>();
            CreateStocks(goodsContext);
            GoodsContextSingleton.InitContext(goodsContext.Object);
        }

        [TestInitialize]
        public void Init()
        {
            stockDao = new StockDao();
        }

        private static void CreateStocks(Mock<IGoodsContext> goodsContext)
        {
            List<Stock> stocks = new List<Stock>();
            for (int i = 1; i <= 3; i++)
            {
                Stock stock = new Stock();
                stock.Id = i;
                stock.Name = "StockName" + i;
                stock.Price = 2 * i;
                stocks.Add(stock);
            }

            Mock<DbSet<Stock>> dbSetMock = new Mock<DbSet<Stock>>();
            dbSetMock.Setup(m => m.Find(It.IsAny<object[]>())).Returns<object[]>(ids => stocks.FirstOrDefault(d => d.Id == (long)ids[0]));
            dbSetMock.As<IQueryable<Stock>>().Setup(x => x.GetEnumerator()).Returns(() => stocks.GetEnumerator());
            goodsContext.Setup(g => g.Stocks).Returns(dbSetMock.Object);
        }

        [TestMethod]
        public void TestUpdateStock()
        {
            Stock stock = new Stock();
            stock.Id = 1;
            stock.Name = "Олио";
            stock.Price = 4.5;
            stockDao.Update(stock);

            stock = stockDao.FindById(1);
            Assert.AreEqual("Олио", stock.Name);
            Assert.AreEqual(4.5, stock.Price);
        }

        [TestMethod]
        public void TestGetAllStocks()
        {
            List<Stock> stocks = stockDao.GetAll();

            Assert.AreEqual(3, stocks.Count);
            Assert.AreEqual(4, stocks[1].Price);
            Assert.AreEqual("StockName3", stocks[2].Name);
        }
    }
}
