using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goods.Models
{
    [Table("delivery")]
    public class Delivery : BaseModel
    {
        private long stockId;
        private long providerId;
        private long documentNumber;
        private DateTime deliveryDate;
        private double quantity;
        private Stock stock;
        private Provider provider;

        [Key, Column("stock_id", Order = 0)]
        [ForeignKey("Stock")]
        public long StockId
        {
            get { return stockId; }
            set { stockId = value; OnPropertyChanged(nameof(StockId)); }
        }

        [Key, Column("provider_id", Order = 1)]
        [ForeignKey("Provider")]
        public long ProviderId
        {
            get { return providerId; }
            set { providerId = value; OnPropertyChanged(nameof(ProviderId)); }
        }

        [Column("document_number")]
        public long DocumentNumber
        {
            get { return documentNumber; }
            set { documentNumber = value; OnPropertyChanged(nameof(DocumentNumber)); }
        }

        [Column("delivery_date")]
        public DateTime DeliveryDate
        {
            get { return deliveryDate; }
            set { deliveryDate = value; OnPropertyChanged(nameof(DeliveryDate)); }
        }

        [Column("quantity")]
        public double Quantity
        {
            get { return quantity; }
            set { quantity = value; OnPropertyChanged(nameof(Quantity)); }
        }

        public Stock Stock
        {
            get { return stock; }
            set { stock = value; }
        }

        public Provider Provider
        {
            get { return provider; }
            set { provider = value; }
        }
    }
}
