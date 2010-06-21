// For more information visit:
// http://studio.jsc-solutions.net/

using System;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;
using System.Xml.Linq;
using InteractiveDAEScene;
using ScriptCoreLib;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Shared.Avalon.Extensions;
using System.Windows.Shapes;
using System.Windows;
using InteractiveDAEScene.AffineEngine;
using InteractiveDAEScene.Avalon.Images;

namespace InteractiveDAEScene
{


    public class ApplicationCanvas : Canvas
    {
        // http://code.google.com/p/kml-library/source/browse/#svn/trunk/KMLib/Abstract

        public const int DefaultWidth = 800;
        public const int DefaultHeight = 500;

        Canvas AffineContent;
        Canvas InfoContent;

        public ApplicationCanvas()
        {
            Width = DefaultWidth;
            Height = DefaultHeight;

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

            new Avalon.Images.white_jsc().AttachTo(this).MoveTo(
                DefaultWidth - Avalon.Images.white_jsc.ImageDefaultWidth,
                DefaultHeight - Avalon.Images.white_jsc.ImageDefaultHeight
            );

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

            AddCollada(a, new AffinePoint(0, 0, 0));
            //AddCollada(a, new AffinePoint(0, 0, 40));
            //AddCollada(a, new AffinePoint(0, 0, 40 * 2));
            //AddCollada(a, new AffinePoint(0, 0, 40 * 3));

            // bottom
            //AddCubeFace(a, "bottom",
            //    new AffinePoint(0, 0, 0),
            //    new AffinePoint(100, 0, 0),
            //    new AffinePoint(0, 100, 0),
            //    new AffinePoint(100, 100, 0),
            //    new AffinePoint(0, 0, -1)
            //);

            //AddCubeFace(a, "bottom",
            //    new AffinePoint(0, 0, 0),
            //    new AffinePoint(100, 0, 0),
            //    new AffinePoint(0, 100, 0),
            //    new AffinePoint(100, 100, 0),

            //    new AffinePoint(-100, -100, -1)
            //);


            // AddCubeFace(a, "back",
            //    new AffinePoint(-100, -100, -100),
            //    new AffinePoint(100, -100, -100),
            //    new AffinePoint(-100, 100, -100),
            //    new AffinePoint(100, 100, -100)
            //);


            //a = a.ToZoom(new AffineZoom { X = 2 });


            //a = a.ToZoom(0.8);
            //a = a.ToZoom(1.2);

            this.MouseMove +=
                (sender, args) =>
                {
                    var pp = args.GetPosition(this);

                    var Rotation = new AffineRotation
                    {
                        XY = 0.01 * pp.X * 0.5,
                        YZ = 0.02 * pp.X * 0.5,
                        XZ = 0.03 * pp.X * 0.5
                    };

                    t.Text = new
                    {
                        XY = Rotation.XZ.RadiansToDegrees(),
                        YZ = Rotation.YZ.RadiansToDegrees(),
                        XZ = Rotation.XZ.RadiansToDegrees()
                    }.ToString();

                    // rotate floor
                    var _a = a.ToZoom((DefaultHeight / 2 + pp.Y) / (DefaultHeight)).ToRotation(Rotation);



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
                }
            ;
            #endregion
        }

        private void AddCollada(AffineMesh a, AffinePoint Offset)
        {
            var COLLADA = XElement.Parse(new Data.ZSource().Text);

            var q = COLLADA
                .Elements("library_geometries")
                .Elements("geometry")
                .Elements("mesh")
                .Elements("source")
                .Elements("float_array");


            q.WithEach(
                 float_array =>
                 {
                     var float_array_i = float_array.Value.Split(' ');
                     var k = float_array_i.Select(kk => double.Parse(kk)).ToArray();

                     // bottom
                     AddCubeFace(a, "DAE",
                         new AffinePoint(k[6], k[7], k[8]),
                         new AffinePoint(k[0], k[1], k[2]),
                         new AffinePoint(k[3], k[4], k[5]),
                         new AffinePoint(k[9], k[10], k[11]),
                         Offset
                     );
                 }
            );
        }



        private void AddCubeFace(AffineMesh a, string t, AffinePoint A, AffinePoint B, AffinePoint C, AffinePoint D, AffinePoint Offset = null)
        {
            if (Offset == null)
                Offset = new AffinePoint();

            var v1 =
               new AffineVertex
               {
                   A = A + Offset,
                   B = B + Offset,
                   C = C + Offset,

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

            var v2 =
                new AffineVertex
                {
                    A = D + Offset,
                    B = C + Offset,
                    C = B + Offset,

                    Element = new Avalon.Images._19g
                    {
                        Width = 100,
                        Height = 100,
                    }.AttachTo(AffineContent),
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
