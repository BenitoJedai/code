using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ScriptCoreLib.Ultra.Library.Extensions
{
	public static class SHA1CryptographyExtensions
	{
        // used by?
        public static byte[] FileNameToSHA1Bytes(this string Input)
        {
            var SourceHash = default(byte[]);

            var h = new System.Security.Cryptography.SHA1CryptoServiceProvider();

            using (var f = File.OpenRead(Input))
                SourceHash = h.ComputeHash(f);

            return SourceHash;
        }
	}
}
