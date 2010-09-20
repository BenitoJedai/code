using ScriptCoreLib.Extensions;
using ScriptCoreLib.Shared.Avalon.Extensions;
using System;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Xml;
using System.Xml.Linq;

namespace AvalonWindowlessWindow
{
    public class ApplicationCanvas : Canvas
    {
        public readonly Image r = new Avalon.Images.dual_monitor_wallpaper();

        public class Position
        {
            public double Left;
            public double Top;
            public double Width;
            public double Height;
        }

        public ApplicationCanvas()
        {
            // http://msdn.microsoft.com/en-us/library/system.windows.controls.childwindow(VS.95).aspx
            // http://jsprunger.com/getting-started-with-lucene-net/

            //r.Opacity = 0.1;

            // if we only had this...
            // http://forum.tabletpcreview.com/microsoft-windows-7-forum/34430-windows-7-multi-touch-magnifier.html
            // http://www.fredshead.info/2010/02/windows-7-magnifier.html

            r.Stretch = Stretch.Fill;
            r.AttachTo(this);
            r.MoveTo(8, 8);
            this.SizeChanged += (s, e) => r.SizeTo(this.Width - 16.0, this.Height - 16.0);


            // DPI? :)


            CreateWindow(

          new Position { Top = 72, Left = 48, Width = 100, Height = 100 }

         );

            var p2 = new Position { Top = 128, Left = 72, Width = 400, Height = 300 };

            CreateWindow(

                p2,

                value => Selection = value

            );
        }

        public WindowInfo Selection;

        public class WindowInfo
        {
            public Position WindowLocation;
            public Canvas GlassArea;
            public AnimatedOpacity<Canvas> GlassOpacity;

            public Action WindowLocationChanged;

            public Action Orphanize;
            public Action Attach;
        }

        public void CreateWindow(Position WindowLocation, Action<WindowInfo> Yield = null)
        {
            var GlassArea = new Canvas();

            GlassArea.AttachTo(this);
            GlassArea.MoveTo(WindowLocation.Left, WindowLocation.Top);
            GlassArea.SizeTo(WindowLocation.Width, WindowLocation.Height);
            GlassArea.ClipToBounds = true;

            for (int i = 0; i < WindowLocation.Width; i += 456)
                for (int j = 0; j < WindowLocation.Height; j += 696)
                {
                    var i2 = new Avalon.Images.s_bg().SizeTo(456, 696);

                    i2.MoveTo(i, j);
                    i2.Opacity = 0.8;

                    i2.AttachTo(GlassArea);
                }

            var GlassOpacity = GlassArea.ToAnimatedOpacity();

            GlassOpacity.Opacity = 1;
            var w = new WindowInfo { GlassArea = GlassArea, GlassOpacity = GlassOpacity, WindowLocation = WindowLocation };


            var Left = new Avalon.Images.s_l
            {
                Stretch = Stretch.Fill
            }
            .AttachTo(this);


            var Top = new Avalon.Images.s_t
            {
                Stretch = Stretch.Fill
            }
            .AttachTo(this);




            var Right = new Avalon.Images.s_r
            {
                Stretch = Stretch.Fill
            }.AttachTo(this);




            var Bottom = new Avalon.Images.s_b
            {
                Stretch = Stretch.Fill
            }
             .AttachTo(this);


            var TopLeft = new Avalon.Images.s_tl
            {
                Stretch = Stretch.Fill
            }
           .AttachTo(this);


            var BottomRight = new Avalon.Images.s_br
            {
                Stretch = Stretch.Fill
            }
             .AttachTo(this);



            var TopRight = new Avalon.Images.s_tr
            {
                Stretch = Stretch.Fill
            }
           .AttachTo(this);


            var BottomLeft = new Avalon.Images.s_bl
            {
                Stretch = Stretch.Fill
            }
           .AttachTo(this);


            w.WindowLocationChanged +=
               delegate
               {
                   Left.MoveTo(WindowLocation.Left - 16, WindowLocation.Top + 6)
                   .SizeTo(16, WindowLocation.Height - 6 - 4);

                   Top.MoveTo(WindowLocation.Left + 6, WindowLocation.Top - 16)
          .SizeTo(WindowLocation.Width - 6 - 4, 22);

                   Right.MoveTo(WindowLocation.Left + WindowLocation.Width, WindowLocation.Top + 6)
               .SizeTo(20, WindowLocation.Height - 4 - 6);


                   Bottom.MoveTo(WindowLocation.Left + 5, WindowLocation.Top + WindowLocation.Height)
                    .SizeTo(WindowLocation.Width - 4 - 5, 20);


                   TopLeft.MoveTo(WindowLocation.Left - 22 + 6, WindowLocation.Top - 22 + 6)
                   .SizeTo(22, 22);
                   BottomRight.MoveTo(WindowLocation.Left + WindowLocation.Width - 4, WindowLocation.Top + WindowLocation.Height - 4)
                .SizeTo(22, 22);
                   TopRight.MoveTo(WindowLocation.Left + WindowLocation.Width - 4, WindowLocation.Top - 22 + 6)
                  .SizeTo(22, 22);
                   BottomLeft.MoveTo(WindowLocation.Left - 22 + 5, WindowLocation.Top + WindowLocation.Height - 4)
                  .SizeTo(22, 22);

               };

            w.WindowLocationChanged();

            w.Orphanize +=
                delegate
                {
                    new FrameworkElement[]
                    {
                        Left,
                        Top,
                        Right,
                        Bottom,
                        TopLeft,
                        BottomRight,
                        TopRight,
                        BottomLeft
                    }.WithEach(k => k.Orphanize());
                };

            w.Attach +=
               delegate
               {
                   new FrameworkElement[]
                    {
                        Left,
                        Top,
                        Right,
                        Bottom,
                        TopLeft,
                        BottomRight,
                        TopRight,
                        BottomLeft
                    }.WithEach(k => k.AttachTo(this));
               };

            if (Yield != null)
            {

                Yield(
                   w
                );
            }
        }

    }
}
