using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GapDetectionDataManager.Library.DataAccess;
using GapDetectionDataManager.Library.Models;
using Microsoft.Win32;
using Excel = Microsoft.Office.Interop.Excel;


namespace WPFExcelBump.Library.ExcelLogic
{
    public static class ExcelBumpData
    {
        public static void GetBumpGapData(ExcelFileModel excelBump, IVenueData db, int id)
        {
            GetBumpRowColumnCounts(excelBump);

            GetExcelBumpRange(excelBump);

            //ReplaceNullCellWithItsFirstMergedCell(excelBump);

           //ReplaceNullCellWithItsFirstMergedCell(excelBump);

            SortGapDataIntoIntegerArray(excelBump, db, id);
        }

        private static void GetBumpRowColumnCounts(ExcelFileModel excelBump)
        {
            excelBump.RowCount = 0;
            excelBump.ColumnCount = 0;

            for (int i = 1; i <= 500; i++)
            {
                if (excelBump.WSBump!.Cells[i, 1].Interior.Color == 16777215)
                {
                    break;
                }

                excelBump.RowCount++;
            }

            excelBump.RideOpCount = excelBump.RowCount - 2;

            for (int i = 1; i <= 500; i++)
            {
                if (excelBump.WSBump!.Cells[1, i].Interior.Color == 16777215)
                {
                    break;
                }

                excelBump.ColumnCount++;
            }
        }

        private static void GetExcelBumpRange(ExcelFileModel excelBump)
        {
            //excelBump.BumpElements = excelBump.WSBump!.Range[excelBump.WSBump.Cells[2, 1], excelBump.WSBump.Cells[excelBump.RowCount, excelBump.ColumnCount]].Value;
            excelBump.BumpElements = new object[excelBump.RowCount, excelBump.ColumnCount];
            for (int i = 1; i < excelBump.RowCount; i++)
            {
                for (int j = 1; j < excelBump.ColumnCount; j++)
                {
                    excelBump.BumpElements![i - 1, j - 1] = excelBump.WSBump!.Cells[i + 1, j].MergeArea(1, 1).Value;

                    if (excelBump.BumpElements![i - 1, j - 1] == null)
                    {
                        excelBump.BumpElements![i - 1, j - 1] = "";
                    }
                }
            }
        }

        private static void ReplaceNullCellWithItsFirstMergedCell(ExcelFileModel excelBump)
        {
            string shiftElement = string.Empty;

            //Converts all null object elements in bumpElements to its MergeArea First Cell Value eqivilant
            for (int j = 1; j < excelBump.RowCount; j++)
            {
                for (int i = 1; i <= excelBump.ColumnCount; i++)
                {
                    if (excelBump.BumpElements![j, i] != null)
                    {
                        shiftElement = excelBump.BumpElements[j, i].ToString()!;
                    }

                    if (excelBump.BumpElements[j, i] == null)
                    {
                        excelBump.BumpElements[j, i]  = shiftElement;
                    }
                }
            }
        }

        private static void SortGapDataIntoIntegerArray(ExcelFileModel excelBump, IVenueData db, int id)
        {
            // Sorts Gap Data per Position and increments position duplication per time interval.
            string position = string.Empty;
            string? bumpElement = string.Empty;
            List<PositionModel> positionsInLegend = db.GetVenuePositions(id).OrderBy(x => x.GapOrder).ToList();
            int[,] gapIntegerData = new int[positionsInLegend.Count, excelBump.ColumnCount];

            for (int i = 0; i < positionsInLegend.Count; i++)
            {
                for (int j = 1; j < excelBump.RowCount; j++)
                {
                    for (int k = 1; k < excelBump.ColumnCount; k++)
                    {
                        position = positionsInLegend[i].Position;
                        bumpElement = excelBump.BumpElements![j - 1, k - 1].ToString();

                        if (bumpElement == position)
                        {
                            gapIntegerData[i, k - 1] = gapIntegerData[i, k - 1] + 1; 
                        }
                    }
                } 
            }

            excelBump.GapData = gapIntegerData;
        }
    }
}
