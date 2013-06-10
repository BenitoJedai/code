//using Shell32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using ScriptCoreLib.Extensions;
using System.Runtime.InteropServices;

namespace com.abstractatech.adminshell.clrlauncher
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //////// http://www.west-wind.com/weblog/posts/2009/Oct/08/Application-that-wont-Pin-to-Taskbar-in-Windows-7
            // http://www.vbforums.com/showthread.php?624773-Local-System-service-starts-process-as-interactive-user-(no-pwd-needed)

            //STARTUPINFO s;
            //GetStartupInfo(out s);

            // pin and uninstall?

            //var p = Environment.CommandLine.SkipUntilIfAny("\"").TakeUntilIfAny("\"");

            //---------------------------

            //---------------------------
            //{ dwFlags = STARTF_USESHOWWINDOW, STARTF_USESTDHANDLES, lpTitle = X:\jsc.svn\examples\javascript\android\com.abstractatech.adminshell\com.abstractatech.adminshell.clrlauncher\bin\Debug\com.abstractatech.adminshell.clrlauncher.exe }
            //---------------------------
            //OK   
            //---------------------------

            //            ---------------------------

            //---------------------------
            //{ dwFlags = STARTF_TITLEISAPPID, lpTitle = com...tion_0000000000000000_684452339c6f6055 }
            //---------------------------
            //OK   
            //---------------------------

            // http://msdn.microsoft.com/en-us/library/windows/desktop/ms686331%28v=vs.85%29.aspx

            //HKEY_CLASSES_ROOT\Software\Microsoft\Windows\CurrentVersion\Deployment\SideBySide\2.0\PackageMetadata\{2ec93463-b0c3-45e1-8364-327e96aea856}_{3f471841-eef2-47d6-89c0-d028f03a4ad5}\com...tion_0000000000000000_684452339c6f6055

            //MessageBox.Show(
            //    new { s.dwFlags, s.lpTitle }.ToString()
            //    );


            //PinUnpinTaskBar(
            //    //new FileInfo(Assembly.GetEntryAssembly().Location).FullName
            //    p
            //);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new WithClickOnceLANLauncherClient.FindServiceProviderOverMulticastForm());
        }

        // http://www.vbforums.com/showthread.php?624773-Local-System-service-starts-process-as-interactive-user-(no-pwd-needed)

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        struct STARTUPINFO
        {
            public Int32 cb;
            public string lpReserved;
            public string lpDesktop;
            public string lpTitle;
            public Int32 dwX;
            public Int32 dwY;
            public Int32 dwXSize;
            public Int32 dwYSize;
            public Int32 dwXCountChars;
            public Int32 dwYCountChars;
            public Int32 dwFillAttribute;
            public StartupInfoFlags dwFlags;
            public Int16 wShowWindow;
            public Int16 cbReserved2;
            public IntPtr lpReserved2;
            public IntPtr hStdInput;
            public IntPtr hStdOutput;
            public IntPtr hStdError;
        }

        [Flags]
        enum StartupInfoFlags : uint
        {
            // Indicates that the cursor is in feedback mode for two seconds after CreateProcess is called. The Working in Background cursor is displayed (see the Pointers tab in the Mouse control panel utility).
            // If during those two seconds the process makes the first GUI call, the system gives five more seconds to the process. If during those five seconds the process shows a window, the system gives five more seconds to the process to finish drawing the window.
            // The system turns the feedback cursor off after the first call to GetMessage, regardless of whether the process is drawing.
            STARTF_FORCEONFEEDBACK = 0x40,

            // Indicates that the feedback cursor is forced off while the process is starting. The Normal Select cursor is displayed.
            STARTF_FORCEOFFFEEDBACK = 0x80,

            // Indicates that any windows created by the process cannot be pinned on the taskbar.
            // This flag must be combined with STARTF_TITLEISAPPID.
            STARTF_PREVENTPINNING = 0x2000,

            // Indicates that the process should be run in full-screen mode, rather than in windowed mode.
            // This flag is only valid for console applications running on an x86 computer.
            STARTF_RUNFULLSCREEN = 0x20,

            // The lpTitle member contains an AppUserModelID. This identifier controls how the taskbar and Start menu present the application, and enables it to be associated with the correct shortcuts and Jump Lists. Generally, applications will use the SetCurrentProcessExplicitAppUserModelID and GetCurrentProcessExplicitAppUserModelID functions instead of setting this flag. For more information, see Application User Model IDs.
            // If STARTF_PREVENTPINNING is used, application windows cannot be pinned on the taskbar. The use of any AppUserModelID-related window properties by the application overrides this setting for that window only.
            // This flag cannot be used with STARTF_TITLEISLINKNAME.
            STARTF_TITLEISAPPID = 0x1000,

            // The lpTitle member contains the path of the shortcut file (.lnk) that the user invoked to start this process. This is typically set by the shell when a .lnk file pointing to the launched application is invoked. Most applications will not need to set this value.
            // This flag cannot be used with STARTF_TITLEISAPPID.
            STARTF_TITLEISLINKNAME = 0x800,

            // The dwXCountChars and dwYCountChars members contain additional information.
            STARTF_USECOUNTCHARS = 0x8,

            // The dwFillAttribute member contains additional information.
            STARTF_USEFILLATTRIBUTE = 0x10,

            // The hStdInput member contains additional information.
            // This flag cannot be used with STARTF_USESTDHANDLES.
            STARTF_USEHOTKEY = 0x200,

            // The dwX and dwY members contain additional information.
            STARTF_USEPOSITION = 0x4,

            // The wShowWindow member contains additional information.
            STARTF_USESHOWWINDOW = 0x1,

            // The dwXSize and dwYSize members contain additional information.
            STARTF_USESIZE = 0x2,

            // The hStdInput, hStdOutput, and hStdError members contain additional information.
            // If this flag is specified when calling one of the process creation functions, the handles must be inheritable and the function//s bInheritHandles parameter must be set to TRUE. For more information, see Handle Inheritance.
            // If this flag is specified when calling the GetStartupInfo function, these members are either the handle value specified during process creation or INVALID_HANDLE_VALUE.
            // This flag cannot be used with STARTF_USEHOTKEY.
            STARTF_USESTDHANDLES = 0x100,
        }

        // http://msdn.microsoft.com/en-us/library/ms683230%28v=VS.85%29.aspx
        [DllImport("kernel32.dll", EntryPoint = "GetStartupInfoW")]
        static extern void GetStartupInfo(out STARTUPINFO lpStartupInfo);


        //// http://stackoverflow.com/questions/12232001/pin-app-in-taskbar-in-visual-studio-setup-project
        //private static void PinUnpinTaskBar(string filePath, bool pin = true)
        //{
        //    if (!File.Exists(filePath)) return;

        //    //Error	2	The type 'Shell32.ShellClass' has no constructors defined	X:\jsc.svn\examples\javascript\android\com.abstractatech.adminshell\com.abstractatech.adminshell.clrlauncher\Program.cs	38	38	com.abstractatech.adminshell.clrlauncher
        //    //Error	3	Interop type 'Shell32.ShellClass' cannot be embedded. Use the applicable interface instead.	X:\jsc.svn\examples\javascript\android\com.abstractatech.adminshell\com.abstractatech.adminshell.clrlauncher\Program.cs	38	42	com.abstractatech.adminshell.clrlauncher


        //    // create the shell application object
        //    Shell shellApplication = new ShellClass();

        //    string path = Path.GetDirectoryName(filePath);
        //    string fileName = Path.GetFileName(filePath);

        //    Folder directory = shellApplication.NameSpace(path);
        //    FolderItem link = directory.ParseName(fileName);

        //    FolderItemVerbs verbs = link.Verbs();
        //    for (int i = 0; i < verbs.Count; i++)
        //    {
        //        FolderItemVerb verb = verbs.Item(i);
        //        string verbName = verb.Name.Replace(@"&", string.Empty).ToLower();

        //        if ((pin && verbName.Equals("pin to taskbar")) || (!pin && verbName.Equals("unpin from taskbar")))
        //        {

        //            verb.DoIt();
        //        }
        //    }

        //    shellApplication = null;
        //}
    }
}
