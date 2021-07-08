using System;

namespace Marlin_LCD_Screen_Editor
{
    public class Project
    {
        public string Name { get; set; }
        public DateTime LastEdited { get; set; }
        public string Path { get; set; }
        public Byte[] Data { get; set; }
    }
}
