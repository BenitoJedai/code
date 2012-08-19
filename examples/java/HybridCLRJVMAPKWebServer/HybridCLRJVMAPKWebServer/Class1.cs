using ScriptCoreLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Shared.IO;

namespace HybridCLRJVMAPKWebServer
{
    public delegate void NetworkStreamAction(NetworkStream s);


    public class Class1
    {
        public static void Main(string[] args)
        {
            // CLR wrapper for JVM app that calls CLR part in release build
            // this app shall run on 
            // - CLR
            // - JVM
            // - Android
            // this app shalls be the server for a jsc application
            // previous effort: 
            // "Y:\jsc.svn\examples\java\android\xavalon.net\xavalon.net.sln"
            // "Y:\jsc.svn\examples\java\android\AndroidTcpListenerActivity\AndroidTcpListenerActivity.sln"

            Console.WriteLine(typeof(object).FullName);

            var random = new System.Random();

            // Error 312 (net::ERR_UNSAFE_PORT): Unknown error.
            var port = random.Next(1024, 32000);

            var ipa = Dns.GetHostAddresses("127.0.0.1")[0];

            var cid = 0;

            NetworkStreamAction AtConnection =
                s =>
                {
                    var id = cid++;

                    Console.WriteLine("#" + cid);

                    //log("AtConnection");

                    var r = new SmartStreamReader(s);

                    var HTTP_METHOD_PATH_QUERY = r.ReadLine();

                    Console.WriteLine("#" + cid + HTTP_METHOD_PATH_QUERY);

                    var HTTP_METHOD = HTTP_METHOD_PATH_QUERY.TakeUntilOrEmpty(" ");

                    if (HTTP_METHOD != "POST")
                        if (HTTP_METHOD != "GET")
                        {
                            var m = new MemoryStream();

                            Action<string> WriteLineASCII = (string e) =>
                            {
                                var x = Encoding.ASCII.GetBytes(e + "\r\n");

                                m.Write(x, 0, x.Length);
                            };

                           
                            WriteLineASCII("HTTP/1.1 500 OK");
                            WriteLineASCII("Connection: close");

                            var oa = m.ToArray();

                            s.Write(oa, 0, oa.Length);

                            s.Flush();
                            s.Close();
                        }

                    var HTTP_PATH_QUERY = HTTP_METHOD_PATH_QUERY.SkipUntilOrEmpty(" ").TakeUntilLastOrEmpty(" ");
                    var HTTP_PATH = HTTP_PATH_QUERY.TakeUntilIfAny("?");
                    var HTTP_QUERY = HTTP_PATH_QUERY.SkipUntilOrEmpty("?");

                    var HTTP_HEADERS = new List<string>();

                    var br = r.ReadLine();

                    while (!string.IsNullOrEmpty(br))
                    {
                        HTTP_HEADERS.Add(br);

                        br = r.ReadLine();
                    }


                    var data = r.ReadStreamToEnd();


                    //log("ReadLine done");

                    {
                        var m = new MemoryStream();

                        Action<string> WriteLineASCII = (string e) =>
                        {
                            var x = Encoding.ASCII.GetBytes(e + "\r\n");

                            m.Write(x, 0, x.Length);
                        };

                        WriteLineASCII("HTTP/1.1 200 OK");
                        WriteLineASCII("Content-Type:	text/html; charset=utf-8");
                        //WriteLineASCII("Content-Length: " + data.Length);
                        WriteLineASCII("Connection: close");


                        WriteLineASCII("");
                        WriteLineASCII("");
                        WriteLineASCII("<html>");

                        WriteLineASCII("<body>");


                        WriteLineASCII("<pre style='color: blue;'>" + new { HTTP_METHOD, HTTP_PATH, HTTP_QUERY, data = data.Length } + "</pre>");


                        foreach (var item in HTTP_HEADERS.ToArray())
                        {
                            WriteLineASCII("<code style='color: green;'>" + item + "</code><br />");
                        }

                        WriteLineASCII("<h1 style='color: red;'>Hello world</h2><h3>jsc</h3><pre>" + HTTP_METHOD_PATH_QUERY + "</pre>");

                        WriteLineASCII("<hr />");

                        WriteLineASCII("<form target='_blank' action='?WebMethod=06000048' method='POST'><br /> <img src='http://i.msdn.microsoft.com/deshae98.pubmethod(en-us,VS.90).gif' /> method: <code><a href='?WebMethod=06000048'>Hello</a></code><input type='submit' value='Invoke'  /><br /> &nbsp;&nbsp;&nbsp;&nbsp;<img src='http://i.msdn.microsoft.com/yxcx7skw.pubclass(en-us,VS.90).gif' /> parameter: <code>data</code> = <input type='text'  name='_06000048_data' value='' /><br /> &nbsp;&nbsp;&nbsp;&nbsp;<img src='http://i.msdn.microsoft.com/yxcx7skw.pubdelegate(en-us,VS.90).gif' /> parameter: <code>result</code></form>");

                        WriteLineASCII("</body>");

                        WriteLineASCII("</html>");


                        var oa = m.ToArray();

                        s.Write(oa, 0, oa.Length);

                        s.Flush();
                        s.Close();
                    }
                };


            var t = new Thread(
                    delegate()
                    {
                        Console.WriteLine(ipa + ":" + port);

                        var r = new TcpListener(ipa, port);

                        //try
                        //{
                        r.Start();

                        while (true)
                        {
                            //log("AcceptTcpClient");
                            var c = r.AcceptTcpClient();
                            //log("AcceptTcpClient done, GetStream");

                            var s = c.GetStream();
                            //log("AcceptTcpClient done, GetStream done");
                            
                            
                            AtConnection(s);

                            //new Thread(
                            //    delegate()
                            //    {
                            //        //log("before AtConnection");
                            //        AtConnection(s);
                            //    }
                            //)
                            //{
                            //    IsBackground = true,
                            //}.Start();
                        }

                        //}
                        //catch
                        //{
                        //    log("AcceptTcpClient error!");

                        //    throw;
                        //}


                    }
                )
            {
                IsBackground = true,
            };

            t.Start();

            t.Join();

            //CLRProgram.CLRMain();
        }
    }

    //[SwitchToCLRContext]
    //static class CLRProgram
    //{
    //    [STAThread]
    //    public static void CLRMain()
    //    {
    //        Console.WriteLine(typeof(object).FullName);
    //    }
    //}
}
