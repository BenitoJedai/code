using jsc.meta.Commands.Rewrite.RewriteToUltraApplication;
using System;
using System.Diagnostics;
using ScriptCoreLib.Desktop;

namespace TestTaskbarStates
{
    /// <summary>
    /// You can debug your application by hitting F5.
    /// </summary>
    internal static class Program
    {
        public static void Main(string[] args)
        {
            // http://stackoverflow.com/questions/1295890/windows-7-progress-bar-in-taskbar-in-c

            //TaskbarProgress.SetState(Process.GetCurrentProcess().MainWindowHandle 
            //    , TaskbarProgress.TaskbarStates.Indeterminate);

            //            or

            TaskbarProgress.SetValue(Process.GetCurrentProcess().MainWindowHandle, 50, 100);
            TaskbarProgress.SetState(Process.GetCurrentProcess().MainWindowHandle, TaskbarProgress.TaskbarStates.Paused);
            TaskbarProgress.SetState(Process.GetCurrentProcess().MainWindowHandle, TaskbarProgress.TaskbarStates.Error);

            RewriteToUltraApplication.AsProgram.Launch(typeof(Application));
        }

    }
}
