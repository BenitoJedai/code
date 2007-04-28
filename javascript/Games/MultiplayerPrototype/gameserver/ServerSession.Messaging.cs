using System;
using System.Threading;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Query;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Reflection;

namespace cncserver
{
    using ScriptCoreLib.Shared;

    using gameclient.source.shared;


    static partial class Extensions
    {

    }



    public partial class ServerSession : Message.ServerToClient
    {
        readonly List<AsyncProxy.Info> Queue = new List<AsyncProxy.Info>();

        void RemoveToClientPending(Message m)
        {
            foreach (AsyncProxy.Info v in Queue.ToArray())
            {
                if (v.IsPending)
                    if (v.m.ToClientMessageId == m.ToClientMessageId)
                    {
                        if (v.done != null)
                            v.done(m);

                        Queue.Remove(v);
                    }
            }
        }

        Message[] MarkPending()
        {
            var a = new List<Message>();

            foreach (AsyncProxy.Info v in Queue.ToArray())
            {
                if (!v.IsPending)
                {
                    a.Add(v.m);

                    v.IsPending = true;
                }
            }

            return a.ToArray();
        }

        int MessageId = 1;


        protected override void Send(Message m, EventHandler<Message> done)
        {
            MessageId++;

            var x = new AsyncProxy.Info { m = m, done = done };

            x.m.ToClientMessageId = MessageId;

            //Console.WriteLine("sending message with id : " + x.m.ToClientMessageId + " to " + this.ClientName);

            Queue.Add(x);
        }


        public Message[] Invoke(Message[] e)
        {
            var a = new List<Message>();

            foreach (Message v in e)
            {
                if (v.ToServerMessageId > 0)
                {
                    this.Invoke(v);

                    // going back
                    a.Add(v);
                }
                else if (v.ToClientMessageId > 0)
                {
                    this.RemoveToClientPending(v);

                    // invoke smth
                }
            }

            var r = this.MarkPending();

            if (r.Length > 0)
            {
                //Console.WriteLine(this.ClientName + " will recieve " + r.Length + " server messages");

                a.AddRange(r);
            }

            return a.ToArray();
        }

        void Invoke(Message e)
        {
            var s = this;

            var x = from i in typeof(Message.IServer).GetMethods()
                    let allfields = e.GetType().GetFields()
                    let j = allfields.Single(xx => xx.Name == i.Name)
                    let v = j.GetValue(e)
                    where v != null
                    select new {i, j, v};

            foreach (var z in x)
            {
                MethodInfo zi = z.i;

                var xvt = z.v.GetType();

                var pinfo = zi.GetParameters().Select(i => xvt.GetField(i.Name).GetValue(z.v)).ToArray();

                ConsoleColor.DarkCyan.Use(
                    delegate
                    {
                        Console.WriteLine("invoke: " + zi.Name);
                    }
                );

                object retval = null;

                try
                {
                    retval = zi.Invoke(s, pinfo);
                }
                catch
                {
                    throw;
                }

                
                var xrt = xvt.GetField(AsyncProxy.ReturnValue);
                if (xrt != null)
                    xrt.SetValue(z.v, retval);
            }
        }


    }

}
