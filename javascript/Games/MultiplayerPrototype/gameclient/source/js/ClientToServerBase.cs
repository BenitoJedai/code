using ScriptCoreLib;
using ScriptCoreLib.Shared;
using ScriptCoreLib.JavaScript.Net;
using ScriptCoreLib.JavaScript.Runtime;

namespace gameclient.source.js
{
    using shared;
    using System.Collections.Generic;

    [Script]
    public class ClientToServerBase : Message.ClientToServer
    {
        public string ClientName = "rpc";


        public Timer PollTimer;

        int PollCount = 1;

        public int MaxConcurrentPolls = 2;

        public ClientToServerBase()
        {
            PollTimer = new Timer();
            PollTimer.StartInterval(500);
            PollTimer.Tick +=
                delegate
                {
                    if (PollCount > MaxConcurrentPolls)
                    {
                        Console.WriteLine(MaxConcurrentPolls + " polls already in progress....");
                        return;
                    }

                    var data = MarkPending();

                    // Console.WriteLine("polling.... sending: " + data.Length);

                    var t = new ClientTansport<Message[]>("");


                    // t.IsVerbose = true;

                    t.BeforeSend += delegate
                    {
                        t.Data = data;
                        t.Descriptor.Description = this.ClientName;

                        PollCount++;
                    };

                    t.Complete += delegate
                    {
                        PollCount--;

                        if (t.Request.IsNoContent)
                        {
                            return;
                        }

                        if (t.Request.IsOffline)
                        {

                            Console.WriteLine("server seems to be offline");

                            this.PollTimer.Stop();

                            return;
                        }

                        if (t.Request.IsOK)
                        {
                            foreach (Message v in t.Data)
                            {
                                if (v.ToServerMessageId > 0)
                                {
                                    RemoveToServerPending(v);
                                }
                                else if (v.ToClientMessageId > 0)
                                {
                                    AsyncInvoke(v);
                                }
                            }

                            t.Data = null;
                        }
                        else
                        {
                            Console.LogError("unknown status: " + t.Request.status);
                        }


                    };

                    t.Send();
                };
        }

        /// <summary>
        /// server called us, so, lets invoke the method we have to and post the replay
        /// </summary>
        /// <param name="v"></param>
        private void AsyncInvoke(Message v)
        {
            var vt = Expando.Of(this).GetFunctions();

            foreach (ExpandoMember vx in Expando.Of(v).GetFields())
            {
                if (!vx.Self.IsNull)
                {
                    foreach (ExpandoMember vm in vt)
                    {
                        if (vx.Name == vm.Name)
                        {
                            AsyncInvoke(vx, vm, v);
                        }
                    }
                }
            }
        }

        private void AsyncInvoke(ExpandoMember vx, ExpandoMember vm, Message m)
        {
            Console.WriteLine("server will call: " + vx.Name);

            var a = new List<object>();

            bool r = false;

            foreach (ExpandoMember v in vx.Self.GetFields())
            {
                if (v.Name == AsyncProxy.ReturnValue)
                    r = true;
                else
                    a.Add(v.Self.To<object>());

            }

            var retval = vm.Invoke(a.ToArray());

            if (r)
            {
                vx.Self.SetMember(AsyncProxy.ReturnValue, retval);
            }

            var x = new AsyncProxy.Info { m = m };

            //Console.WriteLine("sending server-message with id : " + x.m.ToServerMessageId);

            Queue.Add(x);
        }





        readonly List<AsyncProxy.Info> Queue = new List<AsyncProxy.Info>();

        void RemoveToServerPending(Message m)
        {
            foreach (AsyncProxy.Info v in Queue.ToArray())
            {
                if (v.IsPending)
                    if (v.m.ToServerMessageId == m.ToServerMessageId)
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

            var x = new AsyncProxy.Info { m = m, done = done};

            x.m.ToServerMessageId = MessageId;

            Console.WriteLine("sending message with id : " + x.m.ToServerMessageId);


            Queue.Add(x);
        }


    }


}
