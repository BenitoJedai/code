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

namespace AvalonWindowslessWindowDrawer
{
    public class ApplicationCanvas : Canvas
    {
        public readonly AvalonWindowlessWindowDrawer.ApplicationCanvas r = new AvalonWindowlessWindowDrawer.ApplicationCanvas();

        public ApplicationCanvas()
        {
            //r.Fill = Brushes.Red;
            r.AttachTo(this);
            r.MoveTo(0, 0);
            this.SizeChanged += (s, e) => r.SizeTo(this.Width, this.Height);
        }

    }
}
