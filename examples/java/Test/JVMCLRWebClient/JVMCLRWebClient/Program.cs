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
using System.Net;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml.Linq;

namespace JVMCLRWebClient
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

            GetCurrencyRateBasedOnStringAsync("EUR", null);


            CLRProgram.CLRMain();
        }

        public static void GetCurrencyRateBasedOnStringAsync(string curr, Action<string> yield)
        {
            //java.lang.Object, rt
            //{ addressString = http://my.monese.com/xml/GetCurrencyRateBasedOnString }
            //before writeBytes { Length = 106 }
            //System.Object, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
            //before Read
            //yield UploadValuesAsync { Length = 83 }
            //{ Error =  }
            //{ data = <document><TaskComplete><TaskResult>MC44NA==</TaskResult></TaskComplete></document> }

            // X:\jsc.svn\examples\java\Test\JVMCLRWebClient\JVMCLRWebClient\Program.cs

            var _06000039_curr = UTF8ToBase64StringOrDefault(curr);

            //enter UploadValuesAsync
            //System.Object, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
            //yield UploadValuesAsync

            //HtmlElement: Access denied | my.monese.com used CloudFlare to restrict access</title>
            var c = new WebClient();
            c.UploadValuesCompleted +=
                (sender, args) =>
                {

                    //Additional information: An exception occurred during the operation, making the result invalid.  Check InnerException for exception details.

                    //[System.Net.WebException] = {"The underlying connection was closed: An unexpected error occurred on a receive."}
                    Console.WriteLine(new { args.Error });

                    var resultBytes = args.Result;
                    var data = Encoding.UTF8.GetString(resultBytes);

                    //data = "<document><TaskComplete><TaskResult>MC44NA==</TaskResult></TaskComplete></document>"
                    // /xml/GetCurrencyRateBasedOnString 200 84ms

                    Console.WriteLine(new { data });

                    ////var xml = XElement.Parse(data);
                    ////Console.WriteLine(xml.Element("TaskComplete").Element("TaskResult").Value);
                    ////var TaskResult = UTF8FromBase64StringOrDefault(xml.Element("TaskComplete").Element("TaskResult").Value);
                    ////Console.WriteLine(TaskResult);
                    ////yield(TaskResult);
                };


            // X:\jsc.svn\examples\actionscript\Test\TestWebClient\TestWebClient\ApplicationSprite.cs

            c.UploadValuesAsync(
                // generated, whenn will the uri change and break this?
                address: new Uri("http://my.monese.com/xml/GetCurrencyRateBasedOnString"),
                    data: new System.Collections.Specialized.NameValueCollection { 
                        {"WebMethodMetadataToken","06000039"},
                        {"WebMethodMetadataName","GetCurrencyRateBasedOnString"},
                                { "_06000039_currency", _06000039_curr},
                            }
            );
        }

        static string UTF8ToBase64StringOrDefault(string e)
        {
            if (e == null)
                return null;

            var bytes = Encoding.UTF8.GetBytes(e);

            return Convert.ToBase64String(bytes);
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
