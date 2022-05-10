using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goods.Models.Validations.Provider
{
    class DaysValidToHandler : StockHandler
    {
        public DaysValidToHandler(StockHandler successor) : base(successor)
        {
        }

        public override bool Handle(StockValidation stockValidation)
        {
            if (stockValidation.Stock.DaysValidTo <= 0)
            {
                stockValidation.DaysValidToError = "Дните за валидност на стоката трябва да са по-голямо число от 0.";
                return true;
            }
            else
            {
                stockValidation.DaysValidToError = string.Empty;

                if (Successor != null)
                {
                    return Successor.Handle(stockValidation);
                }
            }

            return false;
        }
    }
}
