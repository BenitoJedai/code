using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TestHopFromIFrame;
using TestHopFromIFrame.Design;
using TestHopFromIFrame.HTML.Pages;

namespace TestHopFromIFrame
{
	public struct HopToIFrame : System.Runtime.CompilerServices.INotifyCompletion
	{
		// basically we have to hibernate the current state to resume
		public HopToIFrame GetAwaiter() { return this; }
		public bool IsCompleted { get { return false; } }

		public static Action<HopToIFrame, Action> VirtualOnCompleted;
		public void OnCompleted(Action continuation) { VirtualOnCompleted(this, continuation); }

		public void GetResult() { }


		public IHTMLIFrame frame;
		public static explicit operator HopToIFrame(IHTMLIFrame frame)
		{
			return new HopToIFrame { frame = frame };
		}
	}

	/// <summary>
	/// Your client side code running inside a web browser as JavaScript.
	/// </summary>
	public sealed class Application : ApplicationWebService
	{
		public Application(IApp page) { vctor(page); }

		static Action<IApp> vctor;

		static Application()
		{
			new IHTMLPre {
				new { Native.document.currentScript.src }
			}.AttachToDocument();


			HopToIFrame.VirtualOnCompleted = (that, continuation) =>
			{
				Console.WriteLine("enter VirtualOnCompleted..");

			};

			// fsharpy look
			vctor = (IApp page) =>
			{
				// {{ href = blob:https%3A//192.168.1.196%3A27831/bafa8242-82bd-44ef-8581-9f76f909cd86 }}

				new IHTMLPre {
					new { Native.document.location.href,
					}

				}.AttachToDocument();

				if (Native.window.parent != Native.window)
				{
					// inside iframe

					new IHTMLPre {
						"inside iframe"
					}.AttachToDocument();

					return;
				}

				var codetask = new WebClient().DownloadStringTaskAsync(
						 new Uri(Worker.ScriptApplicationSource, UriKind.Relative)
				);

				new IHTMLButton { "click to switch" }.AttachToDocument().onclick += async delegate
				{
					Native.body.style.backgroundColor = "yellow";

					// can we compile shaders in frames in parallel?

					var code = await codetask;

					var aFileParts = new[] { code };
					var oMyBlob = new Blob(aFileParts, new { type = "text/javascript" });
					//var url = oMyBlob.ToObjectURL();
					var url = URL.createObjectURL(oMyBlob);

					var frame = new IHTMLIFrame {
						new XElement("script", new XAttribute("src", url), " ")
					}.AttachToDocument();

					// can we our code in that blank document?

					// not fired for blank?
					await frame.async.onload;

					////var x = frame.ownerDocument.createElement(IHTMLElement.HTMLElementEnum.button);
					////x.AttachTo(frame.ownerDocument.documentElement);

					Native.body.style.backgroundColor = "cyan";


					await (HopToIFrame)frame;

					var f = new IHTMLButton { "in the frame! click to notify parent" }.AttachToDocument();

					f.onclick += async delegate
					{
						// can we jump back?
						// can we ask how many frames are there?
						// can we jump in any other frame?
					};


				};

			};

		}

	}
}
