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

namespace InteractiveTransformA
{

    public class ApplicationCanvas : Canvas
    {
        public const int DefaultWidth = 800;
        public const int DefaultHeight = 400;

        public ApplicationCanvas()
        {
            this.Width = DefaultWidth;
            this.Height = DefaultHeight;

            var SizeChanged = false;
            this.SizeChanged += (s, e) =>
                {
                    if (SizeChanged)
                        return;
                    SizeChanged = true;
                    InitializeContent();
                };



        }

        public virtual bool SkipNonEssentials
        {
            get
            {
                return false;
            }
        }

        Canvas Shadow;

        public Action<object, int> AtMouseDown;
        public Action<object, int, int, int> AtMouseMove;
        public Action<object, int> AtMouseUp;

        public readonly List<Action<object>> _AtMouseDown = new List<Action<object>>();
        public readonly List<Action<object>> _AtMouseUp = new List<Action<object>>();
        public readonly List<Action<object, int, int>> _AtMouseMove = new List<Action<object, int, int>>();

        TextBox t2;

        void InitializeContent()
        {
            this.AtMouseDown = (sender, context) => { this.Dispatcher.Invoke(new Action(() => _AtMouseDown[context](sender))); };
            this.AtMouseMove = (sender, context, x, y) => { this.Dispatcher.Invoke(new Action(() => _AtMouseMove[context](sender, x, y))); };
            this.AtMouseUp = (sender, context) => { this.Dispatcher.Invoke(new Action(() => _AtMouseUp[context](sender))); };


            var DefaultHeight = Convert.ToInt32(Height);
            var DefaultWidth = Convert.ToInt32(Width);

            if (!SkipNonEssentials)
            {
                Colors.Cyan.ToGradient(Colors.White, DefaultHeight / 4).Select(
                    (c, i) =>
                        new Rectangle
                        {
                            Fill = new SolidColorBrush(c),
                            Width = DefaultWidth,
                            Height = 4,
                        }.MoveTo(0, i * 4).AttachTo(this)
                ).ToArray();

                #region text
                var tbg1 = new Rectangle
                {
                    Width = 300 - 14,
                    Height = 100,
                    Fill = Brushes.Black,
                    Opacity = 0.5
                }.MoveTo(12, 12).AttachTo(this);
            }
            this.Shadow = new Canvas
            {

            }.AttachTo(this);

            if (!SkipNonEssentials)
            {
                var t1 = new TextBox
                {
                    AcceptsReturn = true,
                    FontSize = 10,
                    Text = "ScriptCoreLib needs to be updated before jsc can translate this!",
                    BorderThickness = new Thickness(0),
                    Foreground = Brushes.White,
                    Background = Brushes.Transparent,
                    IsReadOnly = true,
                    Width = 300 - 14,
                    Height = 100,
                }.MoveTo(12, 12).AttachTo(this);


                var tbg2 = new Rectangle
                {
                    Width = 500 - 14,
                    Height = 100,
                    Fill = Brushes.Black,
                    Opacity = 0.5
                }.MoveTo(302, 12).AttachTo(this);

                t2 = new TextBox
               {
                   AcceptsReturn = true,
                   FontSize = 10,
                   Text = "",
                   BorderThickness = new Thickness(0),
                   Foreground = Brushes.White,
                   Background = Brushes.Transparent,
                   IsReadOnly = true,
                   Width = 500 - 14,
                   Height = 100,
               }.MoveTo(302, 12).AttachTo(this);
                #endregion

            }

            var blocksize = 24;
            var blockx = 100;
            var blocky = 150;

            #region floor
            Action<int, int, int, ImageSource> cfloor =
                (ix, iy, iz, src) =>
                {
                    CreateIsometricFloor(blocksize,
                        (blockx + ix * blocksize + iy * blocksize),
                        (blocky + (ix - iz * 2) * blocksize / 2 - iy * blocksize / 2),
                        src
                    );
                };


            for (int ix = -1; ix < 4; ix++)
                for (int iy = 4; iy >= -1; iy--)
                {
                    cfloor(ix, iy, -1, new Avalon.Images.floor().Source);
                }
            #endregion


            #region cblock

            Action<int, int, int, ImageSource> cblock =
                (ix, iy, iz, src) =>
                {
                    CreateIsometricBlock(blocksize,
                        (blockx + ix * blocksize + iy * blocksize),
                        (blocky + (ix - iz * 2) * blocksize / 2 - iy * blocksize / 2),
                        src
                    );
                };

            for (int ix = 1; ix < 2; ix++)
                for (int iy = 2; iy >= 1; iy--)
                {
                    cblock(ix, iy, 0, new Avalon.Images.brown().Source);
                }

            #endregion

            if (!SkipNonEssentials)
            {
                CreateTransformer(t2, 120, 120, 550, 150);
                CreateTransformer(t2, 100, 100, 350, 250);
            }
        }

        private void CreateIsometricFloor(int blocksize, int blockx, int blocky, ImageSource blocksrc)
        {
            var block = new
            {
                c = CreateTransformer(null, blocksize, blocksize, blockx, blocky),
            };




            block.c.SetBounds(
                new[]
				{
					new Point(blockx - blocksize, blocky - blocksize * 0.5),
					new Point(blockx + 1, blocky  - blocksize ),
					new Point(blockx, blocky + 1),
					new Point(blockx + blocksize + 1, blocky - blocksize * 0.5 + 1),
				}
            );


            block.c.HideVisuals();



            block.c.HideMirror();
            block.c.SetSource(blocksrc);
            block.c.SetOpacity(1);

        }

        private void CreateIsometricBlock(int blocksize, int blockx, int blocky, ImageSource blocksrc)
        {
            var block = new
            {
                b = CreateTransformer(null, blocksize, blocksize, blockx, blocky),
                c = CreateTransformer(null, blocksize, blocksize, blockx, blocky),
                a = CreateTransformer(null, blocksize, blocksize, blockx, blocky),
            };


            block.a.SetBounds(
                new[]
				{
					new Point(blockx - blocksize, blocky - blocksize * 0.5),
					new Point(blockx, blocky),
					new Point(blockx - blocksize, blocky + blocksize * 0.5),
					new Point(blockx, blocky + blocksize),
				}
            );


            block.b.SetBounds(
                new[]
				{
					new Point(blockx, blocky),
					new Point(blockx + blocksize, blocky - blocksize * 0.5),
					new Point(blockx, blocky + blocksize),
					new Point(blockx + blocksize, blocky + blocksize * 0.5),
				}
            );


            block.c.SetBounds(
                new[]
				{
					new Point(blockx - blocksize, blocky - blocksize * 0.5),
					new Point(blockx, blocky  - blocksize ),
					new Point(blockx, blocky),
					new Point(blockx + blocksize, blocky - blocksize * 0.5),
				}
            );

            block.a.HideVisuals();
            block.b.HideVisuals();
            block.c.HideVisuals();

            block.a.HideMirror();
            block.a.SetSource(blocksrc);
            block.a.SetOpacity(1);


            block.c.HideMirror();
            block.c.SetSource(blocksrc);
            block.c.SetOpacity(1);

            block.b.HideMirror();
            block.b.SetSource(blocksrc);
            block.b.SetOpacity(1);
        }


        public class TransformerControl
        {
            public Action<double> SetOpacity;
            public Action<ImageSource> SetSource;

            public Action<Point[]> SetBounds;

            public Action HideVisuals;

            public Action HideMirror;
        }


        private TransformerControl CreateTransformer(TextBox t1, int gw, int gh, int gx, int gy)
        {
            var RB = new Rectangle
            {
                Width = gw,
                Height = gh,
                Fill = Brushes.Red,
                Opacity = 0.1,
                Cursor = Cursors.Hand,
            }.AttachTo(this).MoveTo(gx, gy);

            var GB = new Rectangle
            {
                Width = gw,
                Height = gh,
                Fill = Brushes.Green,
                Opacity = 0.1,
                Cursor = Cursors.Hand,
            }.AttachTo(this).MoveTo(gx, gy);

            var g0 = new Rectangle
            {
                Width = gw,
                Height = gh,
                Fill = Brushes.Purple,
                Opacity = 0.2
            }.AttachTo(this.Shadow).MoveTo(gx, gy);

            var g1 = new Image
            {
                Width = gw,
                Height = gh,
                Source = new Avalon.Images.d().Source,
                Opacity = 0.7,
            }.AttachTo(this).MoveTo(gx, gy);

            var g2 = new Image
            {
                Width = gw,
                Height = gh,
                Source = new Avalon.Images.d().Source,
                Opacity = 0.7,
            }.AttachTo(this).MoveTo(gx, gy);

            var R = new Rectangle
            {
                Width = 8,
                Height = 8,
                Fill = Brushes.Red,
                Cursor = Cursors.Hand,
                RenderTransform = new TranslateTransform(-4, -4)
            }.AttachTo(this).MoveTo(gx, gy);

            var G = new Rectangle
            {
                Width = 8,
                Height = 8,
                Fill = Brushes.Green,
                Cursor = Cursors.Hand,
                RenderTransform = new TranslateTransform(-4, -4)
            }.AttachTo(this).MoveTo(gx, gy);

            var M = new Rectangle
            {
                Width = 8,
                Height = 8,
                Fill = Brushes.Magenta,
                Cursor = Cursors.Hand,
                RenderTransform = new TranslateTransform(-4, -4)
            }.AttachTo(this).MoveTo(gx, gy);

            var Y = new Rectangle
            {
                Width = 8,
                Height = 8,
                Fill = Brushes.Yellow,
                Cursor = Cursors.Hand,
                RenderTransform = new TranslateTransform(-4, -4)
            }.AttachTo(this).MoveTo(gx, gy);

            var Orange = new Rectangle
            {
                Width = 8,
                Height = 8,
                Fill = Brushes.Orange,
                Cursor = Cursors.Hand,
                RenderTransform = new TranslateTransform(-4, -4)
            }.AttachTo(this).MoveTo(gx + gw / 2, gy + gh / 2 - 8);

            // rotation origin
            var Black = new Rectangle
            {
                Width = 8,
                Height = 8,
                Fill = Brushes.Black,
                Cursor = Cursors.Hand,
                RenderTransform = new TranslateTransform(-4, -4)
            }.AttachTo(this).MoveTo(gx + gw / 2, gy + gh / 2);

            // rotation handler
            var White = new Rectangle
            {
                Width = 8,
                Height = 8,
                Fill = Brushes.White,
                Cursor = Cursors.Hand,
                RenderTransform = new TranslateTransform(-4, -4)
            }.AttachTo(this).MoveTo(gx + gw, gy + gh / 2);


            var OrangeSelfAverage = true;



            Action Update =
                delegate
                {
                    var w = new StringBuilder();

                    w.AppendLine("Magneta:");
                    w.AppendLine(ApplyMatrix(RB, GB, R, G, M, g2, gx, gy, gw, gh));
                    w.AppendLine("Yellow:");
                    w.AppendLine(ApplyMatrix(RB, GB, R, G, Y, g1, gx, gy, gw, gh));

                    if (t1 != null)
                        t1.Text = w.ToString();

                    if (OrangeSelfAverage)
                    {
                        var args = new[]
						{
					
							new { x = Canvas.GetLeft(R), y = Canvas.GetTop(R) },
							new { x = Canvas.GetLeft(G), y = Canvas.GetTop(G) },
							new { x = Canvas.GetLeft(M), y = Canvas.GetTop(M) },
							new { x = Canvas.GetLeft(Y), y = Canvas.GetTop(Y) }
						};

                        var _x = args.Average(k => k.x);
                        var _y = args.Average(k => k.y);
                        Orange.MoveTo(
                            _x,
                            _y - 8
                        );
                    }
                };

            Action Reset =
                delegate
                {
                    M.MoveTo(gx, gy);
                    R.MoveTo(gx + gw, gy);
                    G.MoveTo(gx, gy + gh);
                    Y.MoveTo(gx + gw, gy + gh);
                };

            AsMovableByMouse(R, Update);
            AsMovableByMouse(G, Update);
            AsMovableByMouse(M, Update);
            AsMovableByMouse(Y, Update);

            var RotationInfo = new { x = 0.0, y = 0.0 };

            RotationInfo = null;

            Action UpdateRotation =
                delegate
                {
                    var p = new
                    {
                        White = new { x = Canvas.GetLeft(White), y = Canvas.GetTop(White) },
                        Black = new { x = Canvas.GetLeft(Black), y = Canvas.GetTop(Black) },
                        R = new { x = Canvas.GetLeft(R), y = Canvas.GetTop(R) },
                        G = new { x = Canvas.GetLeft(G), y = Canvas.GetTop(G) },
                        M = new { x = Canvas.GetLeft(M), y = Canvas.GetTop(M) },
                        Y = new { x = Canvas.GetLeft(Y), y = Canvas.GetTop(Y) }
                    };

                    if (RotationInfo == null)
                    {
                        RotationInfo = p.White;
                        return;
                    }

                    // we now need 
                    // - previous distance and rotation
                    // - current distance and rotation

                    var q = new
                    {
                        o = new { x = RotationInfo.x - p.Black.x, y = RotationInfo.y - p.Black.y },
                        n = new { x = p.White.x - p.Black.x, y = p.White.y - p.Black.y },

                        R = new { x = p.Black.x - p.R.x, y = p.Black.y - p.R.y },
                        G = new { x = p.Black.x - p.G.x, y = p.Black.y - p.G.y },
                        M = new { x = p.Black.x - p.M.x, y = p.Black.y - p.M.y },
                        Y = new { x = p.Black.x - p.Y.x, y = p.Black.y - p.Y.y },
                    };

                    Func<double, double, double> GetLength =
                        (x, y) =>
                        {
                            return new vec2(x, y).Length;
                        };

                    var a = new
                    {
                        o = new { z = GetLength(q.o.x, q.o.y), a = new Point(q.o.x, q.o.y).GetRotation() },
                        n = new { z = GetLength(q.n.x, q.n.y), a = new Point(q.n.x, q.n.y).GetRotation() },

                        R = new { z = -GetLength(q.R.x, q.R.y), a = new Point(q.R.x, q.R.y).GetRotation() },
                        G = new { z = -GetLength(q.G.x, q.G.y), a = new Point(q.G.x, q.G.y).GetRotation() },
                        M = new { z = -GetLength(q.M.x, q.M.y), a = new Point(q.M.x, q.M.y).GetRotation() },
                        Y = new { z = -GetLength(q.Y.x, q.Y.y), a = new Point(q.Y.x, q.Y.y).GetRotation() },
                    };

                    var n = new { z = a.n.z / a.o.z, a = a.n.a - a.o.a };

                    if (n.z == 1)
                        if (n.a == 0)
                        {
                            RotationInfo = p.White;
                            return;
                        }


                    R.MoveTo(
                        p.Black.x + Math.Cos(a.R.a + n.a) * a.R.z * n.z,
                        p.Black.y + Math.Sin(a.R.a + n.a) * a.R.z * n.z
                    );

                    G.MoveTo(
                        p.Black.x + Math.Cos(a.G.a + n.a) * a.G.z * n.z,
                        p.Black.y + Math.Sin(a.G.a + n.a) * a.G.z * n.z
                    );

                    M.MoveTo(
                        p.Black.x + Math.Cos(a.M.a + n.a) * a.M.z * n.z,
                        p.Black.y + Math.Sin(a.M.a + n.a) * a.M.z * n.z
                    );

                    Y.MoveTo(
                        p.Black.x + Math.Cos(a.Y.a + n.a) * a.Y.z * n.z,
                        p.Black.y + Math.Sin(a.Y.a + n.a) * a.Y.z * n.z
                    );

                    Update();

                    RotationInfo = p.White;
                };

            AsMovableByMouse(Black, null);
            AsMovableByMouse(White, UpdateRotation);

            var TranslationInfo = new { x = 0.0, y = 0.0 };

            TranslationInfo = null;

            Action ShowVisuals =
                delegate
                {
                    g0.Show();
                    GB.Show();
                    RB.Show();

                    Orange.Opacity = 1;

                    var e = new[] { M, R, G, Y, White, Black };

                    foreach (var k in e)
                    {
                        k.Show();

                        k.BringToFront();
                    }
                };

            Action UpdateTranslation =
                delegate
                {
                    ShowVisuals();


                    var p = new
                    {
                        Orange = new { x = Canvas.GetLeft(Orange), y = Canvas.GetTop(Orange) },

                        White = new { x = Canvas.GetLeft(White), y = Canvas.GetTop(White) },
                        Black = new { x = Canvas.GetLeft(Black), y = Canvas.GetTop(Black) },
                        R = new { x = Canvas.GetLeft(R), y = Canvas.GetTop(R) },
                        G = new { x = Canvas.GetLeft(G), y = Canvas.GetTop(G) },
                        M = new { x = Canvas.GetLeft(M), y = Canvas.GetTop(M) },
                        Y = new { x = Canvas.GetLeft(Y), y = Canvas.GetTop(Y) }
                    };

                    if (TranslationInfo == null)
                    {
                        TranslationInfo = p.Orange;
                        return;
                    }

                    var q = new
                    {
                        x = p.Orange.x - TranslationInfo.x,
                        y = p.Orange.y - TranslationInfo.y,
                    };

                    if (q.x == 0)
                        if (q.y == 0)
                        {
                            TranslationInfo = p.Orange;
                            return;
                        }

                    RotationInfo = null;

                    R.MoveTo(p.R.x + q.x, p.R.y + q.y);
                    G.MoveTo(p.G.x + q.x, p.G.y + q.y);
                    Y.MoveTo(p.Y.x + q.x, p.Y.y + q.y);
                    M.MoveTo(p.M.x + q.x, p.M.y + q.y);
                    Black.MoveTo(p.Black.x + q.x, p.Black.y + q.y);
                    White.MoveTo(p.White.x + q.x, p.White.y + q.y);

                    Update();

                    TranslationInfo = p.Orange;
                };

            var OrangeDrag = AsMovableByMouse(Orange, UpdateTranslation);

            OrangeDrag.Enter = () => OrangeSelfAverage = false;
            OrangeDrag.Exit = () => OrangeSelfAverage = true;

            Reset();
            UpdateRotation();
            Update();

            var rr = new TransformerControl
            {
                SetOpacity =
                    Opacity =>
                    {
                        g1.Opacity = Opacity;
                        g2.Opacity = Opacity;
                    },
                SetSource =
                    src =>
                    {
                        g1.Source = src;
                        g2.Source = src;
                    },
                HideMirror =
                    delegate
                    {
                        Y.Opacity = 0.2;
                        g1.Hide();
                    },
                HideVisuals =
                    delegate
                    {
                        g0.Hide();
                        GB.Hide();
                        RB.Hide();

                        Orange.Opacity = 0.05;

                        var e = new[] { M, R, G, Y, White, Black };

                        foreach (var k in e)
                        {
                            k.Hide();
                        }

                    },
                SetBounds =
                    args =>
                    {
                        var e = new[] { M, R, G, Y };

                        for (int i = 0; i < args.Length; i++)
                        {
                            e[i].MoveTo(args[i].X, args[i].Y);
                        }

                        var _x = args.Average(k => k.X);
                        var _y = args.Average(k => k.Y);
                        Orange.MoveTo(
                            _x,
                            _y - 8
                        );


                        RotationInfo = null;

                        Update();
                    },
                //g2 = g2
            };


            return rr;
        }

        private static string ApplyMatrix(Rectangle RB, Rectangle GB, Rectangle R, Rectangle G, Rectangle B, Image g, double gx, double gy, double gw, double gh)
        {
            var p = new
            {
                R = new { x = Canvas.GetLeft(R), y = Canvas.GetTop(R) },
                G = new { x = Canvas.GetLeft(G), y = Canvas.GetTop(G) },
                B = new { x = Canvas.GetLeft(B), y = Canvas.GetTop(B) }
            };

            var q = new
            {
                R = new { x = p.R.x - p.B.x, y = p.R.y - p.B.y },
                G = new { x = p.G.x - p.B.x, y = p.G.y - p.B.y },
                B = new { x = p.B.x - gx, y = p.B.y - gy },
            };


            RB.SetBounds(p.R.x, p.R.y, p.B.x, p.B.y);
            GB.SetBounds(p.G.x, p.G.y, p.B.x, p.B.y);

            var m = new
            {
                M11 = q.R.x / gw,
                M12 = q.R.y / gh,

                M21 = q.G.x / gw,
                M22 = q.G.y / gh,


                OX = q.B.x,
                OY = q.B.y
            };

            var s = q.ToString() + Environment.NewLine + m.ToString();

            g.RenderTransform = new MatrixTransform(m.M11, m.M12, m.M21, m.M22, m.OX, m.OY);
            return s;
        }

        public class AsMovableByMouseControl
        {
            public Action Enter;
            public Action Exit;
        }

        public AsMovableByMouseControl AsMovableByMouse(UIElement e, Action Update)
        {
            var c = new AsMovableByMouseControl();

            var q = false;


            #region iAtMouseDown
            Action<object> iAtMouseDown =
                (sender) =>
                {
                    q = true;

                    if (c.Enter != null)
                        c.Enter();
                };

            int iAtMouseDownContext = this._AtMouseDown.Count;
            this._AtMouseDown.Add(iAtMouseDown);

            e.MouseLeftButtonDown +=
                delegate
                {
                    this.AtMouseDown(null, iAtMouseDownContext);
                };
            #endregion


            Action<object, int, int> iAtMouseMove =
                (sender, px, py) =>
                {
                    e.MoveTo(Canvas.GetLeft(e) + px - 3, Canvas.GetTop(e) + py - 3);

                    if (Update != null)
                        Update();
                };

            var cAtMouseMove = this._AtMouseMove.Count;
            this._AtMouseMove.Add(iAtMouseMove);

            this.MouseMove +=
                (sender, args) =>
                {
                    if (!q)
                        return;
                    var p = args.GetPosition(e);

                    //Console.WriteLine(new { p.X, p.Y });

                    this.AtMouseMove(null, cAtMouseMove, Convert.ToInt32(p.X), Convert.ToInt32(p.Y));
                };

            #region iAtMouseUp
            Action<object> iAtMouseUp =
                (sender) =>
                {
                    if (!q)
                        return;

                    q = false;

                    if (c.Exit != null)
                        c.Exit();
                };

            int iAtMouseUpContext = this._AtMouseUp.Count;
            this._AtMouseUp.Add(iAtMouseUp);

            e.MouseLeftButtonUp +=
                delegate
                {
                    this.AtMouseUp(null, iAtMouseUpContext);
                };
            #endregion





            return c;
        }

    }

    // cannot use name Vector in flash! should make use GLSL types instead!
    public class vec2
    {
        public double X;
        public double Y;

        public vec2()
            : this(0, 0)
        {

        }

        public vec2(double X, double Y)
        {
            this.X = X;
            this.Y = Y;
        }

        public double Length
        {
            get
            {
                return Math.Sqrt(X * X + Y * Y);
            }
        }
    }

    public class Point
    {
        public double X;
        public double Y;

        public Point()
            : this(0, 0)
        {

        }

        public Point(double X, double Y)
        {
            this.X = X;
            this.Y = Y;
        }
    }



    static class Extensions
    {
        public static double DegreesToRadians(this int Degrees)
        {
            return (Math.PI * 2) * Degrees / 360;
        }

        public static int RadiansToDegrees(this double Arc)
        {
            return (int)(360 * Arc / (Math.PI * 2));
        }

        public static double GetRotation(this Point p)
        {
            var x = p.X;
            var y = p.Y;

            const double _180 = System.Math.PI;
            const double _90 = System.Math.PI / 2;
            const double _270 = System.Math.PI * 3 / 2;

            if (x == 0)
                if (y < 0)
                    return _270;
                else if (y == 0)
                    return 0;
                else
                    return _90;

            if (y == 0)
                if (x < 0)
                    return _180;
                else
                    return 0;

            var a = System.Math.Atan(y / x);

            if (x < 0)
                a += _180;
            else if (y < 0)
                a += System.Math.PI * 2;


            return a;
        }



        public static void SetBounds(this Rectangle e, double x, double y, double cx, double cy)
        {
            var w = cx - x;
            var h = cy - y;

            if (w < 0)
            {
                x += w;
                w = -w;
            }

            if (h < 0)
            {
                y += h;
                h = -h;
            }

            e.MoveTo(x, y);
            e.Width = w + 1;
            e.Height = h + 1;
        }
    }

}
