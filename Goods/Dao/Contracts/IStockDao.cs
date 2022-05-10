using Goods.Models;
using Goods.Models.Dto;
using System.Collections.Generic;

namespace Goods.Dao
{
    public interface IStockDao
    {
        Stock FindById(long id);

        List<Stock> GetAll();

        List<StockSummaryDto> GetStockSummary(string name, string measure);

        void Save(Stock stock);

        void Update(Stock stock);
    }
}