using jsc.meta.Commands.Rewrite.RewriteToUltraApplication;
using SharedBrowserSessionExperiment.DataLayer.Data;
using System;
using System.Diagnostics;
using ScriptCoreLib.Extensions;

namespace SharedBrowserSessionExperiment
{
    /// <summary>
    /// You can debug your application by hitting F5.
    /// </summary>
    internal static class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
#if DEBUG
            //Additional information: ActiveX control '8856f961-340a-11d0-a96b-00c04fd705a2' cannot be instantiated because the current thread is not in a single-threaded apartment.
            //        _message	"External component has thrown an exception."	string
            //http://stackoverflow.com/questions/7901530/c-sharp-error-attempted-to-read-or-write-protected-memory-or-external-compone

            //at System.Windows.Forms.UnsafeNativeMethods.DispatchMessageA(MSG& msg)
            //   at System.Windows.Forms.Application.ComponentManager.System.Windows.Forms.UnsafeNativeMethods.IMsoComponentManager.FPushMessageLoop(IntPtr dwComponentID, Int32 reason, Int32 pvLoopData)
            //   at System.Windows.Forms.Application.ThreadContext.RunMessageLoopInner(Int32 reason, ApplicationContext context)
            //   at System.Windows.Forms.Application.ThreadContext.RunMessageLoop(Int32 reason, ApplicationContext context)
            //   at System.Windows.Forms.Application.RunDialog(Form form)
            //   at System.Windows.Forms.Form.ShowDialog(IWin32Window owner)
            //   at System.Windows.Forms.Form.ShowDialog()
            //   at SharedBrowserSessionExperiment.Program.Main(String[] args) in x:\jsc.svn\examples\javascript\p2p\SharedBrowserSessionExperiment\SharedBrowserSessionExperiment\Program.cs:line 23

            if (Debugger.IsAttached)
            {
                //new TheBrowserTab().ShowDialog();
                new PositionsWatchdog().ShowDialog();
                return;
            }
#endif

            RewriteToUltraApplication.AsProgram.Launch(typeof(Application));
        }

    }
}
