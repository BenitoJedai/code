using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using OrcasSimpleJavaConsoleApplication.Library;
using System.Runtime.InteropServices;

namespace OrcasSimpleJavaConsoleApplication
{
	public partial class Program
	{


		public static void Main(string[] args)
		{





			// Notes:
			// 1. All referenced assemblies shall
			//    define [assembly:Obfuscation(feature = "script")]
			// 2. Turn off "optimize code" option in release build
			// 3. All used .net APIs must be defined by ScriptCoreLibJava
			// 4. Generics are not supported.
			// 5. Check post build event
			// 6. Build in release build configuration for java version

			Console.WriteLine("OrcasSimpleJavaConsoleApplication. Crosscompiled from C# to Java.");
			Console.WriteLine("---------------------------------");

			Console.WriteLine("This console application can run at .net and java virtual machine!");
			Console.WriteLine();

			Console.WriteLine("running at: " + Environment.CurrentDirectory);
			Console.WriteLine();

			var context = new object();

			// we should see work reordering t1 t3 t2 in context

			var t1 = StartWork(context, 500, "t1");
			var t2 = StartWork(context, 2000, "t2");
			var t3 = StartWork(context, 800, "t3");


			t1.Join();
			t2.Join();
			t3.Join();
			InvokeNativeMethods();

		}

		
	}
}
