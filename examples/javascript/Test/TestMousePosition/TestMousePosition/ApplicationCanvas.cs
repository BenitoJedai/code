using ScriptCoreLib.Extensions;
using ScriptCoreLib.Shared.Avalon.Extensions;
using System;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Xml;
using System.Xml.Linq;

namespace TestMousePosition
{
    public class ApplicationCanvas : Canvas
    {
        public readonly Rectangle r = new Rectangle();

        public ApplicationCanvas()
        {
            r.Fill = Brushes.Gray;
            r.AttachTo(this);
            r.MoveTo(8, 8);
            this.SizeChanged += (s, e) => r.SizeTo(this.Width - 16.0, this.Height - 16.0);

            var t = new TextBox().AttachTo(this);

            var gx = 120;
            var gy = 120;
            var gw = 550;
            var gh = 150;

            
            var Black = new Rectangle
            {
                Width = 8,
                Height = 8,
                Fill = Brushes.Black,
                Cursor = Cursors.Hand,
                RenderTransform = new TranslateTransform(-4, -4)
            }.AttachTo(this).MoveTo(gx + gw / 2, gy + gh / 2);

            this.Name = "ApplicationCanvas";
            Black.Name = "ApplicationCanvas.Black";

            this.MouseMove +=
                (s, e) =>
                {

                    var p = e.GetPosition(Black);

                    t.Text = new { p.X, p.Y }.ToString(); 
                };
        }

    }
}
