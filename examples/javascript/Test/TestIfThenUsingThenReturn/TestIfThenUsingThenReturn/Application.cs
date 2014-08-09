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
using TestIfThenUsingThenReturn;
using TestIfThenUsingThenReturn.Design;
using TestIfThenUsingThenReturn.HTML.Pages;

namespace TestIfThenUsingThenReturn
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
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201408/20140809
            //updating { RestorePackagesFromFile = c:/ util / jsc / nuget / ScriptCoreLib.Async.1.0.0.0.nupkg }
            Action y = delegate
            {

                Console.WriteLine("before if");
                if (page != null)
                {
                    using (new x())
                    {
                        //if (page != null)
                        //{
                        Console.WriteLine("in using 1");
                        //}
                        //else
                        //{
                        //    Console.WriteLine("in using 2");
                        //}

                            // jsc when will we allow it?
                        return;
                    }
                    return;
                }
                Console.WriteLine("after if");
            };


            y();


        }

    }

    class x : IDisposable
    {
        public void Dispose()
        {
        }
    }
}
