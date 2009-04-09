using System.Threading;
using System;

using ScriptCoreLib;


namespace UnsignedByteSupport
{
	[Script]
	public class Program
	{
		// http://www.jguru.com/faq/view.jsp?EID=13647
		// http://www.lykkenborg.no/java/2005/03/how-do-i-read-unsigned-byte-in-java.html
		// http://www.gencoreoperative.co.uk/java-unsigned-bytes.html
		// http://mindprod.com/jgloss/unsigned.html

		public static void Main(string[] args)
		{
			// VM only manages 32 bit numbers
			// or 16bit if you are a javascard2 vm

			SignedByte();

			UnsignedByte();
		}

		private static void SignedByte()
		{
			sbyte sbyte_MinValue = sbyte.MinValue;
			sbyte sbyte_MaxValue = sbyte.MaxValue;

			Console.WriteLine("sbyte_MinValue: " + sbyte_MinValue);
			Console.WriteLine("sbyte_MaxValue: " + sbyte_MaxValue);

			Console.WriteLine("sbyte_MinValue & 0xff: " + (sbyte_MinValue & 0xff));
			Console.WriteLine("sbyte_MinValue & 0xff - 1: " + ((sbyte_MinValue & 0xff) - 1));
			Console.WriteLine("(sbyte)(sbyte_MinValue & 0xff - 1): " + (sbyte)((sbyte_MinValue & 0xff) - 1));

			sbyte_MinValue--;
			Console.WriteLine("sbyte_MinValue - 1: " + sbyte_MinValue);
		}

		private static void UnsignedByte()
		{
			// 1. stloc
			// 1. box u8

			byte byte_MinValue = byte.MinValue;
			byte byte_MaxValue = byte.MaxValue;

			Console.WriteLine("byte_MinValue: " + byte_MinValue);
			Console.WriteLine("byte_MaxValue: " + byte_MaxValue);

			byte_MaxValue--;
			Console.WriteLine("byte_MaxValue - 1: " + byte_MaxValue);

		}
	}
}
