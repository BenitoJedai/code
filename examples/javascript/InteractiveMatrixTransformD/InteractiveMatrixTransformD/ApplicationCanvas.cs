using ScriptCoreLib.Avalon;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Shared.Avalon.Extensions;
using System;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Xml;
using System.Xml.Linq;

namespace InteractiveMatrixTransformD
{
    public class ApplicationCanvas : Canvas
    {
        public const int DefaultWidth = 480;
        public const int DefaultHeight = 320;

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

            Colors.Blue.ToGradient(Colors.Red, Convert.ToInt32(Height) / 4).Select(
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
            }.MoveTo(DefaultWidth - 128, DefaultHeight - 128).AttachTo(this);

            var t = new TextBox
            {
                FontSize = 10,
                Text = "powered by jsc",
                BorderThickness = new Thickness(0),
                Foreground = 0xffffffff.ToSolidColorBrush(),
                Background = Brushes.Transparent,
                IsReadOnly = true,
                Width = DefaultWidth
            }.MoveTo(8, 8).AttachTo(this);

            var t2 = new TextBox
            {
                FontSize = 10,
                AcceptsReturn = true,
                Text = @"
ie and opera are not supported
firefox reports after transform
chrome reports before transform
we cannot rely on mouse position currently
shall wait for improvements like touch API
",
                BorderThickness = new Thickness(0),
                Foreground = 0xffffffff.ToSolidColorBrush(),
                Background = Brushes.Transparent,
                IsReadOnly = true,
                Width = DefaultWidth,
                Height = 128,
            }.MoveTo(8, 32).AttachTo(this);

            //help.Opacity = 1;
            img.Opacity = 0.5;

            t.MouseEnter +=
                delegate
                {
                    //help.Opacity = 0.5;

                    img.Opacity = 1;
                    t.Foreground = 0xffffff00.ToSolidColorBrush();
                };

            t.MouseLeave +=
                delegate
                {
                    //help.Opacity = 1;

                    img.Opacity = 0.5;
                    t.Foreground = 0xffffffff.ToSolidColorBrush();
                };


            var sand = new Image
            {
                Width = 100,
                Height = 100,
                Source = new Avalon.Images.sand().Source
            }.AttachTo(this);

            // structs are not that good for translations...
            // we might be able to skip them it seems
            sand.RenderTransform = new MatrixTransform(1.2, 0.5, 0.2, 1, 100, 100);

            sand.Cursor = Cursors.Hand;

            // jsc/mxmlc cannot handle property name "this" :)

            sand.Opacity = 0.7;

            sand.MouseMove +=
                (sender, args) =>
                {
                    var p1 = args.GetPosition(sand);
                    var p2 = args.GetPosition(this);

                    t.Text = new
                    {
                        sand = new { p1.X, p1.Y },
                        that = new { p2.X, p2.Y }
                    }.ToString();

                };





            var tri = new Image
            {
                Width = 100,
                Height = 100,
                Source = new Avalon.Images._17().Source
            }.AttachTo(this);


            var trig = new Image
            {
                Width = 100,
                Height = 100,
                Source = new Avalon.Images._17g().Source
            }.AttachTo(this);

            var tri2 = new Image
            {
                Width = 100,
                Height = 100,
                Source = new Avalon.Images._17().Source
            }.AttachTo(this);


            var trig2 = new Image
            {
                Width = 100,
                Height = 100,
                Source = new Avalon.Images._17g().Source
            }.AttachTo(this);


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

            Func<Brush, int, int, Movable> m =
                (Color, X, Y) =>
                {
                    var m0 = new Movable
                    {
                        Context = this,
                        Color = Color
                    };

                    m0.MoveTo(X, Y);

                    return m0;
                };

            var m1 = m(Brushes.Green, 250, 50);
            var m2 = m(Brushes.Red, 250 + 100, 50);
            var m3 = m(Brushes.Blue, 250, 50 + 100);
            var m4 = m(Brushes.Yellow, 250 + 100, 50 + 100);

            var m5 = m(Brushes.Green, 250 + 200, 50);
            var m6 = m(Brushes.Blue, 250 + 200, 50 + 100);



            Action Update =
                delegate
                {


                    tri.RenderTransform = new AffineTransform
                    {
                        Left = 0,
                        Top = 0,
                        Width = 100,
                        Height = 100,

                        X1 = m2.X,
                        Y1 = m2.Y,

                        X2 = m3.X,
                        Y2 = m3.Y,

                        X3 = m1.X,
                        Y3 = m1.Y,

                    };


                    trig.RenderTransform = new AffineTransform
                    {
                        Left = 0,
                        Top = 0,
                        Width = 100,
                        Height = 100,

                        X1 = m3.X,
                        Y1 = m3.Y,

                        X2 = m2.X,
                        Y2 = m2.Y,

                        X3 = m4.X,
                        Y3 = m4.Y,

                    };

                    tri2.RenderTransform = new AffineTransform
                    {
                        Left = 0,
                        Top = 0,
                        Width = 100,
                        Height = 100,

                        X1 = m5.X,
                        Y1 = m5.Y,

                        X2 = m4.X,
                        Y2 = m4.Y,

                        X3 = m2.X,
                        Y3 = m2.Y,

                    };


                    trig2.RenderTransform = new AffineTransform
                    {
                        Left = 0,
                        Top = 0,
                        Width = 100,
                        Height = 100,

                        X1 = m4.X,
                        Y1 = m4.Y,

                        X2 = m5.X,
                        Y2 = m5.Y,

                        X3 = m6.X,
                        Y3 = m6.Y,

                    };
                };

            foreach (var k in new[] { m1, m2, m3, m4, m5, m6 })
            {
                k.Container.MouseLeftButtonDown += delegate { shadowa.Opacity = 0.2; };
                k.Container.MouseLeftButtonUp += delegate { shadowa.Opacity = 0; };
                k.Changed += Update;
            }

            Update();
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
