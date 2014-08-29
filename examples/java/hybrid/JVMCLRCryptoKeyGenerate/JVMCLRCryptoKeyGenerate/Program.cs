#define JCE
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLibJava.Extensions;
using System.Xml.Linq;
using java.io;
using java.net;
using java.util.zip;
using System.Collections;
using System.IO;
using System.Diagnostics;
using java.security;
#if JCE
using javax.security;
using javax.crypto;
using java.util.prefs;
#endif

namespace JVMCLRCryptoKeyGenerate
{

    static class Program
    {
        // jce wont like partial scriptcorelib builds?
        //0019 jce create au.net.aba.crypto.provider.ABAProvider
        //System.TypeLoadException: Type 'au.net.aba.crypto.provider.ABAProvider' from assembly 'jce, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null' tried to override method 'isEmpty' but does not implement or inherit that method.
        //   at System.Reflection.Emit.TypeBuilder.TermCreateClass(RuntimeModule module, Int32 tk, ObjectHandleOnStack type)

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main(string[] args)
        {
            // X:\jsc.svn\examples\java\hybrid\JVMCLRRSA\JVMCLRRSA\Program.cs

            #region Do a release build to create a hybrid jvmclr program.
            if (typeof(object).FullName != "java.lang.Object")
            {
                System.Console.WriteLine("Do a release build to create a hybrid jvmclr program.");
                Debugger.Break();
                return;
            }
            #endregion


            System.Console.WriteLine("jvm ready! " + typeof(object).FullName);



#if JCE
            try
            {
                
                KeyPairGenerator keyGen = KeyPairGenerator.getInstance("RSA");
                
                keyGen.initialize(2048);

                KeyPair keyPair = keyGen.generateKeyPair();
                PublicKey publicKey = keyPair.getPublic();
                PrivateKey privateKey = keyPair.getPrivate();  
                //System.Console.WriteLine("Public Key - " + publicKey.ToString());
                //System.Console.WriteLine("Private Key - " + privateKey.ToString());  

                var data = new sbyte[]{ 0x02, 0x03, 0x04, 0x05};
                System.Console.WriteLine(data.Length.ToString());


                Cipher rsaCipher = Cipher.getInstance("RSA");


                //Encrypt
                rsaCipher.init(Cipher.ENCRYPT_MODE, publicKey);
                sbyte[] encByte = rsaCipher.doFinal(data);
                System.Console.WriteLine(encByte.Length.ToString());


                //Decrypt
                rsaCipher.init(Cipher.DECRYPT_MODE, privateKey);
                sbyte[] decByte = rsaCipher.doFinal(encByte);
                System.Console.WriteLine(decByte.Length.ToString());

                if (data == decByte)
                {
                    System.Console.WriteLine("true");
                }
                else
                {
                    System.Console.WriteLine("false");
                }

            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex);
            }
#endif


            CLRProgram.CLRMain();

            System.Console.WriteLine("jvm exit!");
        }


    }


    [SwitchToCLRContext]
    static class CLRProgram
    {
        [STAThread]
        public static void CLRMain()
        {
            System.Console.WriteLine(
                typeof(object).AssemblyQualifiedName
            );


            MessageBox.Show("click to close");

        }
    }


}
