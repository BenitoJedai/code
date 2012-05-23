using System;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Reflection;
using System.Diagnostics;

namespace VerifyPostBuildEvent
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            //if exist C:\util\jsc\bin\ScriptCoreLibAndroid.dll (

            //start /WAIT cmd /C c:\util\jsc\bin\jsc.meta.exe RewriteToAssembly /Output:"c:\util\jsc\bin\ScriptCoreLibAndroid.dll"   /AssemblyMerge:"c:\util\jsc\bin\ScriptCoreLibAndroid.dll"   /AssemblyMergeExtension:"$(TargetPath)" /AttachDebugger:false /RemoveObfuscationMergeAttributes:true /XAttachDebugger:true /EnableDelayedFileMove:true

            //exit 0
            //)

            Environment.CurrentDirectory = @"Y:\jsc.svn\core\ScriptCoreLibAndroid\ScriptCoreLibAndroid\bin\Debug";

            Debugger.Break();



            Assembly.LoadFrom(
                @"c:\util\jsc\bin\jsc.meta.exe"
            ).EntryPoint.Invoke(
                null,
                new[]{
                    new string[]{
                        "RewriteToAssembly",
                        @"/Output:c:\util\jsc\bin\ScriptCoreLibAndroid.dll",
                        @"/AssemblyMerge:c:\util\jsc\bin\ScriptCoreLibAndroid.dll",
                        @"/AssemblyMergeExtension:Y:\jsc.svn\core\ScriptCoreLibAndroid\ScriptCoreLibAndroid\bin\Debug\ScriptCoreLibAndroid.dll",
                        " /AttachDebugger:false /RemoveObfuscationMergeAttributes:true /XAttachDebugger:true /EnableDelayedFileMove:true",
                        "/DisableWorkerDomain"

                    }
                }
            );

            Debugger.Break();
        }
    }
}
