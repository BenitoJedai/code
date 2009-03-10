using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.PHP.BCLImplementation.System.IO
{
	[Script(Implements = typeof(global::System.IO.File))]
	internal class __File
	{
		public static void Delete(string path)
		{
			Native.API.unlink(path);
		}

		public static bool Exists(string path)
		{
			if (Native.API.is_file(path))
				if (Native.API.is_readable(path))
					return true;

			return false;
		}

		public static string ReadAllText(string path)
		{
			return Native.API.file_get_contents(path);
		}

		public static void WriteAllText(string path, string contents)
		{
			Native.API.file_put_contents(path, contents);
		}

	}
}
