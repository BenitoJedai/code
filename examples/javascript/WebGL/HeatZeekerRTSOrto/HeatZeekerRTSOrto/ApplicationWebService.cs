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

namespace HeatZeekerRTSOrto
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public class ApplicationWebService
    {

        //02000003 HeatZeekerRTSOrto.ApplicationWebService
        //{ SourceMethod = Void WebMethod2(System.String, System.Action`1[System.String]) }
        //02000002 HeatZeekerRTSOrto.Application
        //script: error JSC1000: Method: .ctor, Type: HeatZeekerRTSOrto.Application; emmiting failed : System.InvalidOperationException: { Location =
        // assembly: V:\HeatZeekerRTSOrto.Application.exe
        // type: HeatZeekerRTSOrto.Application, HeatZeekerRTSOrto.Application, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
        // offset: 0x033d
        //  method:Void.ctor(HeatZeekerRTSOrto.HTML.Pages.IApp) }

        ///// <summary>
        ///// The static content defined in the HTML file will be update to the dynamic content once application is running.
        ///// </summary>
        //public XElement Header = new XElement(@"h1", @"JSC - The .NET crosscompiler for web platforms. ready.");

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

    }
}
