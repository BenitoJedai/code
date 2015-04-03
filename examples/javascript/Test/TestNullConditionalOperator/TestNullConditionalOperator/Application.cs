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
using TestNullConditionalOperator;
using TestNullConditionalOperator.Design;
using TestNullConditionalOperator.HTML.Pages;

namespace TestNullConditionalOperator
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
			// http://en.wikipedia.org/wiki/Null_coalescing_operator

			// http://channel9.msdn.com/Blogs/Roslyn/Analyzer-sample-in-Dev14-CTP3
			// http://channel9.msdn.com/Events/Visual-Studio/Connect-event-2014/714
			// https://visualstudiogallery.msdn.microsoft.com/530dc77a-a9b0-43bf-9ae2-9498b0ec15da/view/Discussions

			var u = new string[0];

            // http://www.software-architects.com/devblog/2014/12/04/NET-Infoday-Whats-New-in-C-6
            // http://blogs.msdn.com/b/csharpfaq/archive/2014/11/20/new-features-in-c-6.aspx

            //var u0 = 
            var first = u?[0];  // null if customers is null

            new IHTMLPre { new { first } }.AttachToDocument();

            Action y = null;

            Native.document.onclick +=
                delegate
                {
                    Native.css.style.backgroundColor = "yellow";

                    // https://github.com/ljw1004/dotnet-code-analyzer
                    // http://blogs.msdn.com/b/csharpfaq/archive/2014/11/19/post-release-goodies.aspx
                    // this seems ugly
                    y?.Invoke();

                    Native.css.style.backgroundColor = "cyan";

                };
        }

    }
}
