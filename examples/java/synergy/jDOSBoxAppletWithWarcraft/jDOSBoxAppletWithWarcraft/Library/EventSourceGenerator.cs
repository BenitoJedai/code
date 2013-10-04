using ScriptCoreLib.Ultra.WebService;
using ScriptCoreLib.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace AndroidIntegrationToSkyIsland.Library
{
    [Description(@"
The serverside of EventSource. 
Like window messaging.

What if the client was also able to create this instance?
")]
    class EventSourceGenerator
    {
        // Action<ref int, XElement> WebMethod = foo;
        // what if the server can also use delegates set by ctor?
        // what if the client can override such delegates?

        public const string ContentType = "text/event-stream";

        public readonly WebServiceHandler Handler;

        public EventSourceGenerator(WebServiceHandler Handler)
        {
            this.Handler = Handler;

            Handler.Context.Response.ContentType = EventSourceGenerator.ContentType;

            // http://www.whatwg.org/specs/web-apps/current-work/multipage/comms.html
            // its a string actually!

            Handler.Context.Request.Headers["Last-Event-ID"].With(
                 id =>
                 {
                     __id = long.Parse(id);
                 }
             );
        }

        int __retry;

        public int retry
        {
            get
            {
                return __retry;
            }
            set
            {
                __retry = value;

                Handler.Context.Response.Write("retry: " + value + "\n\n");
            }
        }



        long __id;

        public long id
        {
            get
            {
                return __id;
            }
            set
            {
                __id = value;

                Handler.Context.Response.Write("id: " + value + "\n\n");
            }
        }

        public void postMessage(XElement data)
        {
            postMessage(data.ToString());
        }

        public void postMessage(string data)
        {
            this.Handler.Context.Response.Write("data: " + data + "\n\n");
            this.Handler.Context.Response.Flush();
        }


        // what if this instance was considered dynamic?
        public Action<string> this[string name]
        {
            get
            {
                return
                    data =>
                    {
                        this.Handler.Context.Response.Write("event: " + name + "\n");
                        this.Handler.Context.Response.Write("data: " + data + "\n\n");
                        this.Handler.Context.Response.Flush();
                    };
            }
        }
    }
}
