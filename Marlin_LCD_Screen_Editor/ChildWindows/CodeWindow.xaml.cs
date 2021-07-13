using MahApps.Metro.SimpleChildWindow;
using System.Windows;

namespace Marlin_LCD_Screen_Editor.ChildWindows
{
    public partial class CodeWindow : ChildWindow
    {
        string BinaryData = "";

        public CodeWindow(string binaryData)
        {
            InitializeComponent();

            if (binaryData is null || binaryData.Length == 0)
                return;
            else
                ProcessBinaryData(binaryData);

        }

        private void ProcessBinaryData(string data)
        {
            // TODO: Display error message, close self
            if (string.IsNullOrWhiteSpace(data) || data.Length == 0 || !Utilities.StringIsbinary(data))
                return;

            string codePrefix = "const unsigned char custom_start_bmp[] PROGMEM = {\n";
            string codeBody = "  ";
            string codeSuffix = "\n};";
            int counter = 1;

            for (int i = 0; i < data.Length; i++)
            {
                int pixelType = (data[i] == '1') ? 1 : 0; // Active : Inactive

                switch (i % 8)
                {
                    case 0:
                        codeBody = (codeBody + $"B{pixelType}");
                        break;

                    case 7:
                        if (counter % 11 == 0) // Width/8?
                            if (i != data.Length-1)
                                codeBody = (codeBody + $"{pixelType},\n  ");
                            else
                                codeBody = (codeBody + $"{pixelType}");
                        else
                                codeBody = (codeBody + $"{pixelType},");
                        counter++;
                        break;

                    default:
                        codeBody = (codeBody + $"{pixelType}");
                        break;
                }
            }

            FullCodeBox.Text = (codePrefix + codeBody + codeSuffix);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Return anything here? Probably not...?
            this.Close();
        }
    }
}
