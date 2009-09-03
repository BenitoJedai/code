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
		// http://publications.gbdirect.co.uk/c_book/chapter9/input_and_output.html
		// http://stackoverflow.com/questions/955962/how-to-buffer-stdout-in-memory-and-write-it-from-a-dedicated-thread

		[DllImport("msvcrt.dll")]
		public static extern int printf(string e);
		

		public static void Main(string[] args)
		{



			

			// Notes:
			// 1. All referenced assemblies shall
			//    define [assembly:Obfuscation(feature = "script")]
			// 2. Turn off "optimize code" option in release build
			// 3. All used .net APIs must be defined by ScriptCoreLibJava
			// 4. Generics are not supported.
			// 5. Check post build event
			// 6. Build in releas build configuration for java version

			Console.WriteLine("This console application can run at .net and java virtual machine!");

			Console.WriteLine("running at: " + Environment.CurrentDirectory);

			var context = new object();

			// we should see work reordering t1 t3 t2 in context

			var t1 = StartWork(context, 500, "t1");
			var t2 = StartWork(context, 2000, "t2");
			var t3 = StartWork(context, 800, "t3");


			t1.Join();
			t2.Join();
			t3.Join();

			// the VM's for some reason buffer our printf...
			printf("hello world!\n");
			printf("howdy!\n");
		}
	}
}
