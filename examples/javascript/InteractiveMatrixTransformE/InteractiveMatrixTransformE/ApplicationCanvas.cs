using InteractiveMatrixTransformE.AffineEngine;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Shared.Avalon.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Xml;
using System.Xml.Linq;

namespace InteractiveMatrixTransformE
{
    public class ApplicationCanvas : Canvas
    {
        public readonly Rectangle r = new Rectangle();


        public ApplicationCanvas()
        {
            var SizeChanged = 0;
            this.SizeChanged += (s, e) =>
            {
                SizeChanged++;
                if (SizeChanged != 2)
                    return;
                InitializeContent();
            };

        }

        void InitializeContent()
        {
            Colors.Blue.ToGradient(Colors.Red, Convert.ToInt32( Height) / 4).Select(
            (c, i) =>
                new Rectangle
                {
                    Fill = new SolidColorBrush(c),
                    Width = Width,
                    Height = 4,
                }.MoveTo(0, i * 4).AttachTo(this)
        ).ToArray();



            //var help = new Image
            //{
            //    Source = (KnownAssets.Path.Assets + "/help.png").ToSource()
            //}.AttachTo(this);

            //help.Opacity = 0;

            var img = new Image
            {
                Source = new Avalon.Images.jsc().Source
            }.MoveTo(Width - 128, Height - 128).AttachTo(this);

            var t = new TextBox
            {
                FontSize = 10,
                Text = "powered by jsc",
                BorderThickness = new Thickness(0),
                Foreground = 0xffffffff.ToSolidColorBrush(),
                Background = Brushes.Transparent,
                IsReadOnly = true,
                Width = Width
            }.MoveTo(8, 8).AttachTo(this);



            //help.Opacity = 1;
            img.Opacity = 0.5;








            // cursor position calculations are not ready
            // for transofrmed elements.
            // we will provide a floor for those events...
            var shadow = new Rectangle
            {
                Width = Width,
                Height = Height,

                Fill = Brushes.Black,
            }.AttachTo(this);

            var shadowa = shadow.ToAnimatedOpacity();

            shadowa.Opacity = 0;



            var a = new AffineMesh();

            Func<double, double, double, Brush, AffinePoint> a_Add =
                (X, Y, Z, Fill) =>
                {
                    var p = new AffinePoint { Z = Z, X = X, Y = Y };
                    var h = new Rectangle { Fill = Fill, Width = 4, Height = 4 }.AttachTo(this);
                    //var ht = new TextBox { }.AttachTo(this);

                    var historysize = 64;
                    var history = new Queue<Rectangle>();

                    Action<AffinePoint> p_Update =
                        pp =>
                        {
                            if (history.Count > historysize)
                            {
                                history.Dequeue();
                            }

                            // -100 == 0.5
                            // 0 == 1
                            // +100 == 2

                            //var zoom = (pp.Z) / 1000;

                            var zoom = 0.5;

                            var pp_X = Width / 2 - 4 + pp.X * zoom;
                            var pp_Y = Height / 2 - 4 + pp.Y * zoom;
                            // Z is ignored here
                            // but could be used for sorting

                            h.MoveTo(pp_X, pp_Y);

                            //var hh = new Rectangle { Fill = Fill, Width = 4, Height = 4, Opacity = 1 }.MoveTo(pp_X, pp_Y).AttachTo(this);
                            //history.Enqueue(hh);

                            //foreach (var k in history.Select((c, i) => new { c, i }))
                            //{
                            //    k.c.Opacity = ((double)k.i / historysize) * 0.6;
                            //}

                            //ht.MoveTo(
                            //    DefaultWidth / 2 + pp.X - 4,
                            //    DefaultHeight / 2 + pp.Y + 4
                            //);

                            //ht.Text = new { pp.X, pp.Y, pp.Z }.ToString();
                        };

                    p.Tag = p_Update;

                    a.Add(p);

                    p_Update(p);

                    return p;
                };


            Enumerable.Range(0, 10).Select(X => a_Add(X * 10, 0, 0, Brushes.Red)).ToArray();
            Enumerable.Range(0, 10).Select(Y => a_Add(0, Y * 10, 0, Brushes.Blue)).ToArray();
            Enumerable.Range(0, 10).Select(Z => a_Add(0, 0, Z * 10, Brushes.GreenYellow)).ToArray();

            Enumerable.Range(0, 10).Select(X => a_Add(X * 10 + 100, 0, 0, Brushes.Red)).ToArray();
            Enumerable.Range(0, 10).Select(Y => a_Add(100, Y * 10, 0, Brushes.Blue)).ToArray();
            Enumerable.Range(0, 10).Select(Z => a_Add(100, 0, Z * 10, Brushes.GreenYellow)).ToArray();


            Enumerable.Range(0, 100).Select(X => a_Add(X * 10 + 200, 0, 0, Brushes.Red)).ToArray();
            Enumerable.Range(0, 10).Select(Y => a_Add(200, Y * 10, 0, Brushes.Blue)).ToArray();
            Enumerable.Range(0, 10).Select(Z => a_Add(200, 0, Z * 10, Brushes.GreenYellow)).ToArray();

            {
                var radius = 100;
                foreach (var i in Enumerable.Range(0, 90).Select(aa => (aa * 4).DegreesToRadians()))
                {
                    a_Add(Math.Cos(i) * radius, Math.Sin(i) * radius, 0, Brushes.GreenYellow);
                    a_Add(Math.Cos(i) * radius, 0, Math.Sin(i) * radius, Brushes.BlueViolet);
                    a_Add(0, Math.Cos(i) * radius, Math.Sin(i) * radius, Brushes.Magenta);
                }
            }

            {
                var radius = 200;
                foreach (var i in Enumerable.Range(0, 90).Select(aa => (aa * 4).DegreesToRadians()))
                {
                    a_Add(Math.Cos(i) * radius, Math.Sin(i) * radius, 0, Brushes.GreenYellow);
                    a_Add(Math.Cos(i) * radius, 0, Math.Sin(i) * radius, Brushes.BlueViolet);
                    a_Add(0, Math.Cos(i) * radius, Math.Sin(i) * radius, Brushes.Magenta);
                }
            }

            {
                var radius = 500;
                foreach (var i in Enumerable.Range(0, 90).Select(aa => (aa * 4).DegreesToRadians()))
                {
                    a_Add(Math.Cos(i) * radius, Math.Sin(i) * radius, 0, Brushes.GreenYellow);
                    a_Add(Math.Cos(i) * radius, 0, Math.Sin(i) * radius, Brushes.BlueViolet);
                    a_Add(0, Math.Cos(i) * radius, Math.Sin(i) * radius, Brushes.Magenta);
                }
            }

            a_Add(200, 0, 0, Brushes.BlueViolet);
            a_Add(0, 200, 0, Brushes.Yellow);
            a_Add(0, 0, 200, Brushes.Red);

            (1000 / 50).AtIntervalWithCounter(
                c =>
                {
                    // rotate floor
                    var _a = a.ToRotation(
                        new AffineRotation
                        {
                            XY = 0.01,
                            YZ = 0.02,
                            XZ = 0.03
                        }
                    );

                    //var _a = a.ToRotation(0, 0.01);

                    a = _a;

                    //var _a = a.ToRotation(c * 0.01, c * 0.005);
                    //var _a = a.ToRotation(c * 0.01, 0);

                    foreach (var p in _a.Points)
                    {
                        ((Action<AffinePoint>)p.Tag)(p);
                    }
                }
            );
        }

    }

    public class Movable
    {
        public Canvas Container { get; set; }

        const int Size = 8;
        public Brush Color
        {
            set
            {
                Container.Background = value;
            }
        }

        double InternalX;
        double InternalY;

        public double X
        {
            get
            {
                return InternalX;
            }
        }

        public double Y
        {
            get
            {
                return InternalY;
            }
        }

        public event Action Changed;

        Panel InternalContext;
        public Panel Context
        {
            get
            {
                return InternalContext;
            }
            set
            {
                if (InternalContext != null)
                    throw new InvalidOperationException();

                InternalContext = value;
                Container.Orphanize().AttachTo(value);

                var m = false;

                this.Container.MouseLeftButtonDown +=
                    delegate
                    {
                        m = true;
                    };

                this.Context.MouseMove +=
                    (sender, args) =>
                    {
                        if (m)
                        {
                            var p = args.GetPosition(this.Context);

                            var x = p.X;
                            var y = p.Y;

                            MoveTo(x, y);
                        }
                    };

                this.Context.MouseLeftButtonUp +=
                    delegate
                    {
                        m = false;
                    };
            }
        }

        public void MoveTo(double x, double y)
        {
            InternalX = x;
            InternalY = y;

            this.Container.MoveTo(InternalX - Size, InternalY - Size);

            if (Changed != null)
                Changed();
        }

        public Movable()
        {
            this.Container = new Canvas
            {
                Width = Size * 2,
                Height = Size * 2,
                Cursor = Cursors.Hand,
                Background = Brushes.Red
            };


        }
    }

}
