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

namespace WebServiceReturningInterface
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public class ApplicationWebService
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



        //20140910
        // webcrypto will enable static encrypted state sharing
        [Obsolete(
            "this web service is reinitialized for every request, as there will be a replay to call the interface"
            + "\n authorized for internal replay"
            + "\n JSC2014 initiative"
            )]
        public Task<ISpecial> GetInterface(string x)
        {
            // http://stackoverflow.com/questions/13049128/unable-to-declare-interface-async-taskmyobject-mymethodobject-myobj

            //0f7c:01:01 0037:006d WebServiceReturningInterface.Application define interface WebServiceReturningInterface::WebServiceReturningInterface.ISpecial
            //{ Location = X:\jsc.svn\examples\javascript\appengine\WebServiceReturningInterface\WebServiceReturningInterface\bin\Debug\WebServiceReturningInterface.exe, HintModuleName = <module>.SHA1c006c60203c650a20f910e635fbb1ac017adb373@836046658, GetHashCode = 42815556 }
            //ILStringConversion Prepare WebServiceReturningInterface.ISpecial
            //0f7c:01:01 RewriteToAssembly error: System.NotImplementedException: { SourceType = WebServiceReturningInterface.ISpecial }
            //   at jsc.meta.Library.ILStringConversions.Prepare(Type SourceType, Func`2 FieldSelector)
            //   at jsc.meta.Commands.Rewrite.RewriteToJavaScriptDocument.WebServiceForJavaScript.<>c__DisplayClass2b4.<WriteMethod>b__2a0(ILGenerator InvokeCallback_il)
            //   at ScriptCoreLib.Extensions.LinqExtensions.With[T](T e, Action`1 h)

            // we have to study, how we did the interface support for
            // flash and applets to continue

            return

                ((ISpecial)

                new XSpecial
            {
                x = x,

                AtInvokeSpecial = y =>
                {
                    // what other examples do we have here?

                    Debugger.Break();
                }

                // Error	1	Cannot implicitly convert type 'System.Threading.Tasks.Task<WebServiceReturningInterface.XSpecial>' to 'System.Threading.Tasks.Task<WebServiceReturningInterface.ISpecial>'	X:\jsc.svn\examples\javascript\appengine\WebServiceReturningInterface\WebServiceReturningInterface\ApplicationWebService.cs	61	17	WebServiceReturningInterface


                // public static Task<TSource> ToTaskResult<TSource>(this TSource source);

                //}.ToTaskResult<ISpecial>();
            }
                )
                .ToTaskResult();
        }
    }

    public class XSpecial : ISpecial
    {
        public string x;

        public Action<string> AtInvokeSpecial;

        public void InvokeSpecial(string y)
        {
            AtInvokeSpecial(y);
        }
    }

    public interface ISpecial
    {
        void InvokeSpecial(string y);
    }
}
