using System.Threading;
using System;

using ScriptCoreLib;
using System.Text;
using System.IO;


namespace Base64Example
{
	[Script]
	public class Program
	{

		

		public static void TestBase64(params byte[] e)
		{
			var c = Convert.ToBase64String(e);

			Console.WriteLine(c);

			var a = Convert.FromBase64String(c);

			if (a.ByteArrayEquals(a))
				Console.WriteLine("ok");
			else
				Console.WriteLine("fail");
		}



		public static void Main(string[] args)
		{

			TestBase64(
				0, 1, 0xFE, 0xFF, 0x33, 45, 45, 45, 65
			);

			TestBase64(
				0, 1, 0xFE, 0xFF
			);

			TestBase64(
				0, 1, 0xFE
			);


			TestBase64(
				0, 1
			);


			TestBase64(
				0
			);

			TestBase64(
			
			);

			Console.ReadLine();
		}
	}
}
