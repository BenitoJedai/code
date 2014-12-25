using FakeWindowsLoginExperiment.HTML.Pages;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FakeWindowsLoginExperiment.Design
{
    sealed partial class FakeMultimonitorDesktop
    {
        IFakeMultimonitorDesktop e;

        public FakeMultimonitorDesktop(IFakeMultimonitorDesktop e)
            : this()
        {
            this.e = e;

            Action DialogImageToCenter = delegate
            {
                e.DialogImage.InvokeOnComplete(
                    delegate
                    {
                        e.DialogContainer.style.marginLeft = -(e.DialogImage.width / 2) + "px";
                        e.DialogContainer.style.marginTop = -(e.DialogImage.height / 2) + "px";
                    }
                );
            };

            #region find monitors
            Action frame = null;

            frame = delegate
            {
                // http://stackoverflow.com/questions/504052/determining-position-of-the-browser-window-in-javascript

                dynamic window = Native.window;
                int left = window.screenLeft;
                int top = window.screenTop;

                int innerHeight = window.innerHeight;
                int outerHeight = window.outerHeight;

                int innerWidth = window.innerWidth;
                int outerWidth = window.outerWidth;


                // compensate for toolbar while non fullscreen
                top += outerHeight - innerHeight;

                var marginLeft = (outerWidth - innerWidth) / 2;
                //top += marginLeft;
                left += marginLeft;

                dynamic screen = window.screen;
                int width = screen.width;
                int height = screen.height;

                // window.innerHeight

                //Native.Document.title =
                //    new
                //    {
                //        top,
                //        height

                //    }.ToString();

                e.PrimaryScreen.style.SetLocation(-left, -top, width, height);

                Native.window.requestAnimationFrame += frame;
            };



            Native.window.requestAnimationFrame += frame;
            #endregion


            ShadowOverlay.Hide();

            DialogImageToCenter();
            e.DialogContainer.Show();

            e.DialogImage.onclick +=
                delegate
                {
                    Native.Document.body.requestFullscreen();

                    e.DialogContainer.Hide();
                    e.DialogImage = new HTML.Images.FromAssets.BBeforeTrustConnection { id = e.DialogImage.id };
                    DialogImageToCenter();



                    new Timer(
                         delegate
                         {
                             ShadowOverlay.Show();

                             new Timer(
                                 delegate
                                 {
                                     ShadowOverlay.Hide();

                                     e.DialogContainer.Show();
                                 }
                               ).StartTimeout(800);
                         }
                       ).StartTimeout(300);

                    e.DialogImage.onclick +=
                         delegate
                         {
                             new HTML.Audio.FromAssets.Windows_User_Account_Control().play();

                             e.DialogContainer.Hide();
                             e.DialogImage = new HTML.Images.FromAssets.CBeforeConnectAnyway { id = e.DialogImage.id };
                             DialogImageToCenter();

                             e.DialogImage.InvokeOnComplete(
                                delegate
                                {
                                    e.DialogContainer.Show();
                                }
                             );

                             e.DialogImage.onclick +=
                                 delegate
                                 {
                                     e.DialogContainer.Hide();

                                     ShadowOverlay.Show();


                                     e.PrimaryScreenFrame.onload +=
                                         delegate
                                         {
                                             ShadowOverlay.Hide();

                                         };

                                     e.PrimaryScreenFrame.src = "/FakeLoginScreen";
                                     e.ScreenToLeftFrame.src = "/FakeLoginScreen";

                                     RequireFullscreen = true;

                                 };
                         };
                };
        }

        bool RequireFullscreen = false;

        private void applicationExitFullscreen1_ExitFullscreen()
        {
            if (RequireFullscreen)
            {
                new HTML.Audio.FromAssets.Windows_User_Account_Control().play();

                e.PrimaryScreenFrame.src = "/FakeLoginScreen";
                e.ScreenToLeftFrame.src = "/FakeLoginScreen";
            }
        }

        private void applicationExitFullscreen1_EnterFullscreen()
        {
            //new HTML.Audio.FromAssets.Windows_User_Account_Control().play();
        }
    }

}
