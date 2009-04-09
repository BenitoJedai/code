using System.Threading;
using System;

using ScriptCoreLib;
using System.IO;
using System.Text;


namespace UnsignedByteSupport
{
	[Script]
	public static class Program
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

			var bytes = File.ReadAllBytes("binary.bin");

			foreach (var k in bytes)
			{
				Console.WriteLine("byte: " + k);
			}


			Console.WriteLine(bytes.ToHexString());
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
			// stloc
			// ldloc
			// box u8

			byte byte_MinValue = byte.MinValue;
			byte byte_MaxValue = byte.MaxValue;

			Console.WriteLine("byte_MinValue: " + byte_MinValue);
			Console.WriteLine("byte_MaxValue: " + byte_MaxValue);
			Console.WriteLine("(sbyte)byte_MaxValue: " + (sbyte)byte_MaxValue);

			// addition/subtraction 	
			byte_MaxValue--;
			Console.WriteLine("byte_MaxValue - 1: " + byte_MaxValue);

			byte_MinValue--;
			Console.WriteLine("byte_MinValue - 1: " + byte_MinValue);

			// multiplication/division
			byte x200 = 200;
			byte x201 = 201;
			byte x4 = 4;

			byte x50 = (byte)(x200 / x4);

			Console.WriteLine("200 / 4 = 50: " + x50);

			byte x800 = (byte)(x200 * x4);

			Console.WriteLine("200 * 4 = 800: " + x800);

			// remainder
			Console.WriteLine("201 % 4 = 1: " + ((byte)(x201 % x4)));

			// equality
			if (x200 == (x201 -1 ))
				Console.WriteLine("200 == 201 - 1");
			else
				Console.WriteLine("200 != 201 - 1");

			// comparison
			if (x200 < x201 )
				Console.WriteLine("200 < 201");
			else
				Console.WriteLine("200 >= 201");

			// shift
			byte x128 = 1 << 7;

			Console.WriteLine("128: " + x128);
			Console.WriteLine("128 - 1: " + (x128 - 1));

			// bitwise
			byte x128_x8 = (byte)(x128 | (1 << 3));
			Console.WriteLine("via box and ldloc: 128 | 8: " + x128_x8);
			Console.WriteLine("via box: 128 | 8: " + (byte)(x128 | (1 << 3)));

			UnsignedByteField = byte.MaxValue;
			UnsignedByteArray[0] = byte.MinValue;
			UnsignedByteArray[2] = 2;

			UnsignedByte(2);
		}

		public static byte UnsignedByteField;
		public static byte[] UnsignedByteArray = new byte[4];

		public static void UnsignedByte(byte value)
		{
			Console.WriteLine("255 + 2 = 1: " + (byte)(value + UnsignedByteField));
			Console.WriteLine("0 - 2 = 254: " + (byte)(UnsignedByteArray[0] - UnsignedByteArray[2]));

			SignedByte((sbyte)value);
			((sbyte)value).SignedByte();
		}

		public static void SignedByte(this sbyte e)
		{
			UnsignedByte2((byte)e);
		}

		public static void UnsignedByte2(this byte e)
		{

		}

		public static string ToHexString(this byte[] e)
		{
			var w = new StringBuilder();

			foreach (var v in e)
			{
				var x = v.ToHexString();
				w.Append(x);
			}

			return w.ToString();
		}

		public static string ToHexString(this byte e)
		{
			const string u = "0123456789abcdef";

			return u.Substring((e >> 4) & 0xF, 1) + u.Substring((e >> 0) & 0xF, 1);
		}
	}

	[Script]
	public static class Extensions2
	{
		public static string ToHexString(this sbyte[] e)
		{
			var w = new StringBuilder();

			foreach (var v in e)
			{
				var x = (byte)v;

				w.Append(x.ToHexString());
			}

			return w.ToString();
		}
	}
}
