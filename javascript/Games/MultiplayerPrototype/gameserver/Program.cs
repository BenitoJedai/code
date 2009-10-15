using System;
using System.Threading;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;

namespace cncserver
{
    using ScriptCoreLib.Shared;
    using gameclient.source.shared;


    /// <summary>
    /// allows to get console commands async as events
    /// </summary>
    class ConsoleSession
    {
        static public implicit operator ConsoleSession(Action<string> OnCommand)
        {
            return new ConsoleSession(OnCommand);
        }

        public Action<string> OnCommand;


        public ConsoleSession(Action<string> e)
        {

            OnCommand = e;

            new Thread(
                delegate()
                {
                    while (OnCommand != null)
                    {
                        var s = Console.ReadLine();

                        OnCommand(s);
                    }
                }
            ).Start();
        }
    }




    class ConsoleFunctions
    {
        public Action openbrowser;
        public Action clientreload;
        public Action lobby;
        public Action exit;
        public Action help;
    }

    class Program
    {


        static void Main(string[] args)
        {
            //var m = new ServerTransport<Message[]>(new FileInfo(@"x:\json.txt").OpenRead());

            //using (var sf = new FileInfo(@"x:\json.out.txt").OpenWrite())
            //    m.WriteTo(sf);

            // start a server to stream 
            // assets to the client

            var s = new HttpListener();

            s.Prefixes.Add("http://*:8081/");
            s.Start();

            Console.WriteLine("server ready!");

            ConsoleSession cs = null;

            Action InterruptServer = null;

            ConsoleFunctions commands = null;
            
            commands = new ConsoleFunctions {
                openbrowser = 
                    delegate {
                        System.Diagnostics.Process.Start("http://localhost/");
                    }
                ,
                clientreload =
                    delegate {
                        foreach (ServerSession c in Lobby.Clients.Values)
                        {
                           c.ForceReload();
                           
                        }
                    }
                ,
                lobby =
                    delegate {
                        Console.WriteLine("Currently there are {0} clients.", Lobby.Clients.Count);

                        foreach (ServerSession v in Lobby.Clients.Values)
	                    {
                	        Console.WriteLine("  clientname: " + v.ClientName);
                            Console.WriteLine("   firstseen: " + v.FirstSeen);
                            Console.WriteLine("    lastseen: " + v.LastSeen);
                            Console.WriteLine("    endpoint: " + v.LastRequest.RemoteEndPoint);
                            Console.WriteLine("   useragent: " + v.LastRequest.UserAgent);
                            Console.WriteLine();
	                    }
                    }
                ,
                exit = delegate
                    {
                        Console.WriteLine("server is shutting down (1 sec)...");

                        foreach (ServerSession c in Lobby.Clients.Values)
                        {
                            c.IClient_DisplayNotification("server is shutting down...", 0x00FF00);
                        }

                        cs.OnCommand = null;

                        new Timer(
                            delegate
                            {
                                Lobby.Close();

                                if (InterruptServer != null)
                                    InterruptServer();

                            },  null, 1000, Timeout.Infinite);


                        return;
                    }
                ,
                help = delegate
                {
                    ConsoleColor.White.Use(
                        delegate
                        {
                            Console.WriteLine("Supported commands are:");

                            foreach (var pi in commands.GetType().GetFields())
                                Console.WriteLine("    /" + pi.Name);
                        }
                    );
                }
            };

 

            
            cs = (Action<string>)delegate(string cmd)
            {
                if (cmd == "?")
                {
                    commands.help();

                    return;
                }

                if (cmd.StartsWith("/"))
                {

                    var p = commands.GetType().GetFields().Where(pi => pi.Name.ToLower() == cmd.Substring(1).ToLower()).SingleOrDefault();

                    if (p != null)
                    {
                        ConsoleColor.White.Use(
                            p.GetValue(commands) as Action
                        );

                        return;
                    }
                }

                foreach (ServerSession c in Lobby.Clients.Values)
                {
                    c.IClient_DisplayNotification("server: " + cmd, 0x0000FF);
                }
            };

            commands.help();

            while (true)
            {
                var c = Extensions.ToInterruptableCall<HttpListenerContext>(out InterruptServer, s.GetContext);

                if (c == null)
                    break;

                ThreadPool.QueueUserWorkItem(

                    delegate
                    {

                        try
                        {
                            Respond(c);
                        }
                        catch (Exception ex)
                        {
                            Console.Error.WriteLine("error: " + ex.Message);
                            Console.Error.WriteLine(ex.StackTrace);
                        }

                    }

                );
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
                q += typeof(gameclient.source.js.Controls.DemoControl).Name + ".htm";
            

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
				using (var w = new StreamWriter(c.Response.OutputStream))
					w.WriteLine(f.Name + " not found");
                c.Response.Close();
            }

        }

        //static Dictionary<string, string> CacheList = new Dictionary<string, string>();


        private static void SendFileWithCache(HttpListenerContext c, FileInfo f)
        {
            //c.Response.AddHeader("Cache-Control", "public, s-maxage=120");
            ////c.Response.AddHeader("Expires", "Wed, 24 Nov 2009 11:55:45 GMT");

            //var etag = f.ToMD5String();

            //var m = c.Request.Headers["If-None-Match"];

            //if (m != null && m.IndexOf(etag) > -1)
            //{
            //    c.Response.StatusCode = 304;
            //    c.Response.Close();

            //    ConsoleColor.DarkYellow.Use(
            //        delegate
            //        {
            //            Console.WriteLine("cached: " + f.Length.ToString().PadLeft(10) + " bytes " + c.Request.Url.PathAndQuery);
            //        }
            //    );

            //    return;
            //}

            //c.Response.AddHeader("ETag", '"' + etag + '"');

            SendFile(c, f);
        }

        private static void SendFile(HttpListenerContext c, FileInfo f)
        {
            //if (f.Name.EndsWith(".js"))
            //{
            //    var fp = new FileInfo(f.FullName + ".packed.js");

            //    if (fp.Exists)
            //    {
            //        if (!c.Request.IsLocal)
            //            f = fp;
            //    }
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
