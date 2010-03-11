using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestTypeGenerics
{
	public class Class1<T>
	{
		public static T TField;

		public class Nested1<N>
		{
			public void Method<M>(T t, N n, M m)
			{

			}

			public void Method2(T t, N n, int m)
			{

			}
		}


	}

	class Test2X
	{
		static Class1<object>.Nested1<object> n;

		public void Test1()
		{
			var x = new Class2<object>(null);

		}

		public void Test2()
		{
			n.Method(null, null, 0);
			n.Method2(null, null, 0);

			Class1<int>.TField = 5;
		}
	}


	public class Class2<T>
	{
		public Class2(T t)
		{
			
		}
	}
}
