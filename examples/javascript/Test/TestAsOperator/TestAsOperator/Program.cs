using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

[assembly: System.Reflection.Obfuscation(Feature = "script")]

namespace TestAsOperator
{

	static class Program
	{
		static void Main(string[] args)
		{
			string data1 = "data1";
			object data3 = null;
			object data5 = new object();

			Yield(data1 as string);
			Yield(data3 as string);
			Yield(data5 as string);
		}

		static void Yield(string e)
		{

		}
	}
}
