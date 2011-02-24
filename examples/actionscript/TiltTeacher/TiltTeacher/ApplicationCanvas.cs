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

namespace TiltTeacher
{
    public class ApplicationCanvas : Canvas
    {

        public ApplicationCanvas()
        {
            {
                var r = new Rectangle();
                r.Fill = Brushes.Black;
                r.AttachTo(this);
                r.MoveTo(0, 0);
                r.Opacity = 0.9;
                this.SizeChanged += (s, e) => r.SizeTo(this.Width, this.Height / 2);
            }

            {
                var r = new Rectangle();
                r.Fill = Brushes.Black;
                r.AttachTo(this);

                this.SizeChanged += (s, e) => r.MoveTo(0, this.Height / 2).SizeTo(this.Width, this.Height / 2);
            }

            var az = 0.5;

            var A = new TextBox
            {
                BorderThickness = new Thickness(0),
                Background = Brushes.Transparent,
                TextAlignment = System.Windows.TextAlignment.Center,
                Foreground = Brushes.White,
                Text = "suur ettevõte",
                IsReadOnly = true,
                FontSize = 60
            };

            A.AttachTo(this);

            var B = new TextBox
            {
                BorderThickness = new Thickness(0),
                Background = Brushes.Transparent,
                TextAlignment = System.Windows.TextAlignment.Center,
                Foreground = Brushes.White,
                Text = "large-scale enterprise",
                IsReadOnly = true,
                FontSize = 60
            };

            B.AttachTo(this);
            B.MoveTo(0, 64);

            Action Update =
                delegate
                {
                    az = Math.Min(1, az);
                    az = Math.Max(0, az);

                    var max = this.Height / 6;
                    var min = this.Height / 3;

                    // az = 1 is 0 degrees
                    // az = 0 is 90 degrees

                    az = 1 - az;

                    az -= 0.05;
                    az *= 10;

                    az = 1 - az;

                    az = Math.Min(1, az);
                    az = Math.Max(0, az);

                    Console.WriteLine(new { az });

                    A.Opacity = Math.Pow(az, 10);
                    var bz = 1 - az;

                    B.Opacity = Math.Pow(bz, 10);

                    A.MoveTo(0, min + az * max - 16).SizeTo(this.Width, 80);
                    B.MoveTo(0, min + (1 - az) * max - 16).SizeTo(this.Width, 80);
                };

            this.SizeChanged +=
                delegate
                {
                    Update();
                };

            this.MouseMove += (sender, args) =>
            {
                var p = args.GetPosition(this);

                az = p.Y / this.Height;

                Update();
            };

            this.Accelerate = (x, y, z) =>
            {
                az = z;
                Update();
            };
        }

        public readonly Action<double, double, double> Accelerate;

    }
}
