using ScriptCoreLib.JavaScript;

using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Runtime;
using System.Collections;
using System.Collections.Generic;
using ScriptCoreLib.JavaScript.Query;
using ScriptCoreLib.JavaScript.Extensions;
using System.Threading.Tasks;
using ScriptCoreLib.JavaScript.BCLImplementation.System.Threading.Tasks;
using System.Diagnostics;
using System.Xml.Linq;

namespace ScriptCoreLib.JavaScript.DOM
{
    public partial class INode :

        System.IDisposable
    {

        // if a native class
        // has any interfaces
        // where it defines mapped methods
        // as static
        // a dispatcher needs to be created?
        // like proxy?

        [Script(DefineAsStatic = true)]
        public void Dispose()
        {
            //Error CS1674  'IHTMLPre': type used in a using statement must be implicitly convertible to 'System.IDisposable'   TestLongWebMethod Application.cs  59

            // .AsDisposable

            // can a native object method be called via interface?

            // X:\jsc.svn\examples\javascript\css\Test\TestLongWebMethod\TestLongWebMethod\Application.cs

            var ee = this;



            var n = ee.parentNode;
            if (n != null)
                n.removeChild(ee);
        }
    }
}