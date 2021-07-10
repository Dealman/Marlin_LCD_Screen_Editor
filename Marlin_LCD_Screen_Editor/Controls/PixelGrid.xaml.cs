using MahApps.Metro.SimpleChildWindow;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Marlin_LCD_Screen_Editor.Controls
{
    // Default Ender 3 Binary String
    //1111111111111111111111111111111111101111111111111111111111111111111111111111111111111111111111111111111111111111111111111110111111101111111111111111111111111111111111111111111111111111111111111111111111111111111001111101111111111111111111111111111111111111111111111111111111111111111111111111111111100111110111111111111111111111111111111111111111111111111111111111111111111111111111111110001111011111111111111111111111111111111111111111111111111111111111111111111111111111111100111100111111111111111111111111111111111111111111111111111111111111111111111110000111100001110011111111111111111111111111111111111111111111111111111111111001111000000000000000000000000011110111011111111111111111111111111111111111111110111111111000000001111110000000000000000111101110111111111111111111111111111111111111111001111101110011111111110000000000000000001111011101111111111111111111111111111111111111111000111000000110000000000000000000000000011110111011111111111111111111111111111111111111110000000000000000000000000000000000000001111101110111111111111111111111111111111111111111111100000000011111111000000000000000000011111011100111111111111111111111111111111111111111111111111111111111000000000000000011000111110111001111111111111111111111111111111111111111111111111111111000000000000000000110001111100000011111111111111111111111111111111111111111111111111110000000000000000000001000001111111001111111111111111111111111111111111111111111111111000000001111110000000000000000001111000011111111111111111111111111111111111111111110000000001111111111111100000000000000000000001111111111111111111111111111111111111110000000111111111111111111110000000000000000000000111111111111111111111111111111111111111111111111111111111111100100000000000000000000000011111111111111111111111111111111111111111111111111111111111111000000000000000111110000001111111111111111111111111111111111111111111111111111111111111111000000000000011111100000111111111111111111111111111111111111111111111111111111111111111111100000000001111111000111111111111111111111111111111111111111111111111111111111111111111111110000000111111110011111111111111111111111111111111111111111111111111111111111111111111111110000001111111001111111111111111111111111111111111111111111111111111111111111111111111111100000111111100111111111111111111111111111111111111111111111111111111111111111111111111111000011111111111111111111111111111111111111111111111111111111111111111111111111111111111110000111111111111111111111111111111111111111111111111111111111111111111111111111111111111100001011111111111111111111111111111111111111111111111111111111111111111111111111101111111000001111111111111111111111111111111111111111111111111111111111111111111111111110011111100000111111111111111111111111111111111111111111111111111111111111111111111101111110111100001111111111111111111111111111111111111111111111111111111111111111111111111000111000000011111111111111111111111111111111111111111111111111111111111111111111111111111000000001111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111101111111111111111111111111111111111111110000000000000000011111111111111111111111111110000111111111111111111111111111111111111111100000000000000001111111111111111111111111100000011111111111111111111111111111111111111111000011111111001111111111111111111111111111100011111111111111111111111111111111111111111100001111111100111111111111111111111111111110001111111111111111111111111111111111111111100001111111110111111111111111111111111111110001111111111111111111111111111111111111111110000111111111111111111111111111111111111111000111111111111111111111111111111111111111110000111111100111111100110000111111111100001000111111110000111111111111111111111111111111000011111110111110000000000001111111000000000011111100000001111000000000011111111111111000011111110011111000000110000111110001111000011111000111000011000000000001111111111111100000000000011111110001111100011110001111110001111001111100001111000011001111111111111110000000000001111110000111100011110001111110001111000111110000111100011111111111111111110000111111100111111000111110001111000111111000111000000000000111100011111111111111111111000111111100111111000011110001111000111111000111100011111111111110001111111111111111111000011111111111111100011111000111000011111100011100011111111111110001111111111111111111100011111111111111110001111000011100011111100001110001111111111111000111111111111111111100001111111111001110001111100011110001111110001111000111111110111000111111111111111111110000111111111001111000111100001110000111110000111000011111100111100011111111111111111110000111111111001111000111110001111000011111000111100000111100111100011111111111111111111000011111100000111000011110000111100000010000000110000000000111110001111111111111111100000000000000000110000000100000001111000000000001111100000001111000000001111111111111111111111111111111111111111111111111111110000111111111111000011111111111111111111111111111
    
    // TODO: Remove hardcoded values ASAP

    public partial class PixelGrid : UserControl
    {
        MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
        PixelDisplay PD;

        Brush activeBrush = Brushes.Aquamarine;
        Brush inactiveBrush = Brushes.DodgerBlue;
        public Brush ActiveBrush { get { return activeBrush; } set { activeBrush = value; BrushesChanged(true); } }
        public Brush InactiveBrush { get { return inactiveBrush; } set { inactiveBrush = value; BrushesChanged(false); } }
        List<Pixel> PixelArray = new List<Pixel>();

        public PixelGrid()
        {
            InitializeComponent();
        }

        private void PixelContainer_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        public void GenerateGrid(int width, int height, int pixelSize, int pixelOffset)
        {
            int pixelTotalSize = pixelSize + pixelOffset;

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    var xPos = x * pixelTotalSize;
                    var yPos = y * pixelTotalSize;

                    PixelArray.Add(new Pixel((PixelArray.Count), new Rect(xPos, yPos, pixelSize, pixelSize)));
                }
            }

            PD = new PixelDisplay(PixelArray, ActiveBrush, InactiveBrush);

            PixelContainer.Children.Add(PD);
            PixelContainer.Width = 88 * pixelTotalSize;
            PixelContainer.Height = 58 * pixelTotalSize;

            PixelGridChanged();
        }

        void BrushesChanged(bool activeBrush)
        {
            ActiveBrush.Freeze();
            InactiveBrush.Freeze();
            PD.UpdateBrushes(ActiveBrush, InactiveBrush);

            for (int i = 0; i < PixelArray.Count; i++)
            {
                if (PixelArray[i].State == PixelState.On)
                    PixelArray[i].FillColour = ActiveBrush;
                else
                    PixelArray[i].FillColour = InactiveBrush;
            }

            RefreshPixelContainer();
        }

        private int GetPixelUnderCursor(Point mousePos)
        {
            // TODO: These values shouldn't be hardcoded, fix ASAP!
            var xPos = Math.Floor(mousePos.X / 9);
            var yPos = Math.Floor(mousePos.Y / 9);
            int xPosInt = (int)xPos;
            int yPosInt = (int)yPos;
            var index = (yPosInt * 88) + xPosInt;

            return index;
        }

        private void PixelContainer_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (PixelContainer.IsMouseOver)
            {
                if (e.LeftButton == MouseButtonState.Pressed || e.RightButton == MouseButtonState.Pressed)
                {
                    int pixel = GetPixelUnderCursor(e.GetPosition(PixelContainer));

                    if (e.LeftButton == MouseButtonState.Pressed)
                        PixelArray[pixel].SetPixelData(PixelState.On, ActiveBrush);

                    if (e.RightButton == MouseButtonState.Pressed)
                        PixelArray[pixel].SetPixelData(PixelState.Off, InactiveBrush);

                    RefreshPixelContainer();
                }
            }
        }

        // TODO: Use some line algorithm to account for fast mouse movements? Unnecessary?
        private void PixelContainer_MouseMove(object sender, MouseEventArgs e)
        {
            if (PixelContainer.IsMouseOver)
            {
                if (e.LeftButton == MouseButtonState.Pressed || e.RightButton == MouseButtonState.Pressed)
                {
                    int pixel = GetPixelUnderCursor(e.GetPosition(PixelContainer));

                    if (e.LeftButton == MouseButtonState.Pressed)
                        PixelArray[pixel].SetPixelData(PixelState.On, ActiveBrush);

                    if (e.RightButton == MouseButtonState.Pressed)
                        PixelArray[pixel].SetPixelData(PixelState.Off, InactiveBrush);

                    RefreshPixelContainer();
                }
            }
        }

        private void RefreshPixelContainer()
        {
            PixelContainer.Children.Clear();
            PixelContainer.Children.Add(PD);
        }

        // TODO: This shit slow, make async?
        private void PixelGridChanged()
        {
            //string data = PS.ToString();

            //if (!String.IsNullOrWhiteSpace(data))
            //    CodeTextBox.Text = data;
            //else
            //    CodeTextBox.Text = "N/A";
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Fix buttons, broken after new pixel implementation
            if (sender == GenerateButton)
            {
                await ChildWindowManager.ShowChildWindowAsync(mainWindow, new ChildWindows.CodeWindow(PD.ToString()));
            }

            if (sender == InvertButton)
            {
                PD.InvertPixels();
                RefreshPixelContainer();
                PixelGridChanged();
            }

            if (sender == LoadButton)
            {
                var result = await ChildWindowManager.ShowChildWindowAsync<string>(mainWindow, new ChildWindows.ParseCodeWindow());
                if (result is null || String.IsNullOrWhiteSpace(result) || result == "EMPTY" || result == "CANCEL") return;

                if (!Utilities.StringIsbinary(result))
                {
                    MessageBox.Show("An error occurred, the binary code appears to be invalid. Verify the code and try again!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                } else {
                    PD.LoadFromBinaryString(result);
                    RefreshPixelContainer();
                    PixelGridChanged();
                }
            }

            if (sender == PreviewButton)
            {
                await ChildWindowManager.ShowChildWindowAsync(mainWindow, new ChildWindows.PreviewWindow(PixelArray, ActiveBrush, InactiveBrush));
            }

            if (sender == ResetButton)
            {
                PD.SetAllPixelStates(PixelState.Off);
                RefreshPixelContainer();
                PixelGridChanged();
            }
        }

        private void ScreenDropDown_Selected(object sender, RoutedEventArgs e)
        {
            // TODO: Support more screens? Would need examples.
        }

        private void PixelContainer_Initialized(object sender, EventArgs e)
        {
            // TODO: Define a place with screens that contain this data
            GenerateGrid(88, 58, 8, 1);
            ActiveBrush.Freeze();
            InactiveBrush.Freeze();
        }
    }
}
