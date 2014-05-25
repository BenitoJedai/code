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
using TestSwitchRewriteByRef;
using TestSwitchRewriteByRef.Design;
using TestSwitchRewriteByRef.HTML.Pages;

namespace TestSwitchRewriteByRef
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {
    
        // Error	3	Iterators cannot have ref or out parameters	X:\jsc.svn\examples\javascript\test\TestSwitchRewriteByRef\TestSwitchRewriteByRef\Application.cs	27	58	TestSwitchRewriteByRef
        public static IEnumerable<string> Stream(int index = 0)
        {
            do
            {
                yield return new { index }.ToString();
            }
            while (index++ < 10);
        }

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {

            //new IHTMLPre { Stream() }.AttachToDocument();

            Stream().WithEach(x => new IHTMLPre { new { x } }.AttachToDocument());
        }

    }
}
