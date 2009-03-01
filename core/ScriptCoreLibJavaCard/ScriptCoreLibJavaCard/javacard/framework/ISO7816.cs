using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace ScriptCoreLibJavaCard.javacard.framework
{
	[Script(IsNative = true)]
	public interface ISO7816
	{


	}

	[Script(IsNative = true, ExternalTarget = "ISO7816")]
	public static class ISO7816Constants
	{
		/// <summary>
		///  Response status : CLA value not supported = 0x6E00
		/// </summary>
		public static readonly short SW_CLA_NOT_SUPPORTED = 0x6e00;


		public static readonly sbyte OFFSET_CLA = 0;
		public static readonly sbyte OFFSET_INS = 1;
		public static readonly sbyte OFFSET_P1 = 2;
		public static readonly sbyte OFFSET_P2 = 3;
		public static readonly sbyte OFFSET_LC = 4;
	}
}
