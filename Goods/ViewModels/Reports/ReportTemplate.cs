using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Drawing;

namespace Goods.ViewModels.Reports
{
    abstract class ReportTemplate
    {
        private ExcelWorksheet ew;

        protected ExcelWorksheet ExcelWorksheet
        {
            get { return ew; }
        }

        public ReportTemplate(ExcelWorksheet excelWorksheet)
        {
            this.ew = excelWorksheet;
        }

        public void Create()
        {
            SetExcelStyle();
            SetExcelColumnWidht();
            BuildHeaderCells();
        }

        protected virtual void SetExcelStyle()
        {
            ExcelWorksheet.Row(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            ExcelWorksheet.Row(1).Style.Font.Size = 12;
            ExcelWorksheet.Row(1).Style.Font.Bold = true;
            ExcelWorksheet.Row(1).Style.Font.Color.SetColor(Color.White);
            ExcelWorksheet.Cells["A1:E1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            ExcelWorksheet.Cells["A1:E1"].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(48, 84, 150));
        }

        protected virtual void SetExcelColumnWidht()
        {
            ExcelWorksheet.Columns.Width = 22;
        }

        protected abstract void BuildHeaderCells();
    }
}
