using AvalonAffineTexturedCube.AffineEngine;
using ScriptCoreLib.Avalon;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
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

namespace AvalonAffineTexturedCube
{
    public class ApplicationCanvas : Canvas
    {

        public ApplicationCanvas()
        {
            var SizeChanged = false;
            this.SizeChanged += (s, e) =>
            {
                if (Width > 200)
                    if (Height > 200)
                    {
                        if (SizeChanged)
                            return;

                        SizeChanged = true;

                        InitializeContent();
                    }
            };

        }

        Canvas AffineContent;
        Canvas InfoContent;

        public void InitializeContent()
        {
            var DefaultWidth = Convert.ToInt32(Width);
            var DefaultHeight = Convert.ToInt32(Height);

            this.ClipToBounds = true;

            new[] {
				Colors.Black,
				Colors.Blue,
				Colors.Black
			}.ToGradient(DefaultHeight / 2).Select(
                (c, i) =>
                    new Rectangle
                    {
                        Fill = new SolidColorBrush(c),
                        Width = DefaultWidth,
                        Height = 3,
                    }.MoveTo(0, i * 2).AttachTo(this)
            ).ToArray();

            var logo = new Avalon.Images.white_jsc().AttachTo(this);

            logo.MoveTo(
                DefaultWidth - Avalon.Images.white_jsc.ImageDefaultWidth,
                DefaultHeight - Avalon.Images.white_jsc.ImageDefaultHeight
            );

            this.SizeChanged +=
                delegate
                {
                    logo.MoveTo(
                          this.Width - Avalon.Images.white_jsc.ImageDefaultWidth,
                          this.Height - Avalon.Images.white_jsc.ImageDefaultHeight
                      );

                };

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

            #region Initialize
            AffineContent = new Canvas
            {

            }.AttachTo(this);




            InfoContent = new Canvas
            {

            }.AttachTo(this);



            var a = new AffineMesh();




            for (int i = -5; i <= 5; i++)
            {
                if (Math.Abs(i) > 1)
                {
                    AddCube(a, new AffinePoint(210 * i, 0, 0));
                    AddCube(a, new AffinePoint(0, 210 * i, 0));
                    AddCube(a, new AffinePoint(0, 0, 210 * i));
                }
            }

            // AddCubeFace(a, "back",
            //    new AffinePoint(-100, -100, -100),
            //    new AffinePoint(100, -100, -100),
            //    new AffinePoint(-100, 100, -100),
            //    new AffinePoint(100, 100, -100)
            //);


            //a = a.ToZoom(new AffineZoom { X = 2 });


            //a = a.ToZoom(0.8);
            //a = a.ToZoom(1.2);

            var pp = new Point();

            // while jsc initializes local structs it does not do that for fields
            pp = new Point(0, 0);

            var pp_XZ = 0.0;

            Action Update =
                delegate
                {
                    var Rotation = new AffineRotation
                 {
                     XY = 0.01 * pp.X * 0.5,
                     YZ = 0.02 * pp.X * 0.5,
                     XZ = 0.03 * pp.X * 0.5 + pp_XZ
                 };

                    t.Text = new
                    {
                        XY = Rotation.XZ.RadiansToDegrees(),
                        YZ = Rotation.YZ.RadiansToDegrees(),
                        XZ = Rotation.XZ.RadiansToDegrees()
                    }.ToString();

                    // rotate floor
                    var _a = a.ToZoom(0.2 * (DefaultHeight / 2 + pp.Y) / (DefaultHeight)).ToRotation(Rotation);



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

                            X1 = k.B.X + DefaultWidth / 2,
                            Y1 = k.B.Y + DefaultHeight / 2,

                            X2 = k.C.X + DefaultWidth / 2,
                            Y2 = k.C.Y + DefaultHeight / 2,

                            X3 = k.A.X + DefaultWidth / 2,
                            Y3 = k.A.Y + DefaultHeight / 2,


                        };

                        //((Action<AffineVertex>)k.Tag)(k);
                    }
                };

            this.MouseMove +=
                (sender, args) =>
                {
                    pp = args.GetPosition(this);

                    Update();

                }
            ;
            #endregion


            var tt = new DispatcherTimer();

            tt.Tick +=
                delegate
                {
                    pp_XZ += 0.02;
                    Update();
                };

            tt.Interval = TimeSpan.FromMilliseconds(1000 / 30);

            tt.Start();
        }

        private void AddCube(AffineMesh a, AffinePoint Offset = null)
        {

            // front
            AddCubeFace(a, "front",
               new AffinePoint(-100, -100, 100) + Offset,
               new AffinePoint(100, -100, 100) + Offset,
               new AffinePoint(-100, 100, 100) + Offset,
               new AffinePoint(100, 100, 100) + Offset
           );

            // right
            AddCubeFace18(a, "right",
               new AffinePoint(100, -100, 100) + Offset,
               new AffinePoint(100, -100, -100) + Offset,
               new AffinePoint(100, 100, 100) + Offset,
               new AffinePoint(100, 100, -100) + Offset
           );

            // left
            AddCubeFace18(a, "left",
               new AffinePoint(-100, -100, 100) + Offset,
               new AffinePoint(-100, -100, -100) + Offset,
               new AffinePoint(-100, 100, 100) + Offset,
               new AffinePoint(-100, 100, -100) + Offset
           );

            // back
            AddCubeFace18(a, "back",
               new AffinePoint(-100, -100, -100) + Offset,
               new AffinePoint(100, -100, -100) + Offset,
               new AffinePoint(-100, 100, -100) + Offset,
               new AffinePoint(100, 100, -100) + Offset
           );

            // top
            AddCubeFace18(a, "top",
               new AffinePoint(-100, 100, -100) + Offset,
               new AffinePoint(100, 100, -100) + Offset,
               new AffinePoint(-100, 100, 100) + Offset,
               new AffinePoint(100, 100, 100) + Offset
           );

            // bottom
            AddCubeFace18(a, "bottom",
                new AffinePoint(-100, -100, -100) + Offset,
                new AffinePoint(100, -100, -100) + Offset,
                new AffinePoint(-100, -100, 100) + Offset,
                new AffinePoint(100, -100, 100) + Offset
            );
        }



        private void AddCubeFace(
            AffineMesh a, string t, AffinePoint A, AffinePoint B, AffinePoint C, AffinePoint D)
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

            //var t1 = new TextBox { Text = t, Foreground = Brushes.Blue }.AttachTo(InfoContent);

            //v1.Tag = new Action<AffineVertex>(
            //    k =>
            //    {
            //        t1.Text = t + " " + Convert.ToInt32(k.Center.Z);
            //        t1.MoveTo(k.Center.X + DefaultWidth / 2, k.Center.Y + DefaultHeight / 2);

            //    }
            //);

            a.Add(v1);

            //var v2 =
            //    new AffineVertex
            //    {
            //        A = D,
            //        B = C,
            //        C = B,

            //        Element = new Avalon.Images._17.AffineTriangle2
            //        {
            //            Width = 100,
            //            Height = 100,
            //        }.AttachTo(AffineContent),
            //        ElementWidth = 100,
            //        ElementHeight = 100
            //    };

            ////v2.Element.Opacity = 0.5;

            ////var t2 = new TextBox { Text = t }.AttachTo(InfoContent);

            ////v2.Tag = new Action<AffineVertex>(
            ////    k =>
            ////    {
            ////        t2.Text = t + " " + Convert.ToInt32(k.Center.Z);
            ////        t2.MoveTo(k.Center.X + DefaultWidth / 2, k.Center.Y + DefaultHeight / 2);

            ////    }
            ////);

            //a.Add(v2);

        }
        private void AddCubeFace18(
       AffineMesh a, string t, AffinePoint A, AffinePoint B, AffinePoint C, AffinePoint D)
        {
            var v1 =
               new AffineVertex
               {
                   A = A,
                   B = B,
                   C = C,

                   Element = new Avalon.Images._18
                   {
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

            //var v2 =
            //    new AffineVertex
            //    {
            //        A = D,
            //        B = C,
            //        C = B,

            //        Element = new Avalon.Images._18.AffineTriangle2
            //        {
            //            Width = 100,
            //            Height = 100,
            //        }.AttachTo(AffineContent),
            //        ElementWidth = 100,
            //        ElementHeight = 100
            //    };

            ////v2.Element.Opacity = 0.5;

            ////var t2 = new TextBox { Text = t }.AttachTo(InfoContent);

            ////v2.Tag = new Action<AffineVertex>(
            ////    k =>
            ////    {
            ////        t2.Text = t + " " + Convert.ToInt32(k.Center.Z);
            ////        t2.MoveTo(k.Center.X + DefaultWidth / 2, k.Center.Y + DefaultHeight / 2);

            ////    }
            ////);

            //a.Add(v2);

        }

    }
}
