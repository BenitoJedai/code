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
using AsyncWebMethod;
using AsyncWebMethod.Design;
using AsyncWebMethod.HTML.Pages;

namespace AsyncWebMethod
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
            new IHTMLButton { innerText = "invoke with data" }.AttachToDocument().onclick +=
                async e =>
                  {

                      new IHTMLPre { "will call service" }.AttachToDocument();

                      // <document><TaskComplete><TaskResult>{ e = goo }</TaskResult></TaskComplete></document>
                      var y = await service.WebMethod16("goo",

                          x =>
                          {
                              new IHTMLPre { "yield! " + new { x.text } }.AttachToDocument();
                              //new IHTMLPre { "yield!" }.AttachToDocument();
                          }
                      );

                      new IHTMLPre { "done! " + new { y.text } }.AttachToDocument();


                  };

            new IHTMLButton { innerText = "invoke with result" }.AttachToDocument().onclick +=
                async delegate
                {

                    new IHTMLPre { "will call service" }.AttachToDocument();

                    // <document><TaskComplete><TaskResult>{ e = goo }</TaskResult></TaskComplete></document>
                    var y = await service.WebMethod4("goo",

                        x =>
                        {
                            new IHTMLPre { "yield! " + new { x } }.AttachToDocument();
                            //new IHTMLPre { "yield!" }.AttachToDocument();
                        }
                    );

                    new IHTMLPre { "will call service done" }.AttachToDocument();


                };


            new IHTMLButton { innerText = "invoke" }.AttachToDocument().onclick +=
                 async delegate
                 {

                     new IHTMLPre { "will call service" }.AttachToDocument();

                     // <document><TaskComplete><TaskResult>{ e = goo }</TaskResult></TaskComplete></document>
                     await service.WebMethod8("goo",

                         x =>
                         {
                             new IHTMLPre { "yield! " + new { x } }.AttachToDocument();
                             //new IHTMLPre { "yield!" }.AttachToDocument();
                         }
                     );

                     new IHTMLPre { "will call service done" }.AttachToDocument();


                 };



        }

    }
}
