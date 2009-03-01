using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace ScriptCoreLibJavaCard.javacard.security
{
	[Script(IsNative = true)]
	public class Signature
	{
		/// <summary>
		/// Signature algorithm ALG_RSA_SHA_PKCS1 generates a 20-byte SHA digest, pads the digest according to the PKCS#1 (v1.5) scheme, and encrypts it using RSA. 
		/// </summary>
		public static readonly sbyte ALG_RSA_SHA_PKCS1 = 10;


		/// <summary>
		/// Creates a Signature object instance of the selected algorithm.
		/// </summary>
		/// <param name="algorithm"></param>
		/// <param name="externalAccess"></param>
		/// <returns></returns>
		public static Signature getInstance(sbyte algorithm, bool externalAccess)
		{
			return default(Signature);
		}

	}
}
