using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using System;
//using System.Linq;
using ScriptCoreLib.Shared.BCLImplementation.System.Linq;
using System.Text;
using System.Xml.Linq;
using TestSelectMany.Design;
using TestSelectMany.HTML.Pages;

namespace TestSelectMany
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
        public Application(IDefaultPage page)
        {

            Action<string> WriteLine =
                text =>
                {
                    new IHTMLPre { innerText = text }.AttachToDocument();
                };


            Foo.InvokeTest(WriteLine);

            @"Hello world".ToDocumentTitle();
            // Send data from JavaScript to the server tier
            service.WebMethod2(
                @"A string from JavaScript.",
                value => value.ToDocumentTitle()
            );
        }

    }



    public class Foo
    {
        public static void InvokeTest(Action<string> WriteLine)
        {
            var Enumerable_Range_1_2 = new[] { 1, 2 }.AsEnumerable();
            var Enumerable_Range_3_2 = new[] { 3, 4 }.AsEnumerable();


            foreach (var item in

                Enumerable_Range_1_2

                .SelectMany(
                    x =>
                    {
                        WriteLine("SelectMany " + new { x });

                        return Enumerable_Range_3_2.Select(
                            y =>
                            {
                                WriteLine("Select " + new { x, y });

                                return new { x, y };
                            }
                        );
                    }
                )

                // jsc should call it automatically to allow arrays
                .AsEnumerable()

                )
            {
                WriteLine(item.ToString());
            }
        }

    }
}
