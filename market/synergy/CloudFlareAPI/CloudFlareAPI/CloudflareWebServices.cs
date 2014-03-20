using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace cloudflare.experimental
{

    [Obsolete(@"
a generic experimental WebClient based service wrapper. 2013/01/09

2014-01-28 What if we want this component to be exposed via toolbox?

20140303 can we use it in testing and then for air-ios yet?
")]
    public class CloudflareWebServices :
        System.ComponentModel.Component,
        System.ComponentModel.IComponent
    {
        //        ---------------------------
        //Microsoft Visual Studio Express 2012 for Windows Desktop
        //---------------------------
        //Failed to create component 'MoneseWebServices'.  The error message follows:

        // 'Component of type MoneseWebServices could not be created.  
        //        Make sure the type implements IComponent and provides 
        //            an appropriate public constructor.  
        //                Appropriate constructors either take no parameters or 
        //                    take a single IContainer parameter.'
        //---------------------------
        //OK   Help   
        //---------------------------

        //---------------------------
        //Microsoft Visual Studio Express 2012 for Web
        //---------------------------
        //There are no components in 'C:\util\jsc\nuget\CloudFlareAPI.1.0.0.0\lib\monese%20API.dll' that can be placed on the toolbox.
        //---------------------------
        //OK   
        //---------------------------

        public static string token = "73e882084205b05a34abf8a9a89f7dc26ba29";
        public static string email = "admin@monese.com";
        public static string cloudflareUrl = "https://www.cloudflare.com/api_json.html";

        public CloudflareWebServices()
            : this(null)
        {
            // X:\jsc.svn\examples\actionscript\Test\TestEmptyConstructor\TestEmptyConstructor\Class1.cs

        }

        public CloudflareWebServices(System.ComponentModel.IContainer c)
        {

        }

        static string UTF8ToBase64StringOrDefault(string e)
        {
            if (e == null)
                return null;

            var bytes = Encoding.UTF8.GetBytes(e);

            return Convert.ToBase64String(bytes);
        }

        static string UTF8FromBase64StringOrDefault(string e)
        {
            // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/04-monese/2014/201401/20140101

            var o = default(string);
            // allow 0 char do be sent
            if (e != null)
            {
                //Console.WriteLine("before Convert.FromBase64String");
                var bytes = Convert.FromBase64String(e);

                //Console.WriteLine("before Encoding.UTF8.GetString");
                o = Encoding.UTF8.GetString(bytes);
            }

            //Console.WriteLine("exit UTF8FromBase64StringOrDefault");

            return o;
        }

        [Obsolete("do we have async/await available for AIR yet?")]
        public void RetreiveListOOfDomains(Action<string> yield)
        {
            // X:\jsc.svn\examples\java\Test\JVMCLRWebClient\JVMCLRWebClient\Program.cs

            //var _06000039_curr = UTF8ToBase64StringOrDefault(curr);

            var c = new WebClient();
            c.UploadValuesCompleted +=
                (sender, args) =>
                {
                    var resultBytes = args.Result;
                    var data = Encoding.UTF8.GetString(resultBytes);

                    //resultString = "<document><TaskComplete><TaskResult>11</TaskResult></TaskComplete></document>"

                    var xml = XElement.Parse(data);
                    Console.WriteLine(xml.Element("TaskComplete").Element("TaskResult").Value);
                    var TaskResult = UTF8FromBase64StringOrDefault(xml.Element("TaskComplete").Element("TaskResult").Value);
                    Console.WriteLine(TaskResult);
                    yield(TaskResult);
                };


            // X:\jsc.svn\examples\actionscript\Test\TestWebClient\TestWebClient\ApplicationSprite.cs

            c.UploadValuesAsync(
                // generated, whenn will the uri change and break this?
                address: new Uri(cloudflareUrl),
                    data: new System.Collections.Specialized.NameValueCollection { 
                        {"WebMethodMetadataToken","06000039"},
                        {"WebMethodMetadataName","GetCurrencyRateBasedOnString"},
                                { "_06000039_currency", _06000039_curr},
                            }
            );
        }   
    }
}
