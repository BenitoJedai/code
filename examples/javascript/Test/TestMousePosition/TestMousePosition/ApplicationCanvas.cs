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

            var Shadow = new Canvas
            {
                Width = 200,
                Height = 200,
                Background = Brushes.White
            }.MoveTo(16, 16).AttachTo(this);

            var t = new TextBox().AttachTo(Shadow).MoveTo(4, 4);

            var gx = 120;
            var gy = 120;
            var gw = 550;
            var gh = 150;



            var Red = new Rectangle
            {
                Width = 8,
                Height = 8,
                Fill = Brushes.Red,
                Cursor = Cursors.Hand,
                RenderTransform = new TranslateTransform(-4, -4)
            }.AttachTo(this).MoveTo(gx + gw / 2, gy + gh / 2);

     
            var Black = new Rectangle
            {
                Width = 8,
                Height = 8,
                Fill = Brushes.Black,
                Cursor = Cursors.Hand,
                RenderTransform = new TranslateTransform(-4, -4)
            }.AttachTo(this).MoveTo(gx + gw / 2, gy + gh / 2);



            this.MouseMove +=
                (s, e) =>
                {

                    var p = e.GetPosition(Black);

                    Red.MoveTo(p.X + 4, p.Y + 4);

                    var RedLeft = Canvas.GetLeft(Red);
                    var RedTop = Canvas.GetTop(Red);

                    t.Text = new { p.X, p.Y, RedLeft, RedTop }.ToString();

                };
        }

    }
}
