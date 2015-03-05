using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
[assembly: Obfuscation(Feature = "script")]
namespace Test46AnonymousToStringFake
{
	public class Class1<T>
	{
		public T _foo_i__Field;

		// https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201503/20150305
		// https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201501/20150104

		///* let 0007 = */new Array(2);
		//new Array(2)/* dup of 0007 */[0] = a[0]._foo_i__Field;
		//new Array(2)/* dup of 0007 */[1] = a[0]._bar_i__Field;
		//b = AgAABhiEhTqXKiz_aDbVYTA('{{ foo = {0}, bar = {1} }}', /* 0007 */new Array(2));

		public string ToString()
		{
			// {{ needs to go {
			// format is another lagnuage like idl.
			return __String_Format(
				null,
				"{{ foo = {0} }}",
				new object[] { _foo_i__Field }
			);
		}

		public static string __String_Format(IFormatProvider provider, string format, object[] args) => default(string);

	}
}
