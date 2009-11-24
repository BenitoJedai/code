using ScriptCoreLib;

using ScriptCoreLib.PHP.Runtime;
using System;

namespace ScriptCoreLib.PHP.IO
{
	[Script, System.Obsolete]
	public class FileSystemInfo
	{
		public static string ScriptPath
		{
			get
			{
				return Native.API.dirname(Native.SuperGlobals.Server[Native.SuperGlobals.ServerVariables.SCRIPT_FILENAME]);
			}
		}

		public static void EnsureDirectory(string path)
		{

			if (!Native.API.is_dir(path))
			{
				Native.Log("creating path " + path);

				Native.API.mkdir(path);
			}
		}

		public static void WriteFile(string cfile, string data)
		{
			WriteFile(cfile, data, false);
		}

		public static void WriteFile(string cfile, string data, bool bAppend)
		{
			string m = bAppend ? "a" : "w";

			object f = Native.API.fopen(cfile, m);

			Native.API.fwrite(f, data);

			Native.API.fclose(f);
		}

		public static bool IsReadable(string src)
		{
			return Native.API.is_readable(src);
		}

		public static string GetExtension(string p)
		{
			return p.Substring(p.LastIndexOf(".") + 1);
		}

		public static bool Exists(string e)
		{
			return Native.API.is_readable(e);
		}

		public static string GetString(string p)
		{
			return Native.API.file_get_contents(p);
		}

		public static FileInfo[] GetFiles(string path)
		{
			string[] u = GetFilenames(path);

			List<FileInfo> x = new List<FileInfo>();

			foreach (string v in u)
			{
				FileInfo a = FileInfo.OfPath(path + "/" + v);

				if (a.IsFile)
					x.Add(a);
			}


			return x.ToArray();
		}

		public static DirectoryInfo[] GetDirectories(string path)
		{
			string[] u = GetFilenames(path);

			List<DirectoryInfo> x = new List<DirectoryInfo>();

			foreach (string v in u)
			{
				DirectoryInfo a = DirectoryInfo.OfPath(path + "/" + v);

				if (a.IsDirectory)
					x.Add(a);
			}


			return x.ToArray();
		}


		public static string[] GetFilenames(string dir)
        {
			throw new NotSupportedException("");

        }
	}
}
