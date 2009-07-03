using System.Threading;
using System;

using ScriptCoreLib;


namespace DelegateExample
{
	[Script]
	public delegate void StringAction(string e);

	[Script]
	public class Program
	{
		public static void Main(string[] args)
		{
			// Use Release Build to use jsc to generate java program
			// Use Debug Build to develop on .net

			// doubleclicking on the jar will not show the console


		}
	}
}
