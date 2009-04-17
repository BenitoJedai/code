using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace ScriptCoreLibJavaCard.javacard.framework
{
	[Script(IsNative = true)]
	public class Util
	{
		/// <summary>
		/// Copies an array from the specified source array, beginning at the specified position, to the specified position of the destination array (non-atomically).
		/// </summary>
		/// <param name="src"></param>
		/// <param name="srcOff"></param>
		/// <param name="dest"></param>
		/// <param name="destOff"></param>
		/// <param name="length"></param>
		/// <returns></returns>
		public static short arrayCopyNonAtomic(sbyte[] src, short srcOff, sbyte[] dest, short destOff, short length)
		{
			for (int i = 0; i < length; i++)
			{
				dest[destOff + i] = (sbyte)src[srcOff + i];
			}

			return length;
		}

		public static short arrayCopyNonAtomic(byte[] src, short srcOff, byte[] dest, short destOff, short length)
		{
			for (int i = 0; i < length; i++)
			{
				dest[destOff + i] = (byte)src[srcOff + i];
			}
			return length;
		}

		public static short arrayCopyNonAtomic(sbyte[] src, short srcOff, byte[] dest, short destOff, short length)
		{
			for (int i = 0; i < length; i++)
			{
				dest[destOff + i] = (byte)src[srcOff + i];
			}
			return length;
		}

		public static short arrayCopyNonAtomic(byte[] src, short srcOff, sbyte[] dest, short destOff, short length)
		{
			for (int i = 0; i < length; i++)
			{
				dest[destOff + i] = (sbyte)src[srcOff + i];
			}
			return length;
		}
	}
}
