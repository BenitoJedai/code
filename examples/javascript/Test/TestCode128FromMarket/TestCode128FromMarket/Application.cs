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
using TestCode128FromMarket.Design;
using TestCode128FromMarket.HTML.Pages;

namespace TestCode128FromMarket
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
            // what if cache does not have our package, if we are a new project?
            // packages.config
            // <package id="Abstractatech.JavaScript.CODE128" version="1.0.0.0" targetFramework="net40" />
            // "C:\Users\Arvo\AppData\Local\NuGet\Cache\Abstractatech.JavaScript.CODE128.1.0.0.0.nupkg"

            {
                var data = new { foo = "text" }.ToString();


                data.ToCode128(
                    height: 20,
                    yield:
                        img =>
                        {
                            var c = new IHTMLCenter().AttachToDocument();

                            img.AttachTo(c);

                            new IHTMLBreak { }.AttachTo(c);
                            new IHTMLCode { innerText = data }.AttachTo(c).style.fontSize = "x-small";
                        }
                );
            }


            {
                var data = "1234567890";


                data.ToCode128(
                    height: 20,
                    yield:
                        img =>
                        {
                            var c = new IHTMLCenter().AttachToDocument();

                            img.AttachTo(c);
                            new IHTMLBreak { }.AttachTo(c);
                            new IHTMLCode { innerText = data }.AttachTo(c).style.fontSize = "x-small";
                        }
                );
            }

            IStyleSheet.Default.AddRule(".foo",
                r =>
                {
                    r.style.border = "1px solid black";
                    r.style.margin = "2em";
                }
            );

            Native.document.getElementsByClassName("foo").ToArray().WithEach(
                x =>
                {
                    (x.getAttribute("data-value") as string).With(
                        data =>
                        {
                            data.ToCode128(
                                  height: 20,
                                  yield:
                                      img =>
                                      {
                                          var c = new IHTMLCenter().AttachTo(x);

                                          img.AttachTo(c);
                                          new IHTMLBreak { }.AttachTo(c);
                                          new IHTMLCode { innerText = data }.AttachTo(c).style.fontSize = "x-small";
                                      }
                              );
                        }
                    );
                }
            );
        }

    }
}
