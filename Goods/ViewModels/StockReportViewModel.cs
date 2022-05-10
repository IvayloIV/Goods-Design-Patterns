using Goods.Commands;
using Goods.Dao;
using Goods.Models;
using Goods.Models.Dto;
using Goods.Stores;
using Goods.ViewModels.Reports;
using Microsoft.WindowsAPICodePack.Dialogs;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Input;

namespace Goods.ViewModels
{
    public class StockReportViewModel : BaseViewModel
    {
        private const string ALL_STOCK_MEASURE = "-- Всички --";

        public RelayCommand SearchCommand { get; }
        public RelayCommand ExcelExportCommand { get; }
        public ICommand NavigationBackCommand { get; }

        private IStockDao stockDao;
        private ObservableCollection<StockSummaryDto> stockSummaryDtos;

        private string stockName;
        private ObservableCollection<string> measures;
        private string selectedMeasure;

        public StockReportViewModel(NavigationStore navigationStore)
        {
            SearchCommand = new RelayCommand(Search);
            ExcelExportCommand = new RelayCommand(CreateExcelFile);
            NavigationBackCommand = new NavigateCommand<ReportHomeViewModel>(navigationStore, (n) => new ReportHomeViewModel(n));
            stockDao = new StockDaoImpl();
            measures = new ObservableCollection<string>(Enum.GetNames(typeof(StockMeasure)).ToList());
            measures.Insert(0, ALL_STOCK_MEASURE);
            SelectedMeasure = measures[0];
            StockSummaryDtos = new ObservableCollection<StockSummaryDto>(stockDao.GetStockSummary(StockName, null));
        }

        public ObservableCollection<StockSummaryDto> StockSummaryDtos
        {
            get { return stockSummaryDtos; }
            set { stockSummaryDtos = value; OnPropertyChanged(nameof(StockSummaryDtos)); }
        }

        public string StockName
        {
            get { return stockName; }
            set { stockName = value; OnPropertyChanged(nameof(StockName)); }
        }

        public ObservableCollection<string> Measures
        {
            get { return measures; }
        }

        public string SelectedMeasure
        {
            get { return selectedMeasure; }
            set { selectedMeasure = value; OnPropertyChanged(nameof(SelectedMeasure)); }
        }

        private void Search()
        {
            StockSummaryDtos = new ObservableCollection<StockSummaryDto>(stockDao.GetStockSummary(StockName, 
                SelectedMeasure.Equals(ALL_STOCK_MEASURE) ? null : SelectedMeasure));
        }

        private void CreateExcelFile()
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.IsFolderPicker = true;
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                string pathFile = Path.Combine(dialog.FileName, "StockReport.xlsx");
                if (File.Exists(pathFile))
                {
                    if (MessageBox.Show("Искате ли да презапишите файлът?", "Предупреждение!", MessageBoxButtons.YesNo) != DialogResult.Yes)
                    {
                        return;
                    }
                    else
                    {
                        File.Delete(pathFile);
                    }
                }

                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                using (ExcelPackage excelPackage = new ExcelPackage(pathFile))
                {
                    ExcelWorksheet ew = excelPackage.Workbook.Worksheets.Add("StockReport");
                    ReportTemplate template = new StockReportTemplate(ew);
                    template.Create();

                    if (StockSummaryDtos.Count > 0)
                    {
                        ew.Cells[$"B2:B{StockSummaryDtos.Count + 1}"].Style.Numberformat.Format = "0.00";
                    }

                    for (int i = 2; i <= StockSummaryDtos.Count + 1; i++)
                    {
                        StockSummaryDto stock = StockSummaryDtos[i - 2];
                        ew.Cells[$"A{i}"].Value = stock.Name;
                        ew.Cells[$"B{i}"].Value = stock.Price;
                        ew.Cells[$"C{i}"].Value = stock.Measure;
                        ew.Cells[$"D{i}"].Value = stock.DeliveryCount;
                        ew.Cells[$"E{i}"].Value = stock.TotalQuantity;
                    }

                    excelPackage.Save();
                }
            }
        }
    }
}
