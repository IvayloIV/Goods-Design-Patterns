using OfficeOpenXml;

namespace Goods.ViewModels.Reports
{
    class StockReportTemplate : ReportTemplate
    {
        public StockReportTemplate(ExcelWorksheet excelWorksheet) : base(excelWorksheet)
        {
        }

        protected override void SetExcelColumnWidht()
        {
            base.SetExcelColumnWidht();
            ExcelWorksheet.Column(5).Width = 30;
        }

        protected override void BuildHeaderCells()
        {
            ExcelWorksheet.Cells["A1"].Value = "Име на стока";
            ExcelWorksheet.Cells["B1"].Value = "Цена";
            ExcelWorksheet.Cells["C1"].Value = "Мярка количество";
            ExcelWorksheet.Cells["D1"].Value = "Брой доставки";
            ExcelWorksheet.Cells["E1"].Value = "Общо доставено количество";
        }
    }
}
