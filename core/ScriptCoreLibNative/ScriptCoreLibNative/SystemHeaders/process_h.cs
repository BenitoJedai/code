using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace ScriptCoreLibNative.SystemHeaders
{
    // http://msdn.microsoft.com/en-us/library/kdzttdcb(v=vs.120).aspx
    // #include <process.h>    /* _beginthread, _endthread */
    // Threading Win32 APIs are not part of the MSDK.
    // http://msdn.microsoft.com/library/windows/apps/jj606124.aspx
    // http://msdn.microsoft.com/en-us/library/ms235302.aspx
    // http://msdn.microsoft.com/en-us/library/esszf9hw.aspx
    //  Each thread shares all the process's resources.
    // http://msdn.microsoft.com/en-us/library/cbs61zxw.aspx
    // Each thread in a process operates independently. Unless you make them visible to each other, the threads execute individually and are unaware of the other threads in a process. Threads sharing common resources, however, must coordinate their work by using semaphores or another method of interprocess communication. 


    [Script(IsNative = true, Header = "process.h", IsSystemHeader = true)]
    public static class process_h
    {
        // http://blogs.msdn.com/b/winsdk/archive/2009/07/14/launching-an-interactive-process-from-windows-service-in-windows-vista-and-later.aspx
        // http://stackoverflow.com/questions/4278373/how-to-start-a-process-from-windows-service-into-currently-logged-in-users-sess
        // http://programmer2000.blogspot.com/2008/04/create-process-in-another-session.html

        // http://security.stackexchange.com/questions/58009/ways-to-inject-malicious-dlls-to-exe-file-and-run-it
        // You can list a dll under HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Windows\AppInit_DLLs and it will be loaded into every (new) process that links to user32.dll (i.e. pretty much every user-mode process)
        // could we await into a exe?
        // how would we communicate with the new exe?
        // http://migeel.sk/programming/pe-inject/
        // http://www.codeproject.com/Articles/4610/Three-Ways-to-Inject-Your-Code-into-Another-Proces
        // https://support.google.com/chrome/answer/2898334?p=ib_download_blocked&hl=en&rd=2
        // https://micksmix.wordpress.com/2014/05/01/psexec-now-encrypts-all-communication/
        // http://migeel.sk/blog/2007/07/30/injecting-code-into-executables-with-c/


        // http://msdn.microsoft.com/en-us/magazine/cc163327.aspx
        // http://msdn.microsoft.com/en-us/library/windows/desktop/ms682456(v=vs.85).aspx
        // X:\jsc.svn\examples\c\Test\TestThreadStart\TestThreadStart\Program.cs

        // int     ThreadNr;                    // Number of threads started 
        // _beginthread( BounceProc, 0, &ThreadNr );
        // void BounceProc( void *pMyID )
        // http://msdn.microsoft.com/en-us/library/kdzttdcb(v=vs.120).aspx

        //        uintptr_t _beginthread( // NATIVE CODE
        //   void( __cdecl* start_address)( void* ),
        //   unsigned stack_size,
        //   void* arglist 
        //);

        // http://stackoverflow.com/questions/1719784/c-programming-forward-variable-argument-list
        // http://www.digitalmars.com/rtl/process.html
        // The operating system passes arg to func when execution begins. arg can be any 32-bit value cast to void *.
        // http://www.tenouk.com/ModuleS.html
        // http://msdn.microsoft.com/en-us/library/kdzttdcb.aspx
        public static object _beginthread(IntPtr start_address, uint stack_size,

            // Argument list to be passed to a new thread, or NULL.
            object arglist)
        {
            return default(object);
        }

        //int _getpid(void);
    }

}
