using AvalonMouseMaze.Library;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Shared.Avalon.Extensions;
using System;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Xml;
using System.Xml.Linq;

namespace AvalonMouseMaze
{
    public class ApplicationCanvas : Canvas
    {
        public const int DefaultWidth = MouseMazeCanvas.DefaultWidth;
        public const int DefaultHeight = MouseMazeCanvas.DefaultHeight;

        public readonly Rectangle r = new Rectangle();

        public ApplicationCanvas()
        {
            r.Fill = Brushes.Red;
            r.AttachTo(this);
            r.MoveTo(8, 8);
            r.Opacity = 0.1;
            this.SizeChanged += (s, e) => r.SizeTo(this.Width - 16.0, this.Height - 16.0);

            new MouseMazeCanvas().AttachTo(this);
        }

    }
}
