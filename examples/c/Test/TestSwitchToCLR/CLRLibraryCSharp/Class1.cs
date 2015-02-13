using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CLRLibraryCSharp
{
    public class Win32Imports
    {
        [DllImport("version.dll")]
        public static extern bool GetFileVersionInfo(string sFileName,
              int handle, int size, byte[] infoBuffer);
        [DllImport("version.dll")]
        public static extern int GetFileVersionInfoSize(string sFileName,
              out int handle);

        // The third parameter - "out string pValue" - is automatically
        // marshaled from ANSI to Unicode:
        [DllImport("version.dll")]
        unsafe public static extern bool VerQueryValue(byte[] pBlock,
              string pSubBlock, out string pValue, out uint len);
        // This VerQueryValue overload is marked with 'unsafe' because 
        // it uses a short*:
        [DllImport("version.dll")]
        unsafe public static extern bool VerQueryValue(byte[] pBlock,
              string pSubBlock, out short* pValue, out uint len);


        [DllImport("version.dll")]
        unsafe public static extern bool VerQueryValue(byte[] pBlock,
            string pSubBlock, out IntPtr pValue, out uint len);



    }

    public class C
    {
        // http://referencesource.microsoft.com/#System/services/monitoring/system/diagnosticts/ProcessManager.cs
        // https://msdn.microsoft.com/en-us/library/aa288474(v=vs.71).aspx

        // Main is marked with 'unsafe' because it uses pointers:
        unsafe public static int Main(string sFileName = "printversion.exe")
        {
            try
            {
                // x:\jsc.svn\examples\c\test\testswitchtoclr\clrlibrarycsharp\class1.cs

                int handle = 0;
                // Figure out how much version info there is:
                int size =
                      Win32Imports.GetFileVersionInfoSize(sFileName,
                      out handle);

                if (size == 0) return -1;

                byte[] buffer = new byte[size];

                if (!Win32Imports.GetFileVersionInfo(sFileName, handle, size, buffer))
                {
                    Console.WriteLine("Failed to query file version information.");
                    return 1;
                }

                uint len = 0;

                //string spv = @"\StringFileInfo\" + subBlock[0].ToString("X4") + subBlock[1].ToString("X4") + @"\ProductVersion";
                string spv = @"\XKey2";

                var pVersion = default(IntPtr);
                // Get the ProductVersion value for this program:
                string versionInfo;

                // Unhandled exception at 0x77E0E753 (ntdll.dll) in TestSwitchToCLR.exe: 0xC0000374: A heap has been corrupted (parameters: 0x77E44270).

                var key = @"\XKey2";

                if (!Win32Imports.VerQueryValue(buffer, key, out pVersion, out len))
                {
                    Console.WriteLine("Failed to query version information.");
                    return 1;
                }

                // Show Details	Severity	Code	Description	Project	File	Line
                //Error CS0828  Cannot assign void* to anonymous type property CLRLibraryCSharp    Class1.cs   85

                //Console.WriteLine(new { pVersion });
                //Error CS0029  Cannot implicitly convert type 'void*' to 'object'  CLRLibraryCSharp Class1.cs   89

                // VALUE "XKey2", "base64"

                var v = Marshal.PtrToStringAnsi(pVersion);
                //Console.WriteLine("pVersion = \{v} ");

                Console.WriteLine(new { key, v });

                //Console.WriteLine("ProductVersion == {0}", versionInfo);
            }
            catch (Exception e)
            {
                Console.WriteLine("Caught unexpected exception " + e.Message);
            }

            return 0;
        }
    }

    public class Class1
    {
        // https://msdn.microsoft.com/en-us/library/aa288474(v=vs.71).aspx


        static void Main(string[] args)
        {
            Console.WriteLine("diagnostics!");
            // we could relaunch ourselves now.
        }



        //>	CLRLibraryCSharp.exe!CLRLibraryCSharp.Class1.Export4(long u) Line 122	C#
        // 	CLRLibrary.dll!CLRLibrary.Class1.Export4(long u) Line 92	Unknown
        //     [Native to Managed Transition]
        //     TestSwitchToCLR.exe!main(char** args) Line 42	C
        //   TestSwitchToCLR.exe!_printf()  Unknown
        //   kernel32.dll!7693338a() Unknown
        //   [Frames below may be incorrect and / or missing, no symbols loaded for kernel32.dll]
        //   ntdll.dll!77d79f72()    Unknown

        public static long Export4(long u)
        {
            return u + 5;
        }



        //>	CLRLibraryCSharp.exe!CLRLibraryCSharp.Class1.ExportPointer(CLRLibraryDllExportDefinition.uvec3ptr* p) Line 138	C#
        // 	CLRLibrary.dll!CLRLibrary.Class1.ExportPointer(CLRLibraryDllExportDefinition.uvec3ptr* p) Line 119	Unknown
        //     [Native to Managed Transition]
        //     TestSwitchToCLR.exe!main(char** args) Line 78	C
        //   TestSwitchToCLR.exe!@__security_check_cookie@4()   Unknown
        //   kernel32.dll!7693338a() Unknown
        //   [Frames below may be incorrect and / or missing, no symbols loaded for kernel32.dll]
        //   ntdll.dll!77d79f72()    Unknown
        //   ntdll.dll!77d79f45()    Unknown

        public unsafe static long ExportPointer(global::CLRLibraryDllExportDefinition.uvec3ptr* p)
        {
            if (p->position == null)
                return -1;

            return p->position->y;

            //return u + 5;
        }

        public static void Export2()
        {
            Console.WriteLine("hey! Export2");

            var e = Process.GetCurrentProcess();
            // e = null
            //+Assembly.GetExecutingAssembly() { CLRLibraryCSharp, Version = 1.0.0.0, Culture = neutral, PublicKeyToken = null}
            //System.Reflection.Assembly { System.Reflection.RuntimeAssembly}
            //+Assembly.GetCallingAssembly()   { CLRLibrary, Version = 1.0.0.0, Culture = neutral, PublicKeyToken = null}
            //System.Reflection.Assembly { System.Reflection.RuntimeAssembly}
            //Process.GetCurrentProcess() error CS0103: The name 'Process' does not exist in the current context


            //Console.WriteLine(new { e.MainModule.FileVersionInfo });


            C.Main(e.MainModule.FileName);

            Debugger.Launch();
            Debugger.Break();

        }

        public static unsafe long Export196(global::CLRLibraryDllExportDefinition.uvec3* u)
        //public static long Export196(ref global::CLRLibraryDllExportDefinition.uvec3 u)
        {
            //Error CS0208  Cannot take the address of, get the size of, or declare a pointer to a managed type('uvec3')   TestSwitchToCLR Program.cs  38

            //          > CLRLibraryCSharp.exe!CLRLibraryCSharp.Class1.Export196(CLRLibraryDllExportDefinition.uvec3 * u) Line 184 C#
            //CLRLibrary.dll!CLRLibrary.Class1.Export196(CLRLibraryDllExportDefinition.uvec3 * u) Line 135 Unknown
            //    [Native to Managed Transition]
            //  TestSwitchToCLR.exe!TestSwitchToCLR_Program_Invoke196(void* x) Line 96 C
            //  TestSwitchToCLR.exe!main(char * *args) Line 69 C


            return u->x;
            //return u.x;
        }
    }
}
