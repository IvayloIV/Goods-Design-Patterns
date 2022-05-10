using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goods.Models.Validations.Provider
{
    class NameHandler : StockHandler
    {
        public NameHandler(StockHandler successor) : base(successor)
        {
        }

        public override bool Handle(StockValidation stockValidation)
        {
            if (stockValidation.Stock.Name == null || stockValidation.Stock.Name.Length <= 2)
            {
                stockValidation.NameError = "Името на стоката трябва да е поне три символа.";
                return true;
            }
            else
            {
                stockValidation.NameError = string.Empty;

                if (Successor != null)
                {
                    return Successor.Handle(stockValidation);
                }
            }

            return false;
        }
    }
}
