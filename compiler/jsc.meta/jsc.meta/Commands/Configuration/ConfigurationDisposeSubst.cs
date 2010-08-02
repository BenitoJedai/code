using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using jsc.meta.Library.VolumeFunctions;
using System.IO;

namespace jsc.meta.Commands.Configuration
{
    public class ConfigurationDisposeSubst : CommandBase
    {
        public int PID;

        public string VirtualDrive;

        public override void Invoke()
        {
            var FolderName = VolumeFunctionsProvider.DriveIsMappedTo(VirtualDrive);

            if (string.IsNullOrEmpty(FolderName))
                return;


            Console.WriteLine(new { PID, VirtualDrive, FolderName }.ToString());

            var p = Process.GetProcessById(this.PID);

            p.WaitForExit();

            FolderName = VolumeFunctionsProvider.DriveIsMappedTo(VirtualDrive);

            if (string.IsNullOrEmpty(FolderName))
                return;


            VolumeFunctionsProvider.UnmapFolderFromDrive(VirtualDrive, FolderName);
        }

        internal static void Monitor(VolumeFunctionsExtensions.ToVirtualDriveToDirectory x)
        {
            var PID = Process.GetCurrentProcess().Id;
            var VirtualDrive = x.VirtualDrive;

            var p = Process.Start(
                new ProcessStartInfo(
                    new FileInfo(typeof(ConfigurationDisposeSubst).Assembly.Location).FullName,
                    "ConfigurationDisposeSubst /PID:" + PID + " /VirtualDrive:\"" + VirtualDrive + "\""
                    )
                {
                    CreateNoWindow = true,
                    UseShellExecute = false
                }
            );

            x.Disposed +=
                delegate
                {
                    p.Kill();
                };
        }

        static bool InternalRegisterOnce;
        internal static void RegisterOnce()
        {
            if (InternalRegisterOnce)
                return;

            InternalRegisterOnce = true;

            VolumeFunctionsExtensions.ToVirtualDriveToDirectory.AtApplyVirtualDirectory += ConfigurationDisposeSubst.Monitor;
        }
    }
}
