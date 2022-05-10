using Goods.Models;
using Goods.Models.Dto;
using System;
using System.Collections.Generic;

namespace Goods.Dao.Contracts
{
    interface IDeliveryDao
    {
        void Save(Delivery delivery);

        void Update(Delivery delivery);

        Delivery FindByProviderIdAndStockId(long providerId, long stockId);

        List<Delivery> FindByProviderId(long providerId);

        List<Delivery> GetAll();

        List<DeliveryOilDto> GetDeliveryOilDtos(string stockName, DateTime deliveryDate, long providerId);
    }
}
