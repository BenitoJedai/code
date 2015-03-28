using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace ScriptCoreLib.Shared.BCLImplementation.System.IO
{
	// https://github.com/dotnet/coreclr/blob/master/src/mscorlib/src/System/IO/FileSystemInfo.cs

	[Script(Implements = typeof(global::System.IO.FileSystemInfo))]
	public abstract class __FileSystemInfo
	{
		// tested by ?
		public abstract void Delete();

		public abstract bool Exists { get; }

		public virtual string FullName
		{
			get
			{
				return "";
			}
		}

		public abstract string Name { get; }

		public override string ToString()
		{
			return this.FullName;
		}

		public DateTime LastWriteTime
		{
			get
			{
				// depends on the security check in get_LastWriteTimeUtc
				return LastWriteTimeUtc.ToLocalTime();
			}

			set
			{
				//LastWriteTimeUtc = value.ToUniversalTime();
			}
		}


		public virtual DateTime LastWriteTimeUtc
		{
			get
			{
				return default(DateTime);
			}

			set
			{
			}
		}

	}


}
