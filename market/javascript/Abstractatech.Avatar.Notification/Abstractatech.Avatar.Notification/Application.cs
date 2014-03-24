using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Abstractatech.Avatar.Notification;
using Abstractatech.Avatar.Notification.Design;
using Abstractatech.Avatar.Notification.HTML.Pages;
using Abstractatech.JavaScript.Avatar.Design;
using ScriptCoreLib.Lambda;

namespace Abstractatech.Avatar.Notification
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {
        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            WebCamAvatarsSheet1Row lastPicture = null;
            IHTMLImage img = null;

            Action getNewPicture = async delegate
            {
                while (true)
                {

                    var pic = await this.GetLastUserImage();

                    if (lastPicture != null)
                    {
                        if (pic != null)
                        {
                            if (pic.Key == lastPicture.Key)
                            {
                                img.style.visibility = IStyle.VisibilityEnum.hidden;
                            }
                            else
                            {
                                img.src = pic.Avatar96gif;
                                img.AttachTo(page.ImgContainer);
                                img.style.visibility = IStyle.VisibilityEnum.visible;
                            }
                        }
                        else
                        {
                            img.style.visibility = IStyle.VisibilityEnum.hidden;
                        }
                    }
                    else
                    {
                        lastPicture = pic;
                        img.src = pic.Avatar96gif;
                        img.AttachTo(page.ImgContainer);
                        img.style.visibility = IStyle.VisibilityEnum.visible;
                    }
                    await 2000;

                }
            };
            Console.WriteLine("Start");
            getNewPicture();

            var button = new IHTMLButton { innerHTML = "SubmitButton" }.AttachToDocument();

            button.onclick += delegate
            {
                var overlay = new IHTMLDiv { }.AttachTo(Native.document.documentElement);
                overlay.style.position = IStyle.PositionEnum.@fixed;
                overlay.style.left = "0";
                overlay.style.top = "0";
                overlay.style.right = "0";
                overlay.style.bottom = "0";
                overlay.style.backgroundColor = "black";

                var div = new IHTMLDiv { }.AttachTo(overlay);

                var xcss = Native.document.body.css.children;

                xcss.style.display = IStyle.DisplayEnum.none;

                Abstractatech.JavaScript.Avatar.ApplicationImplementation.MakeCamGrabber(
                    div,
                    sizeToWindow: true,
                    yield:
                    y =>
                    {
                        overlay.Orphanize();
                        xcss.style.display = IStyle.DisplayEnum.empty;

                        Console.WriteLine("InsertNewPicture");

                        if (y.Avatar640x480.Length >= (1024 * 64))
                        {
                            //MessageBox.Show("Picture size too big for now");
                            return;
                        }

                        this.InsertNewPicture(y);

                        //var z = (__PictureBox)(object)f.pictureBox1;

                        // dow we like gif of jpg?
                        //z.InternalElement.src = y.Avatar96gif;
                        //z.InternalElement.src = y.Avatar96frame1;
                    }
                );
            };
        }

    }
}
