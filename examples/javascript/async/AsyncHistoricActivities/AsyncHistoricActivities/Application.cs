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
using AsyncHistoricActivities;
using AsyncHistoricActivities.Design;
using AsyncHistoricActivities.HTML.Pages;
using ScriptCoreLib.Lambda;

namespace AsyncHistoricActivities
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {


        //looking at { Name = packages.config }
        //{ FixupHintPath = X:\jsc.svn\examples\javascript\async\AsyncHistoricActivities\packages\ScriptCoreLib.Async.1.0.0.0 }
        //will need to find package  { id = ScriptCoreLib.Async }
        //will find package  { id = ScriptCoreLib.Async }
        //cleaned { id = ScriptCoreLib.Async }
        //updating { id = ScriptCoreLib.Async }
        //updating { RestorePackagesFromFile = c:/util/jsc/nuget/ScriptCoreLib.Async.1.0.0.0.nupkg }
        //updated { id = ScriptCoreLib.Async }


        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp coldpage)
        {
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2013/201312/20131222-form


            //Native.window.onpopstate +=
            //    delegate
            //    {
            //        //Native.document.body.css.style.borderLeft = "1em solid yellow";

            //        new IHTMLPre { "onpopstate " + new { Native.document.location.hash } }.AttachToDocument();
            //    };

            //Native.window.onhashchange +=
            //    delegate
            //    {
            //        //Native.document.body.css.style.borderLeft = "1em solid yellow";

            //        new IHTMLPre { "onhashchange " + new { Native.document.location.hash } }.AttachToDocument();
            //    };

            // reentry shall be discared or a reload shall be done.
            coldpage.XLeft.Historic(
                async scope =>
                {
                    //0:66913ms event: onclick { href = http://192.168.43.252:12989/#/xleft, MouseButton = 1 } view-source:36552
                    //0:66914ms HistoryExtensions pushState before yield 

                    var borderBottom = "1em solid red";

                    Native.document.body.css.With(
                        async css =>
                        {
                            Native.document.body.css.style.borderLeft = "1em solid red";
                            await 100;
                            Native.document.body.css.style.borderLeft = "1em solid yellow";
                            await 100;
                            Native.document.body.css.style.borderLeft = "1em solid red";
                            await 100;
                            Native.document.body.css.style.borderLeft = "1em solid yellow";
                            await 100;
                            Native.document.body.css.style.borderLeft = borderBottom;
                        }
                    );


                    // we are in this state now.

                    // are we able to detect if we a sub state is set?


                    //new IHTMLPre { new { Native.document.location.hash } }.AttachToDocument();


                    // if a refresh happens prior this we will need to do a full refresh?
                    await scope;

                    borderBottom = "1em solid gray";
                    Native.document.body.css.style.borderLeft = borderBottom;
                }
            );


            coldpage.XTop.Historic(
                async scope =>
                {
                    Native.document.body.css.style.borderTop = "1em solid red";

                    // we are in this state now.

                    await scope;

                    Native.document.body.css.style.borderTop = "1em solid gray";
                }
            );

            coldpage.XRight.Historic(
                async scope =>
                {
                    Native.document.body.css.style.borderRight = "1em solid red";

                    // we are in this state now.

                    await scope;

                    Native.document.body.css.style.borderRight = "1em solid gray";
                },

                replace: true
            );

            coldpage.XBottom.Historic(
                 async scope =>
                 {
                     var borderBottom = "1em solid red";

                     Native.document.body.css.With(
                         async css =>
                         {
                             Native.document.body.css.style.borderBottom = "1em solid red";
                             await 100;
                             Native.document.body.css.style.borderBottom = "1em solid yellow";
                             await 100;
                             Native.document.body.css.style.borderBottom = "1em solid red";
                             await 100;
                             Native.document.body.css.style.borderBottom = "1em solid yellow";
                             await 100;

                             // was the scope destroyed?
                             Native.document.body.css.style.borderBottom = borderBottom;
                         }
                      );

                     // we are in this state now.

                     await scope;

                     borderBottom = "1em solid gray";
                     Native.document.body.css.style.borderBottom = borderBottom;
                 },

                 replace: true
             );
        }

    }
}
