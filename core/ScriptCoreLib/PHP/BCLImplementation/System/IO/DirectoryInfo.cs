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
			get
			{
				return Directory.Exists(this.FullPath);
			}
		}

		public DirectoryInfo CreateSubdirectory(string path)
		{
			var np = Path.Combine(this.FullPath, path);

			//Console.WriteLine("__DirectoryInfo.CreateSubdirectory: " + np + "<br />");

			return Directory.CreateDirectory(
				np
			);
		}

		public void Create()
		{
			// http://www.webmasterworld.com/forum88/6665.htm
			// chmod 777
			//Console.WriteLine("__DirectoryInfo.Create: " + this.FullPath + "<br />");
			if (Exists)
				return;

			Native.API.mkdir(this.FullPath);
		}

		public override void Delete()
		{
			Directory.Delete(this.FullPath);
		}

		public DirectoryInfo[] GetDirectories()
		{
			var x = Directory.GetDirectories(FullPath);
			var a = new DirectoryInfo[x.Length];

			for (int i = 0; i < x.Length; i++)
			{
				a[i] = new DirectoryInfo(x[i]);
			}

			return a;
		}

		public override string Name
		{
			get
			{
				return Native.API.basename(this.FullPath);
			}
		}

	}
}
