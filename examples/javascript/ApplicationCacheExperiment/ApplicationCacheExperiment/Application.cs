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
using ApplicationCacheExperiment.Design;
using ApplicationCacheExperiment.HTML.Pages;

namespace ApplicationCacheExperiment
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
            //Creating Application Cache with manifest http://192.168.1.103:24832/cache.manifest 192.168.1.103:1
            //Application Cache Checking event 192.168.1.103:1
            //Application Cache Downloading event 192.168.1.103:1
            //Application Cache Progress event (0 of 4) http://192.168.1.103:24832/assets/ScriptCoreLib/jsc.png 192.168.1.103:1
            //Application Cache Progress event (1 of 4) http://192.168.1.103:24832/assets/ScriptCoreLib/jsc.ico 192.168.1.103:1
            //Application Cache Progress event (2 of 4) http://192.168.1.103:24832/ 192.168.1.103:1
            //Application Cache Progress event (3 of 4) http://192.168.1.103:24832/view-source 192.168.1.103:1
            //Application Cache Progress event (4 of 4)  192.168.1.103:1
            //Application Cache Cached event 

            // Application Cache Error event: Master entry fetch failed (-1) http://192.168.1.100:8571/ 

            // 20141225 / superseded by service worker?
            Native.window.applicationCache.oncached = IFunction.Of(
                delegate
                {
                    page.Content.innerText = "up to date";

                }
            );

            Native.window.applicationCache.onerror = IFunction.Of(
                delegate
                {
                    page.Content.innerText = "offline?";

                }
            );


            Native.window.applicationCache.onnoupdate = IFunction.Of(
               delegate
               {
                   page.Content.innerText = "up to date";

               }
            );

            Native.window.ononline +=
                delegate
                {
                    page.Content.innerText = "online / wifi enabled" + new { Native.window.navigator.onLine };
                };

            Native.window.onoffline +=
              delegate
              {
                  page.Content.innerText = "offline  / wifi disabled" + new { Native.window.navigator.onLine };
              };


        }

    }
}
