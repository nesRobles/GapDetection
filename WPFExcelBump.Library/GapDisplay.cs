using GapDetectionDataManager.Library.DataAccess;
using GapDetectionDataManager.Library.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WPFExcelBump.Library
{
    public static class GapDisplay
    {
        public static void CreateGapArea(Grid bumpGrid2, IVenueData db, int id)
        {
            LabelGenerator.ClearLabels(bumpGrid2);

            List<DateTime> timebar = GetTimeBar();

            List<PositionModel> legendPositions = GetLegendPositions(db, id);

            DisplayTimeBar(timebar, "#B7DDE8", 0, 1, 27, 50, bumpGrid2);

            DisplayTimeBar(timebar, "#B7DDE8", legendPositions.Count + 1, 1, 27, 50, bumpGrid2);

            DisplayGapLegend(legendPositions, "#00B0F0", 0, 3, 25, 27, bumpGrid2);

            DisplayGapLegend(legendPositions, "#00B0F0", timebar.Count + 3, 3, 25, 27, bumpGrid2);

            DisplayEmptyGapGrid(legendPositions!.Count, timebar.Count + 2, 25, 27, bumpGrid2);
        }

        private static List<PositionModel> GetLegendPositions(IVenueData db, int id)
        {
            return db!.GetVenuePositions(id).OrderBy(x => x.GapOrder).ToList();
        }

        private static List<DateTime> GetTimeBar()
        {
            return Timebar.CreateTimeBar();
        }

        public static void DisplayTimeBar(List<DateTime> timebar, string hexColor, int rowNumber, int mergeMultiplier, double Height, double width, Grid bumpGrid2)
        {
            for (int i = 0; i < timebar.Count; i++)
            {
                Label label = LabelGenerator.LabelCreator(timebar[i].ToString("h:mm"), hexColor, Height, width, -90);

                LabelGenerator.DisplayLabel(bumpGrid2, label, rowNumber, i + 3, mergeMultiplier);
            }
        }

        public static void DisplayGapLegend(List<PositionModel> legendPositions, string hexColor, int column, int mergeMultiplier, double height, double width, Grid bumpGrid2)
        {
            int widthSpan = (int)width * mergeMultiplier;

            for (int i = 1; i <= legendPositions.Count; i++)
            {
                Label label = LabelGenerator.LabelCreator(legendPositions[i - 1].Position, hexColor!, height, widthSpan);

                LabelGenerator.DisplayLabel(bumpGrid2, label, i, column, mergeMultiplier);
            }
        }

        public static void DisplayEmptyGapGrid(int rowCount, int columnCount, double height, double width, Grid bumpGrid2)
        {
            for (int i = 1; i <= rowCount; i++)
            {
                for (int j = 3; j <= columnCount; j++)
                {
                    Label label = LabelGenerator.LabelCreator("", "#404040"!, height, width);

                    LabelGenerator.DisplayLabel(bumpGrid2, label, i, j); 
                }
            }
        }

        public static void DisplayGapData(int[,] gapIntegerData, double height, double width, Grid bumpGrid2)
        {
            for (int i = 0; i < gapIntegerData.GetLength(0); i++)
            {
                for (int j = 0; j < gapIntegerData.GetLength(1); j++)
                {
                    if (gapIntegerData[i, j] == 1)
                    {
                        // Shows blue square represenation of position
                        Label label = LabelGenerator.LabelCreator("", "#00B0F0", height, width);

                        LabelGenerator.DisplayLabel(bumpGrid2, label, i + 1, j + 1);
;
                    }
                    else if (gapIntegerData[i, j] > 1)
                    {
                        // Shows red square and number of duplicate positions
                        Label label = LabelGenerator.LabelCreator(gapIntegerData[i, j].ToString(), "#FF0000", height, width);

                        LabelGenerator.DisplayLabel(bumpGrid2, label, i + 1, j + 1);
                    }
                }
            }
        }
    }
}
