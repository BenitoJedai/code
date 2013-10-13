using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SHA1Experiment
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public class ApplicationWebService
    {
        ///// <summary>
        ///// This Method is a javascript callable method.
        ///// </summary>
        ///// <param name="e">A parameter from javascript.</param>
        ///// <param name="y">A callback to javascript.</param>
        //public Task<byte[]> GetSHA1Bytes(byte[] bytes)
        //{
        //    // jsc, how do you do SHA1 C#?
        //    // ...
        //    // toast on tasktray: google says: 
        //    // http://stackoverflow.com/questions/1756188/how-to-use-sha1-or-md5-in-cwhich-one-is-better-in-performance-and-security-fo
        //    // 

        //    SHA1 sha = new SHA1CryptoServiceProvider();
        //    // This is one implementation of the abstract class SHA1.
        //    var result = sha.ComputeHash(
        //        bytes
        //    );

        //    // Send it back to the caller.
        //    return Task.FromResult(result);
        //}

        public Task<string> GetSHA1HexString(string bytes)
        {
            // jsc, how do you do SHA1 C#?
            // ...
            // toast on tasktray: google says: 
            // http://stackoverflow.com/questions/1756188/how-to-use-sha1-or-md5-in-cwhich-one-is-better-in-performance-and-security-fo
            // 

            SHA1 sha = new SHA1CryptoServiceProvider();
            // This is one implementation of the abstract class SHA1.
            var result = sha.ComputeHash(
               Encoding.UTF8.GetBytes(bytes)
            );

            // Send it back to the caller.
            return Task.FromResult(result.ToHexString());
        }
    }
}
