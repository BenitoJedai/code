using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ScriptCoreLib.Archive.ZIP
{
	public static class ZIPFileFromFile
	{
        public static ZIPFile WriteToFile(this ZIPFile that, FileInfo f)
        {
            File.WriteAllBytes(f.FullName, that.ToBytes());
            return that;
        }

		public static ZIPFile ToZIPFile(this FileInfo f)
		{
			var bytes = File.ReadAllBytes(f.FullName);

			return new MemoryStream(bytes);
		}
	}
}
