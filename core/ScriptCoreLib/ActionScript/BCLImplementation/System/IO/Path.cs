using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript.BCLImplementation.System.IO
{
	[Script(Implements = typeof(global::System.IO.Path))]
	internal static class __Path
	{
		public static bool HasExtension(string path)
		{
			var a = path.LastIndexOf("/");
			var b = path.LastIndexOf(@"\");
			var c = path.LastIndexOf(".");

			if (c < 0)
				return false;

			if (a > c)
				return false;

			if (b > c)
				return false;

			return true;
		}

		public static string Combine(string path1, string path2)
		{
			return path1 + "/" + path2;
		}
	}
}
