using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace ScriptCoreLibJava.BCLImplementation.System.IO
{
	[Script(Implements = typeof(global::System.IO.FileSystemInfo))]
	internal abstract class __FileSystemInfo
	{
		public abstract bool Exists { get; }

		public virtual string FullName
		{
			get
			{
				return "";
			}
		}
	}
}
