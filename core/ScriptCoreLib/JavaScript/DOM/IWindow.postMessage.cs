using ScriptCoreLib.Shared;

using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript;

using ScriptCoreLib.JavaScript.DOM.HTML;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Reflection;
using System;

namespace ScriptCoreLib.JavaScript.DOM
{



	partial class IWindow
	{
		public void postMessage(object message, string targetOrigin = "*", params object[] transfer)
		{

		}

		[Script(DefineAsStatic = true)]
		public Task<XElement> postXElementAsync(XElement message, string targetOrigin = "*")
		{
			// X:\jsc.svn\examples\javascript\chrome\apps\ChromeAppletExperiment\ChromeAppletExperiment\Application.cs

			var outdata = message.ToString();
			var x = new TaskCompletionSource<XElement>();

			this.postMessageAsync(outdata, targetOrigin).ContinueWith(
				t =>
				{
					var indata = (string)t.Result.data;
					var inxml = XElement.Parse(indata);
					x.SetResult(inxml);
				}
			);
			return x.Task;
		}

		[Script(DefineAsStatic = true)]
		public Task<MessageEvent> postMessageAsync(object message, string targetOrigin = "*")
		{
			var x = new TaskCompletionSource<MessageEvent>();

			var c = new MessageChannel();

			c.port1.onmessage +=
				e =>
				{
					x.SetResult(e);
				};

			c.port1.start();
			c.port2.start();

			this.postMessage(message,
				targetOrigin,
				c.port2
			);


			return x.Task;
		}

		[Script(DefineAsStatic = true)]
		public Task<MessageEvent<TData>> postMessageAsync<TData>()
		{
			var x = new TaskCompletionSource<MessageEvent<TData>>();

			var c = new MessageChannel();

			c.port1.onmessage +=
				e =>
				{
					// bypass type safety
					x.SetResult((MessageEvent<TData>)(object)e);
				};

			c.port1.start();
			c.port2.start();

			this.postMessage(null,
				targetOrigin: "*",
				transfer: c.port2
			);


			return x.Task;
		}

	}
}
