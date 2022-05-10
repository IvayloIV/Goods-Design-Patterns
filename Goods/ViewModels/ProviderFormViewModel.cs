using Goods.Commands;
using Goods.Dao;
using Goods.Dao.Contracts;
using Goods.Models;
using Goods.Models.Dto;
using Goods.Models.Enums;
using Goods.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace Goods.ViewModels
{
    public class ProviderFormViewModel : BaseViewModel
    {
        private const string ADD_NEW_PROVIDER = "-- Добави нов доставчик --";
        private const string ADD_NEW_DELIVERY = "-- Добави новa доставка --";

        private ObservableCollection<DeliveryOilDto> deliveryOilDtos;
        private IStockDao stockDao;

        private Provider provider;
        private ProviderValidation providerValidation;
        private string successMessage;
        private IProviderDao providerDao;
        private IDeliveryDao deliveryDao;
        private ObservableCollection<string> providerValues;
        private string selectedProviderValue;
        private string labelText;
        
        private Delivery delivery;
        private DeliveryValidation deliveryValidation;
        private string successMessageDelivery;
        private ObservableCollection<string> deliveryValues;
        private string selectedDeliveryValue;
        private string labelTextDelivery;

        private ObservableCollection<string> stockValues;
        private string selectedStockValue;

        private string stockVisible;
        private string deliveryVisible;

        public RelayCommand CreateProviderCommand { get; }
        public RelayCommand CreateDeliveryCommand { get; }
        public ICommand NavigationBackCommand { get; }

        public ProviderFormViewModel(NavigationStore navigationStore)
        {
            CreateProviderCommand = new RelayCommand(CreateProvider);
            CreateDeliveryCommand = new RelayCommand(CreateDelivery);
            NavigationBackCommand = new NavigateCommand<FormHomeViewModel>(navigationStore, (n) => new FormHomeViewModel(n));
            providerDao = new ProviderDaoImpl();
            deliveryDao = new DeliveryDaoImpl();
            stockDao = new StockDaoImpl();
            providerValidation = new ProviderValidation();
            InitProvider();
            InitDelivery();
            InitStock();
            InitDeliveryOilDtos();
        }

        private void InitProvider()
        {
            Provider = new Provider();
            UpdateProviderValus(ADD_NEW_PROVIDER);
            LabelText = Enum.GetName(typeof(OperationType), OperationType.Създаване);
        }

        private void InitDeliveryOilDtos()
        {
            if (Provider.Id != 0)
            {
                List<DeliveryOilDto> deliveries = deliveryDao.GetDeliveryOilDtos(null, DateTime.MaxValue, Provider.Id);
                DeliveryOilDtos = new ObservableCollection<DeliveryOilDto>(deliveries);
            }
            else
            {
                DeliveryOilDtos = new ObservableCollection<DeliveryOilDto>();
            }
        }

        private void InitDelivery()
        {
            Delivery = new Delivery();
            Delivery.DeliveryDate = DateTime.Now;
            DeliveryValidation = new DeliveryValidation();
            UpdateDeliveryValues(ADD_NEW_DELIVERY);
            LabelTextDelivery = Enum.GetName(typeof(OperationType), OperationType.Създаване);
        }

        private void UpdateProviderValus(string defaultValue)
        {
            ProviderValues = new ObservableCollection<string>(providerDao.GetAll().Select(a => $"{a.Id} - {a.Name}").ToList());
            ProviderValues.Insert(0, ADD_NEW_PROVIDER);
            SelectedProviderValue = defaultValue;
        }

        private void UpdateDeliveryValues(string defaultValue)
        {
            DeliveryValues = new ObservableCollection<string>();
            if (Provider.Id != 0)
            {
                foreach (Delivery delivery in deliveryDao.FindByProviderId(Provider.Id))
                {
                    Stock stock = stockDao.FindById(delivery.StockId);
                    DeliveryValues.Add($"{stock.Id} - {stock.Name} - {stock.Price:F2}");
                }
            }
            DeliveryValues.Insert(0, ADD_NEW_DELIVERY);
            SelectedDeliveryValue = defaultValue;
        }

        private void InitStock()
        {
            if (Provider.Id != 0)
            {
                StockValues = new ObservableCollection<string>();
                List<long> stockIdsByProvider = deliveryDao
                    .FindByProviderId(Provider.Id)
                    .Select(p => p.StockId)
                    .ToList();

                foreach (Stock stock in stockDao.GetAll())
                {
                    if (!stockIdsByProvider.Contains(stock.Id))
                    {
                        StockValues.Add($"{stock.Id} - {stock.Name} - {stock.Price:F2} - {stock.Measure}");
                    }
                }

                if (StockValues.Count > 0)
                {
                    SelectedStockValue = StockValues[0];
                }
                else
                {
                    SelectedStockValue = null;
                }
            }
        }

        public Provider Provider
        {
            get { return provider; }
            set { provider = value; OnPropertyChanged(nameof(Provider)); }
        }

        public ProviderValidation ProviderValidation
        {
            get { return providerValidation; }
            set { providerValidation = value; OnPropertyChanged(nameof(ProviderValidation)); }
        }

        public string SuccessMessage
        {
            get { return successMessage; }
            set { successMessage = value; OnPropertyChanged(nameof(SuccessMessage)); }
        }

        public ObservableCollection<string> ProviderValues
        {
            get { return providerValues; }
            set { providerValues = value; OnPropertyChanged(nameof(ProviderValues)); }
        }

        public string LabelText
        {
            get { return labelText; }
            set { labelText = value; OnPropertyChanged(nameof(LabelText)); }
        }

        public ObservableCollection<DeliveryOilDto> DeliveryOilDtos
        {
            get { return deliveryOilDtos; }
            set { deliveryOilDtos = value; OnPropertyChanged(nameof(DeliveryOilDtos)); }
        }

        public Delivery Delivery
        {
            get { return delivery; }
            set { delivery = value; OnPropertyChanged(nameof(Delivery)); }
        }

        public DeliveryValidation DeliveryValidation
        {
            get { return deliveryValidation; }
            set { deliveryValidation = value; OnPropertyChanged(nameof(DeliveryValidation)); }
        }

        public string SuccessMessageDelivery
        {
            get { return successMessageDelivery; }
            set { successMessageDelivery = value; OnPropertyChanged(nameof(SuccessMessageDelivery)); }
        }

        public ObservableCollection<string> DeliveryValues
        {
            get { return deliveryValues; }
            set { deliveryValues = value; OnPropertyChanged(nameof(DeliveryValues)); }
        }

        public string SelectedDeliveryValue
        {
            get { return selectedDeliveryValue; }
            set
            {
                selectedDeliveryValue = value;
                OnPropertyChanged(nameof(SelectedDeliveryValue));
                SwapDelivery(selectedDeliveryValue);
            }
        }

        public string LabelTextDelivery
        {
            get { return labelTextDelivery; }
            set { labelTextDelivery = value; OnPropertyChanged(nameof(LabelTextDelivery)); }
        }

        public string SelectedProviderValue
        {
            get { return selectedProviderValue; }
            set
            {
                selectedProviderValue = value;
                OnPropertyChanged(nameof(SelectedProviderValue));
                SwapProvider(selectedProviderValue);
                InitDelivery();
                InitStock();
                InitDeliveryOilDtos();
            }
        }

        public ObservableCollection<string> StockValues
        {
            get { return stockValues; }
            set { stockValues = value; OnPropertyChanged(nameof(StockValues)); }
        }

        public string SelectedStockValue
        {
            get { return selectedStockValue; }
            set { selectedStockValue = value; OnPropertyChanged(nameof(SelectedStockValue)); }
        }

        public string StockVisible
        {
            get { return stockVisible; }
            set { stockVisible = value; OnPropertyChanged(nameof(StockVisible)); }
        }

        public string DeliveryVisible
        {
            get { return deliveryVisible; }
            set { deliveryVisible = value; OnPropertyChanged(nameof(DeliveryVisible)); }
        }

        private void SwapProvider(string selectedProviderValue)
        {
            SuccessMessage = string.Empty;
            ProviderValidation = new ProviderValidation();

            if (!selectedProviderValue.Equals(ADD_NEW_PROVIDER))
            {
                string providerId = selectedProviderValue.Split('-')[0].Trim();
                Provider = providerDao.FindById(int.Parse(providerId));
                LabelText = Enum.GetName(typeof(OperationType), OperationType.Редактиране);
                DeliveryVisible = "Visible";
            }
            else
            {
                Provider = new Provider();
                LabelText = Enum.GetName(typeof(OperationType), OperationType.Създаване);
                DeliveryVisible = "Hidden";
            }
        }

        private void SwapDelivery(string selectedDeliveryValue)
        {
            SuccessMessageDelivery = string.Empty;
            DeliveryValidation = new DeliveryValidation();

            if (selectedDeliveryValue != null && !selectedDeliveryValue.Equals(ADD_NEW_DELIVERY))
            {
                string stockId = selectedDeliveryValue.Split('-')[0].Trim();
                Delivery = deliveryDao.FindByProviderIdAndStockId(Provider.Id, int.Parse(stockId));
                LabelTextDelivery = Enum.GetName(typeof(OperationType), OperationType.Редактиране);
                StockVisible = "Hidden";
            }
            else
            {
                Delivery = new Delivery();
                Delivery.DeliveryDate = DateTime.Now;
                LabelTextDelivery = Enum.GetName(typeof(OperationType), OperationType.Създаване);
                StockVisible = "Visible";
            }
        }

        private void CreateProvider()
        {
            if (!ProviderValidation.ValidateProvider(Provider))
            {
                if (!selectedProviderValue.Equals(ADD_NEW_PROVIDER))
                {
                    providerDao.Update(Provider);
                    UpdateProviderValus($"{Provider.Id} - {Provider.Name}");
                    SuccessMessage = "Успешно редактирахте доставчика!";
                }
                else
                {
                    providerDao.Save(Provider);
                    InitProvider();
                    SuccessMessage = "Успешно създадохте нов доставчик!";
                }
            }
            else
            {
                SuccessMessage = string.Empty;
            }
        }

        private void CreateDelivery()
        {
            if (!DeliveryValidation.ValidateDelivery(Delivery))
            {
                if (!selectedDeliveryValue.Equals(ADD_NEW_DELIVERY))
                {
                    deliveryDao.Update(Delivery);
                    SuccessMessageDelivery = "Успешно редактирахте доставката!";
                }
                else
                {
                    if (SelectedStockValue == null)
                    {
                        SuccessMessageDelivery = "Нямаме повече налични стоки!";
                        return;
                    }

                    Delivery.ProviderId = Provider.Id;
                    Delivery.StockId = int.Parse(SelectedStockValue.Split('-')[0].Trim());
                    deliveryDao.Save(Delivery);
                    InitDelivery();
                    SuccessMessageDelivery = "Успешно създадохте нова доставка!";
                }

                InitStock();
                InitDeliveryOilDtos();
            }
            else
            {
                SuccessMessageDelivery = string.Empty;
            }
        }
    }
}
