using MahApps.Metro.SimpleChildWindow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Marlin_LCD_Screen_Editor.ChildWindows
{
    public partial class ColourPickerWindow : ChildWindow
    {
        public ColourPickerWindow(Brush defaultColour)
        {
            InitializeComponent();
            SolidColorBrush d = (SolidColorBrush)defaultColour;
            CP.SelectedColor = d.Color;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (sender == ConfirmButton)
                Close(CP.SelectedColor);

            if (sender == CancelButton)
                Close();
        }
    }
}
