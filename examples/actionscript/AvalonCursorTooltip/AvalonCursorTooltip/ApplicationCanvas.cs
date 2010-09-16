using System;
using System.Text;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Windows.Media;
using System.Windows.Shapes;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Shared.Avalon.Extensions;
using System.Windows.Controls;
using System.Windows;
using ScriptCoreLib.Ultra.Components.Avalon.Images;

namespace BrowserAvalonApplicationWithAdobeFlash2
{
    public class ApplicationCanvas : Canvas
    {
        public readonly Rectangle r = new Rectangle();

        public ApplicationCanvas()
        {
            var logo = new JSCSolutionsNETImage();
            logo.AttachTo(this);
            var text = new TextBox
            {
                AcceptsReturn = true,
                BorderThickness = new Thickness(0),
                Background = Brushes.Transparent,
                Foreground = Brushes.Blue,
            };
            text.AttachTo(this);


            r.Fill = Brushes.Yellow;
            r.Opacity = 0.3;
            r.AttachTo(this);
            r.MoveTo(8, 8);

            this.SizeChanged += (s, e) =>
                {
                    r.SizeTo(this.Width - 16, this.Height - 16);

                    logo.MoveTo(
                        this.Width - logo.Width,
                        this.Height - logo.Height
                    );
                };

            r.MouseEnter +=
                (s, e) =>
                {
                    text.Show();
                };

            r.MouseLeave +=
                (s, e) =>
                {
                    text.Hide();
                };


            r.MouseMove +=
                (s, e) =>
                {
                    var p = e.GetPosition(this);
                    
                    text.Text = "jsc-solutions.net\n" + new { p.X, p.Y };

                    text.MoveTo(p.X + 32, p.Y);
                };
        }

    }
}
