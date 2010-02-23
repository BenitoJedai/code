using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.ComponentModel;

namespace jsc.meta.Library.VolumeFunctions
{
	public static class VolumeFunctionsProvider
	{
		// source: http://groups.google.com/group/microsoft.public.dotnet.languages.csharp/browse_thread/thread/2a0a07f79b970482/45171cb6c66a79cc?lnk=st&q=C%23+create+virtual+drive&rnum=1#45171cb6c66a79cc
		// http://msdn.microsoft.com/en-us/library/aa363904(VS.85).aspx

		[DllImport("kernel32.dll")]
		internal static extern bool DefineDosDevice(uint dwFlags, string lpDeviceName, string lpTargetPath);

		[DllImport("Kernel32.dll")]
		internal static extern uint QueryDosDevice(string lpDeviceName, StringBuilder lpTargetPath, uint ucchMax);

		internal const uint DDD_RAW_TARGET_PATH = 0x00000001;
		internal const uint DDD_REMOVE_DEFINITION = 0x00000002;
		internal const uint DDD_EXACT_MATCH_ON_REMOVE = 0x00000004;
		internal const uint DDD_NO_BROADCAST_SYSTEM = 0x00000008;
		const string MAPPED_FOLDER_INDICATOR = @"\??\";
		// --------------------------------------------------------------------------- ------------- 
		// Class Name:  VolumeFunctions 
		// Procedure Name: MapFolderToDrive 
		// Purpose:   Map the folder to a drive letter 
		// Parameters: 
		//  - driveLetter (string)  : Drive letter in the format "C:" without a back slash 
		//  - folderName (string)  : Folder to map without a back slash 
		// --------------------------------------------------------------------------- ------------- 
		internal static string MapFolderToDrive(string driveLetter, string folderName)
		{
			// Is this drive already mapped? If so, we don't remap it! 
			StringBuilder volumeMap = new StringBuilder(1024);
			QueryDosDevice(driveLetter, volumeMap, (uint)1024);
			if (volumeMap.ToString().StartsWith(MAPPED_FOLDER_INDICATOR) == true)
				return "Volume is already mapped - map not changed";
			// Map the folder to the drive 
			DefineDosDevice(0, driveLetter, folderName);
			// Display a status message to the user. 
			string statusMessage = new Win32Exception(Marshal.GetLastWin32Error()).ToString();
			return statusMessage.Substring(statusMessage.IndexOf(":") + 1);
		}
		// --------------------------------------------------------------------------- ------------- 
		// Class Name:  VolumeFunctions 
		// Procedure Name: UnmapFolderFromDrive 
		// Purpose:   Unmap a drive letter. We always unmp the drive, without checking the 
		//                  folder name. 
		// Parameters: 
		//  - driveLetter (string)  :   Drive letter to be released, the the format "C:" 
		//  - folderName (string)  :    Folder name that the drive is mapped to. 
		// --------------------------------------------------------------------------- ------------- 
		internal static string UnmapFolderFromDrive(string driveLetter, string folderName)
		{
			DefineDosDevice(DDD_REMOVE_DEFINITION, driveLetter, folderName);
			// Display the status of the "last" unmap we run. 
			string statusMessage = new Win32Exception(Marshal.GetLastWin32Error()).ToString();
			return statusMessage.Substring(statusMessage.IndexOf(":") + 1);
		}
		// --------------------------------------------------------------------------- ------------- 
		// Class Name:  VolumeFunctions 
		// Procedure Name: DriveIsMappedTo 
		// Purpose:   Returns the folder that a drive is mapped to. If not mapped, we return a blank. 
		// Parameters: 
		//  - driveLetter (string)  : Drive letter in the format "C:" 
		// --------------------------------------------------------------------------- ------------- 
		internal static string DriveIsMappedTo(string driveLetter)
		{
			StringBuilder volumeMap = new StringBuilder(512);
			string mappedVolumeName = "";
			// If it's not a mapped drive, just remove it from the list 
			uint mapped = QueryDosDevice(driveLetter, volumeMap, (uint)512);
			if (mapped != 0)
				if (volumeMap.ToString().StartsWith(MAPPED_FOLDER_INDICATOR) == true)
				{
					// It's a mapped drive, so return the mapped folder name 
					mappedVolumeName = volumeMap.ToString().Substring(4);
				}
			return mappedVolumeName;
		}
	}
}
