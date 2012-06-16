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

namespace Verify
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            // Y:\jsc.internal.svn\compiler\jsc.meta\jsc.meta\Commands\Reference\ReferenceUltraSource\Plugins\IDLCompiler.cs


            Environment.CurrentDirectory = @"Y:\jsc.svn\examples\javascript\Test\TestHistoryAPI\TestHistoryAPI";

            Debugger.Break();


            Assembly.LoadFrom(
                @"c:\util\jsc\bin\jsc.meta.exe"
            ).EntryPoint.Invoke(
                null,
                new[]{
                    new string[]{
                        "ReferenceAssetsLibrary",
                        "/ProjectFileName:TestHistoryAPI.csproj",
                        "/EnableUltraSource:true",
                        //"/PrimaryType:org.osmdroid.util.MyMath",
                        "/DisableWorkerDomain"
                    }
                }
            );

            Debugger.Break();
        }
    }
}
