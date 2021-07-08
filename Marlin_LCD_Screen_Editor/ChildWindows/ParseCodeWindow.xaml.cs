using MahApps.Metro.SimpleChildWindow;
using System;
using System.Windows;

namespace Marlin_LCD_Screen_Editor.ChildWindows
{
    public partial class ParseCodeWindow : ChildWindow
    {
        public ParseCodeWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (sender == ConfirmButton)
            {
                if (!String.IsNullOrWhiteSpace(InputTextBox.Text))
                {
                    this.Close(InputTextBox.Text);
                } else {
                    this.Close("EMPTY");
                }
            }

            if (sender == CancelButton)
                this.Close("CANCEL");
        }
    }
}
