using java.util.zip;
using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLibJava.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace JVMCLRRSAAssetsLibrary
{
    public interface IFooPublicKey
    {
        byte[] Modulus { get; set; }
        byte[] Exponent { get; set; }

        Task<byte[]> Encrypt(byte[] e);
    }

    public struct FooKey : IFooPublicKey
    {
        // structs cannot inherit
        // but they can implement.
        byte[] IFooPublicKey.Modulus { get; set; }
        byte[] IFooPublicKey.Exponent { get; set; }



        Task<byte[]> IFooPublicKey.Encrypt(byte[] e)
        {
            throw new NotImplementedException();
        }
    }


    public static class FooKeyPair
    {
        public static readonly IFooPublicKey PublicKey = new FooKey { };


    }

    class zPrivateKey
    {
        //    RSAParameters p = RSA.ExportParameters(includePrivateParameters: false);

        // ExportParameters saved
        public RSAParameters PrivateParameters = new RSAParameters { Exponent = new byte[] { 7, 8, 9 } };
        public RSAParameters PublicParameters = new RSAParameters { Exponent = new byte[] { 7, 8, 9 } };
    }

    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main(string[] args)
        {
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201409/20140908/rsa-assetslibrary
            // start /MIN /WAIT cmd /C C:\util\jsc\bin\jsc.meta.exe ReferenceAssetsLibrary /ProjectFileName:"$(ProjectPath)" /NamedKeyPairs:JVMToCLR /NamedKeyPairs:CLRToJVM

            System.Console.WriteLine(
               typeof(object).AssemblyQualifiedName
            );

            // this shall be private to JVM process
            // to be used to decrypt! what can we sign also?
            //var x = NamedKeyPairs.JVMToCLR.PrivateKey;

            // public static $ArrayType$2 PublicParameters;
            NamedKeyPairs.CLRToJVMPublicKey.PublicParameters

            //new NamedKeyPairs.CLRToJVMPublicKey().AsyncEncrypt()
            //new NamedKeyPairs.CLRToJVMPublicKey { "hello" }

            //NamedKeyPairs.CLRToJVM.PublicKey.

            // this is a two way channel is it not?

            //FooKeyPair.PublicKey.Encrypt(

            CLRProgram.CLRMain();
        }


    }



    [SwitchToCLRContext]
    static class CLRProgram
    {

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void CLRMain()
        {
            System.Console.WriteLine(
                typeof(object).AssemblyQualifiedName
            );

            //// how shall we join two private keys into a nice encryption channel?
            //var z = NamedKeyPairs.JVMToCLR.PublicKey.Encrypt("data");

            //// decrypt in clr?
            //var zz = NamedKeyPairs.CLRToJVM.PrivateKey

            MessageBox.Show("click to close");

        }
    }


}
