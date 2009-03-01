using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace ScriptCoreLibJavaCard.javacardx.crypto
{
	[Script(IsNative = true)]
	public class Cipher
	{
		/// <summary>
		/// Cipher algorithm ALG_DES_CBC_NOPAD provides a cipher using DES in CBC mode or triple DES in outer CBC mode, and does not pad input data.
		/// </summary>
		public static readonly sbyte ALG_DES_CBC_NOPAD = 1;

		/// <summary>
		/// Cipher algorithm ALG_DES_ECB_NOPAD provides a cipher using DES in ECB mode, and does not pad input data.
		/// </summary>
		public static readonly sbyte ALG_DES_ECB_NOPAD = 5;
          

		/// <summary>
		/// Creates a Cipher object instance of the selected algorithm.
		/// </summary>
		/// <param name="algorithm"></param>
		/// <param name="externalAccess"></param>
		/// <returns></returns>
		public static Cipher getInstance(sbyte algorithm, bool externalAccess)
		{
			return default(Cipher);
		}



	}
}
