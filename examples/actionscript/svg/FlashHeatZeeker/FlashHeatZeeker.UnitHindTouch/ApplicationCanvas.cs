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

namespace FlashHeatZeeker.UnitHindTouch
{
    public class ApplicationCanvas : Canvas
    {
        public readonly Rectangle r = new Rectangle();

        public Rectangle 
            
                    up, 
            space,
                    down, 
            
            control,
            left, right;

        public ApplicationCanvas()
        {
            r.Fill = Brushes.Red;
            r.AttachTo(this);
            r.MoveTo(0, 0);
            this.SizeChanged += (s, e) => r.SizeTo(this.Width, this.Height);



            space = new Rectangle
            {
                Fill = Brushes.White,
                Opacity = 0.5
            }.AttachTo(this);
            space.SizeTo(64, 64 + 4 + 64);
            this.SizeChanged += (s, e) => space.MoveTo(this.Width - 64 - 4 - 64 - 4, this.Height - 64 - 4 - 64 - 4);


            up = new Rectangle
            {
                Fill = Brushes.White,
                Opacity = 0.5
            }.AttachTo(this);
            up.SizeTo(64, 64);
            this.SizeChanged += (s, e) => up.MoveTo(this.Width - 64 - 4, this.Height - 64 - 4 - 64 - 4);

            down = new Rectangle
            {
                Fill = Brushes.White,
                Opacity = 0.5
            }.AttachTo(this);
            down.SizeTo(64, 64);
            this.SizeChanged += (s, e) => down.MoveTo(this.Width - 64 - 4, this.Height - 64 - 4);




            control = new Rectangle
            {
                Fill = Brushes.White,
                Opacity = 0.5
            }.AttachTo(this);
            control.SizeTo(64 + 4 + 64, 64);
            this.SizeChanged += (s, e) => control.MoveTo(4, this.Height - 64 - 4 - 64 - 4);

            left = new Rectangle
            {
                Fill = Brushes.White,
                Opacity = 0.5
            }.AttachTo(this);
            left.SizeTo(64, 64);
            this.SizeChanged += (s, e) => left.MoveTo(4, this.Height - 64 - 4);

            right = new Rectangle
            {
                Fill = Brushes.White,
                Opacity = 0.5
            }.AttachTo(this);
            right.SizeTo(64, 64);
            this.SizeChanged += (s, e) => right.MoveTo(4 + 64 + 4, this.Height - 64 - 4);

        }

    }
}
