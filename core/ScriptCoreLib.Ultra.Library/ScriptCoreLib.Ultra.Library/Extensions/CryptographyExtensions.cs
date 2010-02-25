using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Ultra.Library.Extensions
{
	public static class CryptographyExtensions
	{
		public static byte[] ToMD5Bytes(this byte[] buffer)
		{
			var x = new System.Security.Cryptography.MD5CryptoServiceProvider();


			return x.ComputeHash(buffer);
		}
	}
}
