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
using DCIMCameraAppWithThumbnails.Design;
using DCIMCameraAppWithThumbnails.HTML.Pages;

namespace DCIMCameraAppWithThumbnails
{
    using ystring = Action<string>;
    using ScriptCoreLib.JavaScript.Runtime;


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
            Native.Document.title = "DCIM Thumbnails";

            // see also. http://en.wikipedia.org/wiki/Design_rule_for_Camera_File_system

            ystring ydirectory = path =>
            {

            };

            var container = new IHTMLCenter().AttachToDocument();

            ystring yfile = path =>
            {
                new IHTMLDiv { innerText = path }.With(
                    div =>
                    {
                        if (path.ToLower().EndsWith(".jpg"))
                        {
                            div.innerText = "";

                            div.AttachTo(container);
                            // hide path

                            //new IHTMLBreak().AttachTo(div);
                            div.style.display = IStyle.DisplayEnum.inline_block;

                            new IHTMLImage { }.AttachTo(div).With(
                                img =>
                                {
                                    // portrait mode only!

                                    div.style.color = JSColor.Red;
                                    img.src = "/thumb/" + path;

                                    #region onload +=
                                    img.InvokeOnComplete(
                                        delegate
                                        {
                                            div.style.color = JSColor.Green;


                                            img.onclick += delegate
                                            {
                                                if (img.src == "/thumb/" + path)
                                                {
                                                    img.src = "/io/" + path;
                                                    img.style.width = "100%";
                                                    div.style.display = IStyle.DisplayEnum.block;

                                                    var p = new IHTMLPre { }.AttachTo(div);
                                                    service.GetEXIF("/io/" + path,
                                                        x =>
                                                        {
                                                            p.innerText = x;
                                                        }
                                                    );

                                                    img.onclick += delegate
                                                    {
                                                        if (p == null)
                                                            return;

                                                        p.Orphanize();
                                                        p = null;
                                                        img.src = "/thumb/" + path;
                                                        img.style.width = "";
                                                    };
                                                }

                                            };
                                        }
                                    );
                                    #endregion



                                }
                            );
                        }
                    }
                );
            };

            var skip = 0;
            var take = 30;



            new IHTMLButton { innerText = "more" }.AttachToDocument().With(
                more =>
                {
                    Action MoveNext = delegate
                    {
                        more.disabled = true;
                        more.innerText = "checking for more...";

                        ystring done = delegate
                        {
                            more.innerText = "more";
                            more.disabled = false;

                        };

                        service.File_list("",
                            ydirectory: ydirectory,
                            yfile: yfile,
                            sskip: skip.ToString(),
                            stake: take.ToString(),
                            done: done
                        );

                        skip += take;

                    };


                    MoveNext();

                    more.onclick += delegate
                    {
                        MoveNext();
                    };



                    Native.Window.onscroll +=
                          e =>
                          {

                              Native.Document.body.With(
                                  body =>
                                  {
                                      if (more.disabled)
                                          return;

                                      if (body.scrollHeight - 1 <= Native.Window.Height + body.scrollTop)
                                      {
                                          MoveNext();
                                      }

                                  }
                            );

                          };
                }
            );



        }

    }
}
