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
using ScriptCoreLib.JavaScript.WebGL;
using ScriptCoreLib.JavaScript.Runtime;

namespace WebApplicationSelectingFile
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
        public Application(IDefault  page)
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

                    #region hex
                    new FileReader().With(
                        reader =>
                        {
                            reader.onload = IFunction.Of(
                                delegate
                                {
                                    var x = (ArrayBuffer)reader.result;

                                    var u8 = new Uint8Array(x, 0, 8);

                                    var pre = new IHTMLPre().AttachTo(n.Container);

                                    pre.style.border = "1px solid gray";

                                    for (uint i = 0; i < u8.length; i++)
                                    {
                                        pre.innerText += u8[i].ToString("x2") + " ";

                                    }
                                }
                            );

                            // Read in the image file as a data URL.
                            reader.readAsArrayBuffer(f);
                        }
                     );
                    #endregion

                    #region AsDataURL
                    Action<Action<string>> AsDataURL =
                        h =>
                        {
                            var reader = new FileReader();

                            reader.onload = IFunction.Of(
                                delegate
                                {
                                    var base64 = (string)reader.result;

                                    h(base64);

                                }
                            );
                            
                            // Read in the image file as a data URL.
                            reader.readAsDataURL(f);

                        };
                    #endregion


                    #region image
                    if (f.type.StartsWith("image/"))
                    {
                        AsDataURL(
                            base64 =>
                                new IHTMLImage
                                {
                                    src = base64,

                                }.AttachTo(n.Container)
                        );
                    }
                    #endregion


                    #region audio
                    if (f.type.StartsWith("audio/"))
                    {
                        AsDataURL(
                           base64 =>
                               new IHTMLAudio
                               {
                                   src = base64,
                                   controls = true,
                               }.AttachTo(n.Container)
                       );
                    }
                    #endregion

                    #region video
                    //if (f.type.StartsWith("video/"))
                    //{
                    //    AsDataURL(
                    //       base64 =>
                    //           new IHTMLVideo
                    //           {
                    //               src = base64,
                    //               controls = true,

                    //           }.AttachTo(n.Container)
                    //   );
                    //}
                    #endregion

                    n.Container.AttachTo(page.list);
                };
            #endregion


            #region onchange
            page.files.onchange +=
                e =>
                {
                    FileList x = page.files.files;
                    page.list.Clear();
                    page.list.Add("files: " + x.length);
                    for (uint i = 0; i < x.length; i++)
                    {
                        File f = x[i];

                        AtFile(f);
                    }

                };
            #endregion

            page.drop_zone.ondragleave +=
                delegate
                {
                    page.drop_zone.style.borderColor = JSColor.Gray;
                    page.drop_zone.style.backgroundColor = JSColor.None;
                };

            #region dragover
            page.drop_zone.ondragover +=
                    evt =>
                    {
                        page.drop_zone.style.borderColor = JSColor.Red;
                        page.drop_zone.style.backgroundColor = JSColor.FromRGB(0xff, 0xaf, 0xaf);

                        evt.StopPropagation();
                        evt.PreventDefault();
                        evt.dataTransfer.dropEffect = "copy"; // Explicitly show this is a copy.
                    };

            page.drop_zone.ondrop +=
                  evt =>
                  {
                      page.drop_zone.style.borderColor = JSColor.Green;
                      page.drop_zone.style.backgroundColor = JSColor.FromRGB(0xaf, 0xff, 0xaf);


                      evt.StopPropagation();
                      evt.PreventDefault();

                      FileList x = evt.dataTransfer.files; // FileList object.

                      page.list.Clear();
                      page.list.Add("files: " + x.length);
                      for (uint i = 0; i < x.length; i++)
                      {
                          File f = x[i];

                          AtFile(f);
                      }
                  };
            #endregion


        }

    }
}
