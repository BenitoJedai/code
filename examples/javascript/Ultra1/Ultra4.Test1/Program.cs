using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ultra1.Common;

namespace Ultra4.Test1
{
	class Program : FieldClass1
	{
		public int Field1;

		class Nested1 : Program
		{
			public Nested1()
			{
				Field1 = 1;
				FieldX = "";
			}
		}

		class Nested2 : Program
		{
			public Nested2()
			{
				Field1 = 2;
				FieldX = "";
			}
		}

		static void Main(string[] args)
		{
		}
	}
}
