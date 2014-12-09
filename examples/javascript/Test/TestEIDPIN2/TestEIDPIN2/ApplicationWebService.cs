using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ScriptCoreLib.Ultra.WebService;

namespace TestEIDPIN2
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public class ApplicationWebService
    {

        //---------------------------
        //Microsoft Visual Studio Express 2012 for Web
        //---------------------------
        //Visual Studio cannot start debugging because the debug target 'X:\jsc.svn\examples\javascript\Test\TestEIDPIN2\TestEIDPIN2\bin\Release\TestEIDPIN2.exe' is missing. Please build the project and retry, or set the OutputPath and AssemblyName properties appropriately to point at the correct location for the target assembly.
        //---------------------------
        //OK   
        //---------------------------


        // X:\jsc.svn\core\ScriptCoreLib.Ultra.Library\ScriptCoreLib.Ultra.Library\Extensions\TcpListenerExtensions.cs
        // what?
        // something is up with our port multiplexer?
        //     [HttpException (0x80004005): Path 'ET' is forbidden.]
        //System.Web.HttpMethodNotAllowedHandler.ProcessRequest(HttpContext context) +197
        //System.Web.CallHandlerExecutionStep.System.Web.HttpApplication.IExecutionStep.Execute() +1132
        //System.Web.HttpApplication.ExecuteStep(IExecutionStep step, Boolean& completedSynchronously) +114




        // "C:\util\android-sdk-windows\tools\ddms.bat"


        //{ certificate = [Subject]
        //        SERIALNUMBER=47101010033, G=MARI-LIIS, SN=MÄNNIK, CN="MÄNNIK,MARI-LIIS,47101010033", OU=authentication, O=ESTEID, C=EE

        //      [Issuer]
        //        E = pki @sk.ee, CN = TEST of ESTEID-SK 2011, O=AS Sertifitseerimiskeskus, C = EE

        //           [Serial Number]
        //  55E7F21C99857002543F9F63CB992E8C

        //[Not Before]
        //  2014-10-16 1:35:15 PM

        //[Not After]
        //  2019-10-15 11:59:59 PM

        //[Thumbprint]
        //  719D7077F28DF3B7C5DC5584F8E30D2E2317B449
        //, chain = System.Security.Cryptography.X509Certificates.X509Chain
        //    }




        /// <summary>
        /// This Method is a javascript callable method.
        /// </summary>
        /// <param name="e">A parameter from javascript.</param>
        /// <param name="y">A callback to javascript.</param>
        public void WebMethod2(string e, Action<string> y)
        {
            // Send it back to the caller.
            y(e);
        }

        // <pre id="client"></pre>
        // 

        public XElement client = new XElement(@"pre", @"...");
        //public XElement client = new XElement(@"pre", new { Console.Title }.ToString());

        public void InternalHandler(WebServiceHandler service)
        {
            client.Value = new { Console.Title }.ToString();

            // TcpListenerExtensions
            //  	[AppDomain (TestEIDPIN2.exe, #1) -> AppDomain (50af7929-1-130598245127061492, #2)]	

            // ouch. our ssl router is in the other AppDomain!

            //var x = ScriptCoreLib.Extensions.TcpListenerExtensions.RemoteCertificateValidationCallbackReplay;

        }
    }
}
