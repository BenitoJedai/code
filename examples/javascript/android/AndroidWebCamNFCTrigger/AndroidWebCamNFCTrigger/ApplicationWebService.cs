using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AndroidWebCamNFCTrigger
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public sealed class ApplicationWebService : Component,
        AndroidNFCEvents.IApplicationWebService_poll_onnfc
    {
        public Task<string> poll_onnfc(string last_id, Action<XElement> yield)
        {
#if DEBUG
            Thread.Sleep(500);

            return Task.FromResult(last_id);
#else

            var c = new TaskCompletionSource<string>();

            AndroidNFCEvents.ApplicationWebService_poll_onnfc.poll_onnfc(
                last_id, yield, c.SetResult
            );

            return c.Task;
#endif
        }
    }
}
