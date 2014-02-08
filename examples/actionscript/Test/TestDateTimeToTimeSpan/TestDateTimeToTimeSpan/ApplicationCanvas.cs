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

namespace TestDateTimeToTimeSpan
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

            // X:\jsc.svn\examples\actionscript\FlashTowerDefense\FlashTowerDefenseApp\Program.cs
            var a = DateTime.Now;



            300.AtDelay(
                delegate
                {
                    var b = DateTime.Now;
                    var c = b - a;

                    //C#
                    //   { c = 00:00:00.3150005 }
                    // actionscript
                    // { c = 0.00:00:00 }

                    // 
                    // { TotalMilliseconds = 315 }
                    //new TextBox { Text = new { c.TotalMilliseconds }.ToString() }.AttachTo(this);
                    new TextBox { Text = new { c }.ToString() }.AttachTo(this);
                }
            );
        }

    }
}
