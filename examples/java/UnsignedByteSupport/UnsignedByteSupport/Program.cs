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

			sbyte sbyte_MinValue = sbyte.MinValue;
			sbyte sbyte_MaxValue = sbyte.MaxValue;

			Console.WriteLine("sbyte_MinValue: " + sbyte_MinValue);
			Console.WriteLine("sbyte_MaxValue: " + sbyte_MaxValue);

			sbyte_MinValue--;
			Console.WriteLine("sbyte_MinValue: " + sbyte_MinValue);

		}
	}
}
