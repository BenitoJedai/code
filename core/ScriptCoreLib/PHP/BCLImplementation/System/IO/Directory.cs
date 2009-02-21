using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

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
			// http://www.webmasterworld.com/forum88/6665.htm
			// chmod 777

			Native.API.mkdir(path);

			return null;
		}

		public static void Delete(string path)
		{
			Native.API.rmdir(path);
		}
	}
}
