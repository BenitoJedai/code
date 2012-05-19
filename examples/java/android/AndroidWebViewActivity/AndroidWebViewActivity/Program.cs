using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Media;
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

            var Devices = GetDevices();



            #endregion

            #region  atleast start emulator

            if (Devices.Length == 0)
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
                        @"C:\util\android-sdk-windows\tools\emulator-arm.exe",
                        "-avd " + devices.First().name
                    //+ " -no-boot-anim  -noskin -dpi-device 96 -scale auto"
                        + " -no-boot-anim -verbose"
                    //+ @"-skindir Y:\opensource\github\android-minimal-skins"
                        + @" -skin WVGA800-minimal"

                        + " -onion-alpha 70 -onion " + @"Y:\jsc.internal.svn\compiler\jsc.meta\jsc.meta\Documents\white-jsc.png".Replace("\\", "/")
                    )
                    {
                        UseShellExecute = false
                    }
                );

                //#region http://stackoverflow.com/questions/2510593/how-can-i-set-processor-affinity-in-net
                //long AffinityMask = (long)emulator.ProcessorAffinity;
                //AffinityMask &= 0x000F; // use only any of the first 4 available processors
                //emulator.ProcessorAffinity = (IntPtr)AffinityMask;
                //#endregion

                while (GetDevices().Length != 1)
                {
                    Thread.Sleep(1000);
                }

            }
            // WaitForInputIdle failed.  This could be because the process does not have a graphical interface.
            //emulator.WaitForInputIdle();

            #endregion


            while (GetDevices().Length != 1)
            {
                MessageBox.Show(
                    @"One and only device needs to be running!

- You could close already running emulators
- You could disconnect physical devices
",
                    "Android", 
                    MessageBoxButton.OK, 
                    MessageBoxImage.Information
                );
            }

            // error: device offline

            var install = Install();

            while (!install.Contains("Success"))
            {
                //Console.WriteLine(install);
                Thread.Sleep(2000);
                install = Install();
            }


            // reinstall

            //MessageBox.Show("Stop virtual device?");

        }

        private static string Install()
        {
            var install = Process.Start(
                 new ProcessStartInfo(
                    @"C:\util\android-sdk-windows\platform-tools\adb.exe",
                    @"install -r ""y:\jsc.svn\examples\java\android\AndroidWebViewActivity\AndroidWebViewActivity\staging\bin\AndroidWebViewActivity-debug.apk"""
                )
                 {
                     UseShellExecute = false,
                     RedirectStandardError = true,
                     RedirectStandardOutput = true,
                 }
            );

            return 
            install.StandardOutput.ReadToEnd() +
            install.StandardError.ReadToEnd();

        }

        private static string[] GetDevices()
        {
            var p = Process.Start(
                new ProcessStartInfo(@"C:\util\android-sdk-windows\platform-tools\adb.exe", "devices")
                {
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                }
            );

            p.Start();


            var Devices = p.StandardOutput.ReadToEnd().Split('\n').Where(k => k.Contains("\t")).ToArray();
            return Devices;
        }
    }
}
