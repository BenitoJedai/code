using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace jsc.meta.Library.VolumeFunctions
{
    public static class VolumeFunctionsExtensions
    {
        // http://forum.sysinternals.com/forum_posts.asp?TID=9063&amp;PN=1

        public class ToVirtualDriveToDirectory : IDisposable
        {
            public DirectoryInfo SourceDirectory;

            public DirectoryInfo VirtualDirectory;

            public string VirtualDrive;

            /// <summary>
            /// Current running process may want to register a cleanup to aviod unclaimed drivers on termination.
            /// </summary>
            static public event Action<ToVirtualDriveToDirectory> AtApplyVirtualDirectory;

            public ToVirtualDriveToDirectory ApplyVirtualDirectory()
            {
                var r = VolumeFunctions.VolumeFunctionsProvider.MapFolderToDrive(VirtualDrive, SourceDirectory.FullName);

                // error?

                this.VirtualDirectory = new DirectoryInfo(VirtualDrive);

                if (AtApplyVirtualDirectory != null)
                    AtApplyVirtualDirectory(this);

                return this;
            }

            public event Action Disposed;

            #region IDisposable Members

            public void Dispose()
            {
                if (string.IsNullOrEmpty(VirtualDrive))
                    return;

                VolumeFunctionsProvider.UnmapFolderFromDrive(VirtualDrive, SourceDirectory.FullName);

                if (Disposed != null)
                    Disposed();
            }

            #endregion

            /// <summary>
            /// When Virtual path goes out of scope you may need a real path instead.
            /// </summary>
            /// <param name="n"></param>
            /// <returns></returns>
            public FileInfo FromVirtual(FileInfo n)
            {
                // The specified path, file name, or both are too long. 
                // The fully qualified file name must be less than 260 characters, 
                // and the directory name must be less than 248 characters.

                if (n.FullName.StartsWith(this.SourceDirectory.FullName))
                    return n;

                return new FileInfo(
                    this.SourceDirectory.FullName + "/" + n.FullName.Substring(this.VirtualDirectory.FullName.Length)
                );

            }

            public DirectoryInfo FromVirtual(DirectoryInfo n)
            {
                // The specified path, file name, or both are too long. 
                // The fully qualified file name must be less than 260 characters, 
                // and the directory name must be less than 248 characters.

                return new DirectoryInfo(
                    this.SourceDirectory.FullName + "/" + n.FullName.Substring(this.VirtualDirectory.FullName.Length)
                );

            }
        }

        public class ToVirtualDriveToFile : ToVirtualDriveToDirectory
        {
            public FileInfo SourceFile;

            public FileInfo VirtualFile;

            public ToVirtualDriveToFile ApplyVirtualFile()
            {
                this.ApplyVirtualDirectory();

                // error?

                this.VirtualFile = new FileInfo(Path.Combine(VirtualDrive, SourceFile.Name));



                return this;
            }


        }

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        internal static extern int GetDriveType(string drive);

        public static ToVirtualDriveToDirectory ToVirtualDrive(this DirectoryInfo SourceDirectory)
        {
            // should we map short paths?
            // what drive should we use? first one free?

            // http://www.freevbcode.com/ShowCode.asp?ID=1062
            // http://msdn.microsoft.com/en-us/library/system.io.driveinfo_properties(VS.80).aspx
            // http://msdn.microsoft.com/en-us/library/system.io.driveinfo.name(VS.80).aspx

            //Debugger.Break();

            var VirtualDrive = GetnextAvailableDrive();

            return new ToVirtualDriveToDirectory { SourceDirectory = SourceDirectory, VirtualDrive = VirtualDrive }.ApplyVirtualDirectory();
        }

        public static ToVirtualDriveToFile ToVirtualDrive(this FileInfo SourceFile)
        {
            // should we map short paths?
            // what drive should we use? first one free?

            // http://www.freevbcode.com/ShowCode.asp?ID=1062
            // http://msdn.microsoft.com/en-us/library/system.io.driveinfo_properties(VS.80).aspx
            // http://msdn.microsoft.com/en-us/library/system.io.driveinfo.name(VS.80).aspx

            //Debugger.Break();

            var VirtualDrive = GetnextAvailableDrive();


            return new ToVirtualDriveToFile { SourceDirectory = SourceFile.Directory, SourceFile = SourceFile, VirtualDrive = VirtualDrive }.ApplyVirtualFile();
        }

        private static string GetnextAvailableDrive()
        {
            var AllDrives = Enumerable.Range((int)'A', (int)'Z' - (int)'A').Select(k => (char)k).ToArray();
            var UsedDrives = System.IO.DriveInfo.GetDrives().Select(k => k.Name[0]);
            var AvailableDrives = AllDrives.Except(UsedDrives);
            var NextDrive = AvailableDrives.Last() + ":";
            return NextDrive;
        }


    }
}
