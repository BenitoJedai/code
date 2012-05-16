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

namespace TestFieldVersusPropertyCollision.Extender.PostBuild
{

    internal static class Program
    {
        public static void Main(string[] args)
        {
            Environment.CurrentDirectory = @"Y:\jsc.svn\examples\rewrite\TestFieldVersusPropertyCollision\TestFieldVersusPropertyCollision.Extender\bin\Debug";

            Assembly.LoadFrom(
                @"c:\util\jsc\bin\jsc.meta.exe"
            ).EntryPoint.Invoke(
                null,
                new[]{
                    new string[]{
                        "RewriteToAssembly",
                        "/AssemblyMerge:TestFieldVersusPropertyCollision.dll",
                        "/AssemblyMergeExtension:TestFieldVersusPropertyCollision.Extender.dll",
                        "/Output:TestFieldVersusPropertyCollisionX.dll",
                        "/DisableWorkerDomain"
                    }
                }
            );

            Debugger.Break();
        }
    }
}
