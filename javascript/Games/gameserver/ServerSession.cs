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

namespace cncserver
{
    using ScriptCoreLib.Shared;
    using ScriptCoreLib.Shared.Drawing;
    using cnc.source.shared;



    public class HttpListenerRequestInfo
    {
        public IPEndPoint RemoteEndPoint;
        public string UserAgent;

        public static implicit operator HttpListenerRequestInfo(HttpListenerRequest e)
        {
            return new HttpListenerRequestInfo {
                e.RemoteEndPoint,
                e.UserAgent
            };
        }
    }

    public partial class ServerSession : Message.IServer
    {

        public string ClientName;

        public ServerLobby Lobby;

        public DateTime LastLagWarning;
        public DateTime LastSeen = DateTime.Now;
        public readonly DateTime FirstSeen = DateTime.Now;

        public HttpListenerRequestInfo LastRequest;

        public string MethodA(string A, string B)
        {
            return A + B;
        }

        public IEnumerable<ServerSession> OthersInTheLobby()
        {
            if (Lobby == null) yield break;

            foreach (var v in Lobby.Clients.Values)
            {
                if (v == this)
                    continue;

                yield return v;
            }
        }

        public void CreateExplosionAt(int x, int y)
        {

            foreach (ServerSession c in OthersInTheLobby())
            {
                var to = c;

                Console.WriteLine("server tries to send msg to others: " + to.ClientName);


                to.CreateExplosionByServer(x, y, "yoyo",
                    delegate
                    {
                        Console.WriteLine("confirmed server event: " + ClientName + " -> " + to.ClientName);
                    }
                );
            }
        }

        #region EnterLobby
        public Func<string> OnEnterLobby;

        public string EnterLobby()
        {
            return OnEnterLobby();
        }
        #endregion


        public EventHandler<string> OnTalkToOthers;



        public void TalkToOthers(string text)
        {
            OnTalkToOthers(text);
        }


        internal void ToClient_ShowWhoIsInTheLobby()
        {
            var o = this.OthersInTheLobby().ToArray();

            if (o.Length == 0)
            {
                this.DisplayNotification("You are the first or the last, but alone you are.", Color.Green);

                return;
            }

            var others = "";

            for (int i = 0; i < o.Length; i++)
            {
                if (i > 0)
                    others += ", ";

                others += o[i].ClientName;
            }

            this.DisplayNotification("Currently in the lobby: " + others, Color.Green);
        }

  
        public HttpListenerContext CurrentContext;

        public Message[] Invoke(Message[] m, HttpListenerContext c)
        {
            Message[] v = null;
            lock (this)
            {
                CurrentContext = c;

                v = this.Invoke(m);

                CurrentContext = null;
            }
            return v;
        }
    }

}
