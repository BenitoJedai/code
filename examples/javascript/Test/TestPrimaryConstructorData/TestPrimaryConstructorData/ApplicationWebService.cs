using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TestPrimaryConstructorData
{
    // this looks like an attribute
    // lucky for us roslyn generates fields! :)
    public class FooRow(public string goo, public string zoo) : Attribute { }

    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public class ApplicationWebService
    {
        // X:\jsc.svn\examples\javascript\future\AsyncOrderByExpression\AsyncOrderByExpression\ApplicationWebService.cs


        [FooRow("goo", "zoo")]
        public FooRow Foo = new FooRow("goo", "zoo");

        public void WebMethod2(string e, Action<string> y)
        {
            // Send it back to the caller.
            y(e);
        }

    }
}
