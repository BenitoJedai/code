using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

[assembly: Obfuscation(Feature = "script")]

namespace Test46If
{
	public class Class1
	{
		static void bar() { }
		static void foo(object flag)
		{
			//      if ((flag!=null))

			// if (((flag > null)))
			if (flag != null)
				bar();
			else
				bar();


		}
	}
}
