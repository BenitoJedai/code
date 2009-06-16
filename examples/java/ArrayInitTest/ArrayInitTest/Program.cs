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
	}
}
