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
using DataTypesForWebServiceExperiment;
using DataTypesForWebServiceExperiment.Design;
using DataTypesForWebServiceExperiment.HTML.Pages;
using System.Threading.Tasks;

namespace DataTypesForWebServiceExperiment
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
            @"Hello world".ToDocumentTitle();
            // Send data from JavaScript to the server tier

            //IStyleSheet.Default.AddRule("body", "-webkit-transition: background-color .15s linear .1;", 0);

            (Native.document.body.style as dynamic).webkitTransition = "background-color 500ms ease-out";

            Action<ApplicationWebService> bind =
                service =>
                {

                    service.MakeCyan = delegate
                        {
                            Native.document.body.style.backgroundColor = "cyan";
                        };

                    service.MakeYellow = delegate
                    {
                        Native.document.body.style.backgroundColor = "yellow";
                    };

                    service.set_backgroundColor = value =>
                    {
                        Native.document.body.style.backgroundColor = value;
                    };
                };

            bind(this);


            new IHTMLButton { innerText = "WebMethod2" }.AttachToDocument().WhenClicked(
                async delegate
                {
                    var Result = await this.WebMethod2(
                         42,
                         value => value.ToDocumentTitle()
                     );

                    foreach (var item in Result)
                    {
                        new IHTMLPre { innerText = new { item }.ToString() }.AttachToDocument();

                    }
                }
            );

            new IHTMLButton { innerText = "WebMethod4" }.AttachToDocument().WhenClicked(
                 async delegate
                 {
                     var Result = await this.WebMethod4(
                          42,
                          value => value.ToDocumentTitle()
                      );

                     foreach (var item in Result)
                     {
                         new IHTMLPre { innerText = new { item.e }.ToString() }.AttachToDocument();

                     }
                 }
             );

            Func<ApplicationWebService, Task> y = null;

            y =
                async service =>
                {
                    var Result = await service.WebMethod8(
                      42,
                      value => value.ToDocumentTitle()
                   );

                    new IHTMLBreak { }.AttachToDocument();

                    Result.WithEach(
                        xservice =>
                        {
                            // for new we have to keep rebinding
                            // because the server seems to call our functions even if we do not set them?
                            // can we even send a null delegate to server?
                            bind(xservice);

                            new IHTMLButton { innerText = "WebMethod8 " + new { xservice.xe }.ToString() }.AttachToDocument().WhenClicked(
                                   async delegate
                                   {

                                       await y(xservice);
                                   }
                            );


                        }
                    );

                };

            new IHTMLButton { innerText = "WebMethod8" }.AttachToDocument().WhenClicked(
               async delegate
               {

                   await y(this);
               }
           );
        }

    }
}
