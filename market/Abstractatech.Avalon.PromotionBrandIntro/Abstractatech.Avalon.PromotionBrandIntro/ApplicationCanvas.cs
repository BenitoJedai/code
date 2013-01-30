// For more information visit:
// http://studio.jsc-solutions.net/

using System;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;
using System.Xml.Linq;
using ScriptCoreLib;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Shared.Avalon.Extensions;
using System.Windows.Shapes;
using System.Collections.Generic;

namespace AvalonPromotionBrandIntro
{
    public class ApplicationCanvas : Canvas
    {
        public const int DefaultWidth = 640;
        public const int DefaultHeight = 480;

        public readonly Rectangle Overlay;

        public ApplicationCanvas()
        {
            //return;

            Background = Brushes.Black;

            var OverlayWhite = new Rectangle
            {
                Fill = Brushes.White
            }.AttachTo(this);

            this.Overlay = new Rectangle
            {
                Fill = Brushes.Black
            }.AttachTo(this);

            var c = new Canvas().AttachTo(this);

            this.SizeChanged +=
                (s, e) =>
                {
                    //Console.WriteLine(new { Width, Height, this.ActualWidth, this.ActualHeight });
                    OverlayWhite.SizeTo(
                        this.Width,
                        this.Height
                    );
                    Overlay.SizeTo(
                        this.Width,
                        this.Height
                    );

                    c.MoveTo(
                        this.Width * 0.5,
                        this.Height * 0.5
                    );
                };

            Func<double, double, Image> f =
                (o, x) =>
                {
                    return new Avalon.Images.white_jsc
                    {
                        Opacity = o
                    }.AttachTo(c).MoveTo(
                        Avalon.Images.white_jsc.ImageDefaultWidth / -2 + x,
                        Avalon.Images.white_jsc.ImageDefaultHeight / -2
                    );
                };

            var a = new List<Action<bool>>();

            var ss = 640;
            var ia = 1;


            for (int i = -ss; i <= 0; i += ia)
            {
                ia += 2;

                {
                    var o = (ss + i + 64) / (double)(ss + 64);
                    var l = f(o, -i);
                    var r = f(o, i);

                    Action<bool> j =
                        n =>
                        {
                            l.Show(n);
                            r.Show(n);
                        };
                    j(false);
                    a.Add(j);
                }
            }



            {
                var l = f(1, 0);

                l.Hide();
                Action<bool> j =
                    n =>
                    {
                        if (n)
                        {
                            Overlay.Fill = Brushes.White;
                            l.Show();
                            return;
                        }

                        15.AtDelay(
                            delegate
                            {
                                Overlay.Fill = Brushes.Black;
                            }
                        );

                        Action<int, int, int> ShakeAt = null;

                        ShakeAt =
                            (delay, x, y) =>
                            {
                                delay.AtDelay(
                                    delegate
                                    {
                                        l.MoveTo(
                                            Avalon.Images.white_jsc.ImageDefaultWidth / -2 + x,
                                            Avalon.Images.white_jsc.ImageDefaultHeight / -2 + y
                                        );
                                    }
                                );
                            };

                        if (AnimationShake != null)
                            AnimationShake();


                        ShakeAt(30 * 2, -2, -4);
                        ShakeAt(30 * 3, 2, 3);
                        ShakeAt(30 * 4, -1, -2);
                        ShakeAt(30 * 5, 0, 3);
                        ShakeAt(30 * 6, 0, 4);

                        1000.AtDelay(
                            delegate
                            {
                                if (AnimationAllBlack != null)
                                    AnimationAllBlack();

                                1000.AtDelay(
                                    delegate
                                    {
                                        Overlay.FadeOut(
                                            delegate
                                            {
                                                l.Hide();
                                                OverlayWhite.FadeOut(
                                                    delegate
                                                    {
                                                        if (AnimationCompleted != null)
                                                            AnimationCompleted();
                                                    }
                                                );

                                                if (AnimationAllWhite != null)
                                                    AnimationAllWhite();
                                            }
                                        );
                                    }
                                );
                            }
                        );
                    };
                a.Add(j);
            }

            PrepareAnimation =
                delegate
                {
                    var aa = new Queue<Action<bool>>(a);

                    Action Next = delegate { };


                    Trigger = delegate
                    {
                        if (aa.Count == 0)
                            return;

                        Overlay.Opacity = 1;
                        OverlayWhite.Opacity = 1;

                        Action AnimationLoop = delegate
                        {
                            Next = delegate
                            {
                                var dd = aa.Dequeue();

                                dd(true);


                                (1000 / 24).AtDelay(
                                    () =>
                                    {
                                        dd(false);
                                        if (aa.Count > 0)
                                        {
                                            Next();
                                        }

                                    }
                                );
                            };

                            Next();
                        };

                        Next = delegate
                        {
                            AnimationStartDelay.AtDelay(
                               AnimationLoop
                           );


                        };

                        Next();
                    };

                    this.MouseLeftButtonUp +=
                        delegate
                        {
                            if (TriggerOnClick)
                                Trigger();
                        };

                    return Trigger;
                };


            var once = false;
            this.MouseLeftButtonUp +=
                 delegate
                 {
                     if (once)
                         return;
                     once = true;
                     PrepareAnimation();
                     Trigger();
                 };



            AnimationCompleted += () => PrepareAnimation();
        }
        public bool TriggerOnClick = true;
        public Action Trigger = null;

        public int AnimationStartDelay = 3500;

        public readonly Func<Action> PrepareAnimation;

        public event Action AnimationCompleted;
        public event Action AnimationAllBlack;
        public event Action AnimationAllWhite;
        public event Action AnimationShake;
    }
}
