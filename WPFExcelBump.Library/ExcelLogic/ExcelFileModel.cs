using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;

namespace WPFExcelBump.Library.ExcelLogic
{
    public class ExcelFileModel
    {
        public string FilePath { get; set; } = string.Empty;
        public Excel.Application? ExcelApp { get; set; }
        public Excel.Workbook? WBBump { get; set; }
        public Excel.Worksheet? WSBump { get; set; }
        public Excel.Worksheet? WSGap { get; set; }
        public Excel.Range? BRange { get; set; }
        public int[,]? GapData { get; set; }
        public int RowCount { get; set; } = 0;
        public int ColumnCount { get; set; } = 0;
        public int RideOpCount { get; set; } = 0;
        public object[,]? BumpElements { get; set; }

        public ExcelFileModel()
        {
            ExcelApp = new Excel.Application();
        }
    }
}
