using System.Threading;
using System;

using ScriptCoreLib;


namespace DelegateExample
{
	[Script]
	public delegate void StringAction(string e);

	[Script]
	public delegate string StringFunc();

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

		public static void Do(Action h)
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

			var i = 0;
			StringFunc GetText = 
				delegate
				{
					return "Hello world (" + (i++) + ")"; 
				};


			StringAction h = Say;

			h(GetText());

			StringAction x = "hey!".Say;

			x(GetText());


			Do(() => Console.WriteLine(prefix + ":)"));

			WithClosure.Test();


			StringAction y = h;
			y += x;
			y("Broadcasting!!");

			y -= h;
			y("Broadcasting once?!!");

		}
	}

	[Script]
	public class WithClosure
	{
		public static void Invoke(Action e)
		{
			e();
		}

		public static void Test()
		{
			var x = "WithClosure";

			Invoke(
				delegate
				{
					Console.WriteLine(x);
				}
			);
		}

	}
}
