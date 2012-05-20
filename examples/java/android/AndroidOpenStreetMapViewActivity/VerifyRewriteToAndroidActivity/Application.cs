using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using System;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Reflection;
using System.Diagnostics;

namespace VerifyRewriteToAndroidActivity
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            // c:\util\jsc\bin>jsc.internal.exe RewriteToJavaNatives /source:"C:\Program Files\Java\jre6\lib\rt.jar" /target:foo.dll

            Environment.CurrentDirectory = @"Y:\jsc.svn\examples\java\android\AndroidOpenStreetMapViewActivity\AndroidOpenStreetMapViewActivity\bin\Debug";

            Debugger.Break();


            Assembly.LoadFrom(
                @"c:\util\jsc\bin\jsc.meta.exe"
            ).EntryPoint.Invoke(
                null,
                new[]{
                    new string[]{
                        "RewriteToAndroidActivity",
                        "/EntryPointAssembly:AndroidOpenStreetMapViewActivity.exe",
                        "/DisableWorkerDomain"
                    }
                }
            );

            Debugger.Break();
        }
    }
}
