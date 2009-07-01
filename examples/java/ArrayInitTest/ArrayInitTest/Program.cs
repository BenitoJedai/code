using System.Threading;
using System;

using ScriptCoreLib;


namespace ArrayInitTest
{
	[Script]
	public class Program
	{
		static void Transmit(params byte[] e)
		{

		}

		public static void Main(string[] args)
		{
			Transmit(0x00, 0xa4, 0x04, 0x00, 0x08, 0xf0, 0x45, 0x73, 0x74, 0x45, 0x49, 0x44, 0x20, 0x76, 0x65, 0x72);
		}

		public static void CreateEmptyArray()
		{
			var a = new string[0xF];
		}

		public static void CreateEmptyArray2()
		{
			var a = new string[] { null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, };
		}

		public static void CreateEmptyArray3()
		{
			var a = new string[] { "x", "x", "x", "x", "x", "x", "x", "x", "x", "x", "x", "x", "x", "x", "x", };
		}
	}
}
