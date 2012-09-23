using InteractiveMatrixTransformE.AffineEngine;
using InteractiveMatrixTransformF.AffineEngine;
using ScriptCoreLib.Avalon;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Shared.Avalon.Extensions;
using System;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Xml;
using System.Xml.Linq;

namespace InteractiveMatrixTransformF
{
    public class ApplicationCanvas : Canvas
    {
        public readonly Rectangle r = new Rectangle();

        public const int DefaultWidth = 800;
        public const int DefaultHeight = 500;

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

            //new[] {
            //    Colors.Black,
            //    Colors.Blue,
            //    Colors.Black
            //}.ToGradient(Convert.ToInt32( Height) / 2).Select(
            //    (c, i) =>
            //        new Rectangle
            //        {
            //            Fill = new SolidColorBrush(c),
            //            Width = Width,
            //            Height = 3,
            //        }.MoveTo(0, i * 2).AttachTo(this)
            //).ToArray();



            //var help = new Image
            //{
            //    Source = (KnownAssets.Path.Assets + "/help.png").ToSource()
            //}.AttachTo(this);

            //help.Opacity = 0;

            var img = new Avalon.Images.jsc().MoveTo(Width - 128, Height - 128).AttachTo(this);

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




            AffineContent = new Canvas
            {

            }.AttachTo(this);




            InfoContent = new Canvas
            {

            }.AttachTo(this);



            var a = new AffineMesh();





            // front
            AddCubeFace(a, "front",
               new AffinePoint(-100, -100, 100),
               new AffinePoint(100, -100, 100),
               new AffinePoint(-100, 100, 100),
               new AffinePoint(100, 100, 100)
           );

            // right
            AddCubeFace(a, "right",
               new AffinePoint(100, -100, 100),
               new AffinePoint(100, -100, -100),
               new AffinePoint(100, 100, 100),
               new AffinePoint(100, 100, -100)
           );

            // left
            AddCubeFace(a, "left",
               new AffinePoint(-100, -100, 100),
               new AffinePoint(-100, -100, -100),
               new AffinePoint(-100, 100, 100),
               new AffinePoint(-100, 100, -100)
           );

            // back
            AddCubeFace(a, "back",
               new AffinePoint(-100, -100, -100),
               new AffinePoint(100, -100, -100),
               new AffinePoint(-100, 100, -100),
               new AffinePoint(100, 100, -100)
           );

            // top
            AddCubeFace(a, "top",
               new AffinePoint(-100, 100, -100),
               new AffinePoint(100, 100, -100),
               new AffinePoint(-100, 100, 100),
               new AffinePoint(100, 100, 100)
           );

            // bottom
            AddCubeFace(a, "bottom",
                new AffinePoint(-100, -100, -100),
                new AffinePoint(100, -100, -100),
                new AffinePoint(-100, -100, 100),
                new AffinePoint(100, -100, 100)
            );

            // AddCubeFace(a, "back",
            //    new AffinePoint(-100, -100, -100),
            //    new AffinePoint(100, -100, -100),
            //    new AffinePoint(-100, 100, -100),
            //    new AffinePoint(100, 100, -100)
            //);


            //a = a.ToZoom(new AffineZoom { X = 2 });


            //a = a.ToZoom(0.8);
            //a = a.ToZoom(1.2);

            var pp_X = 0.0;
            var pp = new Point(Width / 2, Height / 2);
            Action Update =
                () =>
                {
                    var Rotation = new AffineRotation
                    {
                        XY = 0.01 * pp.X * 0.5,
                        YZ = 0.02 * pp.X * 0.5,
                        XZ = 0.03 * pp.X * 0.5 + pp_X
                    };

                    t.Text = new
                    {
                        XY = Rotation.XZ.RadiansToDegrees(),
                        YZ = Rotation.YZ.RadiansToDegrees(),
                        XZ = Rotation.XZ.RadiansToDegrees()
                    }.ToString();

                    // rotate floor
                    var _a = a.ToZoom((Height / 2 + pp.Y) / (Height)).ToRotation(Rotation);



                    foreach (var k in _a.Vertecies)
                    {
                        k.Element.Orphanize();
                        k.Element.AttachTo(AffineContent);
                        k.Element.RenderTransform = new AffineTransform
                        {
                            Left = 0,
                            Top = 0,
                            Width = k.ElementWidth,
                            Height = k.ElementHeight,

                            X1 = k.B.X + Width / 2,
                            Y1 = k.B.Y + Height / 2,

                            X2 = k.C.X + Width / 2,
                            Y2 = k.C.Y + Height / 2,

                            X3 = k.A.X + Width / 2,
                            Y3 = k.A.Y + Height / 2,


                        };

                        //((Action<AffineVertex>)k.Tag)(k);
                    }
                };
            this.MouseMove +=
                (sender, args) =>
                {
                     pp = args.GetPosition(this);

                    Update();
                };

            var tt = new DispatcherTimer();

            tt.Tick +=
                delegate
                {
                    pp_X += 0.02;
                    Update();

                
                };

            tt.Interval = TimeSpan.FromMilliseconds(1000 / 30);
            tt.Start();

        }




        private void AddCubeFace(AffineMesh a, string t, AffinePoint A, AffinePoint B, AffinePoint C, AffinePoint D)
        {
            var v1 =
               new AffineVertex
               {
                   A = A,
                   B = B,
                   C = C,

                   Element = new Avalon.Images._17 {
                       Width = 100,
                       Height = 100,
                   
                   }.AttachTo(AffineContent),
                   ElementWidth = 100,
                   ElementHeight = 100
               };

            //var t1 = new TextBox { Text = t, Foreground = Brushes.Blue }.AttachTo(InfoContent);

            //v1.Tag = new Action<AffineVertex>(
            //    k =>
            //    {
            //        t1.Text = t + " " + Convert.ToInt32(k.Center.Z);
            //        t1.MoveTo(k.Center.X + DefaultWidth / 2, k.Center.Y + DefaultHeight / 2);

            //    }
            //);

            a.Add(v1);

            var v2 =
                new AffineVertex
                {
                    A = D,
                    B = C,
                    C = B,

                    Element = new Avalon.Images._19g
                        {
                            Width = 100,
                            Height = 100,
                        }
                    .AttachTo(AffineContent),
                    ElementWidth = 100,
                    ElementHeight = 100
                };

            //v2.Element.Opacity = 0.5;

            //var t2 = new TextBox { Text = t }.AttachTo(InfoContent);

            //v2.Tag = new Action<AffineVertex>(
            //    k =>
            //    {
            //        t2.Text = t + " " + Convert.ToInt32(k.Center.Z);
            //        t2.MoveTo(k.Center.X + DefaultWidth / 2, k.Center.Y + DefaultHeight / 2);

            //    }
            //);

            a.Add(v2);

        }

    }
}
