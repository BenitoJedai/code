using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.Archive.ZIP;
using System.IO;

namespace ScriptCoreLib.Ultra.Studio
{
	public static class SolutionBuilderWithFile
	{
		public static void WriteToArchiveFile(this SolutionBuilder that)
		{
			that.WriteToArchiveFile(new FileInfo(that.Name + ".zip"));
		}

		public static void WriteToArchiveFile(this SolutionBuilder that, FileInfo f)
		{
			var zip = that.WriteToArchive();

			File.WriteAllBytes(f.FullName, zip.ToBytes());
		}
	}
}
