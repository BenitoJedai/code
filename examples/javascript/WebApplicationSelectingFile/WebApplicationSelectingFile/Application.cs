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
using WebApplicationSelectingFile.Design;
using WebApplicationSelectingFile.HTML.Pages;
using ScriptCoreLib.JavaScript.FileAPI;

namespace WebApplicationSelectingFile
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
        public Application(IDefaultPage page)
        {
            #region AtFile
            Action<File> AtFile =
                f =>
                {
                    var n = new MyFile();
                    // databind here?
                    n.name.innerText = f.name;
                    n.type.innerText = f.type;
                    n.lastModifiedDate.innerText = "" + f.lastModifiedDate;

                    if (f.type.StartsWith("image/"))
                    {
                        var reader = new FileReader();

                        reader.onload = IFunction.Of(
                            delegate
                            {
                                var base64 = (string)reader.result;

                                new IHTMLImage
                                {
                                    src = base64,
                                    
                                }.AttachTo(n.Container);
                            }
                        );

                        // Read in the image file as a data URL.
                        reader.readAsDataURL(f);
                    }

                    n.Container.AttachTo(page.list);
                };
            #endregion


            #region onchange
            page.files.onchange +=
                e =>
                {
                    FileList x = page.files.files;
                    page.list.Add("files: " + x.length);
                    for (uint i = 0; i < x.length; i++)
                    {
                        File f = x[i];

                        AtFile(f);
                    }

                };
            #endregion

            #region dragover
            page.drop_zone.ondragover +=
                    evt =>
                    {
                        evt.StopPropagation();
                        evt.PreventDefault();
                        evt.dataTransfer.dropEffect = "copy"; // Explicitly show this is a copy.
                    };

            page.drop_zone.ondrop +=
                  evt =>
                  {
                      evt.StopPropagation();
                      evt.PreventDefault();

                      FileList x = evt.dataTransfer.files; // FileList object.

                      page.list.Add("files: " + x.length);
                      for (uint i = 0; i < x.length; i++)
                      {
                          File f = x[i];

                          AtFile(f);
                      }
                  };
            #endregion


            @"Hello world".ToDocumentTitle();
            // Send data from JavaScript to the server tier
            service.WebMethod2(
                @"A string from JavaScript.",
                value => value.ToDocumentTitle()
            );
        }

    }
}
