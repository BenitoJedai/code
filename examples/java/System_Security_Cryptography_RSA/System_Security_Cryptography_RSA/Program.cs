using System.Threading;
using System;

using ScriptCoreLib;
using System.Security.Cryptography;
using System.IO;


namespace System_Security_Cryptography_RSA
{

    [Script]
    public class Program
    {
        public static void Main(string[] args)
        {
            // Use Release Build to use jsc to generate java program
            // Use Debug Build to develop on .net

            //Revision: 1935
            //Author: zproxy
            //Date: Friday, July 24, 2009 5:16:24 PM
            //Message:
            //more RSA

            //Added : /core/ScriptCoreLibJava/BCLImplementation/System/Security/Cryptography/AsymmetricAlgorithm.cs
            //Added : /core/ScriptCoreLibJava/BCLImplementation/System/Security/Cryptography/RSA.cs
            //Added : /core/ScriptCoreLibJava/BCLImplementation/System/Security/Cryptography/RSACryptoServiceProvider.cs
            //Added : /core/ScriptCoreLibJava/BCLImplementation/System/Security/Cryptography/RSAParameters.cs




            Console.WriteLine("System_Security_Cryptography_RSA. Crosscompiled from C# to Java.");

            var x = new byte[] 
			{
				  0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00
				, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x82, 0x01
				, 0x00, 0x95, 0xcf, 0x63, 0x7d, 0x62, 0x1c, 0x23, 0xba, 0xae, 0x0f, 0xa4, 0xae, 0xb9, 0x91, 0x3d
				, 0xa5, 0x84, 0x4d, 0x94, 0x26, 0x0a, 0x75, 0x49, 0x53, 0xbb, 0x28, 0xa2, 0x07, 0x49, 0x0b, 0xbb
				, 0x68, 0x01, 0xe1, 0xf7, 0x85, 0x4d, 0x9f, 0xa8, 0xc3, 0x56, 0x56, 0x7f, 0xfa, 0xbe, 0x30, 0xe7
				, 0x4a, 0x80, 0x44, 0x57, 0x71, 0xf0, 0xfc, 0xcf, 0x13, 0x6f, 0x70, 0x51, 0x3d, 0x22, 0x22, 0xee
				, 0x50, 0x25, 0x0c, 0x96, 0x66, 0x59, 0xd4, 0x2e, 0xcd, 0x97, 0x71, 0x26, 0x34, 0x09, 0xe9, 0x72
				, 0xaa, 0x9d, 0x9e, 0x3a, 0x4e, 0xed, 0xdf, 0x5c, 0x64, 0x2a, 0x0a, 0xd0, 0x02, 0x90, 0x4c, 0xdf
				, 0x93, 0x08, 0x08, 0xeb, 0x62, 0x23, 0x66, 0xd8, 0x09, 0xa7, 0x40, 0xd7, 0xfc, 0x26, 0x77, 0x0b
				, 0x18, 0xda, 0xe9, 0x2a, 0x3b, 0x9b, 0x14, 0x42, 0x7a, 0x2e, 0xaa, 0x97, 0x4c, 0xf0, 0x4b, 0x30
				, 0xfe, 0xf5, 0x27, 0x32, 0x59, 0xcb, 0x85, 0x21, 0x35, 0x8d, 0xd5, 0x28, 0xb3, 0x7b, 0xeb, 0x97
				, 0x54, 0x28, 0x5f, 0x77, 0x01, 0x2e, 0x95, 0xb0, 0x7f, 0xd4, 0x2f, 0x7a, 0x5e, 0x67, 0x21, 0x11
				, 0xd5, 0x6c, 0x3a, 0x00, 0x4b, 0x88, 0x38, 0x09, 0xb9, 0x6b, 0x55, 0xa1, 0xe2, 0x3e, 0x78, 0xe1
				, 0x54, 0x53, 0x1d, 0xf3, 0x7d, 0x9d, 0x53, 0x8f, 0x81, 0x28, 0x02, 0xaf, 0x76, 0x82, 0x90, 0x0e
				, 0x1c, 0xfe, 0x7b, 0xf5, 0xdc, 0xba, 0xf7, 0xd3, 0x26, 0x75, 0xc7, 0xe1, 0x1e, 0x1e, 0x9d, 0x10
				, 0xc1, 0x4b, 0x9d, 0xc0, 0xfb, 0x30, 0x9e, 0x68, 0x3a, 0x8c, 0xb4, 0xb4, 0xae, 0xa9, 0x19, 0xf7
				, 0x7d, 0xeb, 0xc1, 0xbe, 0xe3, 0x7e, 0x9b, 0xa3, 0xa5, 0xf2, 0xe2, 0xbf, 0xa9, 0x73, 0xb1, 0x34
				, 0x38, 0x50, 0xfb, 0xcc, 0x06, 0x9f, 0x97, 0xd9, 0x4b, 0x3c, 0x98, 0x81, 0xd2, 0x2a, 0x54, 0x68
				, 0xcf, 0x82, 0x04, 0x72, 0x9d, 0x83, 0x9d, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00
				, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00
				, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00
				, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00
				, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00
				, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00
				, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00
				, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00
				, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00
				, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00
			};
            var data = new BinaryReader(new MemoryStream(x));

            data.BaseStream.Position = 0x1E;

            #region modulus
            var modulus_tag = (byte)data.ReadByte();
            var modulus_length = (ushort)data.ReadByte();

            if (modulus_tag != 0x81)
            {
                modulus_length <<= 8;
                modulus_length += (byte)data.ReadByte();
            }

            var modulus_data = data.ReadBytes(modulus_length);

            var exponent_tag = (byte)data.ReadByte();
            var exponent_length = (ushort)data.ReadByte();

            var exponent_data = data.ReadBytes(exponent_length);
            #endregion

            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            RSAParameters RSAKeyInfo = new RSAParameters();
            RSAKeyInfo.Modulus = modulus_data;
            RSAKeyInfo.Exponent = exponent_data;
            rsa.ImportParameters(RSAKeyInfo);
            rsa.ToXmlString(false).ToConsole();
        }

    }


}
