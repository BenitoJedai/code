using ScriptCoreLib.JavaScript.DOM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.Extensions
{
    [Script]
    public static class MessagePortExtensions
    {
        public static void postMessage(this Worker worker, object message, params Action<MessageEvent>[] yield)
        {
            worker.postMessage(message,
                yield.Select(
                    y =>
                    {
                        var c = new MessageChannel();

                        c.port1.onmessage += y;

                        c.port1.start();
                        c.port2.start();

                        return c.port2;
                    }
                ).ToArray()
            );
        }

        public static void postMessage(this MessagePort x, object message, params Action<MessageEvent>[] yield)
        {
            x.postMessage(message,
                yield.Select(
                    y =>
                    {
                        var c = new MessageChannel();

                        c.port1.onmessage += y;

                        c.port1.start();
                        c.port2.start();

                        return c.port2;
                    }
                ).ToArray()
            );
        }
    }
}
