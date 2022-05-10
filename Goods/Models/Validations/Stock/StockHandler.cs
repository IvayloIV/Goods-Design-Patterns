using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goods.Models.Validations.Provider
{
    abstract class StockHandler
    {
        protected StockHandler Successor { get; set; }

        public StockHandler(StockHandler successor)
        {
            this.Successor = successor;
        }

        public abstract bool Handle(StockValidation stockValidation);
    }
}
