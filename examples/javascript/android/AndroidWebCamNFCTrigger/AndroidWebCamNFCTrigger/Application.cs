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
using AndroidWebCamNFCTrigger;
using AndroidWebCamNFCTrigger.Design;
using AndroidWebCamNFCTrigger.HTML.Pages;

namespace AndroidWebCamNFCTrigger
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application
    {
        public readonly ApplicationWebService service = new ApplicationWebService();
        public readonly ApplicationControl control = new ApplicationControl();

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {

            // X:\jsc.svn\examples\javascript\WebCamToGIFAnimation\WebCamToGIFAnimation\Application.cs

            control.nfc.onnfc +=
                nfcid =>
                {
                    page.nfcid.innerText = new { nfcid }.ToString();
                };

            Native.window.navigator.getUserMedia(
               stream =>
               {

                   var v = new IHTMLVideo { src = stream.ToObjectURL() };

                   v.AttachToDocument();

                   v.play();



                   control.nfc.onnfc +=
                        nfcid =>
                        {
                            var c = new CanvasRenderingContext2D(v.clientWidth, v.clientHeight);

                            c.drawImage(
                                v, 0, 0, c.canvas.width, c.canvas.height
                            );

                            c.fillText(
                                new { nfcid }.ToString()
                                , 4, 32, 400
                            );

                            var bytes = c.bytes;

                            c.canvas.AttachToDocument();
                        };

                   new IHTMLButton { innerText = "record frame" }.AttachToDocument().onclick +=
                       delegate
                       {
                           var c = new CanvasRenderingContext2D(v.clientWidth, v.clientHeight);

                           c.drawImage(
                               v, 0, 0, c.canvas.width, c.canvas.height
                           );

                           c.fillText(
                              new { xnfcid = "?", page.nfcid }.ToString()
                              , 4, 32, 400
                          );

                           var bytes = c.bytes;

                           c.canvas.AttachToDocument();
                       };
               }
           );

        }

    }
}
