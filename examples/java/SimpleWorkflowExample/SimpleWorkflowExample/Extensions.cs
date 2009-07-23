using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.IO;

namespace SimpleWorkflowExample
{
	[Script]
	public static class Extensions
	{
		public static string CombinePath(this string category, string target)
		{
			if (string.IsNullOrEmpty(category))
				return target;

			return Path.Combine(category, target);
		}

		public static byte[] ReadAllBytesOrEmpty(this FileInfo f)
		{
			if (f.Exists)
				return File.ReadAllBytes(f.FullName);

			return new byte[0];
		}
	}
}
