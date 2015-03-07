using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
[assembly: Obfuscation(Feature = "script")]

namespace Test46ForEachArray
{
    public class Class1
    {
		static void bar() { }
		static void foo(object[] collection)
		{
			// for (d = 0; d < (~~c.length); d++)
			//  for (num1 = 0; num1; num1++)
			foreach (var item in collection)
			{
				bar();
			}
		}
	}
}
