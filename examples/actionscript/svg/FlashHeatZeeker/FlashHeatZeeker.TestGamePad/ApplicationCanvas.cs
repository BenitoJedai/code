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

namespace FlashHeatZeeker.TestGamePad
{
    public class ApplicationCanvas : Canvas
    {
        Avalon.Images.Promotion3D_iso1_tiltshift_128 ref0;


        public readonly Rectangle r = new Rectangle();

        public Rectangle

                 enter,
                    up,
            space,
                    down,

                    alt,
            control,
            left, right;

        public int fingersize = 96;




        public double tiltx = 0.5;
        public double tilty = 0.5;

        public Action tilt_update;
        public Rectangle rtiltx, rtilty;

        public ApplicationCanvas()
        {
            r.Fill = Brushes.Black;
            r.AttachTo(this);
            r.MoveTo(0, 0);
            this.SizeChanged += (s, e) => r.SizeTo(this.Width, this.Height);


             rtiltx = new Rectangle
            {
                Fill = Brushes.Red,
                Opacity = 0.5
            }.AttachTo(this);

             rtilty = new Rectangle
            {
                Fill = Brushes.Blue,
                Opacity = 0.5
            }.AttachTo(this);

            tilt_update = delegate
            {
                rtiltx.SizeTo(8, this.Height);
                rtiltx.MoveTo((this.Width - 8) * tiltx, 0);

                rtilty.SizeTo(this.Width, 8);
                rtilty.MoveTo(0, (this.Height - 8) * tilty);

                rtiltx.Opacity = tiltx;
                rtilty.Opacity = tilty;

            };
            this.SizeChanged +=
                (s, e) =>
                {
                    tilt_update();

                };

            enter = new Rectangle
            {
                Fill = Brushes.Green,
                Opacity = 0.5
            }.AttachTo(this);
            enter.SizeTo(fingersize + 4 + fingersize, fingersize + 4 + fingersize);
            this.SizeChanged += (s, e) => enter.MoveTo(this.Width - fingersize - 4 - fingersize - 4, this.Height - fingersize - 4 - fingersize - 4 - fingersize - 4);

            space = new Rectangle
            {
                Fill = Brushes.White,
                Opacity = 0.5
            }.AttachTo(this);
            space.SizeTo(fingersize + 4 + fingersize, fingersize);
            this.SizeChanged += (s, e) => space.MoveTo(this.Width - fingersize - 4 - fingersize - 4, this.Height - fingersize - 4);


            //up = new Rectangle
            //{
            //    Fill = Brushes.White,
            //    Opacity = 0.5
            //}.AttachTo(this);
            //up.SizeTo(fingersize, fingersize);
            //this.SizeChanged += (s, e) => up.MoveTo(this.Width - fingersize - 4, this.Height - fingersize - 4 - fingersize - 4);

            //down = new Rectangle
            //{
            //    Fill = Brushes.White,
            //    Opacity = 0.5
            //}.AttachTo(this);
            //down.SizeTo(fingersize, fingersize);
            //this.SizeChanged += (s, e) => down.MoveTo(this.Width - fingersize - 4, this.Height - fingersize - 4);




            control = new Rectangle
            {
                Fill = Brushes.Red,
                Opacity = 0.5
            }.AttachTo(this);
            control.SizeTo(fingersize + 4 + fingersize, fingersize + 4 + fingersize);
            this.SizeChanged += (s, e) => control.MoveTo(4, this.Height - fingersize - 4 - fingersize - 4);

            alt = new Rectangle
            {
                Fill = Brushes.White,
                Opacity = 0.5
            }.AttachTo(this);
            alt.SizeTo(fingersize + 4 + fingersize, fingersize);
            this.SizeChanged += (s, e) => alt.MoveTo(4, this.Height - fingersize - 4 - fingersize - 4 - fingersize - 4);


            //left = new Rectangle
            //{
            //    Fill = Brushes.White,
            //    Opacity = 0.5
            //}.AttachTo(this);
            //left.SizeTo(fingersize, fingersize);
            //this.SizeChanged += (s, e) => left.MoveTo(4, this.Height - fingersize - 4);

            //right = new Rectangle
            //{
            //    Fill = Brushes.White,
            //    Opacity = 0.5
            //}.AttachTo(this);
            //right.SizeTo(fingersize, fingersize);
            //this.SizeChanged += (s, e) => right.MoveTo(4 + fingersize + 4, this.Height - fingersize - 4);

        }


    }
}
