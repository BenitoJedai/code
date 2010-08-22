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
using System.Collections.Generic;
using MultitouchFingerTools.Avalon.Images;
using ScriptCoreLib.Avalon;

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

            var left = new PartialView(Colors.Blue, true).AttachTo(this);
            var right = new PartialView(Colors.Green).AttachTo(this).MoveTo(DefaultWidth / 2, 0);

            var InfoOverlay = new Canvas().AttachTo(this);

            var About = new TextBox
            {
                BorderThickness = new Thickness(0),
                Background = Brushes.Transparent,
                Foreground = Brushes.Black,
                AcceptsReturn = true,

                Text =

@"Windows Presentation Foundation Touch demo
- Debuggable under .NET (fullscreen when maximized and touch events)
- Adobe Flash 10.1 version via jsc
     No touch events in fullscreen
     Browser fullscreen feature shall be used instead
     Tested for IE, Firefox, Chrome

- Javascript version for Firefox4 via jsc
- Tested with 4 touch points on Dell Latitude XT
"


            }.AttachTo(InfoOverlay);

            var c1 = new cloud_mid().AttachTo(InfoOverlay);
            var c2 = new cloud_mid().AttachTo(InfoOverlay);


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
                    var n = new { Content = new Canvas().AttachTo(InfoOverlay) };


                    new Avalon.Images.white_jsc
                    {
                    }.AttachTo(n.Content).MoveTo(
                       Avalon.Images.white_jsc.ImageDefaultWidth / -2,
                       Avalon.Images.white_jsc.ImageDefaultHeight / -2
                   );

                    n.Content.FadeOut();

                    n.Content.MoveTo(e, TouchOverlay);
                    k.Content.MoveTo(e, TouchOverlay);

                };

            this.SizeChanged +=
                (s, e) =>
                {
                    c1.MoveTo(
                        (Width - c1.Width) / 2 , 0);

                    c2.MoveTo(
                        (Width - c1.Width) / 2, Height / 2);

                    left.SizeTo(
                        
                        Width / 2,
                        Height
                    );


                    right.MoveTo(
                        Width / 2, 0).SizeTo(
                        
                        Width / 2,
                        Height
                    );

                    TouchArea.SizeTo(Width, Height);
                };
        }



        public class PartialView : Canvas
        {

            public PartialView(Color color, bool rotate = false)
            {
            //    new[] {
            //    Colors.Black,
            //    color,
            //    Colors.Black
            //}.ToGradient(DefaultHeight / 2).Select(
            //    (c, i) =>
            //        new Rectangle
            //        {
            //            Fill = new SolidColorBrush(c),
            //            Width = DefaultWidth / 2,
            //            Height = 3,
            //        }.MoveTo(0, i * 2).AttachTo(this)
            //).ToArray();

                var bg1 = new bg1().AttachTo(this);



                this.SizeChanged +=
                    delegate
                    {
                        //var Height = Width;

                        var a = new AffineTransform
                        {
                            Width = 720,
                            Height = 720,



                            X2 = Width,
                            Y2 = Height,

                            X3 = 000,
                            Y3 = Height

                        };

                        if (rotate)
                        {
                            a.X1 = Width;
                            a.X3 = Width;
                            a.X2 = 0;
                        }

                        bg1.RenderTransform = a;
                    };

                //this.ClipToBounds = true;
                //this.ClipTo(0, 0, DefaultWidth / 2, DefaultHeight);
            }
        }
    }


}
