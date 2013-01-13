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

namespace TestAvalonBrowserLogosFromMarket
{
    public class ApplicationCanvas : Canvas
    {
        public readonly AvalonBrowserLogos.ApplicationCanvas r = new AvalonBrowserLogos.ApplicationCanvas();

        public ApplicationCanvas()
        {
            r.AttachTo(this);
            r.MoveTo(8, 8);
            this.SizeChanged += (s, e) => r.SizeTo(this.Width - 16.0, this.Height - 16.0);
        }

    }
}
