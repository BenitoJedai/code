﻿using System;
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
using System.Threading;

namespace cncserver
{
    using ScriptCoreLib.Shared;
    using ScriptCoreLib.Shared.Drawing;
    using cnc.source.shared;




    public class ServerLobby
    {
        public readonly ServerSession DefaultSession = new ServerSession();

        static int ClientIndex = 0;
        static readonly Random ClientRandomizer = new Random();

        public readonly Dictionary<string, ServerSession> Clients = new Dictionary<string, ServerSession>();

        public TimeSpan SessionTimeOut = new TimeSpan(0, 0, 15);
        public TimeSpan SessionTimeOutWarning = new TimeSpan(0, 0, 3);

        public ServerLobby()
        {
            DefaultSession.OnEnterLobby =
                delegate
                {
                    var ClientName = (++ClientIndex) + "#" + string.Format("{0:x8}", ClientRandomizer.Next());


                    Console.WriteLine("new client has entered the lobby: " + ClientName);

                    var ss = new ServerSession { ClientName, Lobby = this };

                    ss.OnTalkToOthers = delegate(string text)
                    {
                        if (text == "?lobby")
                        {
                            ss.ToClient_ShowWhoIsInTheLobby();

                            return;
                        }

                        foreach (var v in ss.OthersInTheLobby())
                        {
                            v.DisplayNotification(ss.ClientName + ": " + text, Color.White);
                        }
                    };


                    Clients[ClientName] = ss;

                    foreach (var v in ss.OthersInTheLobby())
                    {
                        v.DisplayNotification(ClientName + " has joined the lobby", Color.Red);
                    }

                    ss.DisplayNotification("Hello " + ClientName, Color.Green);


                    ss.ToClient_ShowWhoIsInTheLobby();
                    
                    return ClientName;
                };


            new Thread(
                delegate()
                {
                    Console.WriteLine("watching the lobby...");

                    while (true)
                    {
                        Thread.Sleep(1000);


                        ServerTick();
                    }
                }
            ).Start();
        }

        private void ServerTick()
        {
            var tn = DateTime.Now;
            var t = tn - SessionTimeOut;
            var tw = tn - SessionTimeOutWarning;

            foreach (ServerSession v in this.Clients.Values)
            {
                if (v.LastSeen < tw && v.LastLagWarning < v.LastSeen)
                {
                    v.LastLagWarning = tn;

                    Console.WriteLine("{0} seems to be lagging...", v.ClientName);

                    foreach (ServerSession o in v.OthersInTheLobby())
                    {
                        o.DisplayNotification("client {0} is slow " + v.ClientName, Color.Yellow);
                    }
                }
            }

            foreach (ServerSession s in (from i in this.Clients.Values
                                          where i.LastSeen < t
                                          select i).ToArray())
            {

                Console.WriteLine("{0} timed out...", s.ClientName);

                foreach (ServerSession o in s.OthersInTheLobby())
                {
                    o.DisplayNotification("client {0} timed out " + s.ClientName, Color.Red);
                }

                this.Clients.Remove(s.ClientName);
            }
        }

        
        
        public void Invoke(HttpListenerContext c)
        {
            var m = new ServerTransport<Message[]>(c.Request.InputStream);

            ServerSession s = null;

            if (!this.Clients.ContainsKey(m.Descriptor.Description))
            {
                s = DefaultSession;
            }
            else
            {
                s = Clients[m.Descriptor.Description];
                var now = DateTime.Now;

                //Console.Write("*");

                //Console.WriteLine("Client: " + s.ClientName + " client was offline " + new TimeSpan(now.Ticks - s.LastSeen.Ticks).ToString());

                s.LastSeen = now;
            }

            m.Data = s.Invoke(m.Data);

            if (m.Data.Length == 0)
            {
                c.Response.StatusCode = 204;
            }
            else
            {
                m.WriteTo(c.Response.OutputStream);
            }

            c.Response.Close();
        }

        

    }
}
