using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

[assembly: Obfuscation(Feature = "script")]

namespace TestLongByteArray
{
	public class Class1
	{
		static void Invoke()
		{

			byte[] hashHdSHA1 = {
															0x30, 0x21, 0x30, 0x09,
															0x06, 0x05, 0x2B, 0x0E,
															0x03, 0x02, 0x1A, 0x05,
															0x00, 0x04, 0x14 };
		}

	}
}
