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
using DCIMCameraApp.Design;
using DCIMCameraApp.HTML.Pages;

namespace DCIMCameraApp
{
    using ystring = Action<string>;


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
            ystring ydirectory = path =>
            {

            };

            var container = new IHTMLDiv().AttachToDocument();

            ystring yfile = path =>
            {
                new IHTMLDiv { innerText = path }.AttachTo(container).With(
                    div =>
                    {
                        if (path.EndsWith(".jpg"))
                        {
                            new IHTMLBreak().AttachTo(div);

                            new IHTMLImage { src = "/io/" + path }.AttachTo(div).With(
                                img =>
                                {
                                    img.style.width = "100%";
                                }
                            );
                        }
                    }
                );
            };

            var skip = 0;
            var take = 10;



            new IHTMLButton { innerText = "more" }.AttachToDocument().With(
                more =>
                {
                    Action MoveNext = delegate
                    {
                        more.disabled = true;

                        ystring done = delegate
                        {
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
