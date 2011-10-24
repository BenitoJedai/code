using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using ScriptCoreLib.Ultra.Components.Avalon.Images;
using System.Windows.Markup;
using System.Windows.Media;
using ScriptCoreLib.Shared.Avalon.Extensions;
using ScriptCoreLib.CSharp.Avalon.Extensions;
using System.Windows.Controls;
using System.Windows.Media.Effects;


namespace ScriptCoreLib.Avalon.Desktop
{
    public class JSCBrandedWindow : Window
    {
        protected JSCSolutionsNETCarouselCanvas cc;
        protected bool EnableSattelites = true;

        public bool DisableStalliteToggleAtStateChange;


        int __HideSattelitesAtDelay;
        public int HideSattelitesAtDelay
        {
            get
            {
                return __HideSattelitesAtDelay;
            }
            set
            {
                __HideSattelitesAtDelay = value;

                if (HideSattelitesAtDelay > 0)
                    HideSattelitesAtDelay.AtDelay(cc.HideSattelites);
            }
        }

        public JSCBrandedWindow()
        {
            this.Icon = new JSCSolutionsNETImage().Source;
            
            // dwmapi.dll will be missing on xp
            this.WithGlass();


            this.Title = "What do you want to create today?";

            var Overlay = new Window
            {
                AllowsTransparency = true,
                Background = Brushes.Transparent,
                ShowInTaskbar = false,
                WindowStyle = System.Windows.WindowStyle.None,
                //Topmost = true,
                Width = 400,
                Height = 400,


            };


            Overlay.SourceInitialized +=
              delegate
              {
                  ScriptCoreLib.CSharp.Avalon.Extensions.DesktopWindowManager.MakeInteractive(
                  Overlay, false);
              };

            var Zoom = 0.4;

            Action MoveOverlay =
                delegate
                {
                    Overlay.MoveTo(
                        this.Left - 200 * Zoom + 20,
                        this.Top - 200 * Zoom + 20
                     );

                };

            this.LocationChanged +=
                delegate
                {
                    MoveOverlay();
                };




            var TitleCanvas = new Canvas().AttachTo(Overlay);

            cc = new JSCSolutionsNETCarouselCanvas();

            cc.StepSpeedToToggleSattelites = 0.20;
            cc.DisableTimerShutdown = true;

            //cc.CloseOnClick = false;
            //cc.AtLogoClick +=
            //    delegate
            //    {
            //        Process.Start("http://www.jsc-solutions.net");
            //    };

            // http://stackoverflow.com/questions/741956/wpf-pan-zoom-image
            cc.Container.RenderTransform = new ScaleTransform(Zoom, Zoom);

            cc.AttachContainerTo(TitleCanvas);



            this.Deactivated +=
                delegate
                {
                    if (!cc.SattelitesHidden)
                        cc.HideSattelites();
                };



            this.Activated +=
                delegate
                {
                    if (EnableSattelites && cc.DisableTimerShutdown)
                        if (cc.SattelitesHidden)
                            cc.ShowSattelitesAgain();


                };


            this.cc.AtSattelitesShownAgain +=
                delegate
                {
                    if (HideSattelitesAtDelay > 0)
                        HideSattelitesAtDelay.AtDelay(cc.HideSattelites);
                };

            this.StateChanged +=
                delegate
                {
                    if (DisableStalliteToggleAtStateChange)
                        return;

                    if (this.WindowState != System.Windows.WindowState.Normal)
                    {
                        if (!cc.SattelitesHidden)
                            cc.HideSattelites();
                    }
                    else
                    {
                        if (EnableSattelites && cc.DisableTimerShutdown)
                            if (cc.SattelitesHidden)
                                cc.ShowSattelitesAgain();
                    }
                };

            this.SourceInitialized +=
               delegate
               {
                   MoveOverlay();

                   // http://blogs.msdn.com/b/wpfsdk/archive/2008/09/08/custom-window-chrome-in-wpf.aspx
                   ScriptCoreLib.CSharp.Avalon.Extensions.DesktopWindowManager.SetWindowThemeAttribute(this, false, false);



                   //Internal.SetParent(
                   //    new WindowInteropHelper(Overlay).Handle,
                   //    new WindowInteropHelper(this).Handle
                   //);

                   Overlay.Owner = this;
               };

            Overlay.Show();
        }
    }
}
