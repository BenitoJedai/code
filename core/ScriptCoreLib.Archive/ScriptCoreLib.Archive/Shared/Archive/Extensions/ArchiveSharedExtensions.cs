using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ScriptCoreLib.Shared.Archive.Extensions
{
	[Script]
	public static class ArchiveSharedExtensions
	{
		public static ZIPFile ToZIPFile(this string e)
		{
			return new BinaryReader(e.ToMemoryStream());
		}
		public static short AssertEqualsTo(this short value, short e)
		{
			if (value != e)
				throw new Exception("expected " + e + " but found " + value);

			return value;
		}

		public static int AssertEqualsTo(this int value, int e)
		{
			if (value != e)
				throw new Exception("expected " + e + " but found " + value);

			return value;
		}
	}
}
