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
using TestAssignArrayToEnumerable;
using TestAssignArrayToEnumerable.Design;
using TestAssignArrayToEnumerable.HTML.Pages;

namespace TestAssignArrayToEnumerable
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
            // X:\jsc.svn\examples\javascript\Test\TestAsyncAssignArrayToEnumerable\TestAsyncAssignArrayToEnumerable\Application.cs
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201408/20140810/asenumerable

            //updating { RestorePackagesFromFile = c:/util/jsc/nuget/ScriptCoreLib.Async.1.0.0.0.nupkg }

            IEnumerable<object> collection = new[] { new object() };

            //  d = c.bwQABoBf2jWIHILvaqtMig();
            foreach (var item in collection)
            {
                Console.WriteLine(new { item });
            }
        }

    }
}
