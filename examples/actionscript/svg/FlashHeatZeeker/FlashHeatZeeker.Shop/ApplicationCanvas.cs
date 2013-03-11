using ScriptCoreLib.Extensions;
using ScriptCoreLib.Shared.Avalon.Extensions;
using System;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Xml;
using System.Xml.Linq;

namespace FlashHeatZeeker.Shop
{
    public class ApplicationCanvas : Canvas
    {
        public event Action BuyShotgun;
        public event Action BuyAmmo;

        public Rectangle overlay_ammo = new Rectangle();
        public Rectangle bg_shotgun = new Rectangle();



        public ApplicationCanvas()
        {
            var r0shadow = new Rectangle();
            r0shadow.Opacity = 0.5;
            r0shadow.Fill = Brushes.Black;
            r0shadow.AttachTo(this);

            var r1shadow = new Rectangle();
            r1shadow.Opacity = 0.5;
            r1shadow.Fill = Brushes.Black;
            r1shadow.AttachTo(this);


            var rcontent = new Canvas().AttachTo(this);

            var r0 = new Rectangle();


            r0.Opacity = 0.8;
            r0.Fill = Brushes.DarkGreen;
            r0.AttachTo(rcontent);

            var r1 = new Rectangle();


            r1.Opacity = 0.8;
            r1.Fill = Brushes.DarkGreen;
            r1.AttachTo(rcontent);
            r1.MoveTo(0, 96);



            r0.SizeTo(500, 96 - 16);
            r1.SizeTo(500, 230);
            r0shadow.SizeTo(500, 96 - 16);
            r1shadow.SizeTo(500, 230);

            var t = new TextBox();

            t.IsReadOnly = true;

            //            type: System.Windows.Controls.TextBlock
            //method: Void set_Foreground(System.Windows.Media.Brush)

            t.Foreground = Brushes.Yellow;
            t.AttachTo(rcontent);
            t.AcceptsReturn = true;
            t.Text = "You have found a shop.\nSee something you like?";
            t.Background = Brushes.Transparent;
            t.BorderThickness = new Thickness(0);
            t.FontSize = 24;
            t.MoveTo(8, 8);


            var t2 = new TextBox();

            t2.IsReadOnly = true;

            //            type: System.Windows.Controls.TextBlock
            //method: Void set_Foreground(System.Windows.Media.Brush)

            t2.Foreground = Brushes.Yellow;
            t2.AttachTo(rcontent);
            t2.AcceptsReturn = true;
            t2.Text = "! First we will log you in to FACEBOOK and then to PAYPAL, takes a few...";
            t2.Background = Brushes.Transparent;
            t2.BorderThickness = new Thickness(0);
            t2.MoveTo(8, 290);

            var t2o = t2.ToAnimatedOpacity();

            t2o.Opacity = 0.2;

            var bg_ammo = new Rectangle();


            bg_ammo.Fill = Brushes.Yellow;
            bg_ammo.AttachTo(rcontent);
            bg_ammo.SizeTo(400, 48);
            bg_ammo.MoveTo(50, 128 + 32 - 8);

            var ammo = new Avalon.Images.avatars_ammo();

            ammo.AttachTo(rcontent);
            ammo.MoveTo(32 + 32, 128 + 32);



            overlay_ammo.Fill = Brushes.Yellow;
            overlay_ammo.Opacity = 0;
            overlay_ammo.AttachTo(rcontent);
            overlay_ammo.SizeTo(400, 48);
            overlay_ammo.MoveTo(50, 128 + 32 - 8);

            var bg_ammo_opacity = bg_ammo.ToAnimatedOpacity();

            overlay_ammo.Cursor = Cursors.Hand;
            bg_ammo_opacity.Opacity = 0.5;
            overlay_ammo.MouseEnter +=
                delegate
                {
                    bg_ammo_opacity.Opacity = 1.0;
                };

            overlay_ammo.MouseLeave +=
                delegate
                {
                    bg_ammo_opacity.Opacity = 0.5;
                };
            overlay_ammo.MouseLeftButtonUp +=
             delegate
             {
                 t2o.Opacity = 1;

                 if (BuyAmmo != null)
                     BuyAmmo();
             };














            bg_shotgun.Opacity = 0.5;
            bg_shotgun.Fill = Brushes.Yellow;
            bg_shotgun.AttachTo(rcontent);
            bg_shotgun.SizeTo(400, 48);
            bg_shotgun.MoveTo(50, 128 + 64 + 32);

            var shotgun = new Avalon.Images.avatars_shotgun();

            shotgun.AttachTo(rcontent);
            shotgun.MoveTo(32 + 32, 128 + 64 + 32 + 8);



            var overlay_shotgun = new Rectangle();


            overlay_shotgun.Opacity = 0;
            overlay_shotgun.Fill = Brushes.Yellow;
            overlay_shotgun.AttachTo(rcontent);
            overlay_shotgun.SizeTo(400, 48);
            overlay_shotgun.MoveTo(50, 128 + 64 + 32);

            var bg_shotgun_opacity = bg_shotgun.ToAnimatedOpacity();

            overlay_shotgun.Cursor = Cursors.Hand;
            bg_shotgun_opacity.Opacity = 0.5;
            overlay_shotgun.MouseEnter +=
                delegate
                {
                    bg_shotgun_opacity.Opacity = 1.0;
                };

            overlay_shotgun.MouseLeave +=
                delegate
                {
                    bg_shotgun_opacity.Opacity = 0.5;
                };

            overlay_shotgun.MouseLeftButtonUp +=
                delegate
                {
                    t2o.Opacity = 1;

                    if (BuyShotgun != null)
                        BuyShotgun();
                };

            this.SizeChanged += (s, e) =>
            {



                r0shadow.MoveTo((this.Width - 500) / 2 + 4, (this.Height - 400) / 2 + 4);
                r1shadow.MoveTo((this.Width - 500) / 2 + 4, (this.Height - 400) / 2 + 4 + 96);
                rcontent.MoveTo((this.Width - 500) / 2, (this.Height - 400) / 2);

            };
        }

    }
}
