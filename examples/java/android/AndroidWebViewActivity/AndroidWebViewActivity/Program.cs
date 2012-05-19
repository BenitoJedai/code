using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using ScriptCoreLib.Shared.Avalon.Extensions;

namespace AndroidWebViewActivity
{
    class Program
    {
        [STAThread]
        public static void Main(string[] e)
        {
            #region how many devices?

            // StandardOut has not been redirected or the process hasn't started yet.
            // The Process object must have the UseShellExecute property set to false in order to redirect IO streams.

            var p = Process.Start(
                new ProcessStartInfo(@"C:\util\android-sdk-windows\platform-tools\adb.exe", "devices")
                {
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                }
            );

            p.Start();


            var x = p.StandardOutput.ReadToEnd().Split('\n').Where(k => k.Contains("\t")).ToArray();



            #endregion

            #region  atleast start emulator

            if (x.Length == 0)
            {
                // C:\Users\Arvo\.android\avd
                // http://heikobehrens.net/2011/03/15/android-skins/
                var devices =
                    from f in Directory.EnumerateDirectories(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "/.android/avd")
                    where f.EndsWith(".avd")
                    let name = Path.GetFileNameWithoutExtension(f)
                    select new { name, f };

                var emulator = Process.Start(
                    new ProcessStartInfo(
                        @"C:\util\android-sdk-windows\tools\emulator.exe",
                        "-avd " + devices.First().name
                    //+ " -no-boot-anim  -noskin -dpi-device 96 -scale auto"
                        + " -no-boot-anim -verbose"
                    //+ @"-skindir Y:\opensource\github\android-minimal-skins"
                        + @" -skin WVGA800-minimal"

                        + " -onion-alpha 100 -onion " + @"Y:\jsc.internal.svn\compiler\jsc.meta\jsc.meta\Documents\jsc.png".Replace("\\", "/")
                    )
                    {
                        UseShellExecute = false
                    }
                );



                Thread.Sleep(7000);
            }
            // WaitForInputIdle failed.  This could be because the process does not have a graphical interface.
            //emulator.WaitForInputIdle();

            #endregion
            
            MessageBox.Show("Should we reinstall application on a device?");


            Process.Start(
                 new ProcessStartInfo(
                    @"C:\util\android-sdk-windows\platform-tools\adb.exe",
                    @"install -r ""y:\jsc.svn\examples\java\android\AndroidWebViewActivity\AndroidWebViewActivity\staging\bin\AndroidWebViewActivity-debug.apk"""
                )
                {
                    UseShellExecute = false
                }
            );

            // reinstall

            MessageBox.Show("Stop virtual device?");

            //Debugger.Break();
        }
    }
}
