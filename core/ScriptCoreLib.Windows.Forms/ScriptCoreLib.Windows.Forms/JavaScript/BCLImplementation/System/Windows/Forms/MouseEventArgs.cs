using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms
{
    [Script(Implements = typeof(global::System.Windows.Forms.MouseEventArgs))]
    internal class __MouseEventArgs : ScriptCoreLib.JavaScript.BCLImplementation.System.__EventArgs
    {
        public __MouseEventArgs(MouseButtons button, int clicks, int x, int y, int delta)
        {
            this.Button = button;
            this.Clicks = clicks;
            this.Delta = delta;
            this.X = x;
            this.Y = y;
        }

        public MouseButtons Button { get; internal set; }
        public int Clicks { get; internal set; }
        public int Delta { get; internal set; }
        public Point Location { get { return new Point(X, Y); } }
        public int X { get; internal set; }
        public int Y { get; internal set; }
    }
}
