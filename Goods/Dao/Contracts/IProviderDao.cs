using Goods.Models;
using System.Collections.Generic;

namespace Goods.Dao
{
    public interface IProviderDao
    {
        Provider FindById(long id);

        List<Provider> GetAll();

        void Save(Provider provider);

        void Update(Provider provider);

    }
}