using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using ScriptCoreLib.PHP.Runtime;

namespace ScriptCoreLib.PHP.BCLImplementation.System.IO
{
	[Script(Implements = typeof(global::System.IO.Directory))]
	internal class __Directory
	{
		public static bool Exists(string path)
		{
			if (Native.API.is_dir(path))
				if (Native.API.is_readable(path))
					return true;

			return false;
		}

		public static DirectoryInfo CreateDirectory(string path)
		{
			//Console.WriteLine("CreateDirectory: " + path + "<br />");
			var c = new DirectoryInfo(path);
			c.Create();

			return c;
		}

		public static void Delete(string path)
		{
			Native.API.rmdir(path);
		}

		public static string[] GetDirectories(string path)
		{
			var list = new IArray();

			if (Native.API.is_readable(path))
			{
				object h = Native.API.opendir(path);
				object p = Native.API.readdir(h);

				while ((bool)p)
				{
					var ps = (string)p;

					if (ps != ".")
						if (ps != "..")
						{
							string npath = Path.Combine(path, ps);

							if (Native.API.is_dir(npath))
								if (Native.API.is_readable(npath))
								{
									list.Push(npath);
								}
						}

					p = Native.API.readdir(h);
				}

				Native.API.closedir(h);

			}

			return (string[])(object)list;
		}


		public static string[] GetFiles(string path)
		{
			var list = new IArray();

			if (Native.API.is_readable(path))
			{
				object h = Native.API.opendir(path);
				object p = Native.API.readdir(h);

				while ((bool)p)
				{
					var ps = (string)p;

					if (ps != ".")
						if (ps != "..")
						{
							string npath = Path.Combine(path, ps);

							if (Native.API.is_file(npath))
								if (Native.API.is_readable(npath))
								{
									list.Push(npath);
								}
						}

					p = Native.API.readdir(h);
				}

				Native.API.closedir(h);

			}

			return (string[])(object)list;
		}
	}
}
