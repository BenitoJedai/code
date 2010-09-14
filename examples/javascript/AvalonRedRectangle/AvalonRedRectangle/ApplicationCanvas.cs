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
    public class ApplicationCanvas : Canvas
    {
        public readonly Rectangle r = new Rectangle();

        public ApplicationCanvas()
        {
            Action<Rectangle, double, SolidColorBrush> f =
                (r, margin, fill) =>
                {
                    r.Fill = fill;
                    r.AttachTo(this);
                    r.MoveTo(margin, margin);
                    this.SizeChanged += (s, e) => r.SizeTo(this.Width - margin * 2, this.Height - margin * 2);
                };

            f(new Rectangle(), 8, Brushes.Red);
            f(new Rectangle(), 16, Brushes.White);


            var a = new TextBox
            {
                AcceptsReturn = true,
                BorderThickness = new Thickness(0),
                Background = Brushes.Yellow,
                Foreground = Brushes.Transparent,
                Text = "hello\nworld\n..."
            }.AttachTo(this).MoveTo(24, 24);

            var c = new TextBox
            {
                AcceptsReturn = true,
                BorderThickness = new Thickness(0),
                Background = Brushes.Transparent,
                Foreground = Brushes.Blue,
                Text = "hello\nworld\n...\nmore data"
            }.AttachTo(this).MoveTo(24 + 0, 24);

            var b = new TextBox
            {
                AcceptsReturn = true,
                BorderThickness = new Thickness(0),
                Background = Brushes.Transparent,
                Foreground = Brushes.Black,
                Text = "hello\nworld\n...\nmore data",
                Opacity = 0
            }.AttachTo(this).MoveTo(24, 24);

 

            Action Update =
                delegate
                {
                    c.Text = b.Text;
                    a.Text = b.Text.TakeUntilLastOrEmpty(Environment.NewLine);
                };

            b.TextChanged += delegate
            {
                Update();
            };
        }

    }
}
