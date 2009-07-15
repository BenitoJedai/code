using System.Threading;
using System;

using ScriptCoreLib;
using System.Collections;
using System.IO;
using System.Text;


namespace ArchiveExample
{


	[Script]
	public class Program
	{
		public static void Main(string[] args)
		{
			Console.WriteLine("ScriptCoreLib.Archive.Zip");


			var n = DateTime.Now;

			foreach (var x in n.GetType().GetPropertyGetMethodsForType(typeof(int)))
			{
				Console.WriteLine(x.Name + " " + (int)x.Invoke(n, new object[0]));
			}


			Console.WriteLine("" + ZIPFile.ToMsDosDateTime(DateTime.Now));

			var m = new MemoryStream(File.ReadAllBytes("archive.zip"));
			var r = new BinaryReader(m);


			ZIPFile z = r;

			foreach (var f in z.Entries)
			{
				Console.WriteLine(f.FileName + " " + f.Data.Length + " bytes");
				Console.WriteLine(Encoding.ASCII.GetString(f.Data.ToArray()));
				Console.WriteLine();
			}

		}

	}
}
