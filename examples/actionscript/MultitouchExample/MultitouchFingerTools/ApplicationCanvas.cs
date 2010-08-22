// For more information visit:
// http://studio.jsc-solutions.net/

using System;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;
using System.Xml.Linq;
using MultitouchFingerTools;
using ScriptCoreLib;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Shared.Avalon.Extensions;
using System.Windows.Shapes;
using System.Windows;

namespace MultitouchFingerTools
{
    public class ApplicationCanvas : Canvas
    {
        public const int DefaultWidth = 800;
        public const int DefaultHeight = 400;

        public ApplicationCanvas()
        {
            Width = DefaultWidth;
            Height = DefaultHeight;


            this.Background = Brushes.Blue;

            var left = new PartialView(Colors.Blue).AttachTo(this);
            var right = new PartialView(Colors.Green).AttachTo(this).MoveTo(DefaultWidth / 2, 0);

            var InfoOverlay = new Canvas().AttachTo(this);

            var About = new TextBox
            {
                BorderThickness = new Thickness(0),
                Background = Brushes.Transparent,
                Foreground = Brushes.Yellow,
                AcceptsReturn = true,

                Text =

@"Windows Presentation Foundation Touch demo
- Adobe Flash 10.1 version via jsc - no fullscreen support
- Javascript version for Firefox4 via jsc
- tested with 4 touch points on Dell Latitude XT
"


            }.AttachTo(InfoOverlay);




            var TouchOverlay = new Canvas
            {



            }.AttachTo(this); //.SizeTo(DefaultWidth, DefaultHeight);

            var TouchArea = new Rectangle
            {
                Width = DefaultWidth,
                Height = DefaultHeight,
                Fill = Brushes.White,
                Opacity = 0
            }.AttachTo(TouchOverlay);


            var t = TouchOverlay.ToTouchEvents(
                () =>
                {
                    var n = new { Content = new Canvas() };


                    new Avalon.Images.white_jsc().AttachTo(n.Content).MoveTo(
                        Avalon.Images.white_jsc.ImageDefaultWidth / -2,
                        Avalon.Images.white_jsc.ImageDefaultHeight / -2
                    );



                    return n;
                }
            );

            t.TouchDown +=
                (k, e) =>
                {
                    k.Content.AttachTo(InfoOverlay);
                };

            t.TouchUp +=
                (k, e) =>
                {
                    k.Content.Orphanize();
                };


            t.TouchMove +=
                (k, e) =>
                {
                    k.Content.MoveTo(e, TouchOverlay);
                };

            this.SizeChanged +=
                (s, e) =>
                {
                    right.MoveTo(
                        Width - (DefaultWidth / 2), Height - DefaultHeight);

                    TouchArea.SizeTo(Width, Height);
                };
        }

        public void SizeContentTo(double x, double y)
        {
            Width = x;
            Height = y;
        }

        public class PartialView : Canvas
        {
            public PartialView(Color color)
            {
                new[] {
				Colors.Black,
				color,
				Colors.Black
			}.ToGradient(DefaultHeight / 2).Select(
                (c, i) =>
                    new Rectangle
                    {
                        Fill = new SolidColorBrush(c),
                        Width = DefaultWidth / 2,
                        Height = 3,
                    }.MoveTo(0, i * 2).AttachTo(this)
            ).ToArray();

                new Avalon.Images.white_jsc().AttachTo(this).MoveTo(
                    DefaultWidth / 2 - Avalon.Images.white_jsc.ImageDefaultWidth,
                    DefaultHeight - Avalon.Images.white_jsc.ImageDefaultHeight
                );

            }
        }
    }


}
