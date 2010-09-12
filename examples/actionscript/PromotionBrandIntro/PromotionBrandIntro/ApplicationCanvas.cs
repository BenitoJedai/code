// For more information visit:
// http://studio.jsc-solutions.net/

using System;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;
using System.Xml.Linq;
using PromotionBrandIntro;
using ScriptCoreLib;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Shared.Avalon.Extensions;
using System.Windows.Shapes;
using System.Collections.Generic;
using System.Speech.Synthesis;

namespace PromotionBrandIntro
{
    public class ApplicationCanvas : Canvas
    {
        public const int DefaultWidth = 640;
        public const int DefaultHeight = 480;

        public readonly Rectangle Overlay;

        public ApplicationCanvas()
        {
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
                    OverlayWhite.SizeTo(
                        this.ActualWidth,
                        this.ActualHeight
                    );
                    Overlay.SizeTo(
                        this.ActualWidth,
                        this.ActualHeight
                    );

                    c.MoveTo(
                        this.ActualWidth * 0.5,
                        this.ActualHeight * 0.5
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

                        


                        ShakeAt(30 * 2, -2, -4);
                        ShakeAt(30 * 3, 2, 3);
                        ShakeAt(30 * 4, -1, -2);
                        ShakeAt(30 * 5, 0, 3);
                        ShakeAt(30 * 6, 0, 4);

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

                    Action Trigger = null;

                    Trigger = delegate
                     {
                         if (aa.Count == 0)
                             return;

                         Overlay.Opacity = 1;
                         OverlayWhite.Opacity = 1;
                         Next = delegate
                         {
                             var dd = aa.Dequeue();

                             dd(true);


                             (1000 / 30).AtDelay(
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

                    this.MouseLeftButtonUp +=
                        delegate
                        {
                            Trigger();
                        };

                    return Trigger;
                };

            PrepareAnimation();

            AnimationCompleted += () => PrepareAnimation();
        }

        public readonly Func<Action> PrepareAnimation;

        public event Action AnimationCompleted;
    }
}
