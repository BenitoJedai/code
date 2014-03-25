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

            new IStyle(page.ImgContainer.css + IHTMLElement.HTMLElementEnum.img)
            {
                transition = "left linear 2000ms",
                position = IStyle.PositionEnum.absolute,
                Opacity = 1
            };

            //new IStyle(Native.css[typeof(IHTMLImage)].hover)
            //{
            //    width = "100px",
            //};

           

            Action getNewPicture = async delegate
            {
                while (true)
                {
                    var pic = await this.GetLastUserImage();

                    if (lastPicture != null)
                    {
                        if (pic != null)
                        {
                            Console.WriteLine(pic.Key.ToString());
                            Console.WriteLine(lastPicture.Key.ToString());
                            if (pic.Key == lastPicture.Key)
                            {
                                page.ImgContainer.Clear();
                            }
                            else
                            {
                                IHTMLImage img = new IHTMLImage();
                                img.AttachTo(page.ImgContainer);
                                img.src = pic.Avatar96frame1;
                                img.style.left = "-150px";
                                lastPicture = pic;

                                await 500;

                                Native.window.requestAnimationFrame += delegate
                                {
                                    img.style.left = "50px";

                                };

                                await 10000;

                                Native.window.requestAnimationFrame += delegate
                                {
                                    img.style.left = "-150px";

                                };
                            }
                        }
                        else
                        {
                            page.ImgContainer.Clear();
                        }
                    }
                    else
                    {
                        IHTMLImage img = new IHTMLImage();
                        img.AttachTo(page.ImgContainer);
                        lastPicture = pic;
                        img.src = pic.Avatar96frame1;
                        img.style.left = "-150px";
                        
                        await 500;

                        Native.window.requestAnimationFrame += delegate
                        {
                            img.style.left = "50px";

                        };

                        await 10000;

                        Native.window.requestAnimationFrame += delegate
                        {
                            img.style.left = "-150px";

                        };
                    }
                    await 4000;
                }
            };
            Console.WriteLine("Start");
            //getNewPicture();

           // new IHTMLDiv { }.AttachToDocument().With(x => ApplicationImplementation.MakeCamGrabber(x, sizeToWindow: true));
           // var service = new AvatarNotificationService();
            page.ImgContainer.With(x => ApplicationImplementation.MakeimageNotification(page.ImgContainer, this));

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

                        this.DiagnosticInsertNewPicture(y);

                        //var z = (__PictureBox)(object)f.pictureBox1;

                        // dow we like gif of jpg?
                        //z.InternalElement.src = y.Avatar96gif;
                        //z.InternalElement.src = y.Avatar96frame1;
                    }
                );
            };
        }

    }
    public static class ApplicationImplementation
    {
        public static async void MakeimageNotification(IHTMLDiv c, IAvatarNotificationInterface service)
        {
            WebCamAvatarsSheet1Row lastPicture = null;

            //new IStyle(page.ImgContainer.css + IHTMLElement.HTMLElementEnum.img)
            //{
            //    transition = "left linear 2000ms",
            //    position = IStyle.PositionEnum.absolute,
            //    Opacity = 1
            //};

            while (true)
            {
                var pic = await service.GetLastUserImage();

                if (lastPicture != null)
                {
                    if (pic != null)
                    {
                        Console.WriteLine(pic.Key.ToString());
                        Console.WriteLine(lastPicture.Key.ToString());
                        if (pic.Key == lastPicture.Key)
                        {
                            c.Clear();
                        }
                        else
                        {
                            IHTMLImage img = new IHTMLImage();
                            img.AttachTo(c);
                            img.src = pic.Avatar96frame1;
                            img.style.left = "-150px";
                            lastPicture = pic;

                            await 500;

                            Native.window.requestAnimationFrame += delegate
                            {
                                img.style.left = "50px";

                            };

                            await 10000;

                            Native.window.requestAnimationFrame += delegate
                            {
                                img.style.left = "-150px";

                            };
                        }
                    }
                    else
                    {
                        c.Clear();
                    }
                }
                else
                {
                    IHTMLImage img = new IHTMLImage();
                    img.AttachTo(c);
                    lastPicture = pic;
                    img.src = pic.Avatar96frame1;
                    img.style.left = "-150px";

                    await 500;

                    Native.window.requestAnimationFrame += delegate
                    {
                        img.style.left = "50px";

                    };

                    await 10000;

                    Native.window.requestAnimationFrame += delegate
                    {
                        img.style.left = "-150px";

                    };
                }
                await 4000;
            }

        }
    }
}
