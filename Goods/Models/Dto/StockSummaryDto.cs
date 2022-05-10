using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goods.Models.Dto
{
    public class StockSummaryDto
    {
        public string Name { get; set; }

        public double Price { get; set; }

        public string Measure { get; set; }

        public int DeliveryCount { get; set; }

        public double TotalQuantity { get; set; }

        public StockSummaryDto(string name, double price, string measure, int deliveryCount, double totalQuantity)
        {
            this.Name = name;
            this.Price = price;
            this.Measure = measure;
            this.DeliveryCount = deliveryCount;
            this.TotalQuantity = totalQuantity;
        }
    }
}
