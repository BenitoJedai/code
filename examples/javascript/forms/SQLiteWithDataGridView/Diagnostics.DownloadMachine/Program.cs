using jsc.meta.Commands.Rewrite.RewriteToUltraApplication;
using ScriptCoreLib.Desktop.Forms.Extensions;
using System;
using System.Diagnostics;
using System.Net;

namespace Diagnostics.DownloadMachine
{
    /// <summary>
    /// You can debug your application by hitting F5.
    /// </summary>
    internal static class Program
    {
        public static void Main(string[] args)
        {
            var w = new WebClient();
            var t = new Stopwatch();
            t.Start();
            Console.WriteLine("downloading...");

            // The remote name could not be resolved: 'jscdatagriddemo.sourceforge.net'

            //C:\Users\Arvo>nslookup jscdatagriddemo.sourceforge.net
            //DNS request timed out.
            //    timeout was 2 seconds.
            //Server:  UnKnown
            //Address:  192.168.0.1

            //DNS request timed out.
            //    timeout was 2 seconds.
            //DNS request timed out.
            //    timeout was 2 seconds.

            var c = w.DownloadString("http://jscdatagriddemo.sourceforge.net/");
            t.Stop();
            // downloading... done 00:00:02.1165726
            // downloading... done 00:00:01.2399908
            Console.WriteLine("downloading... done " + t.Elapsed);

            foreach (var item in w.ResponseHeaders)
	        {
		         Console.WriteLine(item + ": " + w.ResponseHeaders.Get(item));
	        }

            Console.WriteLine(c);

            Console.ReadKey(true);
        }

    }
}
