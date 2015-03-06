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
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ChromeTCPServerAsync;
using ChromeTCPServerAsync.Design;
using ChromeTCPServerAsync.HTML.Pages;
using chrome;
using ScriptCoreLib.JavaScript.WebGL;
using System.Net.Sockets;
using xchrome.BCLImplementation.System.Net.Sockets;

namespace ChromeTCPServerAsync
{
	/// <summary>
	/// Your client side code running inside a web browser as JavaScript.
	/// </summary>
	public sealed class Application : ApplicationWebService
	{
		/// <summary>
		/// This is a javascript application.
		/// </summary>
		/// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
		public Application(IApp page)
		{
			#region += Launched chrome.app.window
			dynamic self = Native.self;
			dynamic self_chrome = self.chrome;
			object self_chrome_socket = self_chrome.socket;

			if (self_chrome_socket != null)
			{
				if (!(Native.window.opener == null && Native.window.parent == Native.window.self))
				{
					Console.WriteLine("chrome.app.window.create, is that you?");

					// pass thru
				}
				else
				{
					// should jsc send a copresence udp message?
					chrome.runtime.UpdateAvailable += delegate
					{
						new chrome.Notification(title: "UpdateAvailable");

					};

					chrome.app.runtime.Launched += async delegate
					{
						// 0:12094ms chrome.app.window.create {{ href = chrome-extension://aemlnmcokphbneegoefdckonejmknohh/_generated_background_page.html }}
						Console.WriteLine("chrome.app.window.create " + new { Native.document.location.href });

						new chrome.Notification(title: "ChromeNetworkInterfaces");

						var xappwindow = await chrome.app.window.create(
							   Native.document.location.pathname, options: null
						);

						//xappwindow.setAlwaysOnTop

						xappwindow.show();

						await xappwindow.contentWindow.async.onload;

						Console.WriteLine("chrome.app.window loaded!");
					};


					return;
				}
			}
			#endregion

			// X:\jsc.svn\examples\javascript\chrome\apps\ChromeNetworkInterfaces\ChromeNetworkInterfaces\Application.cs

			//{{ Length = 4 }}
			//{{ prefixLength = 64, name = {D7020941-742E-4570-93B2-C0372D3D870F}, address = fe80::88c0:f0a:9ccf:cba0 }}
			//{{ prefixLength = 24, name = {D7020941-742E-4570-93B2-C0372D3D870F}, address = 192.168.43.28 }}
			//{{ prefixLength = 64, name = {A8657A4E-8BFA-41CC-87BE-6847E33E1A81}, address = 2001:0:9d38:6abd:20a6:2815:3f57:d4e3 }}
			//{{ prefixLength = 64, name = {A8657A4E-8BFA-41CC-87BE-6847E33E1A81}, address = fe80::20a6:2815:3f57:d4e3 }}

			new { }.With(
				async delegate
				{
					// http://css-infos.net/property/-webkit-user-select
					// http://caniuse.com/#feat=user-select-none
					//(Native.body.style as dynamic).userSelect = "auto";
					(Native.body.style as dynamic).webkitUserSelect = "auto";
					Native.body.style.overflow = IStyle.OverflowEnum.auto;

					new IHTMLPre
					{
						// {{ sockets = null }}
						//new { (Native.window as dynamic).chrome.sockets }
						// {{ socket = [object Object] }}
						new { (Native.window as dynamic).chrome.socket }
						//new { (Native.window as dynamic).sockets.tcpServer  }
					}.AttachToDocument();

					// https://css-tricks.com/almanac/properties/u/user-select/
					//Native.body.style.setProperty(
					// X:\jsc.svn\examples\java\hybrid\JVMCLRNIC\JVMCLRNIC\Program.cs
					// clr does not have it async. 

					var refresh = new IHTMLButton { "refresh" }.AttachToDocument();
					do
					{
						new IHTMLHorizontalRule { }.AttachToDocument();


						var n = await chrome.socket.getNetworkList();
						var n24 = n.Where(x => x.prefixLength == 24).ToArray();

						new IHTMLPre { new { n24.Length } }.AttachToDocument();

						foreach (var item in n24)
						{
							new IHTMLButton { new { item.prefixLength, item.name, item.address } }.AttachToDocument().onclick += Application_onclick;


						}
					}
					while (await refresh.async.onclick);

				}
			);
		}

		private async void Application_onclick(IEvent<IHTMLButton> btn)
		{
			Message();

			//var l = new TcpListener();
			//l.Start();
			//var lc = l.AcceptTcpClientAsync();

			var ix = await chrome.socket.create("tcp", null);
			var isocket = ix.socketId;
			var host = "0.0.0.0";
			var port = 80;
			new IHTMLPre { "listen..." }.AttachToDocument();
			var listen = await isocket.listen(host, port, backlog: 50);

			// http://www.w3schools.com/tags/att_a_target.asp
			// Can't open same-window link to "http://127.0.0.1/"; try target="_blank".
			new IHTMLAnchor
			{

				target = "_blank",
				href = "http://127.0.0.1:80",
				innerText = "accept.. "
			}.AttachToDocument();

			while (true)
			{
				var accept = await isocket.accept();

				new IHTMLPre { "accept " + new { accept.socketId } }.AttachToDocument();

				yield(accept);
			}

		}

		private static void Message()
		{
			new IHTMLPre { "create... " + typeof(TcpListener) }.AttachToDocument();
			//new IHTMLPre { "create... " + typeof(__TcpListener) }.AttachToDocument();

			// create... TcpListener
		}

		private async void yield(AcceptInfo accept)
		{
			//TcpClient c;
			//c.GetStream().ReadAsync(

			var read = await accept.socketId.read();

			// { read = { resultCode = 370 } } 
			//Console.WriteLine(new { read = new { read.resultCode } });



			var u = new Uint8ClampedArray(read.data, 0, (uint)read.data.byteLength);
			var input = Encoding.UTF8.GetString(u);

			new IHTMLPre { new { input } }.AttachToDocument();

			// http://en.wikipedia.org/wiki/Hypertext_Transfer_Protocol
			var outputString = "HTTP/1.0 200 OK \r\nConnection: close\r\n\r\nhello world\r\n";

			var xx = new Uint8ClampedArray(
				Encoding.UTF8.GetBytes(
					outputString
				)
			);

			//nn.Title = "before headers";
			accept.socketId.write(
				 xx.buffer
			);

			accept.socketId.disconnect();
			accept.socketId.destroy();
		}
	}
}
