// For more information visit:
// http://studio.jsc-solutions.net/

using System;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;
using System.Xml.Linq;
using TextboxAutoSize.Avalon;
using ScriptCoreLib;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Shared.Avalon.Extensions;
using System.Windows.Shapes;
using System.Windows.Input;

namespace TextboxAutoSize.Avalon
{
    public class ApplicationCanvas : Canvas
    {
        public const int DefaultWidth = 640;
        public const int DefaultHeight = 480;

        public ApplicationCanvas()
        {
            Width = DefaultWidth;
            Height = DefaultHeight;

            this.ClipToBounds = true;

            new[] {
				Colors.Black,
				Colors.Blue,
				Colors.Black
			}.ToGradient(DefaultHeight / 2).Select(
                (c, i) =>
                    new Rectangle
                    {
                        Fill = new SolidColorBrush(c),
                        Width = DefaultWidth,
                        Height = 3,
                    }.MoveTo(0, i * 2).AttachTo(this)
            ).ToArray();


            {
                var bg = new Rectangle
                {
                    Fill = Brushes.Red
                }.AttachTo(this).MoveTo(32, 64).SizeTo(200, 20);

                var t = new TextBox
                {
                    Text = "AutoSize until resized... from then on explicitly sized",
                }.AttachTo(this).MoveTo(32, 64);
                //.SizeTo(200, 20);
            }

            {
                var bg = new Rectangle
                {
                    Fill = Brushes.Red
                }.AttachTo(this).MoveTo(32, 96).SizeTo(200, 100);

                var t = new TextBox().AttachTo(this).MoveTo(32, 96);
                t.AcceptsReturn = true;
                t.Background = Brushes.Black;
                t.Foreground = Brushes.Yellow;
                t.BorderThickness = new System.Windows.Thickness(0);

                t.Text = "AutoSize until resized\n1\n2\n3";
                //.SizeTo(200, 20);

                var logo = new Avalon.Images.white_jsc().AttachTo(this).MoveTo(
                        DefaultWidth - Avalon.Images.white_jsc.ImageDefaultWidth,
                        DefaultHeight - Avalon.Images.white_jsc.ImageDefaultHeight
                    );

                logo.Cursor = Cursors.Hand;

                logo.MouseLeftButtonUp +=
                    delegate
                    {
                        t.Text = "0. AutoSize until resized\n1\n2\n3";
                    };
            }

        }
    }
}
