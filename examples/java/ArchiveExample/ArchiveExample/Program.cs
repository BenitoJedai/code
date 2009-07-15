using System.Threading;
using System;

using ScriptCoreLib;
using System.Collections;


namespace ArchiveExample
{
	[Script]
	public class ZIPFileEntry
	{
		public string Text;
	}

	[Script]
	public class Program
	{
		public static void Main(string[] args)
		{
			var a = new ArrayList
			{
				new ZIPFileEntry { Text = "File1"},
				new ZIPFileEntry { Text = "File2"},
			};

			foreach (ZIPFileEntry item in a)
			{
				Console.WriteLine(item.Text);
			}
		}
	}
}
