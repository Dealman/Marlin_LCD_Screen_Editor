using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Marlin_LCD_Screen_Editor
{
    public static class Utilities
    {
        public static bool StringIsbinary(string s)
        {
            if (s is null) return false;

            foreach (var c in s)
                if (c != '0' && c != '1')
                    return false;
            return true;
        }

        public static void DisplayError(Exception ex)
        {
            MessageBox.Show($"An error has occurred!\n\nError Message:\n{ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        [DllImport("gdi32.dll", EntryPoint = "DeleteObject")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool DeleteObject([In] IntPtr hObject);

        public static ImageSource ImageSourceFromBitmap(Bitmap bmp)
        {
            var handle = bmp.GetHbitmap();
            try
            {
                return Imaging.CreateBitmapSourceFromHBitmap(handle, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            }
            finally { DeleteObject(handle); }
        }
    }
}
