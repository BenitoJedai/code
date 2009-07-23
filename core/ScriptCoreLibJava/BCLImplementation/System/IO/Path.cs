using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace ScriptCoreLibJava.BCLImplementation.System.IO
{
	[Script(Implements = typeof(global::System.IO.Path))]
	internal static class __Path
	{
		public static string GetFullPath(string e)
		{
			// http://www.devx.com/tips/Tip/13804

			var f = new java.io.File(e);
			var c = default(string);

			try
			{
				c = f.getCanonicalPath();
			}
			catch
			{
				throw new csharp.RuntimeException();
			}

			return c;
		}

		public static string Combine(string path1, string path2)
		{
			return path1 + "/" + path2;
		}
	}
}
