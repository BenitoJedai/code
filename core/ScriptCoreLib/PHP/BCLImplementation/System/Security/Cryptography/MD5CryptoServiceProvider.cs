using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.PHP.BCLImplementation.System.IO;

namespace ScriptCoreLib.PHP.BCLImplementation.System.Security.Cryptography
{
	[Script(Implements = typeof(global::System.Security.Cryptography.MD5CryptoServiceProvider))]
	internal class __MD5CryptoServiceProvider : __MD5
	{
		public override byte[] InternalComputeHash(byte[] buffer)
		{
			// http://ok-cool.com/posts/read/125-php-md5-not-the-same-as-net-md5/

			var data = __File.FromBytes(buffer);

			return __File.ToBytes(Native.API.md5(data, true));
		}
	}
}
