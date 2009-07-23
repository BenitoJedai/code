using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace ScriptCoreLibJava.BCLImplementation.System.IO
{
	[Script(Implements = typeof(global::System.IO.DirectoryInfo))]
	internal class __DirectoryInfo : __FileSystemInfo
	{
		readonly string InternalPath;

		public __DirectoryInfo(string path)
		{
			this.InternalPath = path;
		}

		public override string FullName
		{
			get
			{
				return InternalPath;
			}
		}

		public override bool Exists
		{
			get { return __File.Exists(InternalPath); }
		}
	}
}
