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

namespace TestPreparedWebServiceCallback
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public class ApplicationWebService
    {
        // if this was private, and
        // set by the server for later reuse, it could be encrypted
        public string state;

        public ApplicationWebService StillOnTheClient()
        {
            return new ApplicationWebService { state = "set at PreInvoke" };
        }

        //Error	2	Predefined type 'System.Runtime.CompilerServices.IAsyncStateMachine' is not defined or imported	X:\jsc.svn\examples\javascript\async\Test\TestPreparedWebServiceCallback\TestPreparedWebServiceCallback\CSC	TestPreparedWebServiceCallback
        //Error	4	Cannot find all types required by the 'async' modifier. Are you targeting the wrong framework version, or missing a reference to an assembly?	X:\jsc.svn\examples\javascript\async\Test\TestPreparedWebServiceCallback\TestPreparedWebServiceCallback\ApplicationWebService.cs	27	27	TestPreparedWebServiceCallback


        public async Task Invoke()
        {

            Console.WriteLine(
                new { state }
            );
        }
    }
}
