using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace ScriptCoreLibJava.BCLImplementation.System.IO
{
	[Script(Implements = typeof(global::System.IO.FileInfo))]
	internal class __FileInfo : __FileSystemInfo
	{
		readonly string InternalPath;

		public __FileInfo(string path)
		{
			InternalPath = path;
		}
		public override bool Exists
		{
			get { return __File.Exists(InternalPath); }
		}

		public override string FullName
		{
			get
			{
				return InternalPath;
			}
		}
	}
}
