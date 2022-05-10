using System;

namespace Goods.Models
{
    public class StockValidation : BaseModel
    {
        private string nameError;
        private string creationDateError;
        private string daysValidToError;
        private string priceError;

        public Stock Stock { get; set; }

        public string NameError
        {
            get { return nameError; }
            set { nameError = value; OnPropertyChanged(nameof(NameError)); }
        }

        public string CreationDateError
        {
            get { return creationDateError; }
            set { creationDateError = value; OnPropertyChanged(nameof(CreationDateError)); }
        }

        public string DaysValidToError
        {
            get { return daysValidToError; }
            set { daysValidToError = value; OnPropertyChanged(nameof(DaysValidToError)); }
        }

        public string PriceError
        {
            get { return priceError; }
            set { priceError = value; OnPropertyChanged(nameof(PriceError)); }
        }

        /*public bool ValidateStock(Stock stock)
        {
            bool hasErrors = false;
            this.Stock = stock;

            if (stock.Name == null || stock.Name.Length <= 2)
            {
                NameError = "Името на стоката трябва да е поне три символа.";
                hasErrors = true;
            }
            else
            {
                NameError = string.Empty;
            }

            if (stock.CreationDate == null || stock.CreationDate.Year < 2000 || stock.CreationDate.Year > DateTime.Now.Year)
            {
                CreationDateError = "Датата на създаване трябва да бъде между 2000 и текущата година.";
                hasErrors = true;
            }
            else
            {
                CreationDateError = string.Empty;
            }

            if (stock.DaysValidTo <= 0)
            {
                DaysValidToError = "Дните за валидност на стоката трябва да са по-голямо число от 0.";
                hasErrors = true;
            }
            else
            {
                DaysValidToError = string.Empty;
            }

            if (stock.Price <= 0)
            {
                PriceError = "Цената на стоката трябва да e по-голямо число от 0.";
                hasErrors = true;
            }
            else
            {
                PriceError = string.Empty;
            }

            return hasErrors;
        }*/
    }
}
