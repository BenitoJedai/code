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

            System.Runtime.Remoting.Messaging.CallContext.SetData("onerror", e);

            var z = new Exception(
                e.message
            );


            // http://reflector.webtropy.com/default.aspx/4@0/4@0/untmp/DEVDIV_TFS/Dev10/Releases/RTMRel/ndp/clr/src/BCL/System/Runtime/Remoting/StackBuilderSink@cs/1305376/StackBuilderSink@cs
            // http://stackoverflow.com/questions/15410661/is-it-possible-to-get-a-good-stack-trace-with-net-async-methods

            //ExceptionExtensions.TryDebuggerBreak();
            // 		z.StackTrace	null	string


            Console.WriteLine(new { e });
        }
    }



    //class ErrorEvent(string message, int lineno, string filename) {  }
    [Obsolete("experimental. used by IUncaughtErrorHandler.")]
    public sealed class IUncaughtErrorHandlerArguments
    {
        public string message;

        // can we get the .NET IL info of it?
        // does jsc store IL info for inspection?

        public int lineno;
        public string filename;

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
