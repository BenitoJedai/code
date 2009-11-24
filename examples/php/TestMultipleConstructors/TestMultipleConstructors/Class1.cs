using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

[assembly: Obfuscation(Feature = "script")]

namespace TestMultipleConstructors
{
	public class MultipleConsturctorsBaseBase
	{

		public MultipleConsturctorsBaseBase()
		{

		}
	}

	public class MultipleConsturctorsBase : MultipleConsturctorsBaseBase
	{
		public string A;
		public int B;

		public MultipleConsturctorsBase(string A) :  base()
		{


			this.A = A;
		}

		public void SetValues(string A)
		{
			this.A = A;
		}

		public void SetValues(int B)
		{
			this.B = B;
		}

		public MultipleConsturctorsBase(MultipleConsturctorsBase x)
			: base()
		{
			A = x.A;
			B = x.B;
		}


		public MultipleConsturctorsBase(int B)
			: base()
		{


			this.B = B;
		}
	}

	public class MultipleConstructors : MultipleConsturctorsBase
	{
		int x = 1;
		int y = 1;
		public MultipleConstructors(string A, int Y)
			: base(A)
		{

			this.y = Y;





		}
	}

	public class MultipleConstructors2 : MultipleConsturctorsBase
	{
		int x = 1;
		int y = 1;
		public MultipleConstructors2(string A, int Y)
			: base(A)
		{

			this.y = Y;





		}

		public MultipleConstructors2(string A, int Y, object e)
			: base(A)
		{

			this.y = Y;

		}
	}

	class Consumer
	{
		public Consumer()
		{
			var A = new MultipleConstructors("", 0);
			var B = new MultipleConstructors2("", 0);
			var C = new MultipleConstructors2("", 0, this);
			var D = new MultipleConsturctorsBase(0);


		}
	}
}
