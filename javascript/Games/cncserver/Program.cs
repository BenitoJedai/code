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
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;

namespace cncserver
{
    using ScriptCoreLib.Shared;
    using cnc.source.shared;

    public static class Extensions
    {
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static void Use(this ConsoleColor c, Action e)
        {
            var x = Console.ForegroundColor;

            Console.ForegroundColor = c;

            e();

            Console.ForegroundColor = x;
        }

        public static string ToMD5String(this FileInfo f)
        {
            StringBuilder sb =new StringBuilder();
            using (FileStream fs = f.OpenRead())
            {
                MD5 md5 =new MD5CryptoServiceProvider();
                byte[] hash = md5.ComputeHash(fs);
                fs.Close();
                foreach (byte hex in hash)
                    sb.Append(hex.ToString("x2"));

            }
            return sb.ToString();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var m = new ServerTransport<Message[]>(new FileInfo(@"x:\json.txt").OpenRead());

            //using (var sf = new FileInfo(@"x:\json.out.txt").OpenWrite())
            //    m.WriteTo(sf);

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


        static ServerLobby Lobby = new ServerLobby();


        private static void Respond(HttpListenerContext c)
        {
            if (c.Request.HttpMethod == "POST")
            {
                Lobby.Invoke(c);

                return;
            }

            var q = c.Request.Url.PathAndQuery;

            if (q == "/")
                q += cnc.source.js.Controls.DemoControl.Alias + ".htm";

            var f = new FileInfo("web" + q);

            if (f.Exists)
            {
                switch (f.Extension)
                {
                    case "png": c.Response.AddHeader("Content-Type", "image/png"); break;
                    case "jpg": c.Response.AddHeader("Content-Type", "image/jpeg"); break;
                    case "js": c.Response.AddHeader("Content-Type", "application/x-javascript"); break;
                    case "css": c.Response.AddHeader("Content-Type", "css"); break;
                }


                SendFileWithCache(c, f);
            }
            else
            {
                c.Response.StatusCode = 404;
                c.Response.Close();
            }

        }

        private static void SendFileWithCache(HttpListenerContext c, FileInfo f)
        {
            c.Response.AddHeader("Cache-Control", "public, s-maxage=120");
            //c.Response.AddHeader("Expires", "Wed, 24 Nov 2009 11:55:45 GMT");

            var etag = f.ToMD5String();

            var m = c.Request.Headers["If-None-Match"];

            if (m != null && m.IndexOf(etag) > -1)
            {
                c.Response.StatusCode = 304;
                c.Response.Close();

                ConsoleColor.DarkYellow.Use(
                    delegate
                    {
                        Console.WriteLine("cached: " + f.Length.ToString().PadLeft(10) + " bytes " + c.Request.Url.PathAndQuery);
                    }
                );

                return;
            }

            c.Response.AddHeader("ETag", '"' + etag + '"');

            SendFile(c, f);
        }
        
        private static void SendFile(HttpListenerContext c, FileInfo f)
        {
            //if (f.Name.EndsWith(".js"))
            //{
            //    var fp = new FileInfo(f.FullName + ".packed.js");

            //    if (fp.Exists)
            //        f = fp;
            //}

            ConsoleColor.Yellow.Use(
                    delegate
                    {
                        Console.WriteLine("loading: " + f.Length.ToString().PadLeft(10) + " bytes " + c.Request.Url.PathAndQuery);
                    }
            );

            
            using (var from = f.OpenRead())
            {
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
}
