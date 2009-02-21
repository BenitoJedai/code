using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ScriptCoreLib.PHP.BCLImplementation.System.IO
{
	[Script(Implements = typeof(global::System.IO.DirectoryInfo))]
	internal class __DirectoryInfo : __FileSystemInfo
	{
		
		public __DirectoryInfo(string Path)
		{
			this.OriginalPath = Path;
		}

		public override bool Exists
		{
			get
			{
				return Directory.Exists(this.OriginalPath);
			}
		}

		public DirectoryInfo CreateSubdirectory(string path)
		{
			return Directory.CreateDirectory(
				Path.Combine(this.OriginalPath, path)
			);
		}

		public void Create()
		{
			Directory.CreateDirectory(this.OriginalPath);
		}

		public override void Delete()
		{
			Directory.Delete(this.OriginalPath);
		}
	}
}
