using jsc.meta.Commands.Rewrite.RewriteToUltraApplication;
using ScriptCoreLib.Desktop.Forms.Extensions;
using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace Diagnostics.DownloadMachine
{
    /// <summary>
    /// You can debug your application by hitting F5.
    /// </summary>
    internal static class Program
    {
        public static void Main(string[] args)
        {
            //var path = "/";
            while (true)
            {
                DownloadPath("jscdatagriddemo.sourceforge.net", 80, "/");
                //DownloadPath("/jsc");
                //DownloadPath("/ScriptCoreLib.dll.js");
                //DownloadPath("/ScriptCoreLib.Query.dll.js");
                //DownloadPath("/ScriptCoreLib.Drawing.dll.js");
                //DownloadPath("/ScriptCoreLib.Windows.Forms.dll.js");
                //DownloadPath("/ScriptCoreLib.Avalon.dll.js");
                //DownloadPath("/SQLiteWithDataGridView.Application.exe.js");

                //http://127.0.0.1:21124/assets/AvalonPromotionBrandIntro/white_jsc.png

                //DownloadPath("jscdatagriddemo.sourceforge.net", 80, "/Application.0.js");

                //DownloadPath("127.0.0.1", 21124, "/assets/ScriptCoreLib.Avalon/transparent.cur");
                //Console.ReadKey(true);


                //DownloadPath("jscdatagriddemo.sourceforge.net", 80, "/assets/ScriptCoreLib.Avalon/transparent.cur");
                //DownloadPath("jscdatagriddemo.sourceforge.net",80, "/assets/ScriptCoreLib/jsc.ico");
                DownloadPath("jscdatagriddemo.sourceforge.net", 80, "/assets/ScriptCoreLib.Windows.Forms/FormResizeGrip.png");

                Console.WriteLine("all done");
                Console.ReadKey(true);
            }
        }

        private static void DownloadPath(string Host, int port, string path)
        {
            var t = new Stopwatch();
            t.Start();

            #region WriteLine
            var dt = t.Elapsed;
            var Color = ConsoleColor.Yellow;
            var limiter = 0;
            Action<string> WriteLine =
                rx =>
                {
                    if (limiter > 16)
                    {
                        limiter++;
                        if (limiter % 32 == 0)
                            Console.Write(".");
                        return;
                    }

                    if (t.Elapsed - dt < TimeSpan.FromSeconds(1))
                        Console.ForegroundColor = ConsoleColor.Cyan;
                    else
                        Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(t.Elapsed + " ");
                    if (rx == "")
                        Color = ConsoleColor.Green;

                    Console.ForegroundColor = Color;

                    if (rx.Length > 64)
                        rx = rx.Substring(0, 64) + " ... " + rx.Length;

                    if (Color == ConsoleColor.Green)
                        limiter++;

                    Console.WriteLine(rx);

                    dt = t.Elapsed;
                };
            #endregion


            Console.WriteLine();
            Console.WriteLine("downloading... " + Host + ":" + port + path);
            var tc = new TcpClient();

            tc.Connect(Host, port);

            var s = tc.GetStream();
            var w = new StreamWriter(s);

            w.WriteLine("GET " + path + " HTTP/1.1");
            w.WriteLine("Host: " + Host);
            w.WriteLine("Connection:keep-alive");



            w.WriteLine("Accept-Charset:windows-1257,utf-8;q=0.7,*;q=0.3");
            w.WriteLine("Accept-Encoding:gzip,deflate,sdch");
            w.WriteLine("Accept:text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8");
            w.WriteLine("User-Agent:Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.1 (KHTML, like Gecko) Chrome/21.0.1180.89 Safari/537.1");



            w.WriteLine();
            w.Flush();

            var r = new StreamReader(s);


            {
                string rx;
                while ((rx = r.ReadLine()) != null)
                    WriteLine(rx);

            }

            t.Stop();

            limiter = 0;
            WriteLine("downloading... done");
        }


        public static void __WebClient(string[] args)
        {

            var w = new WebClient();
            var t = new Stopwatch();
            t.Start();
            Console.WriteLine("downloading...");

            //var ww = (HttpWebRequest)HttpWebRequest.Create("http://jscdatagriddemo.sourceforge.net/");


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

            // Accept-Encoding:gzip,deflate,sdch
            //w.Headers[HttpRequestHeader.AcceptEncoding] = "gzip,deflate,sdch";
            // Keep-Alive and Close may not be set using this property.
            //w.Headers[HttpRequestHeader.Connection] = "keep-alive";
            //ww.Headers[HttpRequestHeader.Connection] = "keep-alive";
            //ww.KeepAlive = false;
            //ww.Timeout = 3000;

            //w.Headers[HttpRequestHeader.CacheControl] = "max-age=0";

            // The operation has timed out
            //var cc = ww.GetResponse();
            var url = "http://jscdatagriddemo.sourceforge.net/jsc";
            var c = w.DownloadData(url);
            t.Stop();
            // downloading... done 00:00:02.1165726
            // downloading... done 00:00:01.2399908
            #region downloading... done 00:00:01.0358322
            /*
Pragma: no-cache
X-Varnish: 100452485
Age: 0
Connection: keep-alive
Content-Length: 1304
Cache-Control: no-store, no-cache, must-revalidate, post-check=0, pre-check=0
Content-Type: text/html
Date: Tue, 25 Sep 2012 07:35:58 GMT
Expires: Thu, 19 Nov 1981 08:52:00 GMT
Set-Cookie: PHPSESSID=ei9v8b1dg3qb708q8gbuerom74; path=/
Server: Apache/2.2.3 (CentOS)
Via: 1.1 varnish
*/
            #endregion

            #region downloading... done 00:00:01.1801938
            /*
Pragma: no-cache
X-Varnish: 11862445
Age: 0
Connection: keep-alive
Content-Length: 1304
Cache-Control: no-store, no-cache, must-revalidate, post-check=0, pre-check=0
Content-Type: text/html
Date: Tue, 25 Sep 2012 07:42:05 GMT
Expires: Thu, 19 Nov 1981 08:52:00 GMT
Set-Cookie: PHPSESSID=vfl8eq37jcprsgstc68u9kf1r2; path=/
Server: Apache/2.2.3 (CentOS)
Via: 1.1 varnish
{ Length = 1304 }
*/
            #endregion

            #region downloading... done 00:00:27.3616488
            /*
request:
  Host: jscdatagriddemo.sourceforge.net
  Connection: Keep-Alive
response:
  Pragma: no-cache
  X-Varnish: 1344865607
  Age: 0
  Connection: keep-alive
  Content-Length: 1304
  Cache-Control: no-store, no-cache, must-revalidate, post-check=0, pre-check=0
  Content-Type: text/html
  Date: Tue, 25 Sep 2012 07:56:21 GMT
  Expires: Thu, 19 Nov 1981 08:52:00 GMT
  Set-Cookie: PHPSESSID=vgo7shihslroq6r72ks05d0f50; path=/
  Server: Apache/2.2.3 (CentOS)
  Via: 1.1 varnish
{ ContentLength = 1304 }
*/
            #endregion

            #region downloading... done 00:00:28.1113841
            /*
request:
response:
  Pragma: no-cache
  X-Varnish: 100525803
  Age: 0
  Connection: keep-alive
  Content-Length: 1304
  Cache-Control: no-store, no-cache, must-revalidate, post-check=0, pre-check=0
  Content-Type: text/html
  Date: Tue, 25 Sep 2012 08:02:36 GMT
  Expires: Thu, 19 Nov 1981 08:52:00 GMT
  Set-Cookie: PHPSESSID=kspmbubt794lkqbktmijfoc7i7; path=/
  Server: Apache/2.2.3 (CentOS)
  Via: 1.1 varnish
{ Length = 1304 }
*/
            #endregion

            Console.WriteLine();
            Console.WriteLine("#region downloading... done " + t.Elapsed + " " + url);
            Console.WriteLine("/*");

            Console.WriteLine("request:");

            foreach (var item in w.Headers.AllKeys)
            {
                Console.WriteLine("  " + item + ": " + w.Headers.Get(item));
            }

            Console.WriteLine("response:");

            foreach (var item in w.ResponseHeaders.AllKeys)
            {
                Console.WriteLine("  " + item + ": " + w.ResponseHeaders.Get(item));
            }

            Console.WriteLine(new { c.Length });

            Console.WriteLine("*/");
            Console.WriteLine("#endregion");
            Console.WriteLine();


            Console.ReadKey(true);
        }


        public static void __HttpWebRequest(string[] args)
        {

            //var w = new WebClient();
            var t = new Stopwatch();
            t.Start();
            Console.WriteLine("downloading...");

            var ww = (HttpWebRequest)HttpWebRequest.Create("http://jscdatagriddemo.sourceforge.net/");


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

            // Accept-Encoding:gzip,deflate,sdch
            //w.Headers[HttpRequestHeader.AcceptEncoding] = "gzip,deflate,sdch";
            // Keep-Alive and Close may not be set using this property.
            //w.Headers[HttpRequestHeader.Connection] = "keep-alive";
            //ww.Headers[HttpRequestHeader.Connection] = "keep-alive";
            ww.KeepAlive = false;
            ww.Timeout = 3000;

            //w.Headers[HttpRequestHeader.CacheControl] = "max-age=0";

            // The operation has timed out
            var cc = ww.GetResponse();
            //var c = w.DownloadData("http://jscdatagriddemo.sourceforge.net/");
            t.Stop();
            // downloading... done 00:00:02.1165726
            // downloading... done 00:00:01.2399908
            #region downloading... done 00:00:01.0358322
            /*
Pragma: no-cache
X-Varnish: 100452485
Age: 0
Connection: keep-alive
Content-Length: 1304
Cache-Control: no-store, no-cache, must-revalidate, post-check=0, pre-check=0
Content-Type: text/html
Date: Tue, 25 Sep 2012 07:35:58 GMT
Expires: Thu, 19 Nov 1981 08:52:00 GMT
Set-Cookie: PHPSESSID=ei9v8b1dg3qb708q8gbuerom74; path=/
Server: Apache/2.2.3 (CentOS)
Via: 1.1 varnish
*/
            #endregion

            #region downloading... done 00:00:01.1801938
            /*
Pragma: no-cache
X-Varnish: 11862445
Age: 0
Connection: keep-alive
Content-Length: 1304
Cache-Control: no-store, no-cache, must-revalidate, post-check=0, pre-check=0
Content-Type: text/html
Date: Tue, 25 Sep 2012 07:42:05 GMT
Expires: Thu, 19 Nov 1981 08:52:00 GMT
Set-Cookie: PHPSESSID=vfl8eq37jcprsgstc68u9kf1r2; path=/
Server: Apache/2.2.3 (CentOS)
Via: 1.1 varnish
{ Length = 1304 }
*/
            #endregion

            #region downloading... done 00:00:27.3616488
            /*
request:
  Host: jscdatagriddemo.sourceforge.net
  Connection: Keep-Alive
response:
  Pragma: no-cache
  X-Varnish: 1344865607
  Age: 0
  Connection: keep-alive
  Content-Length: 1304
  Cache-Control: no-store, no-cache, must-revalidate, post-check=0, pre-check=0
  Content-Type: text/html
  Date: Tue, 25 Sep 2012 07:56:21 GMT
  Expires: Thu, 19 Nov 1981 08:52:00 GMT
  Set-Cookie: PHPSESSID=vgo7shihslroq6r72ks05d0f50; path=/
  Server: Apache/2.2.3 (CentOS)
  Via: 1.1 varnish
{ ContentLength = 1304 }
*/
            #endregion

            Console.WriteLine();
            Console.WriteLine("#region downloading... done " + t.Elapsed);
            Console.WriteLine("/*");

            Console.WriteLine("request:");

            foreach (var item in ww.Headers.AllKeys)
            {
                Console.WriteLine("  " + item + ": " + ww.Headers.Get(item));
            }

            Console.WriteLine("response:");

            foreach (var item in cc.Headers.AllKeys)
            {
                Console.WriteLine("  " + item + ": " + cc.Headers.Get(item));
            }

            Console.WriteLine(new { cc.ContentLength });

            Console.WriteLine("*/");
            Console.WriteLine("#endregion");
            Console.WriteLine();


            Console.ReadKey(true);
        }

    }
}
