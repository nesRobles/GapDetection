using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WPFExcelBump.Library
{
    public class LabelGenerator
    {
        public static Label LabelCreator(string item, string hexColor, double height, double width, double rotation = 0)
        {
            Label label = new();
            label.Height = height;
            label.Width = width;
            label.Foreground = Brushes.Black;
            label.Background = new BrushConverter().ConvertFrom(hexColor) as SolidColorBrush;
            label.HorizontalContentAlignment = HorizontalAlignment.Center;
            label.VerticalContentAlignment = VerticalAlignment.Center;
            label.LayoutTransform = new RotateTransform(rotation);
            label.FontSize = 12;
            label.FontWeight = FontWeights.Bold;
            label.Content = item;
            label.BorderBrush = Brushes.Black;
            label.BorderThickness = new Thickness(1.0);

            return label;
        }

        public static void DisplayLabel(Grid bumpGrid2, Label label, int row, int column, int columnSpan = 1)
        {
            Grid.SetColumn(label, column);
            Grid.SetColumnSpan(label, columnSpan);
            Grid.SetRow(label, row);
            bumpGrid2.Children.Add(label);
        }

        public static void ClearLabels(Grid bumpGrid2)
        {
            int elementCount = bumpGrid2.Children.Count;

            if (elementCount > 0)
            {
                for (int i = elementCount; i >= 1; i--)
                {
                    UIElement element = bumpGrid2.Children[i - 1];
                    
                    if (element.GetType() == typeof(Label))
                    {
                        Label label = (Label)element;

                        bumpGrid2.Children.Remove(label);
                    }
                }
            }
        }
    }
}
