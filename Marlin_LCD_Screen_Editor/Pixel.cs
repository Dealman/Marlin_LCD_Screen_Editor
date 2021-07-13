using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Marlin_LCD_Screen_Editor
{
    public enum PixelState
    {
        Off,
        On
    }

    public class Pixel
    {

        public int Index { get; set; }
        public Rect Geometry { get; set; }
        public Brush FillColour { get; set; }
        public Pen OutlineColour { get; set; }
        public PixelState State { get; set; } = PixelState.Off;
        public Pixel(int index, Rect geometry, Brush fill)
        {
            Index = index;
            Geometry = geometry;
            FillColour = fill;
        }

        public void SetPixelData(PixelState newState, Brush newFill)
        {
            State = newState;
            FillColour = newFill;
        }
    }
}
