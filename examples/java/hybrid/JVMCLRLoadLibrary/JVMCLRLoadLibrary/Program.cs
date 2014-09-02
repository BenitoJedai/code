using java.util.zip;
using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLibJava.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Runtime.InteropServices;

namespace JVMCLRLoadLibrary
{

    static class Program
    {
        //https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201409/20140902

        // do we need .dll suffix? shall __PlatformInvocationServices add it?
        //[DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        // WINAPI
        // i think jni will only work with Cdecl !!!
        [DllImport("kernel32", SetLastError = true, CharSet = CharSet.Auto, EntryPoint = "LoadLibraryA", CallingConvention = CallingConvention.Winapi)]
        static extern IntPtr LoadLibrary(string lpFileName);
        // shall jsc redirect kernel32.LoadLibrary into System.loadLibrary to get it to work?


        //static extern IntPtr LoadLibraryW(string lpFileName);

        // wtf?
        // http://msdn.microsoft.com/en-us/library/windows/desktop/ms684175(v=vs.85).aspx

        //{{ __path = X:\jsc.svn\examples\java\hybrid\JVMCLRLoadLibrary\JVMCLRLoadLibrary\bin\Release;C:\Windows\Sun\Java\bin;C:\Windows\system32;C:\Windows;C:\Program Files(x86)\Microsoft Visual Studio 12.0\Common7\IDE\CommonExtensions\Microsoft\TeamFoundation\Team E
        //enter CFunc { lib = kernel32, fname = LoadLibrary }
        //        enter CFunc InternalTryLoadLibrary { lib = kernel32, fname = LoadLibrary
        //    }
        //    enter CFunc InternalTryLoadLibrary { lib = X:\jsc.svn\examples\java\hybrid\JVMCLRLoadLibrary\JVMCLRLoadLibrary\bin\Release\kernel32, fname = LoadLibrary
        //}
        //enter CFunc InternalTryLoadLibrary { lib = X:\jsc.svn\examples\java\hybrid\JVMCLRLoadLibrary\JVMCLRLoadLibrary\bin\Release/kernel32, fname = LoadLibrary }
        //enter CFunc InternalTryLoadLibrary { lib = C:\Windows\Sun\Java\bin/kernel32, fname = LoadLibrary }
        //enter CFunc InternalTryLoadLibrary { lib = C:\Windows\system32/kernel32, fname = LoadLibrary }
        //enter CFunc InternalTryLoadLibrary { lib = C:\Windows/kernel32, fname = LoadLibrary }
        //enter CFunc InternalTryLoadLibrary { lib = C:\Program Files(x86)\Microsoft Visual Studio 12.0\Common7\IDE\CommonExtensions\Microsoft\TeamFoundation\Team Explorer\NativeBinaries/x86/kernel32, fname = LoadLibrary }
        //enter CFunc InternalTryLoadLibrary { lib = C:\Program Files\Common Files\Microsoft Shared\Windows Live/kernel32, fname = LoadLibrary }
        //enter CFunc InternalTryLoadLibrary { lib = C:\Program Files(x86)\Common Files\Microsoft Shared\Windows Live/kernel32, fname = LoadLibrary }
        //enter CFunc InternalTryLoadLibrary { lib = C:\Windows\system32/kernel32, fname = LoadLibrary }
        //enter CFunc InternalTryLoadLibrary { lib = C:\Windows/kernel32, fname = LoadLibrary }
        //enter CFunc InternalTryLoadLibrary { lib = C:\Windows\System32\Wbem/kernel32, fname = LoadLibrary }
        //enter CFunc InternalTryLoadLibrary { lib = C:\Windows\System32\WindowsPowerShell\v1.0\kernel32, fname = LoadLibrary }
        //enter CFunc InternalTryLoadLibrary { lib = C:\Program Files(x86)\Windows Live\Shared/kernel32, fname = LoadLibrary }
        //enter CFunc InternalTryLoadLibrary { lib = C:\Program Files(x86)\Microsoft ASP.NET\ASP.NET Web Pages\v1.0\kernel32, fname = LoadLibrary }
        //enter CFunc InternalTryLoadLibrary { lib = C:\Program Files\Microsoft SQL Server\110\Tools\Binn\kernel32, fname = LoadLibrary }
        //enter CFunc InternalTryLoadLibrary { lib = C:\Program Files\TortoiseSVN\bin/kernel32, fname = LoadLibrary }
        //enter CFunc InternalTryLoadLibrary { lib = C:\Program Files\TortoiseHg\kernel32, fname = LoadLibrary }
        //enter CFunc InternalTryLoadLibrary { lib = C:\Program Files(x86)\MiKTeX 2.9\miktex\bin\kernel32, fname = LoadLibrary }
        //enter CFunc InternalTryLoadLibrary { lib = C:\Program Files\Microsoft Network Monitor 3\kernel32, fname = LoadLibrary }
        //enter CFunc InternalTryLoadLibrary { lib = C:\Program Files\Microsoft\Web Platform Installer\kernel32, fname = LoadLibrary }
        //enter CFunc InternalTryLoadLibrary { lib = C:\Program Files(x86)\Heroku\bin/kernel32, fname = LoadLibrary }
        //enter CFunc InternalTryLoadLibrary { lib = C:\Program Files(x86)\git\bin/kernel32, fname = LoadLibrary }
        //enter CFunc InternalTryLoadLibrary { lib = c:\util\jsc\global/kernel32, fname = LoadLibrary }
        //enter CFunc InternalTryLoadLibrary { lib = C:\Program Files(x86)\Microsoft SDKs\TypeScript\1.0\kernel32, fname = LoadLibrary }
        //enter CFunc InternalTryLoadLibrary { lib = C:\Program Files\SlikSvn\bin/kernel32, fname = LoadLibrary }
        //enter CFunc InternalTryLoadLibrary { lib = C:\Program Files\TortoiseGit\bin/kernel32, fname = LoadLibrary }
        //enter CFunc InternalTryLoadLibrary { lib = C:\Program Files(x86)\MC#\bin/kernel32, fname = LoadLibrary }
        //enter CFunc InternalTryLoadLibrary { lib = C:\Program Files (x86)\Google\google_appengine\kernel32, fname = LoadLibrary }
        //enter CFunc InternalTryLoadLibrary { lib = ./kernel32, fname = LoadLibrary }
        //crash CFunc { lib = kernel32, fname = LoadLibrary }

        //        c:\Windows\System32>dir ke*
        // Volume in drive C is OS
        // Volume Serial Number is 1EF4-549B

        // Directory of c:\Windows\System32

        //2014-05-30  11:08 AM           728,064 kerberos.dll
        //2014-03-04  12:44 PM         1,163,264 kernel32.dll
        //2014-03-04  12:44 PM           424,960 KernelBase.dll
        //2009-07-14  04:41 AM            18,432 kernelceip.dll
        //2009-07-14  04:41 AM            29,184 keyiso.dll
        //2009-07-14  04:41 AM           169,984 keymgr.dll
        //               6 File(s)      2,533,888 bytes
        //               0 Dir(s)   1,209,643,008 bytes free

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main(string[] args)
        {
            // jsc needs to see args to make Main into main for javac..


            // see also>
            // X:\jsc.svn\examples\javascript\android\AndroidBroadcastLogger\AndroidBroadcastLogger\ApplicationWebService.cs

            System.Console.WriteLine(
               typeof(object).AssemblyQualifiedName
            );

            try
            {
                // X:\jsc.svn\core\ScriptCoreLibJava.jni\jni\jnistb10.cs

                Console.WriteLine(
                    new { Environment.CurrentDirectory }
                );

                //        { { CurrentDirectory = X:\jsc.svn\examples\java\hybrid\JVMCLRLoadLibrary\JVMCLRLoadLibrary\bin\Release } }
                //        { { Message = [UnsatisfiedLinkError] lib: kernel32.dll; fname: LoadLibrary; message: LoadLibrary, StackTrace = java.lang.RuntimeException: [UnsatisfiedLinkError]
                //lib: kernel32.dll; fname: LoadLibrary; message:LoadLibrary

                // {{ __path = X:\jsc.svn\examples\java\hybrid\JVMCLRLoadLibrary\JVMCLRLoadLibrary\bin\Release;C:\Windows\Sun\Java\bin;C:\Windows\system32;C:\Windows;C:\Program Files (x86)\Microsoft Visual Studio 12.0\Common7\IDE\CommonExtensions\Microsoft\TeamFoundation\Team Explorer\NativeBinaries/x86;C:\Program Files\Common Files\Microsoft Shared\Windows Live;C:\Program Files (x86)\Common Files\Microsoft Shared\Windows Live;C:\Windows\system32;C:\Windows;C:\Windows\System32\Wbem;C:\Windows\System32\WindowsPowerShell\v1.0\;C:\Program Files (x86)\Windows Live\Shared;C:\Program Files (x86)\Microsoft ASP.NET\ASP.NET Web Pages\v1.0\;C:\Program Files\Microsoft SQL Server\110\Tools\Binn\;C:\Program Files\TortoiseSVN\bin;C:\Program Files\TortoiseHg\;C:\Program Files (x86)\MiKTeX 2.9\miktex\bin\;C:\Program Files\Microsoft Network Monitor 3\;C:\Program Files\Microsoft\Web Platform Installer\;C:\Program Files (x86)\Heroku\bin;C:\Program Files (x86)\git\bin;c:\util\jsc\global;C:\Program Files (x86)\Microsoft SDKs\TypeScript\1.0\;C:\Program Files\SlikSvn\bin;C:\Program Files\TortoiseGit\bin;C:\Program Files (x86)\MC#\bin;C:\Program Files (x86)\Google\google_appengine\;. }}


                // System.setProperty( "java.library.path", "/path/to/libs" ); 

                //Environment.CurrentDirectory = @"c:\Windows\System32";

                var __path = java.lang.JavaSystem.getProperty("java.library.path");

                // X:\jsc.svn\core\ScriptCoreLibJava\java\lang\System.cs

                Console.WriteLine(
                      new { __path }
                  );

                //                j jni.CFunc.callCPtr([Ljava / lang / Object;)Ljni / CPtr; +0
                //j ScriptCoreLibJava.BCLImplementation.ScriptCoreLibA.Shared.__PlatformInvocationServices_Func___c__DisplayClassa._op_Implicit_b__9([Ljava / lang / Object;)LScriptCoreLibJava / BCLImplementation / System / __IntPtr; +13
                //v  ~StubRoutines::call_stub
                //j sun.reflect.NativeMethodAccessorImpl.invoke0(Ljava / lang / reflect / Method; Ljava / lang / Object;[Ljava/lang/Object;)Ljava/lang/Object;+0
                //j  sun.reflect.NativeMethodAccessorImpl.invoke(Ljava / lang / Object;[Ljava/lang/Object;)Ljava/lang/Object;+87
                //j  sun.reflect.DelegatingMethodAccessorImpl.invoke(Ljava / lang / Object;[Ljava/lang/Object;)Ljava/lang/Object;+6
                //j  java.lang.reflect.Method.invoke(Ljava / lang / Object;[Ljava/lang/Object;)Ljava/lang/Object;+57
                //j  ScriptCoreLibJava.BCLImplementation.System.Reflection.__MethodInfo.InternalInvoke(Ljava / lang / Object;[Ljava/lang/Object;)Ljava/lang/Object;+8
                //j  ScriptCoreLibJava.BCLImplementation.System.Reflection.__MethodBase.Invoke(Ljava / lang / Object;[Ljava/lang/Object;)Ljava/lang/Object;+3
                //j  ScriptCoreLibJava.BCLImplementation.ScriptCoreLibA.Shared.__PlatformInvocationServices_IntPtrFunc.Invoke([Ljava/lang/Object;)LScriptCoreLibJava/BCLImplementation/System/__IntPtr;+86
                //j  ScriptCoreLibJava.BCLImplementation.ScriptCoreLibA.Shared.__PlatformInvocationServices.InvokeIntPtr(Ljava / lang / String;Ljava / lang / String;[Ljava/lang/Object;)LScriptCoreLibJava/BCLImplementation/System/__IntPtr;+24
                //j  JVMCLRLoadLibrary.Program.LoadLibrary(Ljava / lang / String;)LScriptCoreLibJava/BCLImplementation/System/__IntPtr;+15

                //Console.WriteLine("before LoadLibrary");

                var winscard = LoadLibrary("winscard.dll");

                //                { { Message = [UnsatisfiedLinkError] lib: kernel32; fname: LoadLibrary; message: LoadLibrary, StackTrace = java.lang.RuntimeException: [UnsatisfiedLinkError]
                //        lib: kernel32; fname: LoadLibrary; message:LoadLibrary
                //at jni.CFunc.<init>(CFunc.java:82)
                //        at ScriptCoreLibJava.BCLImplementation.ScriptCoreLibA.Shared.__PlatformInvocationServices_Func.get_Method(__PlatformInvocationServices_Func.java:64)
                //        at ScriptCoreLibJava.BCLImplementation.ScriptCoreLibA.Shared.__PlatformInvocationServices_Func.To__PlatformInvocationServices_IntPtrFunc(__PlatformInvocationServices_Func.java:129)
                //        at ScriptCoreLibJava.BCLImplementation.ScriptCoreLibA.Shared.__PlatformInvocationServices.InvokeIntPtr(__PlatformInvocationServices.java:106)
                //        at JVMCLRLoadLibrary.Program.LoadLibrary(Program.java:46)
                //        at JVMCLRLoadLibrary.Program.main(Program.java:33)
                // }
                //}


                //                { { Message = [UnsatisfiedLinkError] lib: kernel32.dll; fname: LoadLibrary; message: LoadLibrary, StackTrace = java.lang.RuntimeException: [UnsatisfiedLinkError]
                //        lib: kernel32.dll; fname: LoadLibrary; message:LoadLibrary
                //at jni.CFunc.<init>(CFunc.java:82)
                //        at ScriptCoreLibJava.BCLImplementation.ScriptCoreLibA.Shared.__PlatformInvocationServices_Func.get_Method(__PlatformInvocationServices_Func.java:64)
                //        at ScriptCoreLibJava.BCLImplementation.ScriptCoreLibA.Shared.__PlatformInvocationServices_Func.To__PlatformInvocationServices_IntPtrFunc(__PlatformInvocationServices_Func.java:129)
                //        at ScriptCoreLibJava.BCLImplementation.ScriptCoreLibA.Shared.__PlatformInvocationServices.InvokeIntPtr(__PlatformInvocationServices.java:106)
                //        at JVMCLRLoadLibrary.Program.LoadLibrary(Program.java:46)
                //        at JVMCLRLoadLibrary.Program.main(Program.java:33)
                // }
                //}

                Console.WriteLine(new { winscard });
            }
            catch (Exception ex)
            {

                Console.WriteLine(

                    new { ex.Message, ex.StackTrace }
                    );
            }

            // { winscard = 1086128128 }



            CLRProgram.CLRMain();
        }


    }


    public delegate XElement XElementFunc();

    [SwitchToCLRContext]
    static class CLRProgram
    {
        public static XElement XML { get; set; }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void CLRMain()
        {
            System.Console.WriteLine(
                typeof(object).AssemblyQualifiedName
            );



            MessageBox.Show("click to close");

        }
    }


}
