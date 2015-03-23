using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AndroidNFCEvents
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
	[DesignerCategory("code")]
    public sealed partial class ApplicationWebService : Component,
        IApplicationWebService_poll_onnfc
    {
        // jsc could upgrade this method to use EventSource?
        // async yield?
        public Task<string> poll_onnfc(string last_id, Action<XElement> yield)
        {
#if DEBUG
            Thread.Sleep(500);

            return Task.FromResult(last_id);
#else

            var c = new TaskCompletionSource<string>();

            ApplicationWebService_poll_onnfc.poll_onnfc(
                last_id, yield, c.SetResult
            );

            return c.Task;
#endif
        }
    }

    public interface IApplicationWebService_poll_onnfc
    {
        Task<string> poll_onnfc(string last_id, Action<XElement> yield);
    }
}
