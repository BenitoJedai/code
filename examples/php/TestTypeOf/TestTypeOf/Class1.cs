using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.Reflection;

[assembly:
	Script(IsScriptLibrary = true, ScriptLibraries = new[] { typeof(ScriptCoreLib.Shared.IAssemblyReferenceToken) }),
	ScriptTypeFilter(ScriptType.PHP)

]

namespace TestTypeOf
{
	public class MyClass
	{
		public string BaseInstance1;
		public static string BaseStatic2;
	}

	public class U
	{
		public string F;
		public string X;
	}

	public class Class1 : MyClass
	{
		public string Instance1;
		public static string Static2;

		static Class1()
		{
			Console.WriteLine("Test1\n\n");
			{
				var t = typeof(Class1);

				foreach (var item in t.GetFields())
				{
					Console.WriteLine(item.Name);
				}

			}
			Console.WriteLine("Test2\n\n");
			{
				var t = typeof(MyClass);

				foreach (var item in t.GetFields())
				{
					Console.WriteLine(item.Name);
				}

			}
			Console.WriteLine("Test3\n\n");
			{
				var n = new U { F = "hello", X = "bye" };

				var t = n.GetType();

				foreach (var item in t.GetFields())
				{
					item.SetValue(n, ((string)item.GetValue(n)) + " world");
				}

				Console.WriteLine(n.F);
				Console.WriteLine(n.X);
			}
		}
	}
}
