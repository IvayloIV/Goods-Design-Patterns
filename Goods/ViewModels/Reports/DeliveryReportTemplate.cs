using OfficeOpenXml;

namespace Goods.ViewModels.Reports
{
    class DeliveryReportTemplate : ReportTemplate
    {
        public DeliveryReportTemplate(ExcelWorksheet excelWorksheet) : base(excelWorksheet)
        {
        }

        protected override void BuildHeaderCells()
        {
            ExcelWorksheet.Cells["A1"].Value = "Номер на доставчик";
            ExcelWorksheet.Cells["B1"].Value = "Име на стока";
            ExcelWorksheet.Cells["C1"].Value = "Цена с ДДС";
            ExcelWorksheet.Cells["D1"].Value = "Количество";
            ExcelWorksheet.Cells["E1"].Value = "Дата на доставка";
        }
    }
}
