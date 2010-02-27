using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace jsc.Library
{
	public static class Win32FileExtensions
	{
		public static void WriteToFile(this string e, string path)
		{
			using (var s = System.IO.Win32File.OpenWrite(path))
			{
				var bytes = Encoding.UTF8.GetBytes(e);
				s.Write(bytes, 0, bytes.Length);
			}
		}
	}
}
