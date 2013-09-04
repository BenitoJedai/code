using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScriptCoreLib.JavaScript.Extensions;

namespace ScriptCoreLib.JavaScript.DOM
{
    public static class WorkerAsyncExtensions
    {

        public static Task<MessageEvent> postMessageAsync(this Worker worker, object message)
        {

            var y = new TaskCompletionSource<MessageEvent>();

            worker.postMessage(
                message,
                e =>
                {
                    y.SetResult(
                        e
                    );
                }
            );

            return y.Task;
        }

    }
}
