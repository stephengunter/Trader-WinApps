using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace OrderMaker.Helpers
{
    public class UIHelpers
    {
        public static Label CreateLabel(string text)
            => new Label() { TextAlign = ContentAlignment.MiddleLeft, Text = text };

        public static Label CreateLabel(string text, Color color, DockStyle dock, ContentAlignment textAlign = ContentAlignment.MiddleLeft)
            => new Label() { Text = text, ForeColor = color, Dock = dock, TextAlign = textAlign };

        
    }
}
