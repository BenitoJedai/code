using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.IO;
//using java.security;
//using ScriptCoreLibJava.BCLImplementation.System.IO;

namespace ScriptCoreLib.Shared.BCLImplementation.System.Security.Cryptography
{
    // http://referencesource.microsoft.com/#mscorlib/system/security/cryptography/md5cryptoserviceprovider.cs
    // tested by ?

    [Script(Implements = typeof(global::System.Security.Cryptography.MD5CryptoServiceProvider))]
    internal class __MD5CryptoServiceProvider : __MD5
    {
        public override byte[] InternalComputeHash(byte[] buffer)
        {
            //var value = default(byte[]);

            var x = new __MD5CryptoServiceProviderByMahmood();

            x.ValueAsByte = buffer;

            var m = new MemoryStream();

            //Console.WriteLine(new { x.FingerPrint });

            // 0:2490ms { FingerPrint = 8A2410FB8A2410FB8A2410FB8A2410FB } 


            var A = BitConverter.GetBytes(x.dgFingerPrint.A);

            //Console.WriteLine(__MD5CryptoServiceProviderByMahmood.__MD5Helper.ReverseByte(x.dgFingerPrint.A).ToString("X8"));

            var B = BitConverter.GetBytes(x.dgFingerPrint.B);
            //Console.WriteLine(__MD5CryptoServiceProviderByMahmood.__MD5Helper.ReverseByte(x.dgFingerPrint.B).ToString("X8"));
            var C = BitConverter.GetBytes(x.dgFingerPrint.C);
            //Console.WriteLine(__MD5CryptoServiceProviderByMahmood.__MD5Helper.ReverseByte(x.dgFingerPrint.C).ToString("X8"));
            var D = BitConverter.GetBytes(x.dgFingerPrint.D);
            //Console.WriteLine(__MD5CryptoServiceProviderByMahmood.__MD5Helper.ReverseByte(x.dgFingerPrint.D).ToString("X8"));


            //var A = BitConverter.GetBytes(__MD5CryptoServiceProviderByMahmood.__MD5Helper.ReverseByte(x.dgFingerPrint.A));
            //var B = BitConverter.GetBytes(__MD5CryptoServiceProviderByMahmood.__MD5Helper.ReverseByte(x.dgFingerPrint.B));
            //var C = BitConverter.GetBytes(__MD5CryptoServiceProviderByMahmood.__MD5Helper.ReverseByte(x.dgFingerPrint.C));
            //var D = BitConverter.GetBytes(__MD5CryptoServiceProviderByMahmood.__MD5Helper.ReverseByte(x.dgFingerPrint.D));

            // 9f331e57d5a8f0a3e2291cdc8a2410fb
            m.Write(A, 0, 4);
            m.Write(B, 0, 4);
            m.Write(C, 0, 4);
            m.Write(D, 0, 4);

            //                { hash = 8a2410fb e2291cdc d5a8f0a 39f331e57 }
            //0:2841ms { FingerPrint = 8A2410FB 8A2410FB 8A2410F B8A2410FB } 

            //try
            //{
            //    // http://mindprod.com/jgloss/sha1.html
            //    var a = MessageDigest.getInstance("MD5");

            //    a.update(__File.InternalByteArrayToSByteArray(buffer));

            //    value = __File.InternalSByteArrayToByteArray(a.digest());
            //}
            //catch
            //{
            //    throw;
            //}

            return m.ToArray();
        }
    }
}
