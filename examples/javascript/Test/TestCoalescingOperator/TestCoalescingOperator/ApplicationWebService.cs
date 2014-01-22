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

namespace TestCoalescingOperator
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public class ApplicationWebService
    {
        // X:\jsc.svn\examples\rewrite\Test\TestCoalescingOperator\TestCoalescingOperator\Class1.cs

        public int Counter;

        public Task<string> GetString()
        {
            Counter++;

            if (Counter % 2 == 0)
                return "hi".AsResult();

            // Error	2	Operator '.' cannot be applied to operand of type '<null>'	X:\jsc.svn\examples\javascript\Test\TestCoalescingOperator\TestCoalescingOperator\ApplicationWebService.cs	31	20	TestCoalescingOperator
            return default(string).AsResult();
        }
    }
}
