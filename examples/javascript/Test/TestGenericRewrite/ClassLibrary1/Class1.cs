using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClassLibrary1
{
	public class Class1<TypeT>
	{
		public static void Method1<MethodT>(int a, TypeT t, string x, MethodT m)
		{

		}
	}

	public class Class2
	{
		public static void Method1<MethodT>(int a, string x, MethodT m)
		{

		}
	}

	public class Class3<TypeT>
	{
		public static void Method1(int a, TypeT t, string x)
		{

		}
	}
}
