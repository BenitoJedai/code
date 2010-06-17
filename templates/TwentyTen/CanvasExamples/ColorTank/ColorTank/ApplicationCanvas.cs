// For more information visit:
// http://studio.jsc-solutions.net/

using System;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Xml.Linq;
using ColorTank;
using ColorTank.Avalon.Images;
using ScriptCoreLib;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Shared.Avalon.Extensions;
using ScriptCoreLib.Ultra.Components.Avalon.Images;

namespace ColorTank
{
    public class ApplicationCanvas : Canvas
    {
        public const int DefaultWidth = 120 * 4;
        public const int DefaultHeight = 90 * 4;

        public ApplicationCanvas()
        {
            Width = DefaultWidth;
            Height = DefaultHeight;

            this.ClipToBounds = true;

            var bg = new Rectangle
            {
                Width = DefaultWidth,
                Height = DefaultHeight,
                Fill = Brushes.White
            }.AttachTo(this);

            var tank_bg = new Rectangle
            {
                Width = 184,
                Height = 134,
                Fill = Brushes.White
            }.MoveTo(32, 128).AttachTo(this);

            var tank = new tank_mask
            {
            }.MoveTo(32, 128).AttachTo(this);

            var s = new TextBox
            {
                BorderThickness = new Thickness(0),
                Foreground = Brushes.Blue,
                Width = 184,
                TextAlignment = TextAlignment.Center
            }.MoveTo(32, 128 + 134).AttachTo(this);

            var tank2_bg = new Rectangle
            {
                Width = 184,
                Height = 134,
                Fill = Brushes.White
            }.MoveTo(228, 32).AttachTo(this);

            var tank2 = new tank_mask
            {
            }.MoveTo(228, 32).AttachTo(this);

            var s2 = new TextBox
            {
                BorderThickness = new Thickness(0),
                Foreground = Brushes.Blue,
                Width = 184,
                TextAlignment = TextAlignment.Center
            }.MoveTo(228, 32 + 134).AttachTo(this);

            var t = new TextBox
            {
                BorderThickness = new Thickness(0),
                Text = "A dynamically colored tank.",
                Width = DefaultWidth
            }.MoveTo(12, 12).AttachTo(this);



            var img = new JSCSolutionsNETImage
            {
            }.MoveTo(DefaultWidth - 128, DefaultHeight - 128).AttachTo(this);



            this.MouseMove +=
                (sender, args) =>
                {
                    var p = args.GetPosition(this);

                    var x = (p.X / DefaultWidth);

                    x = Math.Min(x, 1);
                    x = Math.Max(x, 0);

                    var y = (p.Y / DefaultHeight);

                    y = Math.Min(y, 1);
                    y = Math.Max(y, 0);


                    var _r = Convert.ToByte(x * 0xFF);
                    var _g = Convert.ToByte(y * 0xFF);
                    var _b = Convert.ToByte(0xff - y * 0xFF);

                    s.Text = "red: " + _r + "; green: " + _g + ";";
                    s2.Text = "green: " + _g + "; blue: " + _b + ";";


                    tank_bg.Fill = new SolidColorBrush(Color.FromArgb(0xff, _r, _g, 0));
                    tank2_bg.Fill = new SolidColorBrush(Color.FromArgb(0xff, 0, _g, _b));
                };
        }
    }

}
