using Goods.Dao.Contracts;
using Goods.Models;
using Goods.Models.Dto;
using System;
using System.Collections.Generic;

namespace Goods.Dao
{
    public class DeliveryDaoProxy : IDeliveryDao
    {
        private readonly DeliveryDaoImpl deliveryDaoImpl;

        public DeliveryDaoProxy()
        {
            deliveryDaoImpl = new DeliveryDaoImpl();
        }

        public void Save(Delivery delivery)
        {
            deliveryDaoImpl.Save(delivery);
        }

        public void Update(Delivery delivery)
        {
            deliveryDaoImpl.Update(delivery);
        }

        public List<DeliveryOilDto> GetDeliveryOilDtos(string stockName, DateTime deliveryDate, long providerId)
        {
            List<DeliveryOilDto> deliveries = deliveryDaoImpl.GetDeliveryOilDtos(stockName, deliveryDate, providerId);

            foreach (DeliveryOilDto delivery in deliveries)
            {
                delivery.Price *= 1.2;
                delivery.QuantityValue = $"{delivery.Quantity} {delivery.StockMeasure}";
            }

            return deliveries;
        }

        public Delivery FindByProviderIdAndStockId(long providerId, long stockId)
        {
            return deliveryDaoImpl.FindByProviderIdAndStockId(providerId, stockId);
        }

        public List<Delivery> FindByProviderId(long providerId)
        {
            return deliveryDaoImpl.FindByProviderId(providerId);
        }

        public List<Delivery> GetAll()
        {
            return deliveryDaoImpl.GetAll();
        }
    }
}
