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
    sealed class FakeLogin
    {
        public FakeLogin(IFakeLogin e)
        {
            Action frame = null;

            frame = delegate
            {
                // http://stackoverflow.com/questions/504052/determining-position-of-the-browser-window-in-javascript

                dynamic window = Native.Window;
                int left = window.screenLeft;
                int top = window.screenTop;

                int innerHeight = window.innerHeight;
                int outerHeight = window.outerHeight;

                // compensate for toolbar while non fullscreen
                top += outerHeight - innerHeight;

                dynamic screen = window.screen;
                int width = screen.width;
                int height = screen.height;

                // window.innerHeight

                Native.Document.title =
                    new
                    {
                        top,
                        height

                    }.ToString();

                e.PrimaryScreen.style.SetLocation(-left, -top, width, height);

                Native.Window.requestAnimationFrame += frame;
            };

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

            Native.Window.requestAnimationFrame += frame;

            e.ShadowOverlay.Hide();
            DialogImageToCenter();
            e.DialogContainer.Show();
            new HTML.Audio.FromAssets.Windows_Hardware_Insert().play();

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

                             new HTML.Audio.FromAssets.Windows_Hardware_Remove().play();

                             dynamic style = e.ShadowOverlay.style;
                             style.cursor = "none";
                             //e.ShadowOverlay.style.cursor= ScriptCoreLib.JavaScript.DOM.IStyle.CursorEnum.non
                             e.ShadowOverlay.style.backgroundColor = JSColor.Black;
                             e.ShadowOverlay.Show();


                             new Timer(
                                 delegate
                                 {

                                     new HTML.Audio.FromAssets.Windows_Hardware_Insert().play();
                                     e.ShadowOverlay.Hide();
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

                                     e.ShadowOverlay.style.cursor = ScriptCoreLib.JavaScript.DOM.IStyle.CursorEnum.wait;
                                     e.ShadowOverlay.style.backgroundColor = JSColor.FromRGB(0x18, 0x5D, 0x7B);
                                     e.ShadowOverlay.Show();

                                     e.PrimaryScreenFrame.onload +=
                                         delegate
                                         {
                                             e.ShadowOverlay.Hide();

                                         };

                                     e.PrimaryScreenFrame.src = "/FakeLoginScreen";
                                 };
                         };
                };
        }
    }

}
