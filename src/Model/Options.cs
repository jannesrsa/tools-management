using System;
using System.Drawing;
using System.Windows.Forms;

namespace Tools.Management.Model
{
    [Serializable]
    public class Options
    {
        public Point Location { get; set; }
        public Size Size { get; set; }
        public FormWindowState WindowState { get; set; }
    }
}