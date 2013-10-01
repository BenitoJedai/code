using ScriptCoreLib.Avalon;
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

namespace AvalonBrowserLogos
{
    public class ApplicationCanvas : Canvas
    {
        ScriptCoreLib.Ultra.Components.Volatile.Avalon.Images.Google_Chrome ref0;

        public ApplicationCanvas()
        {

            XInitialize();

        }

        private void XInitialize()
        {

            //this.Background = Brushes.Black;
            this.Background = Brushes.White;

            var b = new JSCSolutionsNETCarouselCanvas();

            b.CloseOnClick = false;
            b.Container.AttachTo(this);

            var bg_black = new Canvas
            {
                Background = Brushes.Black
            }.AttachTo(this);

            var bg_black_opacity = bg_black.ToAnimatedOpacity();

            bg_black_opacity.Opacity = 0;

            var w = new JSCSolutionsNETWhiteCarouselCanvas();

            w.CloseOnClick = false;
            w.Container.AttachTo(bg_black);

            bool OtherView = false;

            new { b.step }.With(
                x =>
                {
                    var counter = 0;

                    (1000 / 60).AtIntervalWithTimer(
                        speedboost =>
                        {
                            counter++;

                            b.step = x.step * (1 + counter * 0.1);

                            if (counter == 60 * 2)
                                speedboost.Stop();

                        }
                    );

                }
            );

            Action ChooseView =
                delegate
                {
                    if ((Width > Height) ^ OtherView)
                        bg_black_opacity.Opacity = 0;
                    else
                        bg_black_opacity.Opacity = 1;
                };

            w.AtLogoClick +=
                delegate
                {
                    OtherView = !OtherView;

                    ChooseView();
                };

            b.AtLogoClick +=
                delegate
                {
                    OtherView = !OtherView;

                    ChooseView();

                };

            this.SizeChanged += (s, e) =>
            {
                ChooseView();

                w.Container.MoveTo(
                    (this.Width - 400) / 2, (this.Height - 400) / 2
                );

                b.Container.MoveTo(
                      (this.Width - 400) / 2, (this.Height - 400) / 2
                  );

                bg_black.SizeTo(this.Width, this.Height);
            };


        }

    }
}
