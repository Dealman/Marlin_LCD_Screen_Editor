using System;
using System.Diagnostics;
using System.IO;
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
    public class Project
    {
        [ProtoMember(1)]
        public string Name { get; set; }
        [ProtoMember(2)]
        public string Author { get; set; } = "N/A";
        [ProtoMember(3)]
        public DateTime LastEdited { get; set; }
        [ProtoMember(4)]
        public string Path { get; set; }
        [ProtoMember(5)]
        public int[] Data { get; set; }
        [ProtoMember(6)]
        public ScreenData ScreenData { get; set; }

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

            // TODO: UX Improvement - prompt user to overwrite?
            if (File.Exists(Path))
            {
                // TODO: UI Improvement - update status bar?
                using (var file = File.Create(Path))
                    Serializer.Serialize(file, this);
            }
            
        }
    }
}
