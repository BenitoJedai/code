using ScriptCoreLib.Extensions;
using ScriptCoreLib.Shared.Avalon.Extensions;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Xml;
using System.Xml.Linq;

namespace TestMemoryStream
{
    public class ApplicationCanvas : Canvas
    {
        public readonly Rectangle r = new Rectangle();

        public ApplicationCanvas()
        {
            r.Fill = Brushes.Red;
            r.AttachTo(this);
            r.MoveTo(8, 8);
            this.SizeChanged += (s, e) => r.SizeTo(this.Width - 16.0, this.Height - 16.0);

            new TextBox().AttachTo(this).With(
                t =>
                {
                    var m = new MemoryStream();

                    m.WriteByte((byte)'H');
                    m.WriteByte((byte)'E');
                    m.WriteByte((byte)'L');
                    m.WriteByte((byte)'L');
                    m.WriteByte((byte)'O');

                    var a = m.ToArray();

                    //Native.API.var_dump(a);

                    var w = new StringBuilder();

                    foreach (var item in a)
                    {
                        w.Append(item.ToString("x2"));
                    }

                    w.Append(", " + Convert.ToBase64String(a));

                    // {48454c4c4f, SEVMTE8=}
                    var e = w.ToString();

                    t.Text = e;
                }
            );
        }

    }
}
