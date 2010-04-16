using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace ScriptCoreLib.Archive.ZIP
{
	public static class ZIPFileWithXLinq
	{
		public static void Add(this ZIPFile z, string FileName, XElement value)
		{
			z.Add(FileName, value.ToString());
		}
	}
}
