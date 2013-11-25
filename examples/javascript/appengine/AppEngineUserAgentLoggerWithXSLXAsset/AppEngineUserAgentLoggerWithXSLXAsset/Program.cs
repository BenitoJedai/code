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
            //var x = new ApplicationWebService { ScreenWidth = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width };
            //var y = x.Notfiy();

            ////Error	1	The best overloaded method match for 'AppEngineUserAgentLoggerWithXSLXAsset.ApplicationWebService.GetVisitHeadersFor(AppEngineUserAgentLoggerWithXSLXAsset.Design.Book1Sheet1Key)' has some invalid arguments	X:\jsc.svn\examples\javascript\appengine\AppEngineUserAgentLoggerWithXSLXAsset\AppEngineUserAgentLoggerWithXSLXAsset\Program.cs	16	21	AppEngineUserAgentLoggerWithXSLXAsset
            ////Error	2	Argument 1: cannot convert from 'System.Data.DataTable' to 'AppEngineUserAgentLoggerWithXSLXAsset.Design.Book1Sheet1Key'	X:\jsc.svn\examples\javascript\appengine\AppEngineUserAgentLoggerWithXSLXAsset\AppEngineUserAgentLoggerWithXSLXAsset\Program.cs	16	42	AppEngineUserAgentLoggerWithXSLXAsset


            //var z = x.GetVisitHeadersFor(y.Result.visit);

            RewriteToUltraApplication.AsProgram.Launch(typeof(Application));
        }

    }
}
