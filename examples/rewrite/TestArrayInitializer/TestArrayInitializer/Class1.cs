using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace TestArrayInitializer
{
	//[CompilerGenerated]
	sealed class __c__DisplayClass4<T>
	{
		// Fields
		public Func<T, T, T> f;


		public int i;
		public T x;

		// Methods
		public T[] _SelectWithSeparator_b__3(T c)
		{
			T y = this.x;
			this.x = c;
			this.i++;
			if (this.i > 0)
			{
				return new T[] { this.f(y, c), c };
			}
			return new T[] { c };
		}
	}


    class Foo
    {
        public static void Main(string[] e)
        {
        }

        public Foo()
        {
            var i = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        }
    }
}
