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

        public ApplicationCanvas()
        {
            //r.Opacity = 0.1;

            r.Stretch = Stretch.Fill;
            r.AttachTo(this);
            r.MoveTo(8, 8);
            this.SizeChanged += (s, e) => r.SizeTo(this.Width - 16.0, this.Height - 16.0);


            // DPI? :)

            var WindowLocation = new { Top = 72, Left = 72, Width = 400, Height = 300 };

            var GlassArea = new Canvas();

            GlassArea.AttachTo(this);
            GlassArea.MoveTo(WindowLocation.Left, WindowLocation.Top);
            GlassArea.SizeTo(WindowLocation.Width, WindowLocation.Height);
            GlassArea.ClipToBounds = true;

            var i = new Avalon.Images.s_bg().SizeTo(456, 696);

            i.MoveTo(0, 0);
            i.Opacity = 0.8;

            i.AttachTo(GlassArea);

            var Left = new Avalon.Images.s_l
            {
                Stretch = Stretch.Fill
            }
            .AttachTo(this)
            .MoveTo(WindowLocation.Left - 22, WindowLocation.Top)
            .SizeTo(22, WindowLocation.Height);

            var Top = new Avalon.Images.s_t
            {
                Stretch = Stretch.Fill
            }
            .AttachTo(this)
            .MoveTo(WindowLocation.Left, WindowLocation.Top - 16 )
            .SizeTo(WindowLocation.Width, 22);

            var Right = new Avalon.Images.s_r
            {
                Stretch = Stretch.Fill
            }
              .AttachTo(this)
              .MoveTo(WindowLocation.Left + WindowLocation.Width, WindowLocation.Top)
              .SizeTo(22, WindowLocation.Height);


            var Bottom = new Avalon.Images.s_b
            {
                Stretch = Stretch.Fill
            }
             .AttachTo(this)
             .MoveTo(WindowLocation.Left, WindowLocation.Top + WindowLocation.Height )
             .SizeTo(WindowLocation.Width, 22);

        }

    }
}
