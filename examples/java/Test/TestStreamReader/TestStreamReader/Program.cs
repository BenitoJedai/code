using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Runtime.InteropServices;
using System.IO;

namespace TestStreamReader
{
	public partial class Program
	{


		public static void Main(string[] args)
		{
			{
				var m = new MemoryStream();

				m.WriteByte((byte)'A');
				m.WriteByte((byte)'\n');
				m.Position = 0;

				var x = new StreamReader(m);

				var z = x.ReadLine();

				Console.WriteLine(z);
			}

			{
				var m = new MemoryStream();

				m.WriteByte((byte)'\r');
				m.WriteByte((byte)'\n');
				m.Position = 0;

				var x = new StreamReader(m);

				var z = x.ReadLine();

				Console.WriteLine(z);
			}
		}


	}
}
