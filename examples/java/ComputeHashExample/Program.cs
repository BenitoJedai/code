using System.Threading;
using System;

using ScriptCoreLib;
using System.Security.Cryptography;


namespace ComputeHashExample
{
	[Script]
	public class Program
	{
		public static string Text { get; set; }


		public static void Main(string[] args)
		{
			// Use Release Build to use jsc to generate java program
			// Use Debug Build to develop on .net

			// doubleclicking on the jar will not show the console

			{
				Console.WriteLine("SHA1:");
				var hash = new SHA1CryptoServiceProvider().ComputeHash(new byte[] { 0, 1, 2, 3 });

				foreach (var k in hash)
				{
					Console.Write(" " + k);
				}
				Console.WriteLine();
			}

			{
				Console.WriteLine("MD5:");
				var hash = new MD5CryptoServiceProvider().ComputeHash(new byte[] { 0, 1, 2, 3 });

				foreach (var k in hash)
				{
					Console.Write(" " + k);
				}
				Console.WriteLine();
			}

			Console.ReadLine();
		}
	}
}
