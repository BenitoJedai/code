using System.Threading;
using System;

using ScriptCoreLib;


namespace ReflectionExample
{
	[Script]
	public class CoolClass1
	{
		public void Invoke(string e)
		{

		}
	}

	[Script]
	public class Program
	{
		public static void Main(string[] args)
		{
			var t = typeof(CoolClass1);

			Console.WriteLine("Name: " + t.Name);
			Console.WriteLine("Fullname: " + t.FullName);
		}
	}
}
