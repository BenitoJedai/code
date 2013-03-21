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
using com.abstractatech.appmanager.Design;
using com.abstractatech.appmanager.HTML.Pages;

namespace com.abstractatech.appmanager
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
            // http://stackoverflow.com/questions/2279978/webview-showing-white-bar-on-right-side
            // webview.setScrollBarStyle(WebView.SCROLLBARS_OUTSIDE_OVERLAY);

            var count = 0;

            yield_ACTION_MAIN yield = (
                        packageName,
                        name,
                        icon_base64,
                        label
                    ) =>
                    {
                        count++;

                        var a = new AppPreview();

                        a.Icon.src = "data:image/png;base64," + icon_base64;
                        a.Label.innerText = label;

                        a.Container.AttachTo(page.ScrollArea);

                    };

            #region more
            var skip = 0;
            var take = 10;

            var getmore = "Scroll down for more...";

            new IHTMLButton { innerText = getmore }.AttachToDocument().With(
              more =>
              {
                  more.style.position = IStyle.PositionEnum.@fixed;
                  more.style.left = "2px";
                  more.style.bottom = "2px";

                  Action done = delegate { };


                  Action MoveNext = delegate
                  {
                      more.disabled = true;
                      more.innerText = "checking for more...";

                      Console.WriteLine("MoveNext: " + new { skip, take });

                      service.queryIntentActivities(
                          yield,
                          skip: "" + skip,
                          take: "" + take,
                          yield_done: done

                      );


                      //service.File_list("",
                      //    ydirectory: ydirectory,
                      //    yfile: yfile,
                      //    sskip: skip.ToString(),
                      //    stake: take.ToString(),
                      //    done: done
                      //);

                      skip += take;

                  };

                  done = delegate
                 {
                     more.innerText = getmore;
                     more.disabled = false;

                     if (count == skip)
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
                     }
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
            #endregion



            //@"Hello world".ToDocumentTitle();
            //// Send data from JavaScript to the server tier
        }

    }
}
