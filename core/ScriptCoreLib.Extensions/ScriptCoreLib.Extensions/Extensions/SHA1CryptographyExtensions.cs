using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace ScriptCoreLib.Extensions
{
    public static class SHA1CryptographyExtensions
    {
        public static byte[] ToSHA1(this string e)
        {
            // can we use it javascript yet?

            var sha = new SHA1CryptoServiceProvider();
            // This is one implementation of the abstract class SHA1.
            var result = sha.ComputeHash(Encoding.UTF8.GetBytes(e));

            return result;
        }
    }
}
