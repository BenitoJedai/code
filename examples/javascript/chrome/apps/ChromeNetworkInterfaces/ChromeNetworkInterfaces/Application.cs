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
using ChromeNetworkInterfaces;
using ChromeNetworkInterfaces.Design;
using ChromeNetworkInterfaces.HTML.Pages;

namespace ChromeNetworkInterfaces
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


			//			DeclareMethods {
			//				SourceMethod = Int32 < 0126 > ldarg.0.try(< MoveNext > 06000022, System.Runtime.CompilerServices.TaskAwaiter`1[chrome.NetworkInterface[]] ByRef, << -ctor > b__0_2 > d ByRef) }
			//script: error JSC1000: if block not detected correctly, opcode was { Branch = [0x0018] blt.s + 0 - 2{[0x0006]
			//		ldfld      +1 -1{[0x0001]
			//		ldfld      +1 -1{[0x0000]
			//		ldarg.0    +1 -0}
			//} } {[0x0017]
			//conv.i4    +1 -1{[0x0016]
			//ldlen      +1 -1{[0x0011]
			//ldfld      +1 -1{[0x000c]
			//ldfld      +1 -1{[0x000b]
			//ldarg.0    +1 -0} } } } } , Location =
			// assembly: W:\ChromeNetworkInterfaces.Application.exe
			// type: ChromeNetworkInterfaces.Application+<>c+<<-ctor>b__0_2>d+<MoveNext>06000022, ChromeNetworkInterfaces.Application, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
			// offset: 0x0018
			//  method:Int32<0126> ldarg.0.try(<MoveNext>06000022, System.Runtime.CompilerServices.TaskAwaiter`1[chrome.NetworkInterface[]] ByRef, <<-ctor>b__0_2>d ByRef) }
			//*** Compiler cannot continue... press enter to quit.




			new { }.With(
				async delegate
				{
					// X:\jsc.svn\examples\java\hybrid\JVMCLRNIC\JVMCLRNIC\Program.cs
					// clr does not have it async. 
					var n = await chrome.socket.getNetworkList();

					new IHTMLPre { new { n.Length } }.AttachToDocument();

					foreach (var item in n)
					{
						new IHTMLPre { new { item.name, item.address } }.AttachToDocument();
					}
				}
			);
		}

	}
}
