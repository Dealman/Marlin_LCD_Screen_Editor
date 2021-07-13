using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace Marlin_LCD_Screen_Editor
{
    public class PixelDisplay : FrameworkElement
    {
        List<Pixel> PixelArray = new List<Pixel>();
        Brush ActiveBrush;
        Brush InactiveBrush;

        public PixelDisplay(List<Pixel> pixelArray, Brush activeBrush, Brush inactiveBrush)
        {
            PixelArray = pixelArray;
            ActiveBrush = activeBrush;
            InactiveBrush = inactiveBrush;

            ActiveBrush.Freeze();
            InactiveBrush.Freeze();
        }

        public void UpdateBrushes(Brush newActive, Brush newInactive)
        {
            ActiveBrush = newActive;
            InactiveBrush = newInactive;

            ActiveBrush.Freeze();
            InactiveBrush.Freeze();
        }

        public void LoadFromBinaryString(string s)
        {
            if (String.IsNullOrWhiteSpace(s) || s.Length == 0) return;

            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == '1')
                {
                    PixelArray[i].FillColour = InactiveBrush;
                    PixelArray[i].State = PixelState.Off;
                } else {
                    PixelArray[i].FillColour = ActiveBrush;
                    PixelArray[i].State = PixelState.On;
                }
            }
        }
        Stopwatch sw = new Stopwatch();

        public int[] ToIntArray()
        {
            int[] ints = new int[PixelArray.Count];

            for (int i = 0; i < PixelArray.Count; i++)
            {
                ints[i] = (PixelArray[i].State == PixelState.On ? 1 : 0);
            }

            return ints;
        }

        public override string ToString()
        {
            int[] bitArray = ToIntArray();
            var sb = new StringBuilder();

            for (int i = 0; i < bitArray.Length; i++)
                sb.Append(bitArray[i]);

            string data = sb.ToString();

            return data;
        }

        public void SetAllPixelStates(PixelState newState)
        {
            for (int i = 0; i < PixelArray.Count; i++)
            {
                if (newState == PixelState.On)
                {
                    PixelArray[i].FillColour = ActiveBrush;
                    PixelArray[i].State = PixelState.On;
                } else {
                    PixelArray[i].FillColour = InactiveBrush;
                    PixelArray[i].State = PixelState.Off;
                }
            }
        }

        public void InvertPixels()
        {
            if (PixelArray is null || PixelArray.Count == 0) return;

            for (int i = 0; i < PixelArray.Count; i++)
            {
                if (PixelArray[i].State == PixelState.On)
                {
                    PixelArray[i].FillColour = InactiveBrush;
                    PixelArray[i].State = PixelState.Off;
                } else {
                    PixelArray[i].FillColour = ActiveBrush;
                    PixelArray[i].State = PixelState.On;
                }
            }
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            if (PixelArray is not null && PixelArray.Count > 0)
            {
                for (int i = 0; i < PixelArray.Count; i++)
                {
                    drawingContext.DrawRectangle(PixelArray[i].FillColour, PixelArray[i].OutlineColour, PixelArray[i].Geometry);
                    if (PixelArray[i].OutlineColour is not null)
                        PixelArray[i].OutlineColour = null;
                }
            }
        }
    }
}
