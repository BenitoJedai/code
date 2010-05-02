using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.Archive.ZIP;

namespace ScriptCoreLib.Ultra.Studio
{
	public static class SolutionBuilderWithZIPFile
	{
		public static ZIPFile WriteToArchive(this SolutionBuilder that)
		{
			var zip = new ZIPFile();

			that.WriteTo(zip.Add);

			return zip;
		}
	}
}
