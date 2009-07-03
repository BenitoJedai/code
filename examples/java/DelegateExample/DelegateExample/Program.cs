using System.Threading;
using System;

using ScriptCoreLib;


namespace DelegateExample
{
	[Script]
	public delegate void StringAction(string e);

	[Script]
	public delegate void VoidAction();


	[Script]
	public static class Program
	{
		public static void Say(this string e)
		{
			Console.WriteLine("Say: " + e);
		}

		public static void Say(this string e, string u)
		{
			Console.WriteLine("Say: " + e + "; " + u);
		}

		public static void Do(VoidAction h)
		{
			Console.WriteLine("before");

			h();

			Console.WriteLine("after");

		}

		static  string prefix = "Me: ";

		public static void Main(string[] args)
		{
			// Use Release Build to use jsc to generate java program
			// Use Debug Build to develop on .net

			// doubleclicking on the jar will not show the console

			StringAction h = Say;

			h("hello world2");

			StringAction x = "hey".Say;

			x("hello world7x xxx  1");

			

			Do(
				delegate
				{
					Console.WriteLine(prefix + ":)");
				}
			);
		}
	}
}
