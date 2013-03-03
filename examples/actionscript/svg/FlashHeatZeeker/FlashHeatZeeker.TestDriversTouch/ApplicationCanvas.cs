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

namespace FlashHeatZeeker.TestDriversTouch
{
    public class ApplicationCanvas : Canvas
    {
        public readonly Rectangle r = new Rectangle();

        public Rectangle

                 enter,
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

            var fingersize = 96;

            enter = new Rectangle
            {
                Fill = Brushes.White,
                Opacity = 0.5
            }.AttachTo(this);
            enter.SizeTo(fingersize + 4 + fingersize, fingersize);
            this.SizeChanged += (s, e) => enter.MoveTo(this.Width - fingersize - 4 - fingersize - 4, this.Height - fingersize - 4 - fingersize - 4 - fingersize - 4);

            space = new Rectangle
            {
                Fill = Brushes.White,
                Opacity = 0.5
            }.AttachTo(this);
            space.SizeTo(fingersize, fingersize + 4 + fingersize);
            this.SizeChanged += (s, e) => space.MoveTo(this.Width - fingersize - 4 - fingersize - 4, this.Height - fingersize - 4 - fingersize - 4);


            up = new Rectangle
            {
                Fill = Brushes.White,
                Opacity = 0.5
            }.AttachTo(this);
            up.SizeTo(fingersize, fingersize);
            this.SizeChanged += (s, e) => up.MoveTo(this.Width - fingersize - 4, this.Height - fingersize - 4 - fingersize - 4);

            down = new Rectangle
            {
                Fill = Brushes.White,
                Opacity = 0.5
            }.AttachTo(this);
            down.SizeTo(fingersize, fingersize);
            this.SizeChanged += (s, e) => down.MoveTo(this.Width - fingersize - 4, this.Height - fingersize - 4);




            control = new Rectangle
            {
                Fill = Brushes.White,
                Opacity = 0.5
            }.AttachTo(this);
            control.SizeTo(fingersize + 4 + fingersize, fingersize);
            this.SizeChanged += (s, e) => control.MoveTo(4, this.Height - fingersize - 4 - fingersize - 4);

            left = new Rectangle
            {
                Fill = Brushes.White,
                Opacity = 0.5
            }.AttachTo(this);
            left.SizeTo(fingersize, fingersize);
            this.SizeChanged += (s, e) => left.MoveTo(4, this.Height - fingersize - 4);

            right = new Rectangle
            {
                Fill = Brushes.White,
                Opacity = 0.5
            }.AttachTo(this);
            right.SizeTo(fingersize, fingersize);
            this.SizeChanged += (s, e) => right.MoveTo(4 + fingersize + 4, this.Height - fingersize - 4);

        }

    }
}
