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
using TestIEnumerableForService;
using TestIEnumerableForService.Design;
using TestIEnumerableForService.HTML.Pages;

namespace TestIEnumerableForService
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
            // Error	3	Please see the event log for more details.	X:\jsc.svn\core\ScriptCoreLib.Ultra\ScriptCoreLib.Ultra\CSC	ScriptCoreLib.Ultra


            new IHTMLButton { "Invoke" }.AttachToDocument().WhenClicked(
                delegate
            {

                this.foo = from i in Enumerable.Range(0, 4)
                           select new foo("root#" + i,
                                    //children:
                                    //Enumerable.ToArray(
                                    from j in Enumerable.Range(0, i)
                                    select new foo("child#" + j)
                           //)
                           );


                // .field field_foo:

                this.foo.WithEach(
                    Console.WriteLine
                );

                this.Invoke(this.foo);
            }
            );

        }

    }
}
