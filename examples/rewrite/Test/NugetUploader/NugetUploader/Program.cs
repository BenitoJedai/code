using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace NugetUploader
{
    class Program
    {
        static void Main(string[] args)
        {
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201403/20140326
            // X:\jsc.smokescreen.svn\core\javascript\com.abstractatech.analytics\com.abstractatech.analytics\ApplicationWebService.cs
            // X:\jsc.svn\examples\rewrite\Test\Feature1\Feature1\Class1.cs
            // "C:\util\jsc\nuget\Feature4.1.0.0.0.nupkg"

            var c = new WebClient();

            c.UploadProgressChanged +=
                (sender, e) =>
                {
                    Console.WriteLine(new { e.ProgressPercentage });

                };

            var x = c.UploadFile(
                //"http://192.168.1.84:26994/upload",
                "http://my.jsc-solutions.net/upload",

                @"C:\util\jsc\nuget\Feature4.1.0.0.0.nupkg"
            );

            // 		Encoding.UTF8.GetString(x)	"<ok>\r\n  <ContentKey>2</ContentKey>\r\n</ok>"	string
            // 		Encoding.UTF8.GetString(x)	"<ok><ContentKey>6</ContentKey></ok>"	string


            Debugger.Break();
        }
    }
}
