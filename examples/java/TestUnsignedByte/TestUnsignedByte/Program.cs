using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

[assembly: Obfuscation(Feature = "script")]

namespace TestUnsignedByte
{
	class Program
	{
		public static byte A { get; set; }

		static void Main(string[] args)
		{
			int X = A;
 
		}
	}
}
