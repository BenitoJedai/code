using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using ScriptCoreLib.Shared.Avalon.Extensions;

namespace APKWebServer
{
    class Program
    {
        [STAThread]
        public static void Main(string[] e)
        {
            /*
 
             * Current Path: Y:\jsc.svn\examples\java\HybridCLRJVMAPKWebServer\APKWebServer\bin\Debug

            Unhandled Exception: System.InvalidOperationException:  *** FATAL ERROR: internal compiler failure: System.IO.FileNotFoundException: Could not load file or assembly 'ClassLibrary1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null' or one of its dependencies. The system cannot find the file specified.
            File name: 'ClassLibrary1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'
               at System.Reflection.RuntimeAssembly._nLoad(AssemblyName fileName, String codeBase, Evidence assemblySecurity, RuntimeAssembly locationHint, StackCrawlMark& stackMark, IntPtr pPrivHostBinder, Boolean throwOnFileNotFound, Boolean forIntrospection, Boolean suppressSecurityChecks)
               at System.Reflection.RuntimeAssembly.nLoad(AssemblyName fileName, String codeBase, Evidence assemblySecurity, RuntimeAssembly locationHint, StackCrawlMark& stackMark, IntPtr pPrivHostBinder, Boolean throwOnFileNotFound, Boolean forIntrospection, Boolean suppressSecurityChecks)
               at System.Reflection.RuntimeAssembly.InternalLoadAssemblyName(AssemblyName assemblyRef, Evidence assemblySecurity, RuntimeAssembly reqAssembly, StackCrawlMark& stackMark, IntPtr pPrivHostBinder, Boolean throwOnFileNotFound, Boolean forIntrospection, Boolean suppressSecurityChecks)
               at System.Reflection.Assembly.Load(AssemblyName assemblyRef)
               at jsc.Loader.LoaderStrategy.<LoadReferencedAssemblies>d__f.MoveNext()
               at System.Linq.Buffer`1..ctor(IEnumerable`1 source)
               at System.Linq.Enumerable.<ReverseIterator>d__a0`1.MoveNext()
               at System.Linq.Enumerable.<DistinctIterator>d__81`1.MoveNext()
               at System.Linq.Buffer`1..ctor(IEnumerable`1 source)
               at System.Linq.Enumerable.ToArray[TSource](IEnumerable`1 source)
               at jsc.Program.TypedMain(CompileSessionInfo sinfo)

            === Pre-bind state information ===
            LOG: User = XT7\Arvo
            LOG: DisplayName = ClassLibrary1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
 
             */
            global::jsc.AndroidLauncher.Launch(
                 typeof(APKWebServer.Activities.ApplicationActivity)
            );
        }
    }
}
