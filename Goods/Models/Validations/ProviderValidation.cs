using System;

namespace Goods.Models
{
    public class ProviderValidation : BaseModel
    {
        private string nameError;
        private string addressError;
        private string phoneError;
        private string contactPersonError;

        public string NameError
        {
            get { return nameError; }
            set { nameError = value; OnPropertyChanged(nameof(NameError)); }
        }

        public string AddressError
        {
            get { return addressError; }
            set { addressError = value; OnPropertyChanged(nameof(AddressError)); }
        }

        public string PhoneError
        {
            get { return phoneError; }
            set { phoneError = value; OnPropertyChanged(nameof(PhoneError)); }
        }

        public string ContactPersonError
        {
            get { return contactPersonError; }
            set { contactPersonError = value; OnPropertyChanged(nameof(ContactPersonError)); }
        }

        public bool ValidateProvider(Provider provider)
        {
            bool hasErrors = false;

            if (provider.Name == null || provider.Name.Length <= 2)
            {
                NameError = "Името на доставчика трябва да е поне три символа.";
                hasErrors = true;
            }
            else
            {
                NameError = string.Empty;
            }

            if (provider.Address == null || provider.Address.Length <= 2)
            {
                AddressError = "Адресът на доставчика трябва да е поне три символа.";
                hasErrors = true;
            }
            else
            {
                AddressError = string.Empty;
            }

            if (provider.Phone == null || provider.Phone.Length != 10)
            {
                PhoneError = "Телефонът на доставчика трябва да е точно 10 символа.";
                hasErrors = true;
            }
            else
            {
                PhoneError = string.Empty;
            }

            return hasErrors;
        }
    }
}
