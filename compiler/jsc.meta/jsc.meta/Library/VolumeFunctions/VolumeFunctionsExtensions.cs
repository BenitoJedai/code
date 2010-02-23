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

			public ToVirtualDriveToDirectory ApplyVirtualDirectory()
			{
				var r = VolumeFunctions.VolumeFunctionsProvider.MapFolderToDrive(VirtualDrive, SourceDirectory.FullName);

				// error?

				this.VirtualDirectory = new DirectoryInfo(VirtualDrive);



				return this;
			}

			#region IDisposable Members

			public void Dispose()
			{
				if (string.IsNullOrEmpty(VirtualDrive))
					return;

				VolumeFunctionsProvider.UnmapFolderFromDrive(VirtualDrive, SourceDirectory.FullName);
			}

			#endregion
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
