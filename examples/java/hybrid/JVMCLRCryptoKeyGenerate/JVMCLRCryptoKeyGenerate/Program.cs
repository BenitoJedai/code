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
using javax.security;

namespace JVMCLRCryptoKeyGenerate
{

    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main(string[] args)
        {
            #region Do a release build to create a hybrid jvmclr program.
            if (typeof(object).FullName != "java.lang.Object")
            {
                System.Console.WriteLine("Do a release build to create a hybrid jvmclr program.");
                Debugger.Break();
                return;
            }
            #endregion


            System.Console.WriteLine("jvm ready! " + typeof(object).FullName);

            //X:\jsc.internal.svn\examples\java\synergy\JVMCLRCryptoKeyGenerate\JVMCLRCryptoKeyGenerate\bin\Release>JVMCLRCryptoKeyGenerate.exe
            //jvm ready! java.lang.Object
            //{ p = au.net.aba.crypto.PublicKeySecuredObject@147c5fc }
            //jvm->clr
            //clr->jvm
            //clr->jvm
            //jvm->clr
            //clr exit!
            //jvm exit!

            try
            {
                
                KeyPairGenerator keyGen = KeyPairGenerator.getInstance("RSA");
                
                keyGen.initialize(2048);

                KeyPair pair = keyGen.generateKeyPair();
                PrivateKey priv = pair.getPrivate();
                PublicKey pub = pair.getPublic();

                System.Console.WriteLine(priv.ToString());
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex);
            }



            CLRProgram.CLRMain(
                yield:
                    delegate
                    {
                        System.Console.WriteLine("clr->jvm");


                    }
            );

            System.Console.WriteLine("jvm exit!");
        }


    }


    [SwitchToCLRContext]
    static class CLRProgram
    {
        [STAThread]
        public static void CLRMain(
             Action yield = null
            )
        {
            System.Console.WriteLine("jvm->clr");

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(
                new Form
                {
                    Text = "Close this to continue..."
                }.With(f =>
                {
                    f.FormClosing +=
                        delegate
                        {
                            System.Console.WriteLine("clr->jvm");
                            yield();
                            System.Console.WriteLine("jvm->clr");
                        };
                }
            ));
            System.Console.WriteLine("clr exit!");
        }
    }


}
