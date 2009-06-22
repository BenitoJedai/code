using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace java.security
{
	// http://java.sun.com/j2se/1.4.2/docs/api/java/security/MessageDigest.html
	[Script(IsNative = true)]
	public abstract class MessageDigest : MessageDigestSpi
	{
		/// <summary>
		/// Generates a MessageDigest object that implements the specified digest algorithm.
		/// </summary>
		/// <param name="algorithm"></param>
		/// <returns></returns>
		public static MessageDigest getInstance(string algorithm)
		{
			return default(MessageDigest);
		}

		/// <summary>
		/// Updates the digest using the specified array of bytes.
		/// </summary>
		/// <param name="input"></param>
		public void update(sbyte[] input)
		{
		}

		/// <summary>
		/// Completes the hash computation by performing final operations such as padding.
		/// </summary>
		/// <returns></returns>
		public sbyte[] digest()
		{
			return default(sbyte[]);
		}
          
	}
}
