using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace Marlin_LCD_Screen_Editor
{
    public class PixelGridGenerator : FrameworkElement
    {
        Dictionary<Rect, Brush> PixelData = new Dictionary<Rect, Brush>();

        public PixelGridGenerator(List<RectangleGeometry> geos)
        {
            if (geos is null || geos.Count == 0) return;

            foreach (RectangleGeometry rect in geos)
            {
                if(!PixelData.ContainsKey(rect.Rect))
                    PixelData.Add(rect.Rect, Brushes.Aquamarine);
            }
        }

        public void SetPixelColour(int index, Brush colour)
        {
            Rect rect = PixelData.ElementAt(index).Key;
            PixelData[rect] = colour;
        }

        public void SetAllPixelColour(Brush colour)
        {
            for (int i = 0; i < PixelData.Count; i++)
            {
                PixelData[PixelData.ElementAt(i).Key] = colour;
            }
        }

        public void InvertPixelColours()
        {
            foreach (KeyValuePair<Rect, Brush> pixel in PixelData)
            {
                if (pixel.Value == Brushes.Aquamarine)
                    PixelData[pixel.Key] = Brushes.Blue;
                else
                    PixelData[pixel.Key] = Brushes.Aquamarine;
            }
        }

        public void LoadFromBinary(string code)
        {
            int codeLength = Math.Clamp(code.Length, 1, PixelData.Count);

            for (int i = 0; i < codeLength; i++)
            {
                switch (code[i])
                {
                    case '0':
                        PixelData[PixelData.ElementAt(i).Key] = Brushes.Aquamarine;
                        break;

                    case '1':
                        PixelData[PixelData.ElementAt(i).Key] = Brushes.Blue;
                        break;
                }
            }
        }

        public override string ToString()
        {
            string data = "";

            foreach (KeyValuePair<Rect, Brush> pixel in PixelData)
            {
                if (pixel.Value == Brushes.Aquamarine)
                    data = data + "1";
                else
                    data = data + "0";
            }

            return data;
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            foreach (KeyValuePair<Rect, Brush> pixel in PixelData)
            {
                drawingContext.DrawRectangle(pixel.Value, null, pixel.Key);
            }
            //foreach (KeyValuePair<Rect, Brush> pixel in PixelData)
            //{
            //    drawingContext.DrawRectangle(pixel.Value, null, pixel.Key);
            //}
        }
    }
}
