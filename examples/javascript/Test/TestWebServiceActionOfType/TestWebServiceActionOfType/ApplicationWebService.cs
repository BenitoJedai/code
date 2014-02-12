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

namespace TestWebServiceActionOfType
{
    public class SearchItem
    {
        //0001 02000084 com.abstractatech.analytics.ApplicationWebService::<>f__AnonymousType$812$73`1
        //script: error JSC1000: Java : class import: no implementation for com.abstractatech.analytics.SearchItem at com.abstractatech.analytics.Global

        //      Title: C:\Windows\system32\cmd.exe
        //CommandLine: c:\util\jsc\bin\jsc.meta.exe  RewriteToJavaScriptDocument /assembly:"com.abstractatech.analytics.exe" /AttachDebugger:false /DisableWebServiceJava:false  /WebServiceJavaSettings.application:jsc-analytics /WebServiceJavaSettings.version:10 /DisableWorkerDomain
        //   Location: c:\util\jsc\bin\jsc.meta.exe

        //[jsc.internal] UnhandledException:
        //{ FullName = System.InvalidOperationException, InnerException =  }

        //{ ExceptionObject = System.InvalidOperationException: Java : class import: no implementation for com.abstractatech.analytics.SearchItem at com.abstractatech.analytics.Global

        public string Name;

        public long Size;
    }

    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public class ApplicationWebService
    {
        public void Search(string searchTerm, Action<SearchItem> yield)
        {

        }

    }
}
