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
using TestAsyncAssignArrayToEnumerable;
using TestAsyncAssignArrayToEnumerable.Design;
using TestAsyncAssignArrayToEnumerable.HTML.Pages;

namespace TestAsyncAssignArrayToEnumerable
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
            // X:\jsc.svn\examples\javascript\Test\TestAssignArrayToEnumerable\TestAssignArrayToEnumerable\Application.cs

            Invoke();
        }


        async void Invoke()
        {
            IEnumerable<object> collection = new[] { new object() };

 
            await Task.Yield();

            //  d = c.bwQABoBf2jWIHILvaqtMig();
            foreach (var item in collection)
            {
                Console.WriteLine(new { item });
            }
        }
    }
}
