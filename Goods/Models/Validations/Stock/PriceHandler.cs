using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goods.Models.Validations.Provider
{
    class PriceHandler : StockHandler
    {
        public PriceHandler(StockHandler successor) : base(successor)
        {
        }

        public override bool Handle(StockValidation stockValidation)
        {
            if (stockValidation.Stock.Price <= 0)
            {
                stockValidation.PriceError = "Цената на стоката трябва да e по-голямо число от 0.";
                return true;
            }
            else
            {
                stockValidation.PriceError = string.Empty;

                if (Successor != null)
                {
                    return Successor.Handle(stockValidation);
                }
            }

            return false;
        }
    }
}
