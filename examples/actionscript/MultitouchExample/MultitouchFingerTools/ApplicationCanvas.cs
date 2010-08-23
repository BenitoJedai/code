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
using System.Windows.Input;

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
                    // a new reusable finger introduced by the system!
                    var Content = new Canvas();

                    new Avalon.Images.white_jsc().AttachTo(Content).MoveTo(
                        Avalon.Images.white_jsc.ImageDefaultWidth / -2,
                        Avalon.Images.white_jsc.ImageDefaultHeight / -2
                    );

                    var CurrentTouchPoint = default(Tuple<double, double>);

                    Func<Tuple<double, double>> GetTouchPoint = () => CurrentTouchPoint;

                    Action<TouchEventArgs> TouchDown = e =>
                    {
                        var p = e.GetTouchPoint(TouchOverlay).Position;

                        CurrentTouchPoint = Tuple.Create(p.X, p.Y);
                        Content.AttachTo(InfoOverlay);
                    };

                    Action<TouchEventArgs> TouchUp = e =>
                    {
                        CurrentTouchPoint = null;
                        Content.Orphanize();
                    };

                    Action<TouchEventArgs> TouchMove = e =>
                    {
                        var p = e.GetTouchPoint(TouchOverlay).Position;

                        CurrentTouchPoint = Tuple.Create(p.X, p.Y);

                       

                        Content.MoveTo(e, TouchOverlay);
                    };

                    return new
                    {
                        Content,
                        TouchDown,
                        TouchUp,
                        TouchMove,
                        GetTouchPoint
                    };
                }
            );

            (1000 / 15).AtInterval(
                delegate
                {
                    var touches = from k in t.Touches
                                  let p = k.GetTouchPoint()
                                  where p != null
                                  let x = p.Item1
                                  let y = p.Item2



                                  select new { x, y };

                    #region visualize all touches
                    foreach (var item in touches)
                    {
                        var n = new { Content = new Canvas().AttachTo(InfoOverlay) };

                        //Tuple

                        new Avalon.Images.white_jsc
                        {

                        }.AttachTo(n.Content).MoveTo(
                           Avalon.Images.white_jsc.ImageDefaultWidth / -4,
                           Avalon.Images.white_jsc.ImageDefaultHeight / -4
                       ).SizeTo(
                           Avalon.Images.white_jsc.ImageDefaultWidth / 2,
                           Avalon.Images.white_jsc.ImageDefaultHeight / 2
                       );

                        n.Content.FadeOut();

                        n.Content.MoveTo(item.x, item.y);
                    }
                    #endregion




                    var left_touch = from k in touches
                                     let x = k.x
                                     let y = k.y
                                     where x < 64
                                     where y > Height - 64
                                     select k;

                    var left_buildmode = left_touch.Any();

                    if (left_buildmode)
                    {
                        // sound: build mode engaged!

                        // all other touches in range are now build orders!

                        left.buildmode_off.Hide();
                        left.buildmode_on.Show();
                    }
                    else
                    {
                        left.buildmode_on.Hide();
                        left.buildmode_off.Show();
                    }
                }
            );

            t.TouchDown += (k, e) => { k.TouchDown(e); };
            t.TouchUp += (k, e) => { k.TouchUp(e);  };
            t.TouchMove += (k, e) => { k.TouchMove(e); };


            this.SizeChanged +=
                (s, e) =>
                {
                    c1.MoveTo(
                        (Width - c1.Width) / 2, 0);

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
            public Image buildmode_off;
            public Image buildmode_on;
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

                var buildmode = new Canvas().AttachTo(this);

                buildmode_off = new buildmode().AttachTo(buildmode);
                buildmode_on = new buildmode_on().AttachTo(buildmode);

                buildmode_on.Hide();

                this.SizeChanged +=
                    delegate
                    {
                        //var Height = Width;

                        {
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
                        }
                        {
                            var a = new AffineTransform
                            {
                                Width = 128,
                                Height = 128,


                                X1 = 0,
                                Y1 = Height - 64,

                                X2 = 64,
                                Y2 = Height,

                                X3 = 0,
                                Y3 = Height

                            };

                            if (!rotate)
                            {
                                a.X1 += Width - (64 + 32);
                                a.X3 += Width - (64 + 32);
                                a.X2 += Width - (64 + 32);
                            }
                            else
                            {
                                a.X1 = 64;
                                a.Y1 = Height;

                                a.X2 = 0;
                                a.Y2 = Height - 64;

                                a.X3 = 64;
                                a.Y3 = Height - 64;

                                a.X1 += 32;
                                a.X3 += 32;
                                a.X2 += 32;
                            }

                            buildmode_off.RenderTransform = a;
                            buildmode_on.RenderTransform = a;
                        }
                    };

                this.ClipToBounds = true;
                //this.ClipTo(0, 0, DefaultWidth / 2, DefaultHeight);
            }
        }
    }


}
