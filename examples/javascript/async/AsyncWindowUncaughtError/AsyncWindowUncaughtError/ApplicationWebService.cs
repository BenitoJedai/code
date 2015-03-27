using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AsyncWindowUncaughtError
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public class ApplicationWebService : IUncaughtErrorHandler
    {
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


        //public void window_onerror(string message, int lineno, string filename)
        public void onerror(IUncaughtErrorHandlerArguments e)
        {
            // { e = { message = Uncaught TypeError: Cannot set property 'innerHTML' of null, lineno = 54985, filename = http://192.168.43.252:2855/view-source } }

            //Debugger.Break();
            // http://www.tutorialspoint.com/java/lang/throwable_setstacktrace.htm

            // CallerMemberName
            // X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Exception.cs

            //var q = ExceptionDispatchInfo.Capture(
            System.Runtime.Remoting.Messaging.CallContext.SetData("onerror", e);


            // http://referencesource.microsoft.com/#mscorlib/system/exception.cs#a445c4e8ae46b283
            var z = new Exception(
                e.message
            );

            //  _remoteStackTraceString = info.GetString("RemoteStackTraceString");

            //(z as dynamic)._remoteStackTraceString = e.stack;
            //TypeError: Cannot set property 'innerHTML' of null
            //    at AgAABvomQzij7z_apyke6mQ (http://192.168.1.76:29053/view-source:54106:17)
            //    at HTMLHtmlElement.<anonymous> (http://192.168.1.76:29053/view-source:872:85)

            //    -		(z as dynamic)._remoteStackTraceString = e.stack	'(z as dynamic)._remoteStackTraceString = e.stack' threw an exception of type 'Microsoft.CSharp.RuntimeBinder.RuntimeBinderException'	dynamic {Microsoft.CSharp.RuntimeBinder.RuntimeBinderException}
            //+		base	{"'System.Exception._remoteStackTraceString' is inaccessible due to its protection level"}	System.Exception {Microsoft.CSharp.RuntimeBinder.RuntimeBinderException}


            var _remoteStackTraceString = typeof(Exception).GetField("_remoteStackTraceString", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);

            _remoteStackTraceString.SetValue(z, e.stack);


            // http://reflector.webtropy.com/default.aspx/4@0/4@0/untmp/DEVDIV_TFS/Dev10/Releases/RTMRel/ndp/clr/src/BCL/System/Runtime/Remoting/StackBuilderSink@cs/1305376/StackBuilderSink@cs
            // http://stackoverflow.com/questions/15410661/is-it-possible-to-get-a-good-stack-trace-with-net-async-methods

            //ExceptionExtensions.TryDebuggerBreak();
            // 		z.StackTrace	null	string

            // http://connect.microsoft.com/VisualStudio/feedback/details/689516/exceptiondispatchinfo-api-modifications

            //   _stackTraceString = null;

            // this is the way we need to serialize exceptions and stacktraces.
            // z.TargetSite = null
            // since we have the jsc compiler running, do we also have the TypeInfo available
            // of the original target site?
            // we would not be able to interact with it directly tho?
            Console.WriteLine(new { z.TargetSite });
            Console.WriteLine(new { z });
            Console.WriteLine(new { e });

            //enter NewGlobalInvokeMethod { Name = onerror }
            //{ z = System.Exception: Uncaught TypeError: Cannot set property 'innerHTML' of null
            //TypeError: Cannot set property 'innerHTML' of null
            //    at AgAABvomQzij7z_apyke6mQ (http://192.168.1.76:8100/view-source:54106:17)
            //    at HTMLHtmlElement.<anonymous> (http://192.168.1.76:8100/view-source:872:85) }
            //{ e = { message = Uncaught TypeError: Cannot set property 'innerHTML' of null, lineno = 54106, filename = http://192.168.1.76:8100/view-source } }
            //exit NewGlobalInvokeMethod { Name = onerror }

            // can jsc debugger translate our collected, obfuscated stacktrace back to the original?
            // we could then remove the comments in code?
            // would we need a chrome debugger extension too?

        }
    }



    //class ErrorEvent(string message, int lineno, string filename) {  }
    [Obsolete("experimental. used by IUncaughtErrorHandler.")]
    public sealed class IUncaughtErrorHandlerArguments
    {
		// X:\jsc.svn\examples\javascript\CodeTraceExperiment\CodeTraceExperiment\Application.cs

		public string message;

        // can we get the .NET IL info of it?
        // does jsc store IL info for inspection?

        public int lineno;
        public string filename;
        public string stack;

        public override string ToString()
        {
            return new { message, lineno, filename }.ToString();
        }
    }

    [Description("events like java have returned! :)")]
    [Obsolete("experimental. like IDispose.")]
    interface IUncaughtErrorHandler
    {


        [Obsolete("what about web workers?")]
        void onerror(IUncaughtErrorHandlerArguments e);

    }
}
