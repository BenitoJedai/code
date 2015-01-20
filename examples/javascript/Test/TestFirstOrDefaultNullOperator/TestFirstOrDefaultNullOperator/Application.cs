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
using TestFirstOrDefaultNullOperator;
using TestFirstOrDefaultNullOperator.Design;
using TestFirstOrDefaultNullOperator.HTML.Pages;

namespace TestFirstOrDefaultNullOperator
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {
        static string Invoke(IEnumerable<string> e)
        {
            // this will create a pop opcode
            // switch rewriter should be triggered for such cases

            // X:\jsc.svn\core\ScriptCoreLib\Shared\BCLImplementation\System\Runtime\CompilerServices\IAsyncMethodBuilder.cs


            return e.FirstOrDefault()?.Trim();
        }


        public Application(IApp page)
        {
            var Length = Invoke(
                new[] { "  a", " b" }
            ).Length;

            new IHTMLPre { new { Length } }.AttachToDocument();

        }

    }
}
