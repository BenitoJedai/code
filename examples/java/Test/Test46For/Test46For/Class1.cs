using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
[assembly: Obfuscation(Feature = "script")]

namespace Test46For
{
	public class Class1
	{
		static void bar() { }
		static void foo(int length)
		{
			//  for (num0 = 0; (num0 < length); num0 = (num1 + 1))
			for (int i = 0; i < length; i++)
			{
				bar();
			}
		}
	}
}
