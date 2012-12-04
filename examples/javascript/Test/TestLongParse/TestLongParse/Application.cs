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
using TestLongParse.Design;
using TestLongParse.HTML.Pages;

namespace TestLongParse
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
            Action<string> parse =
                text =>
                {
                    var value = 0L;

                    try
                    {
                        value = long.Parse(text);

                        Native.Window.alert("ok: " + value);
                    }
                    catch
                    {
                        Native.Window.alert("fail: " + text);
                    }
                };

            page.ok.onclick +=
                delegate
                {
                    parse(page.ok.innerText);
                };


            page.fail.onclick +=
                delegate
                {
                    parse(page.fail.innerText);
                };

            @"Hello world".ToDocumentTitle();
            // Send data from JavaScript to the server tier
            service.WebMethod2(
                @"A string from JavaScript.",
                value => value.ToDocumentTitle()
            );
        }

    }
}
