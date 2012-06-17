using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Runtime.InteropServices;

namespace TestUnsignedByteConsole
{
	public partial class Program
	{
		public static byte A { get; set; }

		public static void Main(string[] args)
		{
			A = 0xFF;

			int X = A;

			Console.WriteLine("X: " + X);

		}
	}
}
