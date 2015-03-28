using System;
using System.Collections.Generic;
using System.Text;
using ScriptCoreLib;
using System.Threading;

namespace ScriptCoreLibJava.BCLImplementation.System
{
    // http://referencesource.microsoft.com/#mscorlib/system/environment.cs
    // https://github.com/dotnet/coreclr/blob/master/src/mscorlib/src/System/Environment.cs
    // https://github.com/Microsoft/referencesource/blob/master/mscorlib/system/environment.cs
    // https://github.com/mono/mono/blob/master/mcs/class/corlib/System/Environment.cs

    // https://github.com/Reactive-Extensions/IL2JS/blob/master/mscorlib/System/Environment.cs
    // https://github.com/erik-kallen/SaltarelleCompiler/blob/develop/Runtime/CoreLib/Environment.cs
    // https://github.com/kswoll/WootzJs/blob/master/WootzJs.Runtime/Environment.cs

    // X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Environment.cs
    // X:\jsc.svn\core\ScriptCoreLibJava\BCLImplementation\System\Environment.cs


    [Script(Implements = typeof(global::System.Environment))]
    internal class __Environment
    {
        // X:\jsc.svn\examples\java\hybrid\Test\TestJVMCLRYieldStatement\TestJVMCLRYieldStatement\Program.cs
        // X:\jsc.svn\examples\java\hybrid\JVMCLRLoadLibrary\JVMCLRLoadLibrary\Program.cs

        public static int CurrentManagedThreadId
        {
            get
            {
                return Thread.CurrentThread.ManagedThreadId;
            }
        }

        public static int ProcessorCount
        {
            get
            {
                // X:\jsc.svn\examples\java\hybrid\Test\JVMCLRProcessorCount\JVMCLRProcessorCount\Program.cs

                // http://stackoverflow.com/questions/13834692/threads-configuration-based-on-no-of-cpu-cores
                // http://stackoverflow.com/questions/4759570/finding-number-of-cores-in-java
                int cores = java.lang.Runtime.getRuntime().availableProcessors();

                return cores;
            }
        }

        public static string NewLine
        {
            get
            {
                return "\r\n";
            }
        }


        public static string CurrentDirectory
        {
            // X:\jsc.svn\examples\java\hybrid\JVMCLRLINQOrderByLastWriteTime\JVMCLRLINQOrderByLastWriteTime\Program.cs

            // You cannot change the library path for a running JVM.
            // http://stackoverflow.com/questions/5013547/how-to-influence-search-path-of-system-loadlibrary-through-java-code

            get
            {
                // http://www.devx.com/tips/Tip/13804

                var f = new java.io.File(".");
                var c = default(string);

                try
                {
                    c = f.getCanonicalPath();
                }
                catch
                {
                    throw;
                }

                return c;
            }
        }
    }
}
