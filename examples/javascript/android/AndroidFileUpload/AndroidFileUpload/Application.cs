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
using AndroidFileUpload.Design;
using AndroidFileUpload.HTML.Pages;
using ScriptCoreLib.JavaScript.Runtime;
using System.Collections.Generic;
using System.Media;

namespace AndroidFileUpload
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


            // http://html5doctor.com/drag-and-drop-to-server/

            Native.Document.body.ondragover +=
                evt =>
                {
                    evt.stopPropagation();
                    evt.preventDefault();

                    evt.dataTransfer.dropEffect = "copy"; // Explicitly show this is a copy.


                    page.Header.style.color = JSColor.Green;

                    Console.WriteLine("ondragover: " + new
                    {

                        types = evt.dataTransfer.types.Length,
                        files = evt.dataTransfer.files.length
                    }
                    );
                };


            Native.Document.body.ondragleave +=
                delegate
                {
                    page.Header.style.color = JSColor.None;
                };

            Native.Document.body.ondrop +=
                evt =>
                {
                    //if (evt.dataTransfer == null)
                    //    return;



                    page.Header.style.color = JSColor.None;



                    #region files



                    // http://html5doctor.com/drag-and-drop-to-server/

#if FUTURE
                            service.XUpload(f, delegate { });
#endif


                    var d = new FormData();


                    evt.dataTransfer.files.AsEnumerable().WithEachIndex(
                          (f, index) =>
                          {
                              d.append(
                                  "foo" + index,
                                  f,
                                  f.name
                              );
                          }
                    );


                    var xhr = new IXMLHttpRequest();

                    xhr.open(ScriptCoreLib.Shared.HTTPMethodEnum.POST, "/upload");

                    xhr.InvokeOnComplete(
                        delegate
                        {
                            SystemSounds.Beep.Play();

                        }
                    );

                    xhr.send(d);

                    #endregion


                    // let's disable other handlers
                    //evt.dataTransfer = null;

                    evt.stopPropagation();
                    evt.stopImmediatePropagation();

                    evt.preventDefault();
                };
        }

    }

    public static class X
    {
        public static IEnumerable<File> AsEnumerable(this FileList f)
        {
            return Enumerable.Range(0, (int)f.length).Select(k => f[(uint)k]);
        }

    }

}
