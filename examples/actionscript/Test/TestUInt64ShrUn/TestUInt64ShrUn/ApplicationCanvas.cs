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

namespace TestUInt64ShrUn
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

            //b = 1095216660480;
            //c = (b >>> 4);

            //IL_0000:  nop
            //IL_0001:  ldc.i8     0xff00000000
            //IL_000a:  stloc.0
            //IL_000b:  ldloc.0
            //IL_000c:  ldc.i4.4
            //IL_000d:  shr.un
            //IL_000e:  stloc.1

            ulong x = 0xff00000000;

            //   c = (b / Math.pow(2, 4));
            // { y = 0 }
            var y = x >> 4;
            //var y = x / Math.Pow(2, 4);
            // { y = 68451041280 }

            // { y = 68451041280 }
            // X:\jsc.svn\examples\actionscript\Test\TestULongToByteCast\TestULongToByteCast\ApplicationCanvas.cs
            new TextBox { Text = new { y }.ToString() }.AttachTo(this);

        }

    }
}
