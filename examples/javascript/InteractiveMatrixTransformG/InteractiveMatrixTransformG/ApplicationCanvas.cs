using InteractiveMatrixTransformE.AffineEngine;
using InteractiveMatrixTransformF.AffineEngine;
using ScriptCoreLib.Avalon;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Shared.Avalon.Extensions;
using System;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Xml;
using System.Xml.Linq;

namespace InteractiveMatrixTransformG
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
                if (SizeChanged != 1)
                    return;

                InitializeContent();
            };

        }

        Canvas AffineContent;
        Canvas InfoContent;
        void InitializeContent()
        {

            this.ClipToBounds = true;

            new[] {
				Colors.Black,
				Colors.Blue,
				Colors.Black
			}.ToGradient(Convert.ToInt32( Height) / 2).Select(
                (c, i) =>
                    new Rectangle
                    {
                        Fill = new SolidColorBrush(c),
                        Width = Width,
                        Height = 3,
                    }.MoveTo(0, i * 2).AttachTo(this)
            ).ToArray();



            //var help = new Image
            //{
            //    Source = (KnownAssets.Path.Assets + "/help.png").ToSource()
            //}.AttachTo(this);

            //help.Opacity = 0;

      



            AffineContent = new Canvas
            {

            }.AttachTo(this);




            InfoContent = new Canvas
            {

            }.AttachTo(this);

            //var sandcount = 9;
            //var cubecount = 2;

            1000.AtDelay(
                delegate
                {
                    CreateContent(15, 2, false);
                    CreateContent(0, 0, true);
                }
            );
        }
        private void CreateContent(int sandcount, int cubecount, bool rotor)
        {
            var t = new TextBox
            {
                FontSize = 10,
                Text = "powered by jsc",
                BorderThickness = new Thickness(0),
                Foreground = 0xffffffff.ToSolidColorBrush(),
                Background = Brushes.Transparent,
                IsReadOnly = true,
                Width = Width
            }.MoveTo(8, 8).AttachTo(InfoContent);

            if (rotor)
                t.MoveTo(8, 32);

            var a = new AffineMesh();


            var _17 = new Avalon.Images._17().Source;
            var _17g = new Avalon.Images._17g().Source;
            var _18 = new Avalon.Images._18().Source;
            var _18g = new Avalon.Images._18g().Source;




            for (int cubex = -cubecount; cubex < cubecount; cubex++)
            {


                AddCube(a, _18, _18g, new AffinePoint(-1 + cubex * 4, -1, 0));
                AddCube(a, _18, _18g, new AffinePoint(-1 + cubex * 4, 1, 0));

                AddCube(a, _18, _18g, new AffinePoint(1 + cubex * 4, -1, 0));
                AddCube(a, _18, _18g, new AffinePoint(1 + cubex * 4, 1, 0));

                AddCube(a, _17, _17g, new AffinePoint(-1 + cubex * 4, -1, 1));
                AddCube(a, _17, _17g, new AffinePoint(-1 + cubex * 4, 1, 1));

                AddCube(a, _17, _17g, new AffinePoint(1 + cubex * 4, -1, 1));
                AddCube(a, _17, _17g, new AffinePoint(1 + cubex * 4, 1, 1));


            }


            var top = default(AffineMesh);

            if (rotor)
                top = AddCube(a, _18, _18g, new AffinePoint(0, 0, 2));

            var topdef = top;

            if (!rotor)
                for (int ix = -sandcount; ix <= sandcount; ix++)
                    for (int iy = -sandcount; iy <= sandcount; iy++)
                    {
                        AddCubeFace(a,
                            new Avalon.Images.sandv().Source,
                            new Avalon.Images.sandv().Source,
                            new AffinePoint(-100 + ix * 200, -100, -100 + iy * 200),
                            new AffinePoint(100 + ix * 200, -100, -100 + iy * 200),
                            new AffinePoint(-100 + ix * 200, -100, 100 + iy * 200),
                            new AffinePoint(100 + ix * 200, -100, 100 + iy * 200)
                        );
                    }





            //z			a = a.ToZoom(0.5);


            //a = a.ToZoom(0.8);
            //a = a.ToZoom(1.2);

            var _a = a;
            var Rotation = new AffineRotation
            {
                XY = (180 + 22).DegreesToRadians(),
                YZ = -22.DegreesToRadians(),
                XZ = 45.DegreesToRadians()
            };

            var MouseOffset0 = 0.0;
            var MouseOffset1 = 0.0;
            var MouseOffset2 = 0.0;

            var MouseMode = 0;

            this.MouseLeftButtonUp +=
                delegate
                {
                    MouseMode++;
                };

            this.MouseMove +=
                (sender, args) =>
                {

                    var pp = args.GetPosition(this);

                    if ((MouseMode % 4) == 1)
                    {
                        MouseOffset1 = pp.X;
                        Rotation = new AffineRotation
                        {
                            XZ = Rotation.XZ,
                            YZ = Rotation.YZ,

                            XY = 0.01 * (pp.X - MouseOffset0) * 2,

                        };
                    }

                    if ((MouseMode % 4) == 2)
                    {
                        MouseOffset2 = pp.X;
                        Rotation = new AffineRotation
                        {
                            XY = Rotation.XY,
                            YZ = Rotation.YZ,


                            XZ = 0.01 * (pp.X - MouseOffset1) * 2,

                        };
                    }

                    if ((MouseMode % 4) == 3)
                    {
                        MouseOffset0 = pp.X;
                        Rotation = new AffineRotation
                        {
                            XY = Rotation.XY,
                            XZ = Rotation.XZ,

                            YZ = 0.01 * (pp.X - MouseOffset2) * 2,

                        };
                    }

                };


            Action<int> nextframe = null;

            var sw2 = new Stopwatch();
            sw2.Start();

            nextframe =
                c =>
                {

                    sw2.Stop();

                    var sw = new Stopwatch();

                    sw.Start();

                    if (top != null)
                    {
                        a.Meshes.Remove(top);

                        top = topdef.ToTranslation(
                            new AffinePoint(0, -200 * 3, 0)
                        ).ToRotation(
                            new AffineRotation { XZ = 0.01 * c }
                        ).ToTranslation(
                            new AffinePoint(0, 200 * 3, 0)
                        );

                        a.Meshes.Add(top);
                    }



                    // rotate floor

                    if (rotor)
                    {
                        _a = a.ToZoom(0.5).ToRotation(Rotation);
                        Show(_a);
                    }
                    else
                        if (c == 1)
                        {
                            _a = a.ToZoom(0.5).ToRotation(Rotation);
                            Show(_a);
                        }

                    sw.Stop();

                    t.Text = new
                    {
                        rotor,
                        ShowCounter,
                        XY = Rotation.XY.RadiansToDegrees() % 360,
                        YZ = Rotation.YZ.RadiansToDegrees() % 360,
                        XZ = Rotation.XZ.RadiansToDegrees() % 360,
                        Renderer = sw.ElapsedMilliseconds + "ms",
                        Other = sw2.ElapsedMilliseconds + "ms"
                    }.ToString();

                    sw2 = new Stopwatch();
                    //sw2.Reset();
                    sw2.Start();
                    1.AtDelay(() => nextframe(c + 1));

                    //this.UpdateLayout();
                }
            ;

            1.AtDelay(() => nextframe(0));
        }


        int ShowCounter;

        private void Show(AffineMesh _a)
        {
            ShowCounter++;
            double Zoom = 0.2;

            // js: 130
            // as: 70
            // c#: 30

            // simple z sort
            // js: 63
            // as: 28
            // c#: 27

            // js: 61
            // as: 26

            // n.Vertecies = v.OrderBy(k => k.Center.Z).ToList();
            foreach (var k in _a.GetCombinedVertices().OrderBy(k => k.Center.Z))
            //foreach (var k in _a.GetSortedCombinedVertices())
            {
                if (k != null)
                {

                    k.Element.Orphanize();
                    k.Element.AttachTo(AffineContent);


                    k.Element.RenderTransform = new AffineTransform
                    {
                        Left = 0,
                        Top = 0,
                        Width = k.ElementWidth,
                        Height = k.ElementHeight,

                        X1 = k.B.X * Zoom + Width / 2,
                        Y1 = k.B.Y * Zoom + Height / 2,

                        X2 = k.C.X * Zoom + Width / 2,
                        Y2 = k.C.Y * Zoom + Height / 2,

                        X3 = k.A.X * Zoom + Width / 2,
                        Y3 = k.A.Y * Zoom + Height / 2,


                    };

                }
                //((Action<AffineVertex>)k.Tag)(k);
            }


        }

        private AffineMesh AddCube(AffineMesh context, ImageSource Source, ImageSource Source2, AffinePoint TopLocation)
        {
            var a = new AffineMesh();


            // front
            AddCubeFace(a, Source, Source2,
               new AffinePoint(-100, -100, 100),
               new AffinePoint(100, -100, 100),
               new AffinePoint(-100, 100, 100),
               new AffinePoint(100, 100, 100)
           );

            // right
            AddCubeFace(a, Source, Source2,
               new AffinePoint(100, -100, 100),
               new AffinePoint(100, -100, -100),
               new AffinePoint(100, 100, 100),
               new AffinePoint(100, 100, -100)
           );

            // left
            AddCubeFace(a, Source, Source2,
               new AffinePoint(-100, -100, 100),
               new AffinePoint(-100, -100, -100),
               new AffinePoint(-100, 100, 100),
               new AffinePoint(-100, 100, -100)
           );

            // back
            AddCubeFace(a, Source, Source2,
               new AffinePoint(-100, -100, -100),
               new AffinePoint(100, -100, -100),
               new AffinePoint(-100, 100, -100),
               new AffinePoint(100, 100, -100)
           );

            // top
            AddCubeFace(a, Source, Source2,
               new AffinePoint(-100, 100, -100),
               new AffinePoint(100, 100, -100),
               new AffinePoint(-100, 100, 100),
               new AffinePoint(100, 100, 100)
           );

            var _a = a.ToTranslation(new AffinePoint(TopLocation.X * 200, TopLocation.Z * 200, TopLocation.Y * 200));

            context.Meshes.Add(
                _a
            );

            return _a;
        }

        private void AddCubeFace(AffineMesh a, ImageSource Source, ImageSource Source2, AffinePoint A, AffinePoint B, AffinePoint C, AffinePoint D)
        {
            var v1 =
               new AffineVertex
               {
                   A = A,
                   B = B,
                   C = C,

                   Element = new Image
                   {
                       Width = 100,
                       Height = 100,
                       Source = Source
                   }.AttachTo(AffineContent),
                   ElementWidth = 100,
                   ElementHeight = 100
               };



            a.Vertecies.Add(v1);

            var v2 =
                new AffineVertex
                {
                    A = D,
                    B = C,
                    C = B,

                    Element = new Image
                    {
                        Width = 100,
                        Height = 100,
                        Source = Source2
                    }.AttachTo(AffineContent),
                    ElementWidth = 100,
                    ElementHeight = 100
                };



            a.Vertecies.Add(v2);

        }



        private void AddCubeFace(AffineMesh a, string t, AffinePoint A, AffinePoint B, AffinePoint C, AffinePoint D)
        {
            var v1 =
               new AffineVertex
               {
                   A = A,
                   B = B,
                   C = C,

                   Element = new Avalon.Images._17
                   {
                       Width = 100,
                       Height = 100,
                   }.AttachTo(AffineContent),
                   ElementWidth = 100,
                   ElementHeight = 100
               };

            a.Vertecies.Add(v1);

            var v2 =
                new AffineVertex
                {
                    A = D,
                    B = C,
                    C = B,

                    Element = new Avalon.Images._17g
                    {
                        Width = 100,
                        Height = 100,
                    }.AttachTo(AffineContent),
                    ElementWidth = 100,
                    ElementHeight = 100
                };



            a.Vertecies.Add(v2);

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
