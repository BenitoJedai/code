using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ScriptCoreLib.PHP.BCLImplementation.System.IO
{
	[Script(Implements = typeof(global::System.IO.FileSystemInfo))]
	internal abstract class __FileSystemInfo
	{
		protected string FullPath;
		protected string OriginalPath;

		public abstract bool Exists { get; }

		public abstract void Delete();

		public virtual string FullName { get { return FullPath; } }

		public void Refresh()
		{
			Native.API.clearstatcache();
		}
	}
}
