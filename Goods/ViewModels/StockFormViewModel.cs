using Goods.Commands;
using Goods.Dao;
using Goods.Models;
using Goods.Models.Enums;
using Goods.Models.Validations.Provider;
using Goods.Stores;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace Goods.ViewModels
{
    public class StockFormViewModel : BaseViewModel
    {
        private const string ADD_NEW_STOCK = "-- Добави нова стока --";

        private Stock stock;
        private StockValidation stockValidation;
        private StockHandler stockHandler;
        private string[] measures;
        private string selectedMeasure;
        private string successMessage;
        private IStockDao stockDao;
        private ObservableCollection<string> stockValues;
        private string selectedStockValue;
        private string labelText;

        public RelayCommand CreateCommand { get; }
        public ICommand NavigationBackCommand { get; }

        public StockFormViewModel(NavigationStore navigationStore)
        {
            CreateCommand = new RelayCommand(Create);
            NavigationBackCommand = new NavigateCommand<FormHomeViewModel>(navigationStore, (n) => new FormHomeViewModel(n));
            stockDao = new StockDaoImpl();
            measures = Enum.GetNames(typeof(StockMeasure));
            InitValidations();
            InitStock();
        }

        private void InitStock()
        {
            SelectedMeasure = measures[0];
            Stock = new Stock();
            Stock.CreationDate = DateTime.Now;
            UpdateStockValues(ADD_NEW_STOCK);
            LabelText = Enum.GetName(typeof(OperationType), OperationType.Създаване);
        }

        private void InitValidations() 
        {
            stockValidation = new StockValidation();

            StockHandler priceHandler = new PriceHandler(null);
            StockHandler daysValidToHandler = new DaysValidToHandler(priceHandler);
            StockHandler creationDateHandler = new CreationDateHandler(daysValidToHandler);
            StockHandler nameHandler = new NameHandler(creationDateHandler);
            stockHandler = nameHandler;
        }

        private void UpdateStockValues(string defaultValue)
        {
            StockValues = new ObservableCollection<string>(stockDao.GetAll().Select(a => $"{a.Id} - {a.Name}").ToList());
            StockValues.Insert(0, ADD_NEW_STOCK);
            SelectedStockValue = defaultValue;
        }

        public Stock Stock
        {
            get { return stock; }
            set { stock = value; OnPropertyChanged(nameof(Stock)); }
        }

        public StockValidation StockValidation
        {
            get { return stockValidation; }
            set { stockValidation = value; OnPropertyChanged(nameof(StockValidation)); }
        }

        public string[] Measures
        {
            get { return measures; }
        }

        public string SelectedMeasure
        {
            get { return selectedMeasure; }
            set { selectedMeasure = value; OnPropertyChanged(nameof(SelectedMeasure)); }
        }

        public string SuccessMessage
        {
            get { return successMessage; }
            set { successMessage = value; OnPropertyChanged(nameof(SuccessMessage)); }
        }

        public ObservableCollection<string> StockValues
        {
            get { return stockValues; }
            set { stockValues = value; OnPropertyChanged(nameof(StockValues)); }
        }

        public string LabelText
        {
            get { return labelText; }
            set { labelText = value; OnPropertyChanged(nameof(LabelText)); }
        }

        public string SelectedStockValue
        {
            get { return selectedStockValue; }
            set
            {
                selectedStockValue = value;
                OnPropertyChanged(nameof(SelectedStockValue));
                SwapStock(selectedStockValue);
            }
        }

        private void SwapStock(string selectedStockValue)
        {
            SuccessMessage = string.Empty;
            StockValidation = new StockValidation();

            if (!selectedStockValue.Equals(ADD_NEW_STOCK))
            {
                string stockId = selectedStockValue.Split('-')[0].Trim();
                Stock = stockDao.FindById(int.Parse(stockId));
                SelectedMeasure = Stock.Measure.ToString();
                LabelText = Enum.GetName(typeof(OperationType), OperationType.Редактиране);
            }
            else
            {
                Stock = new Stock();
                Stock.CreationDate = DateTime.Now;
                LabelText = Enum.GetName(typeof(OperationType), OperationType.Създаване);
            }
        }

        private void Create()
        {
            StockValidation.Stock = Stock;
            if (!stockHandler.Handle(StockValidation))
            {
                Stock.Measure = SelectedMeasure;
                if (!selectedStockValue.Equals(ADD_NEW_STOCK))
                {
                    stockDao.Update(Stock);
                    UpdateStockValues($"{Stock.Id} - {Stock.Name}");
                    SuccessMessage = "Успешно редактирахте стоката!";
                }
                else
                {
                    stockDao.Save(Stock);
                    InitStock();
                    SelectedStockValue = ADD_NEW_STOCK;
                    SuccessMessage = "Успешно създадохте новата стока!";
                }
            }
            else
            {
                SuccessMessage = string.Empty;
            }
        }
    }
}
