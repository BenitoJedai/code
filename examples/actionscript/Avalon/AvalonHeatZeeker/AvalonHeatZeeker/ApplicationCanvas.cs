using AvalonHeatZeeker.Avalon.Images;
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

namespace AvalonHeatZeeker
{
    public class ApplicationCanvas : Canvas
    {
        public readonly Rectangle r = new Rectangle();

        public TextBlock label;

        public ApplicationCanvas()
        {
            // #B27D51

            r.Fill = 0xffB27D51.ToSolidColorBrush();
            r.AttachTo(this);
            r.MoveTo(8, 8 + 24);
            this.SizeChanged += (s, e) => r.SizeTo(this.Width - 16.0, this.Height - 16.0);


            label = new TextBlock { Text = "fps = ?", Opacity = 0.2 }.AttachTo(this);
            label.MoveTo(8, 8);


            label.MouseEnter +=
                delegate
                {
                    //label.Foreground = Brushes.Blue;

                    label.Opacity = 1.0;
                };

            label.MouseLeave +=
                delegate
                {
                    label.Opacity = 0.2;

                    //label.Foreground = Brushes.Black;
                };

            Action<double, double> CreateUnit =
                (x, y) =>
                {
                    var scale = 0.8;

                    var unit = new Canvas().AttachTo(this);


                    unit.MoveTo(x, y);
                    unit.RenderTransform = new ScaleTransform { ScaleX = scale, ScaleY = scale };

                    var nowings = new hind0_nowings();
                    nowings.AttachTo(unit);

                    var wings_translate = new Canvas().AttachTo(unit);
                    var wings_rot = new Canvas().AttachTo(wings_translate);

                    wings_translate.MoveTo(200, 200);

                    var wings = new hind0_wings();

                    wings.MoveTo(-200, -200);
                    wings.AttachTo(wings_rot);

                    var unitlabel = new TextBlock { Text = "c = ?", Opacity = 1 }.AttachTo(unit);


                    (1000 / 30).AtIntervalWithCounter(
                        rot =>
                        {
                            unitlabel.Text = new { rot }.ToString();

                            wings_rot.RenderTransform = new RotateTransform(rot * 12.0);
                        }
                    );
                };

            this.MouseLeftButtonUp +=
                (sender, e) =>
                {
                    var p = e.GetPosition(this);

                    CreateUnit(p.X, p.Y);
                };

            CreateUnit(200, 200);


        }

    }
}
