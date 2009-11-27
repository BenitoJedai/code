using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
[assembly: Obfuscation(Feature = "script")]
namespace TestIsinst
{
	public class Class1
	{
	}

	class MyClass
	{
		void Test1(object e)
		{
			var y = e is Class1;
		}



		void Test2(object e)
		{
			var y = e as Class1;
		}
	}
}
