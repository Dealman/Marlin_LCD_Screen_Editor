using MahApps.Metro.SimpleChildWindow;
using System.Collections.Generic;
using System.Windows.Media;
using System.Windows;

namespace Marlin_LCD_Screen_Editor.ChildWindows
{
    public partial class PreviewWindow : ChildWindow
    {
        List<Pixel> PixelArray = new List<Pixel>();

        public PreviewWindow(List<Pixel> pixelArray, Brush activeBrush, Brush inactiveBrush)
        {
            InitializeComponent();

            for (int i = 0; i < pixelArray.Count; i++)
            {
                Rect oldRect = pixelArray[i].Geometry;
                Pixel newPixel = new Pixel(PixelArray.Count, new Rect((oldRect.Left/8), (oldRect.Top/8), 2, 2), inactiveBrush); // TODO: Remove hardcoded value! Pixel size, remove offset
                newPixel.FillColour = (pixelArray[i].State == PixelState.On ? activeBrush : inactiveBrush);
                PixelArray.Add(newPixel);
            }

            PixelDisplay PD = new PixelDisplay(PixelArray, activeBrush, inactiveBrush);

            LeGrid.Children.Add(PD);
            LeGrid.Margin = new Thickness(-88, 0, 0, 0); // TODO: Hardcoded value, fetch from active screen
        }
    }
}
