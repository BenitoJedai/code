using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using System;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Abstractatech.JavaScript.FileStorage.Design;
using Abstractatech.JavaScript.FileStorage.HTML.Pages;
using ScriptCoreLib.JavaScript.Runtime;
using System.Collections.Generic;

namespace Abstractatech.JavaScript.FileStorage
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application
    {
        public readonly ApplicationWebService service = new ApplicationWebService();

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            var content = new ApplicationContent();

        }

    }

    public static class X
    {
        public static IEnumerable<File> AsEnumerable(this FileList f)
        {
            return Enumerable.Range(0, (int)f.length).Select(k => f[(uint)k]);
        }

    }

    public class ApplicationContent
    {
        public ApplicationContent()
        {
            // first order of business.
            // enable drop zone.
            var dz = new DropZone();


            dz.Container.AttachToDocument();
            dz.Container.Hide();

            #region ondrop

            var TimerHide = new Timer(
                delegate
                {
                    dz.Container.Hide();
                }
            );

            Native.Document.body.ondragover +=
                evt =>
                {
                    evt.stopPropagation();
                    evt.preventDefault();

                    evt.dataTransfer.dropEffect = "copy"; // Explicitly show this is a copy.

                    dz.Container.Show();
                    //Console.WriteLine(" Native.Document.body.ondragover");
                };

            dz.Container.ondragover +=
                evt =>
                {
                    evt.stopPropagation();
                    evt.preventDefault();

                    evt.dataTransfer.dropEffect = "copy"; // Explicitly show this is a copy.
                    //Console.WriteLine(" dz.Container.ondragover");

                    TimerHide.Stop();
                };

            dz.Container.ondragleave +=
                 evt =>
                 {
                     //Console.WriteLine(" dz.Container.ondragleave");

                     evt.stopPropagation();
                     evt.preventDefault();

                     TimerHide.StartTimeout(50);
                 };

            dz.Container.ondrop +=
                evt =>
                {
                    TimerHide.StartTimeout(50);

                    evt.stopPropagation();
                    evt.stopImmediatePropagation();

                    evt.preventDefault();



                    var xhr = new IXMLHttpRequest();

                    // does not work for chrome?
                    //xhr.setRequestHeader("WebServiceMethod", "FileStorageUpload");

                    // which server?
                    xhr.open(ScriptCoreLib.Shared.HTTPMethodEnum.POST, "/FileStorageUpload");

                    var d = new FormData();

                    evt.dataTransfer.files.AsEnumerable().WithEachIndex(
                        (f, index) =>
                        {
                            d.append("file" + index, f, f.name);
                        }
                    );

                    xhr.InvokeOnComplete(
                        delegate
                        {
                            Console.WriteLine("upload complete!");
                        }
                    );

                    xhr.send(d);


                    //if (evt.dataTransfer == null)
                    //    return;



                    //                    page.Header.style.color = JSColor.None;



                    //                    #region files
                    //                    evt.dataTransfer.files.AsEnumerable().WithEachIndex(
                    //                        (f, index) =>
                    //                        {
                    //                            var ff = new Form();
                    //                            ff.PopupInsteadOfClosing(HandleFormClosing: false);



                    //                            ff.Text = new { f.type, f.name, f.size }.ToString();


                    //                            ff.Show();

                    //                            ff.MoveTo(
                    //                                evt.CursorX + 32 * index,
                    //                                evt.CursorY + 24 * index
                    //                            );

                    //                            var fc = ff.GetHTMLTargetContainer();

                    //                            fc.title = ff.Text;

                    //                            var i = default(IHTMLImage);

                    //                            if (f.type.StartsWith("image/"))
                    //                            {
                    //                                f.ToDataURLAsync(
                    //                                    src =>
                    //                                    {
                    //                                        i = new IHTMLImage { src = src }.AttachTo(fc);
                    //                                        i.style.width = "100%";

                    //                                        i.InvokeOnComplete(
                    //                                            delegate
                    //                                            {
                    //                                                ff.ClientSize = new System.Drawing.Size(i.width, i.height);
                    //                                            }
                    //                                        );
                    //                                    }
                    //                                );
                    //                            }

                    //                            // http://html5doctor.com/drag-and-drop-to-server/

                    //#if FUTURE
                    //                            service.XUpload(f, delegate { });
                    //#endif


                    //                            var d = new FormData();

                    //                            d.append("foo", f, f.name);

                    //                            var xhr = new IXMLHttpRequest();

                    //                            xhr.open(ScriptCoreLib.Shared.HTTPMethodEnum.POST, "/upload");

                    //                            xhr.InvokeOnComplete(
                    //                                delegate
                    //                                {
                    //                                    SystemSounds.Beep.Play();

                    //                                    //Console.Beep();
                    //                                    XElement.Parse(xhr.responseText).Elements("ContentKey").WithEach(
                    //                                        ContentKey =>
                    //                                        {
                    //                                            var __ContentKey = (Table1_ContentKey)int.Parse(ContentKey.Value);

                    //                                            var src = "/io/" + ContentKey.Value;
                    //                                            i.Orphanize();

                    //                                            var web = new WebBrowser { Dock = DockStyle.Fill };

                    //                                            web.AttachTo(ff);

                    //                                            web.Navigate(src);

                    //                                            //if (i != null)
                    //                                            //{
                    //                                            //    i.src = src;
                    //                                            //}

                    //                                            __ContentKey
                    //                                                     .SetLeft(ff.Left)
                    //                                                     .SetTop(ff.Top);

                    //                                            ff.LocationChanged +=
                    //                                                delegate
                    //                                                {
                    //                                                    __ContentKey
                    //                                                        .SetLeft(ff.Left)
                    //                                                        .SetTop(ff.Top);
                    //                                                };

                    //                                            ff.SizeChanged +=
                    //                                                delegate
                    //                                                {
                    //                                                    __ContentKey
                    //                                                        .SetWidth(ff.Width)
                    //                                                        .SetHeight(ff.Height);
                    //                                                };

                    //                                            ff.FormClosing +=
                    //                                                delegate
                    //                                                {
                    //                                                    __ContentKey
                    //                                                        .Delete();
                    //                                                };


                    //                                            ff.GetHTMLTarget().With(
                    //                                                ffh =>
                    //                                                {
                    //                                                    dynamic ffhs = ffh.style;
                    //                                                    // http://css-infos.net/property/-webkit-transition
                    //                                                    //ffhs.webkitTransition = "webkitTransform 0.3s linear";

                    //                                                    ffh.onmousewheel +=
                    //                                                        e =>
                    //                                                        {
                    //                                                            e.preventDefault();
                    //                                                            e.stopPropagation();


                    //                                                            if (e.WheelDirection > 0)
                    //                                                            {
                    //                                                                ff.Width = (int)(ff.Width * 1.1);
                    //                                                                ff.Height = (int)(ff.Height * 1.1);
                    //                                                            }
                    //                                                            else
                    //                                                            {
                    //                                                                ff.Width = (int)(ff.Width * 0.9);
                    //                                                                ff.Height = (int)(ff.Height * 0.9);
                    //                                                            }

                    //                                                        };

                    //                                                }
                    //                                            );
                    //                                        }
                    //                                    );
                    //                                }
                    //                            );

                    //                            xhr.send(d);
                    //                        }
                    //                    );
                    //                    #endregion


                    // let's disable other handlers
                    //evt.dataTransfer = null;


                };
            #endregion

        }
    }
}
