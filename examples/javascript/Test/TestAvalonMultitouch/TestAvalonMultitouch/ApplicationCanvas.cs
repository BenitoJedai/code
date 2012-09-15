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

namespace TestAvalonMultitouch
{
    public class ApplicationCanvas : Canvas
    {
        public readonly Rectangle r = new Rectangle();

        public ApplicationCanvas()
        {
            var a = Enumerable.Range(0, 64).Select(
                i =>
                {
                    var rr = new Rectangle();
                    rr.Fill = Brushes.Blue;
                    rr.AttachTo(this);
                    rr.SizeTo(32, 32);
                    return rr;
                }
            ).ToArray();

            r.Opacity = 0.3;
            r.Fill = Brushes.Red;
            r.AttachTo(this);
            r.MoveTo(8, 8);
            this.SizeChanged += (s, e) => r.SizeTo(this.Width - 16.0, this.Height - 16.0);


            this.TouchDown +=
                (s, e) =>
                {
                    e.Handled = true;

                    Console.WriteLine("TouchDown");
                };

            this.TouchMove +=
                (s, e) =>
                {
                    var p = e.GetTouchPoint(this).Position;

                    a[e.TouchDevice.Id].MoveTo(p);

                    Console.WriteLine("TouchMove " + e.TouchDevice.Id + " " + p);
                };

            this.TouchUp +=
             (s, e) =>
             {
                 Console.WriteLine("TouchUp");
             };

        }

    }
}
