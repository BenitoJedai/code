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
using AsyncNestedTaskDelay;
using AsyncNestedTaskDelay.Design;
using AsyncNestedTaskDelay.HTML.Pages;
using System.Threading.Tasks;

namespace AsyncNestedTaskDelay
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

            Func<Task> go = async delegate
            {
                new IHTMLPre { innerText = "enter" }.AttachToDocument();

                Func<Task> f = async delegate
                {
                    new IHTMLPre { innerText = "f" }.AttachToDocument();

                    await Task.Delay(1000);
                };
                new IHTMLPre { innerText = "before f" }.AttachToDocument();
                await f();
                new IHTMLPre { innerText = "after f" }.AttachToDocument();

                await Task.Delay(300);

                new IHTMLPre { innerText = "exit" }.AttachToDocument();


            };


            go().GetAwaiter().OnCompleted(
                delegate
                {
                    new IHTMLPre { innerText = "done" }.AttachToDocument();
                }
            );

        }

    }
}
