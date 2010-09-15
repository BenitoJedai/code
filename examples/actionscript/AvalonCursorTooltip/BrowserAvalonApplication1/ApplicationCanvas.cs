using System;
using System.Text;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Windows.Media;
using System.Windows.Shapes;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Shared.Avalon.Extensions;
using System.Windows.Controls;
using System.Windows;

namespace BrowserAvalonApplication1
{
    public class ApplicationCanvas : global::BrowserAvalonApplicationWithAdobeFlash2.ApplicationCanvas
    {
        public readonly Rectangle r = new Rectangle();

        public ApplicationCanvas()
        {
            r.Fill = Brushes.Red;
            r.Opacity = 0.1;
            //r.AttachTo(this);
            r.MoveTo(8, 8);
            this.SizeChanged += (s, e) => r.SizeTo(this.Width - 16, this.Height - 16);
        }

    }
}
