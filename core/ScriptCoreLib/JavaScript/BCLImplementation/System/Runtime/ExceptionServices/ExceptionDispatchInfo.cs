using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.Shared.BCLImplementation.Microsoft.CSharp;
using ScriptCoreLib.Shared.BCLImplementation.System.Dynamic;
using ScriptCoreLib.Shared.BCLImplementation.System.Runtime.CompilerServices;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Runtime.ExceptionServices
{
    // http://referencesource.microsoft.com/#mscorlib/system/runtime/exceptionservices/exceptionservicescommon.cs
    // https://github.com/dotnet/coreclr/blob/master/src/mscorlib/src/System/Runtime/ExceptionServices/ExceptionServicesCommon.cs


#if NET45
    [Script(Implements = typeof(global::System.Runtime.ExceptionServices.ExceptionDispatchInfo))]
#else
    [Script(ImplementsViaAssemblyQualifiedName = "System.Runtime.ExceptionServices.ExceptionDispatchInfo")]
#endif
    internal class __ExceptionDispatchInfo
    {
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201405/20140524

        public Exception SourceException { get; set; }

        public __ExceptionDispatchInfo(Exception e)
        {
            this.SourceException = e;

            // X:\jsc.svn\examples\javascript\forms\async\AsyncFinally\AsyncFinally\ApplicationControl.cs

        }



        //        Throw() : void
        //Analysis
        //Attributes
        //Signature Types
        //Declaring Module
        //Declaring Type
        //maxstack 8 (used 2)
        //IL Code (7)
        //0x0000 . ldarg.0        this [mscorlib] System.Runtime.ExceptionServices.ExceptionDispatchInfo
        //0x0001 . ldfld          [mscorlib] System.Runtime.ExceptionServices.ExceptionDispatchInfo.m_Exception : Exception
        //0x0006 . . ldarg.0      RestoreExceptionDispatchInfo(... exceptionDispatchInfo: this [mscorlib] System.Runtime.ExceptionServices.ExceptionDispatchInfo
        //0x0007 callvirt         [mscorlib] System.Exception.RestoreExceptionDispatchInfo(exceptionDispatchInfo : ExceptionDispatchInfo) : void
        //0x000c . ldarg.0        this [mscorlib] System.Runtime.ExceptionServices.ExceptionDispatchInfo
        //0x000d . ldfld          [mscorlib] System.Runtime.ExceptionServices.ExceptionDispatchInfo.m_Exception : Exception
        //0x0012 throw 

        public void Throw()
        {
            // ? restore async stacktrace?
            ((__Exception)(object)this.SourceException).RestoreExceptionDispatchInfo(this);

            throw this.SourceException;
        }

        public static __ExceptionDispatchInfo Capture(Exception e)
        {
            return new __ExceptionDispatchInfo(e);
        }
    }
}
