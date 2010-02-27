using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Win32.SafeHandles;
using System.Runtime.InteropServices;
using System.IO;


namespace System.IO
{
	// source: http://www.codeproject.com/KB/files/LongFileNames.aspx
    public static class Win32File
    {
        
        // Error
        const int ERROR_ALREADY_EXISTS = 183;
        // seek location
        const uint FILE_BEGIN = 0x0;
        const uint FILE_CURRENT = 0x1;
        const uint FILE_END = 0x2;


        // access
        const uint GENERIC_READ = 0x80000000;
        const uint GENERIC_WRITE = 0x40000000;
        const uint GENERIC_EXECUTE = 0x20000000;
        const uint GENERIC_ALL = 0x10000000;

        const uint FILE_APPEND_DATA = 0x00000004;

        // attribute
        const uint FILE_ATTRIBUTE_NORMAL = 0x80;

        // share
        const uint FILE_SHARE_DELETE = 0x00000004;
        const uint FILE_SHARE_READ = 0x00000001;
        const uint FILE_SHARE_WRITE = 0x00000002;

        //mode
        const uint CREATE_NEW = 1;
        const uint CREATE_ALWAYS = 2;
        const uint OPEN_EXISTING = 3;
        const uint OPEN_ALWAYS = 4;
        const uint TRUNCATE_EXISTING = 5;

		// http://www.pinvoke.net/default.aspx/kernel32.createdirectory
		[DllImport("kernel32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		static extern bool CreateDirectory(string lpPathName,
		   IntPtr lpSecurityAttributes);

		public static bool CreateDirectory(string lpPathName)
		{
			var filepath = lpPathName;

			filepath = filepath.Replace("/", "\\");
			if (filepath.StartsWith(@"\\"))
			{
				filepath = @"\\?\UNC\" + filepath.Substring(2, filepath.Length - 2);
			}
			else
				filepath = @"\\?\" + filepath;

			return CreateDirectory(filepath, IntPtr.Zero);
		}
		// http://msdn.microsoft.com/en-us/library/aa363858(VS.85).aspx
		// An application cannot create a directory by using CreateFile, therefore only the OPEN_EXISTING value 
		// is valid for dwCreationDisposition for this use case. To create a directory, the application must call 
		// CreateDirectory or CreateDirectoryEx.

        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        static extern SafeFileHandle CreateFileW(string lpFileName, uint dwDesiredAccess,
                                              uint dwShareMode, IntPtr lpSecurityAttributes, uint dwCreationDisposition,
                                              uint dwFlagsAndAttributes, IntPtr hTemplateFile);

        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        static extern uint SetFilePointer(SafeFileHandle hFile, long lDistanceToMove, IntPtr lpDistanceToMoveHigh, uint dwMoveMethod);

        // uint GetMode( FileMode mode )
        // Converts the filemode constant to win32 constant
        #region GetMode
        private static uint GetMode(FileMode mode)
        {
            uint umode = 0;
            switch (mode)
            {
                case FileMode.CreateNew:
                    umode = CREATE_NEW;
                    break;
                case FileMode.Create:
                    umode = CREATE_ALWAYS;
                    break;
                case FileMode.Append:
                    umode = OPEN_ALWAYS;
                    break;
                case FileMode.Open:
                    umode = OPEN_EXISTING;
                    break;
                case FileMode.OpenOrCreate:
                    umode = OPEN_ALWAYS;
                    break;
                case FileMode.Truncate:
                    umode = TRUNCATE_EXISTING;
                    break;
            }
            return umode;
        }
        #endregion


        // uint GetAccess(FileAccess access)
        // Converts the FileAccess constant to win32 constant
        #region GetAccess
        private static uint GetAccess(FileAccess access)
        {
            uint uaccess = 0;
            switch (access)
            {
                case FileAccess.Read:
                    uaccess = GENERIC_READ;
                    break;
                case FileAccess.ReadWrite:
                    uaccess = GENERIC_READ | GENERIC_WRITE;
                    break;
                case FileAccess.Write:
                    uaccess = GENERIC_WRITE;
                    break;
            }
            return uaccess;
        }
        #endregion

        // uint GetShare(FileShare share)
        // Converts the FileShare constant to win32 constant
        #region GetShare
        private static uint GetShare(FileShare share)
        {
            uint ushare = 0;
            switch (share)
            {
                case FileShare.Read:
                    ushare = FILE_SHARE_READ;
                    break;
                case FileShare.ReadWrite:
                    ushare = FILE_SHARE_READ | FILE_SHARE_WRITE;
                    break;
                case FileShare.Write:
                    ushare = FILE_SHARE_WRITE;
                    break;
                case FileShare.Delete:
                    ushare = FILE_SHARE_DELETE;
                    break;
                case FileShare.None:
                    ushare = 0;
                    break;

            }
            return ushare;
        }
        #endregion


        public static FileStream Open(string filepath, FileMode mode)
        {
            //opened in the specified mode and path, with read/write access and not shared
            FileStream fs = null;
            uint umode = GetMode(mode);
            uint uaccess = GENERIC_READ | GENERIC_WRITE;
            uint ushare = 0;    //not shared
            if (mode == FileMode.Append)
                uaccess = FILE_APPEND_DATA;
            // If file path is disk file path then prepend it with \\?\
            // if file path is UNC prepend it with \\?\UNC\ and remove \\ prefix in unc path.
			filepath = filepath.Replace("/", "\\");
            if (filepath.StartsWith(@"\\"))
            {
                filepath = @"\\?\UNC\" + filepath.Substring(2, filepath.Length - 2);
            }
            else
                filepath = @"\\?\" + filepath;
            SafeFileHandle sh = CreateFileW(filepath, uaccess, ushare, IntPtr.Zero, umode, FILE_ATTRIBUTE_NORMAL, IntPtr.Zero);
            int iError = Marshal.GetLastWin32Error();

			var AlreadyExistsError = ((mode == FileMode.Append || mode == FileMode.OpenOrCreate) && iError == ERROR_ALREADY_EXISTS);

            if ((iError > 0 && !AlreadyExistsError) || sh.IsInvalid)
            {
				throw new IOException("Error opening file Win32 Error:" + iError);
            }
            else
            {
                fs = new FileStream(sh, FileAccess.ReadWrite);
            }

            // if opened in append mode
            if (mode == FileMode.Append)
            {
                if (!sh.IsInvalid)
                {
                    SetFilePointer(sh, 0, IntPtr.Zero, FILE_END);
                }
            }

            return fs;
        }

        public static FileStream Open(string filepath, FileMode mode, FileAccess access)
        {
            //opened in the specified mode and access and not shared
            FileStream fs = null;
            uint umode = GetMode(mode);
            uint uaccess = GetAccess(access);
            uint ushare = 0;//not shared
            if (mode == FileMode.Append)
                uaccess = FILE_APPEND_DATA;
            // If file path is disk file path then prepend it with \\?\
            // if file path is UNC prepend it with \\?\UNC\ and remove \\ prefix in unc path.
			filepath = filepath.Replace("/", "\\");
            if (filepath.StartsWith(@"\\"))
            {
                filepath = @"\\?\UNC\" + filepath.Substring(2, filepath.Length - 2);
            }
            else
                filepath = @"\\?\" + filepath;
            SafeFileHandle sh = CreateFileW(filepath, uaccess, ushare, IntPtr.Zero, umode, FILE_ATTRIBUTE_NORMAL, IntPtr.Zero);
            int iError = Marshal.GetLastWin32Error();
			var AlreadyExistsError = ((mode == FileMode.Append || mode == FileMode.OpenOrCreate) && iError == ERROR_ALREADY_EXISTS);

			if ((iError > 0 && !AlreadyExistsError) || sh.IsInvalid)
			{
				throw new IOException("Error opening file Win32 Error:" + iError);
			}
            else
            {
                fs = new FileStream(sh, access);
            }
            // if opened in append mode
            if (mode == FileMode.Append)
            {
                if (!sh.IsInvalid)
                {
                    SetFilePointer(sh, 0, IntPtr.Zero, FILE_END);
                }
            }
            return fs;

        }

        public static FileStream Open(string filepath, FileMode mode, FileAccess access, FileShare share)
        {
            //opened in the specified mode , access and  share
            FileStream fs = null;
            uint umode = GetMode(mode);
            uint uaccess = GetAccess(access);
            uint ushare = GetShare(share);
            if (mode == FileMode.Append)
                uaccess = FILE_APPEND_DATA;
            // If file path is disk file path then prepend it with \\?\
            // if file path is UNC prepend it with \\?\UNC\ and remove \\ prefix in unc path.

			filepath = filepath.Replace("/", "\\");

            if (filepath.StartsWith(@"\\"))
            {
                filepath = @"\\?\UNC\" + filepath.Substring(2, filepath.Length - 2);
            }
            else
                filepath = @"\\?\" + filepath;
            SafeFileHandle sh = CreateFileW(filepath, uaccess, ushare, IntPtr.Zero, umode, FILE_ATTRIBUTE_NORMAL, IntPtr.Zero);
            int iError = Marshal.GetLastWin32Error();
			var AlreadyExistsError = ((mode == FileMode.Append || mode == FileMode.OpenOrCreate) && iError == ERROR_ALREADY_EXISTS);

			if ((iError > 0 && !AlreadyExistsError) || sh.IsInvalid)
			{
				throw new IOException("Error opening file Win32 Error:" + iError);
			}
            else
            {
                fs = new FileStream(sh, access);
            }
            // if opened in append mode
            if (mode == FileMode.Append)
            {
                if (!sh.IsInvalid)
                {
                    SetFilePointer(sh, 0, IntPtr.Zero, FILE_END);
                }
            }
            return fs;
        }

        public static FileStream OpenRead(string filepath)
        {

            // Open readonly file mode open(String, FileMode.Open, FileAccess.Read, FileShare.Read)
            return Open(filepath, FileMode.Open, FileAccess.Read, FileShare.Read);
        }

        public static FileStream OpenWrite(string filepath)
        {
            //open writable open(String, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None).
            return Open(filepath, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None);
        }


    }
}
