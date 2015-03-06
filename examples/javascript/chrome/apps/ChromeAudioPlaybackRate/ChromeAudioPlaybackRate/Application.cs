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
using ChromeAudioPlaybackRate;
using ChromeAudioPlaybackRate.Design;
using ChromeAudioPlaybackRate.HTML.Pages;

namespace ChromeAudioPlaybackRate
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

						new chrome.Notification(title: "Launched2");

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

			new IHTMLButton { "new rooster" }.AttachToDocument().onclick +=
				async delegate
				{
					// https://developer.mozilla.org/en-US/Apps/Build/Audio_and_video_delivery/WebAudio_playbackRate_explained

					// in flash we had to work with samples
					// in chrome we get an api. nice can we loop it ?


					var a = new RoosterAudioExample.HTML.Audio.FromAssets.rooster
					{
					};

					a.load();

					var rate = new IHTMLInput { type = ScriptCoreLib.Shared.HTMLInputTypeEnum.range, min = 0, max = 200 }.AttachToDocument();
					var play = new IHTMLButton { "play" }.AttachToDocument();

					Native.window.onframe += delegate
					{
						// cannot go lower than 0.5 it seems?
						a.playbackRate = Math.Max(0.5, rate.GetDouble() * 0.01);

						play.innerText = "play " + new { a.playbackRate };

					};

					while (await play.async.onclick)
					{
						a.currentTime = 0;

						a.play();
					}



				};
		}

	}
}
