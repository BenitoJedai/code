using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// ScriptCoreLib.Windows ?
namespace ScriptCoreLib.Desktop
//namespace TestTaskbarStates
{
    using System;
    using System.Runtime.InteropServices;

    // http://stackoverflow.com/questions/1295890/windows-7-progress-bar-in-taskbar-in-c

    //TaskbarProgress.SetState(Process.GetCurrentProcess().MainWindowHandle 
    //    , TaskbarProgress.TaskbarStates.Indeterminate);

    //            or

    //TaskbarProgress.SetValue(Process.GetCurrentProcess().MainWindowHandle, 50, 100);
    //            TaskbarProgress.SetState(this.Handle, TaskbarProgress.TaskbarStates.Error);


    public static class TaskbarProgress
    {
        // X:\jsc.svn\examples\javascript\Test\TestTaskbarStates\TestTaskbarStates\Program.cs

        public enum TaskbarStates
        {
            NoProgress = 0,
            Indeterminate = 0x1,
            Normal = 0x2,
            Error = 0x4,
            Paused = 0x8
        }

        [ComImportAttribute()]
        [GuidAttribute("ea1afb91-9e28-4b86-90e9-9e9f8a5eefaf")]
        [InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
        private interface ITaskbarList3
        {
            // ITaskbarList
            [PreserveSig]
            void HrInit();
            [PreserveSig]
            void AddTab(IntPtr hwnd);
            [PreserveSig]
            void DeleteTab(IntPtr hwnd);
            [PreserveSig]
            void ActivateTab(IntPtr hwnd);
            [PreserveSig]
            void SetActiveAlt(IntPtr hwnd);

            // ITaskbarList2
            [PreserveSig]
            void MarkFullscreenWindow(IntPtr hwnd, [MarshalAs(UnmanagedType.Bool)] bool fFullscreen);

            // ITaskbarList3
            [PreserveSig]
            void SetProgressValue(IntPtr hwnd, UInt64 ullCompleted, UInt64 ullTotal);
            [PreserveSig]
            void SetProgressState(IntPtr hwnd, TaskbarStates state);
        }

        [GuidAttribute("56FDF344-FD6D-11d0-958A-006097C9A090")]
        [ClassInterfaceAttribute(ClassInterfaceType.None)]
        [ComImportAttribute()]

        private class TaskbarInstance
        {
        }

        private static ITaskbarList3 taskbarInstance = (ITaskbarList3)new TaskbarInstance();
        private static bool taskbarSupported = Environment.OSVersion.Version >= new Version(6, 1);

        public static void SetState(IntPtr windowHandle, TaskbarStates taskbarState)
        {
            if (taskbarSupported) taskbarInstance.SetProgressState(windowHandle, taskbarState);
        }

        public static void SetValue(IntPtr windowHandle, double progressValue, double progressMax)
        {
            if (taskbarSupported) taskbarInstance.SetProgressValue(windowHandle, (ulong)progressValue, (ulong)progressMax);
        }


        // called by
        // X:\jsc.internal.git\compiler\jsc.meta\jsc.meta\Commands\Rewrite\RewriteToJavaScriptDocument.WebService.cs
        public static void SetMainWindowIndeterminate()
        {
            ScriptCoreLib.Desktop.TaskbarProgress.SetState(
                 System.Diagnostics.Process.GetCurrentProcess().MainWindowHandle,
                 ScriptCoreLib.Desktop.TaskbarProgress.TaskbarStates.Indeterminate
             );
        }

        public static void SetMainWindowNoProgress()
        {
            ScriptCoreLib.Desktop.TaskbarProgress.SetState(
                 System.Diagnostics.Process.GetCurrentProcess().MainWindowHandle,
                 ScriptCoreLib.Desktop.TaskbarProgress.TaskbarStates.NoProgress
             );
        }

        public static void SetMainWindowError()
        {
            ScriptCoreLib.Desktop.TaskbarProgress.SetState(
                 System.Diagnostics.Process.GetCurrentProcess().MainWindowHandle,
                 ScriptCoreLib.Desktop.TaskbarProgress.TaskbarStates.Error
             );
        }

        public static void SetMainWindowProgress(double progress)
        {
            // X:\jsc.svn\examples\merge\Test\TestYouTubeExtractor\TestYouTubeExtractor\Program.cs

            ScriptCoreLib.Desktop.TaskbarProgress.SetState(
                 System.Diagnostics.Process.GetCurrentProcess().MainWindowHandle,
                 ScriptCoreLib.Desktop.TaskbarProgress.TaskbarStates.Normal
             );

            ScriptCoreLib.Desktop.TaskbarProgress.SetValue(
                 System.Diagnostics.Process.GetCurrentProcess().MainWindowHandle,
                 progress, 1.0
             );


        }
    }
}
