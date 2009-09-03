using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using OrcasSimpleJavaConsoleApplication.Library;
using System.Runtime.InteropServices;

namespace OrcasSimpleJavaConsoleApplication
{
	partial class Program
	{




		private static void InvokeNativeMethods()
		{
			// the VM's for some reason buffer our printf...
			printf("Width trick: %*d \n", 5, 10);
			printf("%s \n", "A string");

			printf("jni and java can be tricky!\n");

		}

		// http://publications.gbdirect.co.uk/c_book/chapter9/input_and_output.html
		// http://stackoverflow.com/questions/955962/how-to-buffer-stdout-in-memory-and-write-it-from-a-dedicated-thread

		// ! important
		// we can only use the cdecl calling convention (otherwise JRE will throw EXCEPTION_ACCESS_VIOLATION)
		// we can only use double, float, string, void*, int32 
		// we cannot use __varargs


		const string msvcrt = "msvcrt.dll";
		const string kernel32 = "kernel32.dll";

		[DllImport(msvcrt)]
		public static extern int printf(string e);


		[DllImport(msvcrt)]
		public static extern int printf(string e, string a);


		[DllImport(msvcrt)]
		public static extern int printf(string e, int a, int b);


	}
}
