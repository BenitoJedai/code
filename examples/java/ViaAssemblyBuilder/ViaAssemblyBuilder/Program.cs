using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViaAssemblyBuilder
{
	public class Program
	{
		public static Action ExtensionPoint;

		public static void Main(string[] args)
		{
			Console.WriteLine("This console application can run at .net and java virtual machine!");
			Console.WriteLine("We are also introducing extension points!");

			if (ExtensionPoint != null)
				ExtensionPoint();
		}
	}
}
