using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

[assembly: Script, ScriptTypeFilter(ScriptType.Java)]

namespace TestAutoCast
{
	[Script]
	class Program
	{
		static void Main(string[] args)
		{
			var x = default(bool[][]);

			var i = 10;
			x = new bool[i][];

			// opcodes.ldc is not supported?
			x[0] = new bool[i];

			NewMethod(x);
			x[0][0] = false;

			X(new string(' ', 5));
		}

		private static void NewMethod(bool[][] x)
		{
			x[0][0] = true;
		}

		static void X(string e)
		{
		}
	}

	[Script(
	Implements = typeof(global::System.String),
		ImplementationType = typeof(global::java.lang.String),
	InternalConstructor = true

	)]
	internal class __String
	{

		public __String(char c, int count)
		{
		}

		public static string InternalConstructor(char c, int count)
		{
			return "";
		}
	}

}

namespace java.lang
{
	[Script(IsNative = true)]
	public class String
	{
	}
}