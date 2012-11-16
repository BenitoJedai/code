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
using DropFileIntoSQLite.Design;
using DropFileIntoSQLite.HTML.Pages;
using System.Collections.Generic;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.Windows.Forms;
using System.Windows.Forms;
using System.Media;

namespace DropFileIntoSQLite
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
            Native.Document.body.ondragover +=
                evt =>
                {
                    evt.StopPropagation();
                    evt.PreventDefault();

                    evt.dataTransfer.dropEffect = "copy"; // Explicitly show this is a copy.


                    page.Header.style.color = JSColor.Green;

                };


            Native.Document.body.ondragleave +=
                delegate
                {
                    page.Header.style.color = JSColor.None;
                };

            Native.Document.body.ondrop +=
                evt =>
                {
                    evt.StopPropagation();
                    evt.PreventDefault();

                    evt.dataTransfer.files.AsEnumerable().WithEach(
                        (File f) =>
                        {
                            var ff = new Form();


                            ff.Text = new { f.type, f.name, f.size }.ToString();


                            ff.Show();

                            ff.MoveTo(evt.CursorX, evt.CursorY);

                            var fc = ff.GetHTMLTargetContainer();

                            fc.title = ff.Text;

                            if (f.type.StartsWith("image/"))
                            {
                                f.ToDataURLAsync(
                                    src =>
                                    {
                                        var i = new IHTMLImage { src = src }.AttachTo(fc);

                                        i.InvokeOnComplete(
                                            delegate
                                            {
                                                ff.ClientSize = new System.Drawing.Size(i.width, i.height);
                                            }
                                        );
                                    }
                                );
                            }

                            // http://html5doctor.com/drag-and-drop-to-server/

#if FUTURE
                            service.XUpload(f, delegate { });
#endif


                            var d = new FormData();

                            d.append("foo", f, f.name);

                            var xhr = new IXMLHttpRequest();

                            xhr.open(ScriptCoreLib.Shared.HTTPMethodEnum.POST, "/upload");

                            xhr.InvokeOnComplete(
                                delegate
                                {
                                    SystemSounds.Beep.Play();

                                    //Console.Beep();
                                }
                            );

                            xhr.send(d);
                        }
                    );
                };

            @"Hello world".ToDocumentTitle();
            // Send data from JavaScript to the server tier
            service.WebMethod2(
                @"A string from JavaScript.",
                value => value.ToDocumentTitle()
            );
        }

    }

    public static class X
    {
        public static IEnumerable<File> AsEnumerable(this FileList f)
        {
            return Enumerable.Range(0, (int)f.length).Select(k => f[(uint)k]);
        }

        public static void ToDataURLAsync(this Blob f, Action<string> y)
        {
            var reader = new FileReader();

            reader.onload = IFunction.Of(
                delegate
                {
                    var base64 = (string)reader.result;

                    y(base64);

                }
            );

            // Read in the image file as a data URL.
            reader.readAsDataURL(f);
        }
    }

}
