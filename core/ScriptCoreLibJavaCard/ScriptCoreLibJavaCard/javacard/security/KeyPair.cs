using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace ScriptCoreLibJavaCard.javacard.security
{
	[Script(IsNative = true)]
	public sealed class KeyPair
	{
		/// <summary>
		/// KeyPair object containing a RSA key pair with private key in its Chinese Remainder Theorem form.
		/// </summary>
		public static readonly sbyte ALG_RSA_CRT = 2;
          

		/// <summary>
		///  Constructs a KeyPair instance for the specified algorithm and keylength; the encapsulated keys are uninitialized.
		/// </summary>
		/// <param name="algorithm"></param>
		/// <param name="keyLength"></param>
		public KeyPair(sbyte algorithm, short keyLength) 
		{

		}

		/// <summary>
		/// (Re)Initializes the key objects encapsulated in this KeyPair instance with new key values.
		/// </summary>
		public void genKeyPair()
		{
		}


		/// <summary>
		/// Returns a reference to the public key component of this KeyPair object.
		/// </summary>
		/// <returns></returns>
		public PublicKey getPublic()
		{
			return default(PublicKey);
		}

          
	}
}
