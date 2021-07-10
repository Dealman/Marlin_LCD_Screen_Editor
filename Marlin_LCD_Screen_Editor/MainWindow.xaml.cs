using MahApps.Metro.Controls;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using MahApps.Metro.Controls.Dialogs;
using MahApps.Metro.SimpleChildWindow;

namespace Marlin_LCD_Screen_Editor
{
    public partial class MainWindow : MetroWindow
    {
        // TODO: Add project support
        // TODO: Add saving of app settings (position, size, last used screen, etc)

        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Rectangle_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (sender == ActiveColourRect)
            {
                var oldColour = ActiveColourRect.Fill;
                var newColour = await ChildWindowManager.ShowChildWindowAsync<Color>(this, new ChildWindows.ColourPickerWindow(oldColour));
                var newBrush = new SolidColorBrush(newColour);
                ActiveColourRect.Fill = newBrush;
                PixelGridControl.ActiveBrush = newBrush;
            }
            
            if (sender == InactiveColourRect)
            {
                var oldColour = InactiveColourRect.Fill;
                var newColour = await ChildWindowManager.ShowChildWindowAsync<Color>(this, new ChildWindows.ColourPickerWindow(oldColour));
                var newBrush = new SolidColorBrush(newColour);
                InactiveColourRect.Fill = newBrush;
                PixelGridControl.InactiveBrush = newBrush;
            }
        }
    }
}
