using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goods.Models.Validations.Provider
{
    class CreationDateHandler : StockHandler
    {
        public CreationDateHandler(StockHandler successor) : base(successor)
        {
        }

        public override bool Handle(StockValidation stockValidation)
        {
            if (stockValidation.Stock.CreationDate == null || stockValidation.Stock.CreationDate.Year < 2000 
                || stockValidation.Stock.CreationDate.Year > DateTime.Now.Year)
            {
                stockValidation.CreationDateError = "Датата на създаване трябва да бъде между 2000 и текущата година.";
                return true;
            }
            else
            {
                stockValidation.CreationDateError = string.Empty;

                if (Successor != null)
                {
                    return Successor.Handle(stockValidation);
                }
            }

            return false;
        }
    }
}
