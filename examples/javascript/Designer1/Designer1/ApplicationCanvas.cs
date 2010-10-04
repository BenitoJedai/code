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
using Designer1.Components;

namespace Designer1
{
    public class ApplicationCanvas : Canvas
    {

        public ApplicationCanvas()
        {

            var n = new UserControl2();

            n.AttachTo(this);

            n.MoveTo(8, 8);

            this.SizeChanged += (s, e) => n.SizeTo(this.Width - 16.0, this.Height - 16.0);
        }

    }
}
