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
using System.Security.Cryptography;

namespace JVMCLRRSACryptoStream
{
    class RSAEnCryptoTransform : ICryptoTransform, IDisposable
    {

        public bool CanReuseTransform { get { return (true); } }

        public bool CanTransformMultipleBlocks { get { return (false); } }

        public int InputBlockSize { get { return (OutputBlockSize * 8 - 384) / 8 + 7; } }

        public int OutputBlockSize { get { return 128; } }

        private RSACryptoServiceProvider rsaEncryptor;


        public RSAEnCryptoTransform(RSACryptoServiceProvider rsaCSP)

        { rsaEncryptor = rsaCSP; }

        public void Dispose() { }

        public int TransformBlock(byte[] inputBuffer,

        int inputOffset, int inputCount, byte[] outputBuffer,

        int outputOffset)
        {

            byte[] plaintext = new byte[inputCount];

            Array.Copy(inputBuffer, inputOffset, plaintext, 0, inputCount);

            byte[] ciphertext;

            ciphertext = rsaEncryptor.Encrypt(plaintext, true);
            // Additional information: Bad Length.

            ciphertext.CopyTo(outputBuffer, outputOffset);

            return (ciphertext.Length);

        }

        public byte[] TransformFinalBlock(byte[] inputBuffer,

        int inputOffset, int inputCount)
        {

            byte[] plaintext = new byte[inputCount];

            Array.Copy(inputBuffer, inputOffset, plaintext, 0, inputCount);

            byte[] ciphertext;

            ciphertext = rsaEncryptor.Encrypt(

             plaintext, true);

            return (ciphertext);
        }
    }

    class RSADeCryptoTransform : ICryptoTransform, IDisposable
    {

        public bool CanReuseTransform { get { return (true); } }

        public bool CanTransformMultipleBlocks { get { return (false); } }

        public int InputBlockSize { get { return 128; } }

        public int OutputBlockSize { get { return (InputBlockSize * 8 - 384) / 8 + 7; ; } }

        private RSACryptoServiceProvider rsaDecryptor;


        public RSADeCryptoTransform(RSACryptoServiceProvider rsaCSP)

        { rsaDecryptor = rsaCSP; }

        public void Dispose() { }

        public int TransformBlock(byte[] inputBuffer,

         int inputOffset, int inputCount, byte[] outputBuffer,

         int outputOffset)
        {

            byte[] ciphertext = new byte[inputCount];

            Array.Copy(inputBuffer, inputOffset, ciphertext, 0, inputCount);

            byte[] plaintext;

            plaintext = rsaDecryptor.Decrypt(ciphertext, true);

            plaintext.CopyTo(outputBuffer, outputOffset);

            return (plaintext.Length);
        }

        public byte[] TransformFinalBlock(byte[] inputBuffer,

         int inputOffset, int inputCount)
        {

            byte[] ciphertext = new byte[inputCount];
            byte[] plaintext = new byte[0];


            if (inputCount > 0)
            {
                Array.Copy(inputBuffer, inputOffset, ciphertext, 0, inputCount);


                plaintext = rsaDecryptor.Decrypt(

                ciphertext, true);
            }
            // Additional information: Error occurred while decoding OAEP padding.

            return (plaintext);
        }
    }

    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main(string[] args)
        {
            System.Console.WriteLine(
               typeof(object).AssemblyQualifiedName
            );

            // http://www.drdobbs.com/windows/programming-public-key-cryptostreams-par/184416907
            // X:\jsc.svn\examples\java\hybrid\JVMCLRRSACryptoServiceProviderExport\JVMCLRRSACryptoServiceProviderExport\Program.cs




            //var dwKeySize = (0x100 + 0x100) * 8;
            var dwKeySize = 128 * 8;
            var MaxData = (dwKeySize - 384) / 8 + 7;

            var RSA = new RSACryptoServiceProvider(
                   dwKeySize: dwKeySize,
                   parameters: new CspParameters { }
               );

            RSAParameters p = RSA.ExportParameters(includePrivateParameters: false);



            var value = RSA.Encrypt(
                    Encoding.UTF8.GetBytes("hello from server"), fOAEP: true
            //Encoding.UTF8.GetBytes("hello from server"), fOAEP: false
            );


            var data = new MemoryStream();
            var goo = new CryptoStream(data, new RSAEnCryptoTransform(RSA), CryptoStreamMode.Write);

            var text = Encoding.UTF8.GetBytes("hello".PadRight(8000) + "world");

            goo.WriteAsync(text, 0, text.Length).Wait();

            goo.FlushFinalBlock();

            goo.FlushAsync().Wait();


            var foo = new CryptoStream(data, new RSADeCryptoTransform(RSA), CryptoStreamMode.Read);

            //var buffer = new byte[100];
            //var buffer = new byte[MaxData];
            var buffer = new byte[0x1000];
            var result = foo.Read(buffer, 0, buffer.Length);
            //var result = foo.ReadAsync(buffer, 0, buffer.Length).Result;


            CLRProgram.CLRMain();
        }


    }


    public delegate XElement XElementFunc();

    [SwitchToCLRContext]
    static class CLRProgram
    {
        public static XElement XML { get; set; }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void CLRMain()
        {
            System.Console.WriteLine(
                typeof(object).AssemblyQualifiedName
            );

            // X:\jsc.svn\examples\javascript\forms\FormsNIC\FormsNIC\ApplicationWebService.cs


            MessageBox.Show("click to close");

        }
    }


}
