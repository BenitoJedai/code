using jsc.meta.Commands.Rewrite.RewriteToUltraApplication;
using ScriptCoreLib.Desktop.Forms.Extensions;
using System;

namespace AppEngineUserAgentLoggerWithXSLXAsset
{
    /// <summary>
    /// You can debug your application by hitting F5.
    /// </summary>
    internal static class Program
    {
        public static void Main(string[] args)
        {
            var x = new Design.Book1B.Sheet1();

            var visit = x.Insert(
                new Design.Book1BSheet1Row
                {
                    // jsc experience should auto detect, 
                    // implicit column types

                    // should we infer the type?
                    // should we use dynamic? dynamic has no intellisense

                    //ScreenWidth = this.ScreenWidth,
                    //ScreenHeight = this.ScreenHeight,


                    //// not available for AppEngine?
                    //// http://stackoverflow.com/questions/8787463/how-to-identify-ip-address-of-requesting-client

                    //IPAddress = WebServiceHandler.Context.Request.UserHostAddress,

                    //// we are now logging all headers
                    ////UserAgent = WebServiceHandler.Context.Request.UserAgent,

                    //ClientTime = this.ClientTime,
                    //ServiceTime = ServiceTime,
                }
            );

            // 2AD6F7000000000
            // visit = 167582013207871488
            Console.WriteLine(new { visit });


            //var x = new ApplicationWebService { ScreenWidth = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width };
            //var y = x.Notfiy();

            ////Error	1	The best overloaded method match for 'AppEngineUserAgentLoggerWithXSLXAsset.ApplicationWebService.GetVisitHeadersFor(AppEngineUserAgentLoggerWithXSLXAsset.Design.Book1Sheet1Key)' has some invalid arguments	X:\jsc.svn\examples\javascript\appengine\AppEngineUserAgentLoggerWithXSLXAsset\AppEngineUserAgentLoggerWithXSLXAsset\Program.cs	16	21	AppEngineUserAgentLoggerWithXSLXAsset
            ////Error	2	Argument 1: cannot convert from 'System.Data.DataTable' to 'AppEngineUserAgentLoggerWithXSLXAsset.Design.Book1Sheet1Key'	X:\jsc.svn\examples\javascript\appengine\AppEngineUserAgentLoggerWithXSLXAsset\AppEngineUserAgentLoggerWithXSLXAsset\Program.cs	16	42	AppEngineUserAgentLoggerWithXSLXAsset


            //var z = x.GetVisitHeadersFor(y.Result.visit);

            RewriteToUltraApplication.AsProgram.Launch(typeof(Application));
        }

    }
}
