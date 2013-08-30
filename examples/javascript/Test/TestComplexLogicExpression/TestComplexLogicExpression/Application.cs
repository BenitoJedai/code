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
using TestComplexLogicExpression;
using TestComplexLogicExpression.Design;
using TestComplexLogicExpression.HTML.Pages;

namespace TestComplexLogicExpression
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application
    {
        public readonly ApplicationWebService service = new ApplicationWebService();

        int z = 0;

        public bool x = true;
        public bool y = false;

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {

            Func<string, bool> log =
                text =>
                {
                    Console.WriteLine(new { text });
                    return false;
                };

            {
                //var x = y || (z == 1) || log("y not true, nor z is 1");
                var u = y || (z == 1) || (x != y);

                Console.WriteLine(new { u });
            }


            @"Hello world".ToDocumentTitle();
            // Send data from JavaScript to the server tier
            service.WebMethod2(
                @"A string from JavaScript.",
                value => value.ToDocumentTitle()
            );
        }

    }
}
