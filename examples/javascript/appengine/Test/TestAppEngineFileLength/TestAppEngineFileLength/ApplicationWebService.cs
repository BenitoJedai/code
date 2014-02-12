using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TestAppEngineFileLength
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
            // e = "http://192.168.43.252:11477/assets/TestAppEngineFileLength/App.css"

            //// Send it back to the caller.
            //y(e);

            //0001 02000005 com.abstractatech.analytics.ApplicationWebService::com.abstractatech.analytics.ApplicationWebService+<>c__DisplayClass1


            // Implementation not found for type import :
            // type: System.IO.FileInfo
            // method: Int64 get_Length()
            // Did you forget to add the [Script] attribute?
            // Please double check the signature!

            // assembly: W:\com.abstractatech.analytics.ApplicationWebService.exe
            // type: com.abstractatech.analytics.ApplicationWebService+<>c__DisplayClass9, com.abstractatech.analytics.ApplicationWebService, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
            // offset: 0x0028
            //  method:Void <Search>b__7(<>f__AnonymousType$21$2`3[System.String,System.String,System.String])

            y(
                new { new FileInfo(e).Length }.ToString()
            );

        }

    }
}
