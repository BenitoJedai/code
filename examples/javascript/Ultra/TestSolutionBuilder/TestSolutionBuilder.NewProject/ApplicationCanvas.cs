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
using System.Windows.Media.Effects;

namespace TestSolutionBuilder.NewProject
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

            var btn = new Button { Content = "Hello" }.MoveTo(16, 16).SizeTo(96, 32);

            btn.Click +=
                delegate
                {

                    var w = new Window();

                    //w = new ApplicationCanvas().ToW
                    w.Content = new ApplicationCanvas();

                    w.ShowInTaskbar = false;

                    var p = this.Parent as FrameworkElement;

                    while (!(p is Window))
                    {
                        p = p.Parent as FrameworkElement;
                    }

                    var Owner = p as Window;

                    w.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                    w.Owner = Owner;

                    w.ShowDialog();

                };

            btn.AttachTo(this);
        }

    }
}
