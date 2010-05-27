using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.CSharp.Avalon.Extensions;
using ScriptCoreLib.Shared.Avalon.Extensions;
using System.Threading;
using System.Windows.Media;
using ScriptCoreLib.Avalon;

namespace ScriptCoreLib.Avalon.Desktop
{
    static class JSCSolutionsNETCarouselProgram
    {
        public static Thread ShowDialogSplash()
        {
            return ShowDialog(c => 4500.AtDelay(c));
        }

        public static void ShowDialogSplash(Action h)
        {
            var t = ShowDialogSplash();

            h();

            t.Join();
        }

        public static Thread ShowDialog()
        {
            return ShowDialog(null);
        }

        internal static void Main(string[] args)
        {
            JSCSolutionsNETCarouselProgram.ShowDialogSplash(
                // primary task executes longer than splash
                () => Thread.Sleep(7000)
            );

            //JSCSolutionsNETCarouselProgram.ShowDialogSplash(
            //        // primary task executes longer than splash
            //    () => Thread.Sleep(7000)
            //);
        }

        internal static void Main_Debug1(string[] args)
        {
            JSCSolutionsNETCarouselProgram.ShowDialogSplash(
                // primary task executes longer than splash
                () => Thread.Sleep(7000)
            );
        }

        public static Thread ShowDialog(Action<Action> AnnounceCloseAction)
        {
            var t = new Thread(
                delegate()
                {
                    InternalShowDialog(AnnounceCloseAction);
                }
            )
            {
                ApartmentState = ApartmentState.STA,

                //IsBackground = true,

                // our animation runs only for a while
                Priority = ThreadPriority.Highest
            };

            t.Start();

            return t;
        }

        private static void InternalShowDialog(Action<Action> AnnounceCloseAction)
        {
            //var c = new JSCSolutionsNETWhiteCarouselCanvas();
            var c = new JSCSolutionsNETCarouselCanvas();

            if (AnnounceCloseAction != null)
                AnnounceCloseAction(c.Close);

            //c.Container.Effect = new DropShadowEffect();
            //c.Container.BitmapEffect = new DropShadowBitmapEffect();

            new System.Windows.Controls.TextBox
            {
                IsReadOnly = true,
                Background = Brushes.Transparent,
                BorderThickness = new System.Windows.Thickness(0),
                Foreground = Brushes.Red,
                BitmapEffect = new System.Windows.Media.Effects.OuterGlowBitmapEffect(),
                Text = "Evaluation version",
                FontFamily = new FontFamily("Microsoft Sans Serif"),
                TextAlignment = System.Windows.TextAlignment.Right
            }.AttachTo(c).MoveTo(0, ImageCarouselCanvas.DefaultHeight - 96).SizeTo(ImageCarouselCanvas.DefaultWidth, 96);

            var w = c.ToWindow();

            w.ToTransparentWindow();

            c.AtClose += w.Close;

            // http://blog.joachim.at/?p=39
            // http://blogs.msdn.com/changov/archive/2009/01/19/webbrowser-control-on-transparent-wpf-window.aspx
            // http://blogs.interknowlogy.com/johnbowen/archive/2007/06/20/20458.aspx
            w.AllowsTransparency = true;
            w.WindowStyle = System.Windows.WindowStyle.None;
            w.Background = Brushes.Transparent;
            w.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            w.Topmost = true;
            w.ShowInTaskbar = false;
            w.Focusable = false;

            w.ShowDialog();
        }
    }
}
