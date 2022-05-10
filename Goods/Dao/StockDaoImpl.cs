using Goods.Models;
using Goods.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goods.Dao
{
    public class StockDaoImpl : IStockDao
    {
        private readonly IGoodsContext goodsContext;

        public StockDaoImpl()
        {
            goodsContext = GoodsContextSingleton.GetContext();
        }

        public void Save(Stock stock)
        {
            goodsContext.Stocks.Add(stock);
            goodsContext.SaveChanges();
        }

        public void Update(Stock stock)
        {
            Stock currentStock = FindById(stock.Id);
            currentStock.Name = stock.Name;
            currentStock.CreationDate = stock.CreationDate;
            currentStock.DaysValidTo = stock.DaysValidTo;
            currentStock.Price = stock.Price;
            currentStock.Measure = stock.Measure;
            goodsContext.SaveChanges();
        }

        public Stock FindById(long id)
        {
            return goodsContext.Stocks.Find(id);
        }

        public List<Stock> GetAll()
        {
            return goodsContext.Stocks.ToList();
        }

        public List<StockSummaryDto> GetStockSummary(string name, string measure)
        {
            IQueryable<Stock> query = goodsContext.Stocks
                .Include("Deliveries");

            if (name != null && name.Length > 0)
            {
                query = query.Where(s => s.Name.Equals(name));
            }

            if (measure != null && measure.Length > 0)
            {
                query = query.Where(s => s.Measure.Equals(measure));
            }

            return query.GroupBy(s => new { s.Name, s.Price, s.Measure })
                .Select(s =>
                    new
                    {
                        name = s.Key.Name,
                        price = s.Key.Price,
                        measure = s.Key.Measure,
                        deliveryCount = s.Sum(d => d.Deliveries.Count()),
                        totalQuantity = s.Sum(d => d.Deliveries.Count() == 0 ? 0 : d.Deliveries.Sum(de => de.Quantity)),
                    })
                .OrderBy(s => s.price)
                .ToList()
                .Select(s => new StockSummaryDto(s.name, s.price, s.measure, s.deliveryCount, s.totalQuantity))
                .ToList();
        }
    }
}
