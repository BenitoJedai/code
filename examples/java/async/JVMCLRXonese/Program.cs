using java.util.zip;
using monese.experimental;
using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLibJava.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace JVMCLRXonese
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

            //Task.Run
            new Func<Task>(
                async delegate
                {
                    //java.lang.Object, rt
                    //{ addressString = http://my.monese.com/xml/RegisterUserShort }
                    //before writeBytes { Length = 133 }
                    //before Read
                    //yield UploadValuesAsync { Length = 77 }
                    //24
                    //{ RegisterUserShort_value = 24, ElapsedMilliseconds = 4609 }
                    //System.Object, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089


                    var s = Stopwatch.StartNew();

                    // {"The remote name could not be resolved: 'my.monese.com'"}
                    var x = new MoneseWebServices();

                    var RegisterUserShort_value = await x.RegisterUserShort("xmoneseAPK@", "1234");

                    // how can we get back to the main thread?
                    // then we could send the data back to the UI!

                    //xx = 398
                    Console.WriteLine(new { RegisterUserShort_value, s.ElapsedMilliseconds });

                }
            )().Wait();

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
