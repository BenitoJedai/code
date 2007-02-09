using System;
using System.Threading;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Query;
using System.Xml.XLinq;
using System.Data.DLinq;
using System.Net.Sockets;
using System.Net;
using System.IO;

namespace cncserver
{
    using ScriptCoreLib.Shared;
    using cnc.source.shared;



    class Program
    {
        static void Main(string[] args)
        {
            var m = new ServerTransport<Message>(new FileInfo(@"x:\json.txt").OpenRead());

            // start a server to stream 
            // assets to the client

            var s = new HttpListener();

            s.Prefixes.Add("http://*:80/");

            s.Start();

            Console.WriteLine("server ready!");

            while (true)
            {
                var c = s.GetContext();

                new Thread(
                    delegate()
                    {

                        try
                        {
                            Respond(c);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("error: " + ex.Message);
                        }

                    }
                ).Start();
            }


            s.Stop();



        }

        private static void Respond(HttpListenerContext c)
        {
            if (c.Request.HttpMethod == "POST")
            {
                var m = new ServerTransport<Message>(c.Request.InputStream);

                Console.WriteLine(DateTime.Now + " : " + m.Data.Text);

                c.Response.StatusCode = 204;
                c.Response.Close();

                return;
            }

            var q = c.Request.Url.PathAndQuery;

            if (q == "/")
                q += cnc.source.js.Controls.DemoControl.Alias + ".htm";

            var f = new FileInfo("web" + q);

            if (f.Exists)
            {
                SendFile(c, f);
            }
            else
            {
                var o = new StreamWriter(q);
                o.WriteLine("<p> CnC server 1.0</p>" + DateTime.Now);
                o.WriteLine("<hr />");


                o.WriteLine(c.Request.Url.PathAndQuery);


                o.WriteLine("<hr />");


                o.Flush();
                c.Response.Close();
            }

        }

        private static void SendFile(HttpListenerContext c, FileInfo f)
        {
            if (f.Name.EndsWith(".js"))
            {
                var fp = new FileInfo(f.FullName + ".packed.js");

                if (fp.Exists)
                    f = fp;
            }

            Console.WriteLine("loading: " + f.Length.ToString().PadLeft(10) + " bytes " + f.FullName);


            var from = f.OpenRead();
            var to = c.Response.OutputStream;

            c.Response.ContentLength64 = f.Length;

            var b = new byte[8000];

        next:
            var i = from.Read(b, 0, b.Length);

            if (i > 0)
            {
                to.Write(b, 0, i);

                if (f.Length > b.Length)
                    Console.Write(".");

                goto next;
            }

            if (f.Length > b.Length)
                Console.WriteLine();


            c.Response.Close();
        }
    }
}
