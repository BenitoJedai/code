using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace OrcasNativeApplication
{
	[Script]
	public unsafe class NativeClass1
	{
		[Script(NoDecoration = true)]
		public static int main()
		{
			Console.WriteLine("hello world");

			for (int i = 100; i < 2200; i += 100)
			{
				Console.Beep(i, 50);
				
			}


			Console.Write('j');
			Console.Write('s');
			Console.Write('c');

			Console.Beep(1200, 400);

			return 0;
		}

		public unsafe static void DoWork()
		{
			var memory = new int[2000];


			// http://mochiads.com/zeitgeist#flash_version
			// http://www.cplusplus.com/reference/clibrary/cstdlib/calloc.html

			// http://msdn.microsoft.com/en-us/library/system.reflection.emit.opcodes.localloc.aspx

			//var xx =  stackalloc int[2000];


			
			// ok, now what?
			var x = 6;
			var y = 2;

			for (int i = 0; i < 800; i++)
			{
				x += y;	
			}

			y *= 5;
		}
	}

	class Program
	{
		static void Main(string[] args)
		{
			NativeClass1.main();
		}
	}
}
