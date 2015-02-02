using ScriptCoreLib;
using ScriptCoreLibNative.SystemHeaders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

[assembly: Obfuscation(Feature = "script")]
//[assembly: Script(ScriptLibraries = new[] { typeof(CLRLibraryDllExportDefinition.uvec3) })]

namespace TestSwitchToCLR
{
    class Program
    {
        // https://msdn.microsoft.com/en-us/library/windows/desktop/aa381058(v=vs.85).aspx



        // http://mattshaw.org/news/fix-for-error-rc2144-primary-language-id-not-a-number/
        // This happens when Visual Studio 2010 breaks itself. It rewrites your Resources.rc and then complains that you fucked it up. Thanks. Adding #include <windows.h> seems to fix it…for now.

        // http://blogs.msdn.com/b/zakramer/archive/2006/05/22/603558.aspx

        // X:\jsc.svn\compiler\ScriptCoreLibA\SwitchToCLRContextAttribute.cs

        [Script(IsNative = true)]
        internal delegate void __Action();


        [Script(IsNative = true)]
        internal delegate long __LongToLong(long u);


        // public unsafe static long Export196(CLRLibraryDllExportDefinition.uvec3* u) => CLRLibraryCSharp.Class1.Export196(u);
        [Script(IsNative = true)]
        unsafe internal delegate long __Export196(CLRLibraryDllExportDefinition.uvec3* u);

        [Script(IsNative = true)]
        unsafe internal delegate long ExportPointer(CLRLibraryDllExportDefinition.uvec3ptr* u);

        //unsafe internal delegate long __Export196(ref CLRLibraryDllExportDefinition.uvec3 u);

        // should jsc generate delegate definitions?

        // foo.rc(3) : fatal error RC1015: cannot open include file 'resource.h'.
        // LINK : warning LNK4224: /INCREMENTAL:YES is no longer supported;  ignored

        unsafe static void Main(string[] args)
        {
            // Binary was not built with debug information.

            // http://choorucode.com/2012/09/13/visual-studio-2010-conversion-to-coff-failure/

            ScriptCoreLibNative.SystemHeaders.stdio_h.puts("hello. will switch to CLR...");

            // http://www.willus.com/mingw/colinp/win32/tools/dlltool.html
            // http://stackoverflow.com/questions/17935113/declspecdllimport-how-to-load-library

            // http://www.codeproject.com/Articles/146652/Creating-Import-Library-from-a-DLL-with-Header-Fil
            // http://hi.baidu.com/hainei_/item/dfcffb5cf442743932e0a912
            // http://andyrushton.co.uk/csharp-dynamic-loading-of-a-c-dll-at-run-time/

            var x = windows_h.LoadLibrary("CLRLibrary.dll");

            if (x == null)
            {
                ScriptCoreLibNative.SystemHeaders.stdio_h.puts("err 1");

                return;
            }

            {
                var y = windows_h.GetProcAddress(x, "Export2");
                if (y == null)
                {
                    ScriptCoreLibNative.SystemHeaders.stdio_h.puts("err 2");

                    return;
                }

                ScriptCoreLibNative.SystemHeaders.stdio_h.puts("before invoke...");

                var f = (__Action)y;

                f();
            }

            //         Unhandled Exception: System.IO.FileNotFoundException: Could not load file or assembly 'CLRLibraryCSharp, Version=1.0.0.0,
            //at CLRLibrary.Class1.Export2()

            {
                ScriptCoreLibNative.SystemHeaders.stdio_h.puts("before Export4...");

                var y = (__LongToLong)windows_h.GetProcAddress(x, "Export4");
                var yv = y(5);

                ScriptCoreLibNative.SystemHeaders.stdio_h.puts("after Export4...");

                // http://www.cplusplus.com/reference/cstdio/printf/
                // http://stackoverflow.com/questions/38561/what-is-the-argument-for-printf-that-formats-a-long
                ScriptCoreLibNative.SystemHeaders.stdio_h.printf("%lld", __arglist(yv));
            }

            Invoke196(x);

            {
                ScriptCoreLibNative.SystemHeaders.stdio_h.puts("before ");
                ScriptCoreLibNative.SystemHeaders.stdio_h.puts(nameof(ExportPointer));
                ScriptCoreLibNative.SystemHeaders.stdio_h.puts("...");
                var y = (ExportPointer)windows_h.GetProcAddress(x, nameof(ExportPointer));
                var yargsxyz = new CLRLibraryDllExportDefinition.uvec3();
                yargsxyz.y = 11;

                var yargs = new CLRLibraryDllExportDefinition.uvec3ptr();
                yargs.position = &yargsxyz;

                var yv = y(&yargs);
            }

            ScriptCoreLibNative.SystemHeaders.stdio_h.puts("done!");
        }

        private static unsafe void Invoke196(object x)
        {
            // script: error JSC1000: type not supported: CLRLibraryDllExportDefinition.uvec3 ; consider adding [ScriptAttribute]

            // dynamic callsite

            ScriptCoreLibNative.SystemHeaders.stdio_h.puts("before Export196...");
            var y = (__Export196)windows_h.GetProcAddress(x, "Export196");
            //var yargs = new CLRLibraryDllExportDefinition.uvec3 { x = 11, y = 22, z = 33 };
            var yargs = new CLRLibraryDllExportDefinition.uvec3();

            yargs.z = 66;

            //uvec31 = NULL;
            //&uvec31->z = ((signed long)(66));


            ScriptCoreLibNative.SystemHeaders.stdio_h.puts("yargs.z = ");
            ScriptCoreLibNative.SystemHeaders.stdio_h.printf("%lld", __arglist(yargs.z));

            var yargsref = &yargs;

            var yret = y(yargsref);

            ScriptCoreLibNative.SystemHeaders.stdio_h.puts("after Export196...");
            ScriptCoreLibNative.SystemHeaders.stdio_h.printf("%lld", __arglist(yret));

        }
    }
}
