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
		private static uint[] Crc32Table = new uint[] { 
        0, 0x77073096, 0xee0e612c, 0x990951ba, 0x76dc419, 0x706af48f, 0xe963a535, 0x9e6495a3, 0xedb8832, 0x79dcb8a4, 0xe0d5e91e, 0x97d2d988, 0x9b64c2b, 0x7eb17cbd, 0xe7b82d07, 0x90bf1d91, 
        0x1db71064, 0x6ab020f2, 0xf3b97148, 0x84be41de, 0x1adad47d, 0x6ddde4eb, 0xf4d4b551, 0x83d385c7, 0x136c9856, 0x646ba8c0, 0xfd62f97a, 0x8a65c9ec, 0x14015c4f, 0x63066cd9, 0xfa0f3d63, 0x8d080df5, 
        0x3b6e20c8, 0x4c69105e, 0xd56041e4, 0xa2677172, 0x3c03e4d1, 0x4b04d447, 0xd20d85fd, 0xa50ab56b, 0x35b5a8fa, 0x42b2986c, 0xdbbbc9d6, 0xacbcf940, 0x32d86ce3, 0x45df5c75, 0xdcd60dcf, 0xabd13d59, 
        0x26d930ac, 0x51de003a, 0xc8d75180, 0xbfd06116, 0x21b4f4b5, 0x56b3c423, 0xcfba9599, 0xb8bda50f, 0x2802b89e, 0x5f058808, 0xc60cd9b2, 0xb10be924, 0x2f6f7c87, 0x58684c11, 0xc1611dab, 0xb6662d3d, 
        0x76dc4190, 0x1db7106, 0x98d220bc, 0xefd5102a, 0x71b18589, 0x6b6b51f, 0x9fbfe4a5, 0xe8b8d433, 0x7807c9a2, 0xf00f934, 0x9609a88e, 0xe10e9818, 0x7f6a0dbb, 0x86d3d2d, 0x91646c97, 0xe6635c01, 
        0x6b6b51f4, 0x1c6c6162, 0x856530d8, 0xf262004e, 0x6c0695ed, 0x1b01a57b, 0x8208f4c1, 0xf50fc457, 0x65b0d9c6, 0x12b7e950, 0x8bbeb8ea, 0xfcb9887c, 0x62dd1ddf, 0x15da2d49, 0x8cd37cf3, 0xfbd44c65, 
        0x4db26158, 0x3ab551ce, 0xa3bc0074, 0xd4bb30e2, 0x4adfa541, 0x3dd895d7, 0xa4d1c46d, 0xd3d6f4fb, 0x4369e96a, 0x346ed9fc, 0xad678846, 0xda60b8d0, 0x44042d73, 0x33031de5, 0xaa0a4c5f, 0xdd0d7cc9, 
        0x5005713c, 0x270241aa, 0xbe0b1010, 0xc90c2086, 0x5768b525, 0x206f85b3, 0xb966d409, 0xce61e49f, 0x5edef90e, 0x29d9c998, 0xb0d09822, 0xc7d7a8b4, 0x59b33d17, 0x2eb40d81, 0xb7bd5c3b, 0xc0ba6cad, 
        0xedb88320, 0x9abfb3b6, 0x3b6e20c, 0x74b1d29a, 0xead54739, 0x9dd277af, 0x4db2615, 0x73dc1683, 0xe3630b12, 0x94643b84, 0xd6d6a3e, 0x7a6a5aa8, 0xe40ecf0b, 0x9309ff9d, 0xa00ae27, 0x7d079eb1, 
        0xf00f9344, 0x8708a3d2, 0x1e01f268, 0x6906c2fe, 0xf762575d, 0x806567cb, 0x196c3671, 0x6e6b06e7, 0xfed41b76, 0x89d32be0, 0x10da7a5a, 0x67dd4acc, 0xf9b9df6f, 0x8ebeeff9, 0x17b7be43, 0x60b08ed5, 
        0xd6d6a3e8, 0xa1d1937e, 0x38d8c2c4, 0x4fdff252, 0xd1bb67f1, 0xa6bc5767, 0x3fb506dd, 0x48b2364b, 0xd80d2bda, 0xaf0a1b4c, 0x36034af6, 0x41047a60, 0xdf60efc3, 0xa867df55, 0x316e8eef, 0x4669be79, 
        0xcb61b38c, 0xbc66831a, 0x256fd2a0, 0x5268e236, 0xcc0c7795, 0xbb0b4703, 0x220216b9, 0x5505262f, 0xc5ba3bbe, 0xb2bd0b28, 0x2bb45a92, 0x5cb36a04, 0xc2d7ffa7, 0xb5d0cf31, 0x2cd99e8b, 0x5bdeae1d, 
        0x9b64c2b0, 0xec63f226, 0x756aa39c, 0x26d930a, 0x9c0906a9, 0xeb0e363f, 0x72076785, 0x5005713, 0x95bf4a82, 0xe2b87a14, 0x7bb12bae, 0xcb61b38, 0x92d28e9b, 0xe5d5be0d, 0x7cdcefb7, 0xbdbdf21, 
        0x86d3d2d4, 0xf1d4e242, 0x68ddb3f8, 0x1fda836e, 0x81be16cd, 0xf6b9265b, 0x6fb077e1, 0x18b74777, 0x88085ae6, 0xff0f6a70, 0x66063bca, 0x11010b5c, 0x8f659eff, 0xf862ae69, 0x616bffd3, 0x166ccf45, 
        0xa00ae278, 0xd70dd2ee, 0x4e048354, 0x3903b3c2, 0xa7672661, 0xd06016f7, 0x4969474d, 0x3e6e77db, 0xaed16a4a, 0xd9d65adc, 0x40df0b66, 0x37d83bf0, 0xa9bcae53, 0xdebb9ec5, 0x47b2cf7f, 0x30b5ffe9, 
        0xbdbdf21c, 0xcabac28a, 0x53b39330, 0x24b4a3a6, 0xbad03605, 0xcdd70693, 0x54de5729, 0x23d967bf, 0xb3667a2e, 0xc4614ab8, 0x5d681b02, 0x2a6f2b94, 0xb40bbe37, 0xc30c8ea1, 0x5a05df1b, 0x2d02ef8d
     };
		// http://www.jguru.com/faq/view.jsp?EID=13647
		// http://www.lykkenborg.no/java/2005/03/how-do-i-read-unsigned-byte-in-java.html
		// http://www.gencoreoperative.co.uk/java-unsigned-bytes.html
		// http://mindprod.com/jgloss/unsigned.html


		public enum MyEnum : byte
		{
			A, B, C
		}

		public static void UseEnum(MyEnum x)
		{
		}

		private static void UseEnumTest()
		{
			byte q = 1;

			UseEnum((MyEnum)q);
		}

		public static void IntMethod(int u)
		{

		}

		static uint UInt32Value;

		public static void Main(string[] args)
		{
			UInt32ToByte((uint)0xfffffffe);

			UInt32Value = 0xFFFFFFFFu;
			IntMethod((int)UInt32Value);

			CallUnsignedByte();
			CallUnsignedByte2();

			UseEnumTest();

			uint x = uint.MaxValue;


			Console.WriteLine("uint.MaxValue: " + x);
			Console.WriteLine();


			foreach (var xx in UnsignedUInt32Array)
			{
				Console.WriteLine("xx: " + xx.ToBytes().ToHexString());
			}


			var z = UnsignedUInt32Array;
			var zz = SignedInt32Array;
			for (int i = 0; i < z.Length; i++)
			{
				zz[i] = (int)z[i];
			}

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
			Console.WriteLine(Encoding.ASCII.GetString(bytes));
		}

		private static void UInt32ToByte(uint p)
		{
			Console.WriteLine("UInt32ToByte: p = " + p);
			Console.WriteLine("UInt32ToByte: (p & 0xff) = " + (p & 0xff));
			Console.WriteLine("UInt32ToByte: (byte)p = " + ((byte)p));
			// jsc has a bug with this kind of casting... to be fixed!
			// at this time you cannot cast from unsigned type to an unsigned type without local variable!
			Console.WriteLine("UInt32ToByte: (uint)(byte)p = " + ((uint)(byte)p));

			var u8 = (byte)p;

			Console.WriteLine("UInt32ToByte: u8 = " + u8);

			var u32 = (uint)u8;
			Console.WriteLine("UInt32ToByte: u32 = " + u32);

			uint n = (byte)p;

			Console.WriteLine("" + n);
		}

		private static void CallUnsignedByte2()
		{
			byte xxx = 0;

			Console.WriteLine("0x" + xxx.ToHexString());
		}

		private static void CallUnsignedByte()
		{
			byte xxx = 0;

			Console.WriteLine("0x" + xxx.ToString("x2"));
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
			if (x200 == (x201 - 1))
				Console.WriteLine("200 == 201 - 1");
			else
				Console.WriteLine("200 != 201 - 1");

			// comparison
			if (x200 < x201)
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
		public static uint[] UnsignedUInt32Array = new uint[] { 0xDEADBEEF };
		public static int[] SignedInt32Array = new int[] { 0x7EADBEEF };

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
		public static byte[] ToBytes(this uint e)
		{
			return new[]
			{
				(byte)((e >> (3 * 8)) & 0xff),
				(byte)((e >> (2 * 8)) & 0xff),
				(byte)((e >> (1 * 8)) & 0xff),
				(byte)((e >> (0 * 8)) & 0xff),
			};
		}

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
