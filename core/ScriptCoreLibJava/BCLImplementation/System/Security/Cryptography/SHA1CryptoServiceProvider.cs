using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using java.security;
using ScriptCoreLibJava.BCLImplementation.System.IO;

namespace ScriptCoreLibJava.BCLImplementation.System.Security.Cryptography
{
	[Script(Implements = typeof(global::System.Security.Cryptography.SHA1CryptoServiceProvider))]
	internal class __SHA1CryptoServiceProvider : __SHA1
	{
		public override byte[] InternalComputeHash(byte[] buffer)
		{
			var value = default(byte[]);

			try
			{
				// http://mindprod.com/jgloss/sha1.html
				var a = MessageDigest.getInstance("SHA");

				a.update(__File.InternalByteArrayToSByteArray(buffer));

				value = __File.InternalSByteArrayToByteArray(a.digest());
			}
			catch
			{
				// yay, see the antipattern?
				throw new csharp.RuntimeException();
			}

			return value;
		}
	}
}
