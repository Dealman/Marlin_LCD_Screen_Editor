using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Media;
using ProtoBuf;

namespace Marlin_LCD_Screen_Editor
{
    [ProtoContract]
    public struct ScreenData
    {
        [ProtoMember(1)]
        public int Width { get; set; }
        [ProtoMember(2)]
        public int Height { get; set; }
        [ProtoMember(3)]
        public int Columns { get; set; }
        [ProtoMember(4)]
        public int Rows { get; set; }
        [ProtoMember(5)]
        public string ActiveBrush { get; set; }
        [ProtoMember(6)]
        public string InactiveBrush { get; set; }

        public ScreenData(int width, int height, int cols, int rows, Brush activeBrush, Brush inactiveBrush)
        {
            Width = width;
            Height = height;
            Columns = cols;
            Rows = rows;
            ActiveBrush = new BrushConverter().ConvertToString(activeBrush);
            InactiveBrush = new BrushConverter().ConvertToString(inactiveBrush);
        }

        public bool IsValid()
        {
            if (Width > 0 && Height > 0 && Columns > 0 && Rows > 0)
                return true;

            return false;
        }
    }

    [ProtoContract]
    public class Project : IChangeTracking
    {
        string name;
        string author;
        string path;
        int[] data;
        ScreenData screenData;

        [ProtoMember(1)]
        public string Name
        { 
            get => name;
            set
            {
                if (name != value)
                {
                    name = value;
                    IsChanged = true;
                    PropertyChanged();
                }
            }
        }
        [ProtoMember(2)]
        public string Author
        {
            get => author;
            set
            {
                if (author != value)
                {
                    author = value;
                    IsChanged = true;
                    PropertyChanged();
                }
            }
        }
        [ProtoMember(3)]
        public DateTime LastEdited { get; set; }
        [ProtoMember(4)]
        public string Path
        {
            get => path;
            set
            {
                if (path != value)
                {
                    path = value;
                    IsChanged = true;
                    PropertyChanged();
                }
            }
        }
        [ProtoMember(5)]
        public int[] Data
        {
            get => data;
            set
            {
                if (data != value)
                {
                    data = value;
                    IsChanged = true;
                    PropertyChanged();
                }
            }
        }
        [ProtoMember(6)]
        public ScreenData ScreenData
        {
            get => screenData;
            set
            {
                if (!screenData.Equals(value))
                {
                    screenData = value;
                    IsChanged = true;
                    PropertyChanged();
                }
            }
        }

        MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
        bool initialized = false;
        public bool IsChanged { get; private set; }

        public void PropertyChanged()
        {
            if (initialized)
            {
                mainWindow.SetStatusBarText($"Project: {Name} | You have unsaved changes pending!");
                mainWindow.SetSaveButtonVisibility(true);
            }
        }

        public void AcceptChanges()
        {
            if (!initialized)
                initialized = true;

            mainWindow.SetStatusBarText($"Project: {Name}");

            mainWindow.SetSaveButtonVisibility(false);
            IsChanged = false;
        }
        
        public void AcceptChanges(bool updateStatus)
        {
            if (!initialized)
                initialized = true;

            if (updateStatus)
                mainWindow.SetStatusBarText($"Project: {Name}");

            mainWindow.SetSaveButtonVisibility(false);
            IsChanged = false;
        }

        public Project()
        {

        }

        public Project(string name)
        {
            if (String.IsNullOrWhiteSpace(name)) return;

            Name = name;
        }

        public bool IsValid()
        {
            if (!String.IsNullOrWhiteSpace(Name) && !String.IsNullOrWhiteSpace(Path) && ScreenData.IsValid())
                return true;

            return false;
        }

        public void Save()
        {
            if (!IsValid()) return;

            LastEdited = DateTime.Now;

            using (var file = File.Create(Path))
                Serializer.Serialize(file, this);

            AcceptChanges();
        }
    }
}
