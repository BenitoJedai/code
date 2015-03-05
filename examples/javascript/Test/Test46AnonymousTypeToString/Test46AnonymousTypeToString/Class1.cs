using ScriptCoreLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

[assembly: Obfuscation(Feature = "script")]

namespace Test46AnonymousTypeToString
{

	public static class Class1
	{
		// broken for java?
		//new Object[1];
		//      new Object[1][0] = this._foo3_i__Field;
		//      return __String.Format(null, "{{ foo3 = {0} }}", new Object[1]);

		// works for js already?
		//var __0007 = new Array(1);
		//__0007[0] = a[0].foo3;
		//  return CAAABl764zm4JMHd2KvwWw(null, '{{ foo3 = {0} }}', __0007);


		// https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201503/20150305
		// https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201503/2010303
		static string foo() => new
		{

			//foo = 1,
			//foo2 = 2,

			foo3 = "3"
		}.ToString();

	}
	//  [mscorlib] System.String.Format(provider : IFormatProvider(Type) -> Object, format : string, args : params object[]) : String

	[Script(Implements = typeof(global::System.IFormatProvider), InternalConstructor = true)]
	internal class __IFormatProvider
	{
	}


	[Script(Implements = typeof(global::System.String), InternalConstructor = true)]
	internal class __String
	{
		public static string Format(IFormatProvider provider, string format, object[] args)
		{
			return "";
		}

		public static string Format(string format, params object[] b)
		{
			return "";
		}


		//		Implementation not found for type import :
		// type: System.String
		// method: System.String Format(System.IFormatProvider, System.String, System.Object[])
		// Did you forget to add the[Script] attribute?
		//Please double check the signature!

		// script: error JSC1000: Java : class import: no implementation for System.IFormatProvider at Test46AnonymousTypeToString.__String

	}
}
