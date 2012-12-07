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
using System.Windows.Threading;
using System.Xml;
using System.Xml.Linq;

namespace InteractiveMatrixTransformB
{
    public class ApplicationCanvas : Canvas
    {
        public const int DefaultWidth = 480;
        public const int DefaultHeight = 320;

        Canvas Shadow;

        public Action<object, int> AtMouseDown;
        public Action<object, int, int, int> AtMouseMove;
        public Action<object, int> AtMouseUp;

        public readonly List<Action<object>> _AtMouseDown = new List<Action<object>>();
        public readonly List<Action<object>> _AtMouseUp = new List<Action<object>>();
        public readonly List<Action<object, int, int>> _AtMouseMove = new List<Action<object, int, int>>();



        public ApplicationCanvas()
        {
            this.Width = DefaultWidth;
            this.Height = DefaultHeight;

            var SizeChanged = 0;
            this.SizeChanged += (s, e) =>
            {
                //SizeChanged++;
                //if (SizeChanged != 2)
                //    return;
                InitializeContent();
            };

        }

        void InitializeContent()
        {
            this.AtMouseDown = (sender, context) => { this.Dispatcher.Invoke(new Action(() => _AtMouseDown[context](sender))); };
            this.AtMouseMove = (sender, context, x, y) => { this.Dispatcher.Invoke(new Action(() => _AtMouseMove[context](sender, x, y))); };
            this.AtMouseUp = (sender, context) => { this.Dispatcher.Invoke(new Action(() => _AtMouseUp[context](sender))); };



            Colors.Blue.ToGradient(Colors.Red, Convert.ToInt32(Height) / 4).Select(
                (c, i) =>
                    new Rectangle
                    {
                        Fill = new SolidColorBrush(c),
                        Width = Width,
                        Height = 4,
                    }.MoveTo(0, i * 4).AttachTo(this)
            ).ToArray();

            this.Shadow = new Canvas
            {

            }.AttachTo(this);

            var t = new TextBox
            {
                FontSize = 16,
                Text = "This example shows a pyramid on a sand hill. \nYou can rotate the world with your mouse. \nThe elements are not sorted for zordering.",
                BorderThickness = new Thickness(0),
                Foreground = 0xffffffff.ToSolidColorBrush(),
                Background = Brushes.Transparent,
                IsReadOnly = true,
                Width = DefaultWidth,
                Height = 100
            }.MoveTo(8, 8).AttachTo(this);

            var yawn = 0.2;
            var yawn2 = Math.Cos(yawn * 0.5);
            var rotation1 = 3.900;
            var rotation2 = 0.0;

            this.MouseMove +=
                (sender, args) =>
                {
                    // 1..-1

                    var pp = args.GetPosition(this);

                    var angle = (pp.Y / DefaultHeight) * Math.PI * 2 + Math.PI * 0.75;

                    rotation2 = (pp.X / DefaultWidth) * Math.PI * 2;


                    yawn = Math.Cos(angle);

                    yawn2 = Math.Sin(angle);

                    //Console.Title = new { angle = angle.RadiansToDegrees() % 360, yawn, yawn2 }.ToString();

                };



            var point = new { x = 0.0, y = 0.0, z = 0.0 }.AsConstructor((double x, double y, double z) => new { x, y, z });
            var size = 12;

            var examples_pyramid = point(0, 0, 0).AsEmptyList();

            examples_pyramid.AddRange(Enumerable.Range(-5, 11).Select(i => point(-i, -5, 0)));
            examples_pyramid.AddRange(Enumerable.Range(-5, 11).Select(i => point(-5, -i, 0)));
            examples_pyramid.AddRange(Enumerable.Range(-5, 11).Select(i => point(i, 5, 0)));
            examples_pyramid.AddRange(Enumerable.Range(-5, 11).Select(i => point(5, i, 0)));

            examples_pyramid.AddRange(Enumerable.Range(0, 6).Select(i => point(i, i, 5 - (i))));
            examples_pyramid.AddRange(Enumerable.Range(0, 6).Select(i => point(i, -i, 5 - (i))));
            examples_pyramid.AddRange(Enumerable.Range(0, 6).Select(i => point(-i, i, 5 - (i))));
            examples_pyramid.AddRange(Enumerable.Range(0, 6).Select(i => point(-i, -i, 5 - (i))));

            var examples = new
            {
                cube = new[] {
					// bottom
					point(-1, -1, -1),
					point(-1, 1, -1),
					point(1, 1, -1),
					point(1, -1, -1),

					// top
					point(-1, -1, 1),
					point(-1, 1, 1),
					point(1, 1, 1),
					point(1, -1, 1),
				}
                ,
                slide = new[] {
					
				

					// bottom
					point(-2, -1, -1),
					point(-2, 1, -1),
					point(2, 1, -1),
					point(2, -1, -1),

					// top
					point(-2, -1, 1),
					point(-2, 1, 1),
					point(0, 1, 0),
					point(0, -1, 0),


							point(0, 1, -1),
					point(0, -1, -1),
				}
            };

            var p = examples_pyramid.ToArray();


            var v = p.Select(
                    k =>
                    {
                        var r = new Rectangle { Width = 4, Height = 4, Fill = Brushes.Black, Opacity = 0.3 }.AttachTo(this);
                        return new { r, k };
                    }

                ).ToArray();

            var timer = new DispatcherTimer();


            var vertex = new
            {
                sand_a = CreateTransformer(null, 100, 100, 100, 100),
                sand_b = CreateTransformer(null, 100, 100, 100, 100),
                sand_c = CreateTransformer(null, 100, 100, 100, 100),

                sand_wall = CreateTransformer(null, 100, 100, 100, 100),

                floor = CreateTransformer(null, 100, 100, 100, 100),

                a = CreateTransformer(null, 100, 100, 100, 100),
                b = CreateTransformer(null, 100, 100, 100, 100),
                c = CreateTransformer(null, 100, 100, 100, 100),
                d = CreateTransformer(null, 100, 100, 100, 100),

            };

            vertex.sand_a.SetSource(new Avalon.Images.sandv().Source);
            vertex.sand_a.HideVisuals();
            vertex.sand_a.SetOpacity(1);

            vertex.sand_b.SetSource(new Avalon.Images.sandv().Source);
            vertex.sand_b.HideVisuals();
            vertex.sand_b.SetOpacity(1);

            vertex.sand_wall.SetSource(new Avalon.Images.sandv().Source);
            vertex.sand_wall.HideVisuals();
            vertex.sand_wall.SetOpacity(1);

            vertex.sand_c.SetSource(new Avalon.Images.sandv().Source);
            vertex.sand_c.HideVisuals();
            vertex.sand_c.SetOpacity(1);

            vertex.c.SetSource(new Avalon.Images._17dark().Source);
            vertex.b.SetSource(new Avalon.Images._17().Source);
            vertex.d.SetSource(new Avalon.Images._17dark().Source);

            vertex.a.HideMirror();
            vertex.a.HideVisuals();
            //vertex.a.SetOpacity(1);

            vertex.b.HideMirror();
            vertex.b.HideVisuals();
            //vertex.b.SetOpacity(1);

            vertex.c.HideMirror();
            vertex.c.HideVisuals();
            //vertex.c.SetOpacity(1);

            vertex.d.HideMirror();
            vertex.d.HideVisuals();
            //vertex.d.SetOpacity(1);

            vertex.floor.HideMirror();
            vertex.floor.HideVisuals();
            vertex.floor.SetOpacity(1);

            Action AtTick =
                delegate
                {

                    rotation1 += 0.0025;

                    var rotation = rotation1 + rotation2;

                    foreach (var k in v)
                    {
                        var s = new Vector(k.k.x, k.k.y).Length;
                        var r = new Point { X = k.k.x, Y = k.k.y }.GetRotation();

                        k.r.MoveTo(
                            DefaultWidth / 2 + Math.Cos(r + rotation) * size * s,
                            DefaultHeight / 2 + (Math.Sin(r + rotation) * size * s * yawn - size * k.k.z * yawn2)
                        );
                    }

                    var SortHints = new { y = 0.0, e = default(FrameworkElement) }.AsEmptyList();

                    #region right
                    {
                        var vv = new[] { 
                            point(5, -5, 0),
                            point(0, 0, 5),
                            point(5, 5, 0),
                        };

                        var vvx = vv.Select(
                            k =>
                            {
                                var s = new Vector(k.x, k.y).Length;
                                var r = new Point { X = k.x, Y = k.y }.GetRotation();

                                return new Point
                                {
                                    X = DefaultWidth / 2 + Math.Cos(r + rotation) * size * s,
                                    Y = DefaultHeight / 2 + (Math.Sin(r + rotation) * size * s * yawn - size * k.z * yawn2)
                                };
                            }
                        ).ToArray();

                        vertex.a.SetBounds(vvx);
                        SortHints.Add(new { y = vvx.Average(k => k.Y), e = (FrameworkElement)vertex.a.g2 });
                    }
                    #endregion

                    #region left
                    {
                        var vv = new[] { 
                            point(-5, -5, 0),
                            point(0, 0, 5),
                            point(-5, 5, 0),
                        };

                        var vvx = vv.Select(
                            k =>
                            {
                                var s = new Vector(k.x, k.y).Length;
                                var r = new Point { X = k.x, Y = k.y }.GetRotation();

                                return new Point
                                {
                                    X = DefaultWidth / 2 + Math.Cos(r + rotation) * size * s,
                                    Y = DefaultHeight / 2 + (Math.Sin(r + rotation) * size * s * yawn - size * k.z * yawn2)
                                };
                            }
                        ).ToArray();

                        vertex.b.SetBounds(vvx);
                        SortHints.Add(new { y = vvx.Average(k => k.Y), e = (FrameworkElement)vertex.b.g2 });
                    }
                    #endregion

                    #region top
                    {
                        var vv = new[] { 
                            point(-5, -5, 0),
                            point(0, 0, 5),
                            point(5, -5, 0),
                        };

                        var vvx = vv.Select(
                            k =>
                            {
                                var s = new Vector(k.x, k.y).Length;
                                var r = new Point { X = k.x, Y = k.y }.GetRotation();

                                return new Point
                                {
                                    X = DefaultWidth / 2 + Math.Cos(r + rotation) * size * s,
                                    Y = DefaultHeight / 2 + (Math.Sin(r + rotation) * size * s * yawn - size * k.z * yawn2)
                                };
                            }
                        ).ToArray();

                        vertex.c.SetBounds(vvx);
                        SortHints.Add(new { y = vvx.Average(k => k.Y), e = (FrameworkElement)vertex.c.g2 });
                    }
                    #endregion

                    #region bottom
                    {
                        var vv = new[] { 
                            point(-5, 5, 0),
                            point(0, 0, 5),
                            point(5, 5, 0),
                        };

                        var vvx = vv.Select(
                            k =>
                            {
                                var s = new Vector(k.x, k.y).Length;
                                var r = new Point { X = k.x, Y = k.y }.GetRotation();

                                return new Point
                                {
                                    X = DefaultWidth / 2 + Math.Cos(r + rotation) * size * s,
                                    Y = DefaultHeight / 2 + (Math.Sin(r + rotation) * size * s * yawn - size * k.z * yawn2)
                                };
                            }
                        ).ToArray();

                        vertex.d.SetBounds(vvx);
                        SortHints.Add(new { y = vvx.Average(k => k.Y), e = (FrameworkElement)vertex.d.g2 });
                    }
                    #endregion

                    #region floor
                    {
                        var vv = new[] { 
                            point(-5, -5, 0),
                            point(5, -5, 0),
                            point(5, 5, 0),
                            point(-5, 5, 0),
                        };

                        var vvx = vv.Select(
                            k =>
                            {
                                var s = new Vector(k.x, k.y).Length;
                                var r = new Point { X = k.x, Y = k.y }.GetRotation();

                                return new Point
                                {
                                    X = DefaultWidth / 2 + Math.Cos(r + rotation) * size * s,
                                    Y = DefaultHeight / 2 + (Math.Sin(r + rotation) * size * s * yawn - size * k.z * yawn2)
                                };
                            }
                        ).ToArray();

                        vertex.floor.SetBounds(vvx);
                        SortHints.Add(new { y = vvx.Average(k => k.Y), e = (FrameworkElement)vertex.floor.g2 });
                    }
                    #endregion

                    #region sand_a
                    {
                        var vv = new[] { 
                            point(-15, -5, -2),
                            point(-5, -5, 0),
                            point(-15, 5, -2),
                            point(-5, 5, 0),
                        };

                        var vvx = vv.Select(
                            k =>
                            {
                                var s = new Vector(k.x, k.y).Length;
                                var r = new Point { X = k.x, Y = k.y }.GetRotation();

                                var value = new Point
                                {
                                    X = DefaultWidth / 2 + Math.Cos(r + rotation) * size * s,
                                    Y = DefaultHeight / 2 + (Math.Sin(r + rotation) * size * s * yawn - size * k.z * yawn2)
                                };

                                //Console.WriteLine(value);

                                return value;
                            }
                        ).ToArray();

                        vertex.sand_a.SetBounds(vvx);
                        SortHints.Add(new { y = vvx.Average(k => k.Y), e = (FrameworkElement)vertex.sand_a.g2 });
                    }
                    #endregion

                    #region sand_b
                    {
                        var vv = new[] { 
                            point(-15, -15, -2),
                            point(-5, -15, -2),
                            point(-15, -5, -2),
                            point(-5, -5, 0),
                        };


                        var vvx = vv.Select(
                            k =>
                            {
                                var s = new Vector(k.x, k.y).Length;
                                var r = new Point { X = k.x, Y = k.y }.GetRotation();

                                return new Point
                                {
                                    X = DefaultWidth / 2 + Math.Cos(r + rotation) * size * s,
                                    Y = DefaultHeight / 2 + (Math.Sin(r + rotation) * size * s * yawn - size * k.z * yawn2)
                                };
                            }
                        ).ToArray();

                        vertex.sand_b.SetBounds(vvx);
                        SortHints.Add(new { y = vvx.Average(k => k.Y), e = (FrameworkElement)vertex.sand_b.g2 });
                    }
                    #endregion

                    #region sand_c
                    {
                        var vv = new[] { 
                            point(-5, -15, -2),
                            point(5, -15, -2),
                            point(-5, -5, 0),
                            point(5, -5, 0),
                        };


                        var vvx = vv.Select(
                            k =>
                            {
                                var s = new Vector(k.x, k.y).Length;
                                var r = new Point { X = k.x, Y = k.y }.GetRotation();

                                return new Point
                                {
                                    X = DefaultWidth / 2 + Math.Cos(r + rotation) * size * s,
                                    Y = DefaultHeight / 2 + (Math.Sin(r + rotation) * size * s * yawn - size * k.z * yawn2)
                                };
                            }
                        ).ToArray();

                        vertex.sand_c.SetBounds(vvx);
                        SortHints.Add(new { y = vvx.Average(k => k.Y), e = (FrameworkElement)vertex.sand_c.g2 });
                    }
                    #endregion

                    #region sand_wall
                    {
                        var vv = new[] { 
                            point(-5, -15, -2),
                            point(5, -15, -2),
                            point(-5, -15, -12),
                            point(5, -15, -12),
                        };


                        var vvx = vv.Select(
                            k =>
                            {
                                var s = new Vector(k.x, k.y).Length;
                                var r = new Point { X = k.x, Y = k.y }.GetRotation();

                                return new Point
                                {
                                    X = DefaultWidth / 2 + Math.Cos(r + rotation) * size * s,
                                    Y = DefaultHeight / 2 + (Math.Sin(r + rotation) * size * s * yawn - size * k.z * yawn2)
                                };
                            }
                        ).ToArray();

                        vertex.sand_wall.SetBounds(vvx);
                        SortHints.Add(new { y = vvx.Average(k => k.Y), e = (FrameworkElement)vertex.sand_wall.g2 });
                    }
                    #endregion
                    //var SortVertex = SortHints.OrderBy(k => k.y).ToArray();

                    //foreach (var k in SortVertex)
                    //{
                    //    k.e.Orphanize();
                    //}

                    //foreach (var k in SortVertex)
                    //{
                    //    k.e.AttachTo(this);
                    //}
                };

            timer.Tick += delegate { AtTick(); };
            timer.Interval = TimeSpan.FromMilliseconds(1000 / 30);
            timer.Start();

            //AtTick();
        }




        public class TransformerControl
        {
            public Action<double> SetOpacity;
            public Action<ImageSource> SetSource;

            public Action<Point[]> SetBounds;

            public Action HideVisuals;

            public Action HideMirror;

            public Image g2;
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
                Source = new Avalon.Images._17().Source,
                Opacity = 0.7,
            }.AttachTo(this).MoveTo(gx, gy);

            var g2 = new Image
            {
                Width = gw,
                Height = gh,
                Source = new Avalon.Images._17().Source,
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
                    //Console.WriteLine("Update");

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
                    // Reset
                    R.MoveTo(gx + gw, gy);
                    G.MoveTo(gx, gy + gh);
                    Y.MoveTo(gx + gw, gy + gh);
                };

            //AsMovableByMouse(R, Update);
            //AsMovableByMouse(G, Update);
            //AsMovableByMouse(M, Update);
            //AsMovableByMouse(Y, Update);

            var RotationInfo = new { x = 0.0, y = 0.0 };

            RotationInfo = null;

            #region UpdateRotation
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
                            return new Vector(x, y).Length;
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

                    // UpdateRotation
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
            #endregion 


            //AsMovableByMouse(Black, null);
            //AsMovableByMouse(White, UpdateRotation);

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

                    // UpdateTranslation
                    R.MoveTo(p.R.x + q.x, p.R.y + q.y);
                    G.MoveTo(p.G.x + q.x, p.G.y + q.y);
                    Y.MoveTo(p.Y.x + q.x, p.Y.y + q.y);
                    M.MoveTo(p.M.x + q.x, p.M.y + q.y);
                    Black.MoveTo(p.Black.x + q.x, p.Black.y + q.y);
                    White.MoveTo(p.White.x + q.x, p.White.y + q.y);

                    Update();

                    TranslationInfo = p.Orange;
                };

            //var OrangeDrag = AsMovableByMouse(Orange, UpdateTranslation);

            //OrangeDrag.Enter = () => OrangeSelfAverage = false;
            //OrangeDrag.Exit = () => OrangeSelfAverage = true;

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

                        Orange.Opacity = 0.2;

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
                            //Console.WriteLine(new { i, SetBounds = args[i] });

                            e[i].MoveTo(args[i].X, args[i].Y);

                            //Console.WriteLine(new { i, x = Canvas.GetLeft( e[i]), y = Canvas.GetTop(e[i]) });

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
            //Console.WriteLine("ApplyMatrix");
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

            var s = p.ToString() + Environment.NewLine + q.ToString() + Environment.NewLine + m.ToString();
            //Console.WriteLine(s);

            g.RenderTransform = new MatrixTransform(m.M11, m.M12, m.M21, m.M22, m.OX, m.OY);
            return s;
        }

    }

    public class Vector
    {
        public double X;
        public double Y;

        public Vector()
            : this(0, 0)
        {

        }

        public Vector(double X, double Y)
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

        public override string ToString()
        {
            return new { X, Y }.ToString();
        }
    }

    public static class MyExtensions
    {
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


        public static List<T> AsEmptyList<T>(this T e)
        {
            return new List<T>();
        }

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


        public static Func<A0, A1, A2, T> AsConstructor<A0, A1, A2, T>(this T t, Func<A0, A1, A2, T> h)
        {
            return h;
        }


    }
}
