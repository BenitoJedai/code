using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace ScriptCoreLibJavaCard.javacard.security
{
	[Script(IsNative = true)]
	public class KeyBuilder
	{
		/// <summary>
		/// DES Key Length LENGTH_DES3_2KEY = 128.
		/// </summary>
		public static readonly short LENGTH_DES3_2KEY = 128;

          

		/// <summary>
		/// Key object which implements interface type DESKey with persistent key data.
		/// </summary>
		public static readonly sbyte TYPE_DES = 3;


		/// <summary>
		/// Creates uninitialized cryptographic keys for signature and cipher algorithms.
		/// </summary>
		/// <param name="keyType"></param>
		/// <param name="keyLength"></param>
		/// <param name="keyEncryption"></param>
		/// <returns></returns>
		public static Key buildKey(sbyte keyType, short keyLength, bool keyEncryption)
		{
			return default(Key);
		}

	}
}
