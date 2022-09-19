using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Excel = Microsoft.Office.Interop.Excel;

namespace WPFExcelBump.Library.ExcelLogic
{
    public static class ExcelFileMethods
    {
        public static void OpenExcelFileDialog(ExcelFileModel excelBump)
        {
            OpenFileDialog excelFileDialog = new();

            excelFileDialog.Multiselect = false;
            excelFileDialog.Filter = "Excel Files|*.xls;*.xlsx;*.xlsm";

            excelFileDialog!.ShowDialog();

            excelBump.FilePath = excelFileDialog.FileName;
        }

        public static bool OpenExcelFile(ExcelFileModel excelBump)
        {
            bool isValidFile = true;

            excelBump.WBBump = excelBump.ExcelApp!.Workbooks.Open(excelBump.FilePath, 0, false, 5, "", "", false,
                                                                 Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);

            excelBump.WSBump = excelBump.WBBump.Worksheets[1];
            string name = excelBump.WSBump.Name;

            if (name != "Bump")
            {
                isValidFile = false;
                MessageBox.Show("Please open a valid Excel Bump file.");
                excelBump.WBBump.Close(isValidFile);
                return isValidFile;
            }

            excelBump.ExcelApp!.Visible = true;

            excelBump.WSBump = excelBump.WBBump.Worksheets["Bump"];
            //excelBump.WSGap = excelBump.WBBump.Worksheets["Gap Detection"];

            return isValidFile;
        }
    }
}
