using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.Linq;
using System.Xml.Linq;
using AndroidNuGetSQLiteActivity;
using ScriptCoreLib.Ultra.WebService;

namespace PHPNuGetSQLite
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public sealed class ApplicationWebService
    {
        /// <summary>
        /// This Method is a javascript callable method.
        /// </summary>
        /// <param name="e">A parameter from javascript.</param>
        /// <param name="y">A callback to javascript.</param>
        public void WebMethod2(string e, Action<string, string> y)
        {
            MyDatabase.Write();
            var db = MyDatabase.Read("-");

            // Send it back to the caller.
            y(e, db);
        }

        public void Handler(WebServiceHandler h)
        {
            bool debug = false;

            MyDatabase.Write();

            string contentRead = "-";

            contentRead = MyDatabase.Read(contentRead);

            if(debug)
                Console.WriteLine("PHP MySQL Output Begin");


            if (contentRead != null)
                Console.WriteLine(contentRead);
            else
                Console.WriteLine("empty.");


            if(debug)
                Console.WriteLine("PHP MySQL Output End");

        }
    }
}
