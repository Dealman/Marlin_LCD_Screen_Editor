using MahApps.Metro.Controls;
using System.Windows.Input;
using System.Windows.Media;
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
