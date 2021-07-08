using MahApps.Metro.SimpleChildWindow;
using System.Windows.Media;

namespace Marlin_LCD_Screen_Editor.ChildWindows
{
    public partial class PreviewWindow : ChildWindow
    {
        public PreviewWindow(string code)
        {
            InitializeComponent();

            DirectBitmap bmp = new DirectBitmap(88, 58);

            int counter = 0;

            for (int y = 0; y < 58; y++)
            {
                for (int x = 0; x < 88; x++)
                {
                    if (code[counter] == '1')
                        bmp.SetPixel(x, y, System.Drawing.Color.FromArgb(Colors.Aquamarine.A, Colors.Aquamarine.R, Colors.Aquamarine.G, Colors.Aquamarine.B));
                    else
                        bmp.SetPixel(x, y, System.Drawing.Color.FromArgb(Colors.Blue.A, Colors.Blue.R, Colors.Blue.G, Colors.Blue.B));

                    counter++;
                }
            }

            PreviewImage.Source = Utilities.ImageSourceFromBitmap(bmp.Bitmap);
        }
    }
}
