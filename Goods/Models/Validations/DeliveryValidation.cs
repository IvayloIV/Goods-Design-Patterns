using System;

namespace Goods.Models
{
    public class DeliveryValidation : BaseModel
    {
        private string documentNumberError;
        private string deliveryDateError;
        private string quantityError;

        public string DocumentNumberError
        {
            get { return documentNumberError; }
            set { documentNumberError = value; OnPropertyChanged(nameof(DocumentNumberError)); }
        }

        public string DeliveryDateError
        {
            get { return deliveryDateError; }
            set { deliveryDateError = value; OnPropertyChanged(nameof(DeliveryDateError)); }
        }

        public string QuantityError
        {
            get { return quantityError; }
            set { quantityError = value; OnPropertyChanged(nameof(QuantityError)); }
        }

        public bool ValidateDelivery(Delivery delivery)
        {
            bool hasErrors = false;

            if (delivery.DocumentNumber <= 0)
            {
                DocumentNumberError = "Номерът на документа трябва да бъде число по-голямо от 0.";
                hasErrors = true;
            }
            else
            {
                DocumentNumberError = string.Empty;
            }

            if (delivery.DeliveryDate == null)
            {
                DeliveryDateError = "Датата на доставка е задължителна.";
                hasErrors = true;
            }
            else
            {
                DeliveryDateError = string.Empty;
            }

            if (delivery.Quantity <= 0)
            {
                QuantityError = "Количеството трябва да бъде число по-голямо от 0.";
                hasErrors = true;
            }
            else
            {
                QuantityError = string.Empty;
            }

            return hasErrors;
        }
    }
}
