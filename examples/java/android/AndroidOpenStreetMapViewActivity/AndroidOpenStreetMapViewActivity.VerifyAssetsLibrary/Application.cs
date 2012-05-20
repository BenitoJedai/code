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

namespace AndroidOpenStreetMapViewActivity.VerifyAssetsLibrary
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            //  ReferenceAssetsLibrary /ProjectFileName:"$(ProjectPath)" /EnableUltraSource:true

            Environment.CurrentDirectory = @"Y:\jsc.svn\examples\java\android\AndroidOpenStreetMapViewActivity\AndroidOpenStreetMapViewActivity";

            Debugger.Break();


            Assembly.LoadFrom(
                @"c:\util\jsc\bin\jsc.meta.exe"
            ).EntryPoint.Invoke(
                null,
                new[]{
                    new string[]{
                        "ReferenceAssetsLibrary",
                        "/ProjectFileName:" + @"Y:\jsc.svn\examples\java\android\AndroidOpenStreetMapViewActivity\AndroidOpenStreetMapViewActivity\AndroidOpenStreetMapViewActivity.csproj",
                        "/EnableUltraSource:true",
                        "/DisableWorkerDomain"
                    }
                }
            );

            Debugger.Break();
        }
    }
}
