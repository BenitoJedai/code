using java.util.zip;
using monese.experimental;
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

namespace JVMCLRXoneseAPI
{

    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main(string[] args)
        {
            // jsc needs to see args to make Main into main for javac..


            // see also>
            // X:\jsc.svn\examples\javascript\android\AndroidBroadcastLogger\AndroidBroadcastLogger\ApplicationWebService.cs

            System.Console.WriteLine(
               typeof(object).AssemblyQualifiedName
            );

            //0001 02000002 JVMCLRXoneseAPI__i__d.jvm::JVMCLRXoneseAPI.Program


            // Implementation not found for type import :
            // type: System.Threading.Interlocked
            // method: T CompareExchange[T](T ByRef, T, T)
            // Did you forget to add the [Script] attribute?
            // Please double check the signature!

            // assembly: Y:\staging\JVMCLRXoneseAPI__i__d.jvm.exe
            // type: monese.experimental.MoneseWebServices, JVMCLRXoneseAPI__i__d.jvm, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
            // offset: 0x001e
            //  method:Void add_RegisterUserShortAsyncComplete(System.Action`1[System.Int64])
            //089c:02:01 after worker yield...

            new MoneseWebServices().With(
                 x =>
                 {
                     x.GetCurrencyRateBasedOnStringAsync("GBP",
                           GetCurrencyRateBasedOnStringAsync_value =>
                           {
                               // { GetCurrencyRateBasedOnStringAsync_value = 0.84 }

                               Console.WriteLine(new { GetCurrencyRateBasedOnStringAsync_value });
                           }
                       );

                 }
             );


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


            MessageBox.Show("click to close");

        }
    }


}
