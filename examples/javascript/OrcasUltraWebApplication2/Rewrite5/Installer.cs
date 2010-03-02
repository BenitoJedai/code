using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.Archive.ZIP;
using System.IO;

namespace Rewrite5
{
	public static class Installer
	{
		public static ZIPFile Archive
		{
			get
			{
				return null;
			}
		}

		public static void Main(string[] e)
		{
			var zip = new FileInfo(Path.ChangeExtension(new FileInfo(typeof(Installer).Assembly.Location).FullName, ".zip"));



			foreach (var template in Archive.Entries.Where(k => k.FileName.StartsWith("templates/")))
			{
			
			}

		
		}
	}
}
