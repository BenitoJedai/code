using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.IO;

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
				return Path.GetFullPath(InternalPath);
			}
		}

		public override bool Exists
		{
			get { return __File.Exists(FullName); }
		}
	}
}
