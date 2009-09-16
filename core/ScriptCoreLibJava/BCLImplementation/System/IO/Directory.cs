using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.IO;

namespace ScriptCoreLibJava.BCLImplementation.System.IO
{
	[Script(Implements = typeof(global::System.IO.Directory))]
	internal static class __Directory 
	{
		public static DirectoryInfo CreateDirectory(string e)
		{
			var f = Path.GetFullPath(e);

			if (!new java.io.File(e).mkdir())
				f = null;

			if (f == null)
				return null;

			return new DirectoryInfo(f);
		}
	}
}
