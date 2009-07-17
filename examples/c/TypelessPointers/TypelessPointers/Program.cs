using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.Threading;
using System.IO;

namespace TypelessPointers
{
	

	[Script]
	public unsafe class NativeClass1
	{
		[Script]
		public class DynamicA
		{
			// does alchemy support structs?
			public DynamicA ANextA;
			public DynamicB ANextB;

			public string Text;
		}

		[Script]
		public class DynamicB
		{
			// does alchemy support structs?
			public DynamicA BNextA;
			public DynamicB BNextB;

			public string Text;
		}

		[Script(NoDecoration = true)]
		public static int main()
		{
			var a = new DynamicA
			{
				Text = "Hello world",

				ANextB = new DynamicB
				{
					Text = "So?",

					BNextA = new DynamicA
					{
						Text = @"

Can we have circular references without multiple typedef's?
'''
http://stackoverflow.com/questions/888386/resolve-circular-typedef-dependency",
					}
				}
			};

			Console.WriteLine(a.Text);
			Console.WriteLine(a.ANextB.Text);
			Console.WriteLine(a.ANextB.BNextA.Text);

			return 0;
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
