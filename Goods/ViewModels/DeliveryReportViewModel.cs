using Goods.Commands;
using Goods.Dao;
using Goods.Dao.Contracts;
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
using System.Windows.Forms;
using System.Windows.Input;

namespace Goods.ViewModels
{
    public class DeliveryReportViewModel : BaseViewModel
    {
        public RelayCommand SearchCommand { get; }
        public RelayCommand ExcelExportCommand { get; }
        public ICommand NavigationBackCommand { get; }

        private IDeliveryDao deliveryDao;
        private ObservableCollection<DeliveryOilDto> deliveriesOilDto;

        private string stockName;
        private DateTime deliveryDate;

        public DeliveryReportViewModel(NavigationStore navigationStore)
        {
            SearchCommand = new RelayCommand(Search);
            ExcelExportCommand = new RelayCommand(CreateExcelFile);
            NavigationBackCommand = new NavigateCommand<ReportHomeViewModel>(navigationStore, (n) => new ReportHomeViewModel(n));
            deliveryDao = new DeliveryDaoProxy();
            StockName = "Олио";
            DeliveryDate = DateTime.Now;
            Search();
        }

        public ObservableCollection<DeliveryOilDto> DeliveriesOilDto
        {
            get { return deliveriesOilDto; }
            set { deliveriesOilDto = value; OnPropertyChanged(nameof(DeliveriesOilDto)); }
        }

        public string StockName
        {
            get { return stockName; }
            set { stockName = value; OnPropertyChanged(nameof(StockName)); }
        }

        public DateTime DeliveryDate
        {
            get { return deliveryDate; }
            set { deliveryDate = value; OnPropertyChanged(nameof(DeliveryDate)); }
        }

        private void Search()
        {
            DeliveriesOilDto = new ObservableCollection<DeliveryOilDto>(deliveryDao.GetDeliveryOilDtos(StockName, DeliveryDate, 0));
        }

        private void CreateExcelFile()
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.IsFolderPicker = true;
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                string pathFile = Path.Combine(dialog.FileName, "DeliveryReport.xlsx");
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
                    ExcelWorksheet ew = excelPackage.Workbook.Worksheets.Add("DeliveryReport");
                    ReportTemplate template = new DeliveryReportTemplate(ew);
                    template.Create();

                    if (deliveriesOilDto.Count > 0)
                    {
                        ew.Cells[$"C2:C{deliveriesOilDto.Count + 1}"].Style.Numberformat.Format = "0.00";
                    }

                    for (int i = 2; i <= deliveriesOilDto.Count + 1; i++)
                    {
                        DeliveryOilDto delivery = deliveriesOilDto[i - 2];
                        ew.Cells[$"A{i}"].Value = delivery.ProviderId;
                        ew.Cells[$"B{i}"].Value = delivery.StockName;
                        ew.Cells[$"C{i}"].Value = delivery.Price;
                        ew.Cells[$"D{i}"].Value = delivery.QuantityValue;
                        ew.Cells[$"E{i}"].Value = $"{delivery.DeliveryDate:g}";
                    }

                    excelPackage.Save();
                }
            }
        }
    }
}
