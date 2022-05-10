using System;

namespace Goods.Models.Dto
{
    public class DeliveryOilDto
    {
        public long ProviderId { get; set; }
        public string StockName { get; set; }
        public double Price { get; set; }
        public double Quantity { get; set; }
        public string StockMeasure { get; set; }
        public string QuantityValue { get; set; }
        public DateTime DeliveryDate { get; set; }
    }
}
