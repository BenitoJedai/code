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
using AnimatedToolbar.Library;

namespace AnimatedToolbar
{
    public class ApplicationCanvas : Canvas
    {
        public readonly Rectangle r = new Rectangle();

        public ApplicationCanvas()
        {
            r.Fill = Brushes.Red;
            r.Opacity = 0.2;
            r.AttachTo(this);
            r.MoveTo(8, 8);
            this.SizeChanged += (s, e) => r.SizeTo(this.Width - 16.0, this.Height - 16.0);

            var f = new TextBox().AttachTo(this).MoveTo(8 + 4, 64);
            this.SizeChanged += (s, e) => f.SizeTo(this.Width - 16.0 - 4, 24);

            var t = new AnimatedToolbarCanvas();

            t.AttachTo(this);
            t.MoveTo(8, 8);

            t.ItemClicked +=
                SourceItem =>
                {
                    f.Text = "" + SourceItem.Tag;
                };

            #region More
            var c = 0;
            Action More =
                delegate
                {
                    c++;
                    t.Items.Add(
                        new AnimatedToolbarCanvas.AnimatedToolbarItem
                        {
                            Image = new Avalon.Images.SolutionProjectDependentUpon(),
                            Tag = "hello world (" + c + ")"
                        }
                    );
                };
            #endregion


            More();

            5000.AtInterval(More);

            r.MouseLeftButtonUp +=
                delegate
                {
                    More();
                };
        }

    }
}
