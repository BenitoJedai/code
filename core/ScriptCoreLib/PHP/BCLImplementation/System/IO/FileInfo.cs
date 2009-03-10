using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ScriptCoreLib.PHP.BCLImplementation.System.IO
{
	[Script(Implements = typeof(global::System.IO.FileInfo))]
	internal class __FileInfo : __FileSystemInfo
	{
		public __FileInfo(string Path)
		{
			//Console.WriteLine("__DirectoryInfo: " + Path + "<br />");

			if (Path.Contains(":"))
				this.FullPath = Path;
			else if (Path.StartsWith("/"))
				this.FullPath = Path;
			else
				this.FullPath = __Path.Combine(Environment.CurrentDirectory, Path);

			//Console.WriteLine("__DirectoryInfo.FullPath: " + this.FullPath + "<br />");

			this.OriginalPath = Path;
		}


		public override bool Exists
		{
			get { return File.Exists(this.FullPath); }
		}

		public override void Delete()
		{
			File.Delete(this.FullPath);
		}

		public override string Name
		{
			get { return Native.API.basename(this.FullPath); }
		}
	}
}
