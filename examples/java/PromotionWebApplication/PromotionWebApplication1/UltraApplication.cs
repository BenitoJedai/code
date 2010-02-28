using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using java.applet;
using PromotionWebApplication1.Library;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.Shared.Drawing;
using PromotionWebApplication1.Services;
using PromotionWebApplication1.HTML.Pages.FromAssets;
using ScriptCoreLib.Ultra.Library.Delegates;
namespace PromotionWebApplication1
{


	public sealed class UltraApplication
	{
		public UltraApplication(IHTMLElement e)
		{
			var DefaultTitle = "jsc solutions";


			Native.Document.title = DefaultTitle;

			StringActionAction GetTitleFromServer = new UltraWebService().GetTitleFromServer;



			GetTitleFromServer(
				n => Native.Document.title = n
			);

			var MyPages = new IHTMLDiv
			{

			};

			MyPages.style.overflow = IStyle.OverflowEnum.auto;
			MyPages.style.position = IStyle.PositionEnum.absolute;
			MyPages.style.width = "100%";
			MyPages.style.height = "100%";
			MyPages.AttachToDocument();

			var MyPagesInternal = new IHTMLDiv();

			MyPagesInternal.style.margin = "4em";
			MyPagesInternal.AttachTo(MyPages);

			#region Contact Us
			{
				// using wordpress as CMS are we?
				// http://en.support.wordpress.com/pages/hide-pages/
				// http://en.support.wordpress.com/pages/page-attributes/
				// we will build a snapshot of the site.
				// hidden pages need to be subpages :)


				new About().Container.AttachToDocument();

			}
			#endregion

			#region logo
			{
				if (Native.Document.location.hash == "#/audio")
				{
					Action AtTimer = delegate { };

					(1000 / 15).AtInterval(
						tt =>
						{
							AtTimer();
						}
					);

					new SoundCloudHeader().Container.AttachTo(MyPagesInternal);

					new UltraWebService().SoundCloudTracksDownload(
						ee =>
						{
							var t = new SoundCloudTrack();

							t.Content.ApplyToggleConcept(t.HideContent, t.ShowContent).Hide();

							t.Title.innerHTML = ee.trackName;
							t.Waveform.src = ee.waveformUrl;
							t.Audio.src = ee.streamUrl;
							t.Identity.innerText = ee.uid;

							t.Play.onclick += eee => { eee.PreventDefault(); t.Audio.play(); };
							t.Pause.onclick += eee => { eee.PreventDefault(); t.Audio.pause(); };

							DoubleAction SetProgress1 = p =>
							{

								t.Gradient3.style.width = System.Convert.ToInt32(800 * p) + "px";
								t.Gradient4.style.width = System.Convert.ToInt32(800 * p) + "px";
							};

							t.Gradient5.style.Opacity = 0.4;
							t.Gradient6.style.Opacity = 0.4;

							DoubleAction SetProgress2 = p =>
							{

								t.Gradient5.style.width = System.Convert.ToInt32(800 * p) + "px";
								t.Gradient6.style.width = System.Convert.ToInt32(800 * p) + "px";
							};

							AtTimer +=
								delegate
								{
									if (t.Audio.duration == 0)
									{
										t.Play.Hide();
										t.Pause.Hide();
										return;
									}
									else
									{
										t.Play.Show(t.Audio.paused);
										t.Pause.Show(!t.Audio.paused);
									}

									var p = t.Audio.currentTime / t.Audio.duration;
									SetProgress1(p);
								};

							t.Waveform.onmouseout +=
								delegate
								{
									SetProgress2(0);
								};

							t.Waveform.onmousemove +=
								eee =>
								{
									SetProgress2(eee.OffsetX / 800.0);
								};

							t.Waveform.onclick +=
								eee =>
								{
									t.Audio.currentTime = t.Audio.duration * (eee.OffsetX / 800.0);
									t.Audio.play();
								};

							t.Waveform.style.cursor = IStyle.CursorEnum.pointer;

							SetProgress1(0);
							SetProgress2(0);

							t.Container.AttachTo(MyPagesInternal);
						}
					);
				}
				else
				{
					//new PromotionWebApplication1.HTML.Audio.FromAssets.Track1 { controls = true }.AttachToDocument();
					//new PromotionWebApplication1.HTML.Audio.FromWeb.Track1 { controls = true, autobuffer = true }.AttachToDocument();

					var cc = new HTML.Pages.FromAssets.Controls.Named.CenteredLogo_Kamma();

					cc.Container.AttachToDocument();

					// see: http://en.wikipedia.org/wiki/Perl_control_structures
					// "Unless" == "if not"  ;)

					IsMicrosoftInternetExplorer.YetIfNotThen(cc.TheLogoImage.BeginPulseAnimation).ButIfSoThen(cc.TheLogoImage.HideNowButShowAtDelay);
				}
			}
			#endregion

			"UA-13087448-1".ToGoogleAnalyticsTracker(
				pageTracker =>
				{
					pageTracker._setDomainName(".jsc-solutions.net");
					pageTracker._trackPageview();
				}
			);


		}



		/// <summary>
		/// Microsoft Internet Explorer does not support using opacity on an image with an alpha layer.
		/// </summary>
		public static bool IsMicrosoftInternetExplorer
		{
			get
			{
				return (bool)new IFunction("/*@cc_on return true; @*/ return false;").apply(null);
			}
		}



	}

	public delegate void StringAction(string e);
	public delegate void StringActionAction(StringAction e);

	public sealed class UltraWebService : ISoundCloudTracksDownload
	{

		public void Hello(string data, StringAction result)
		{
			result(data + " hello");
			result(data + " world");
		}

		public void GetTitleFromServer(StringAction result)
		{
			var r = new Random();

			var Targets = new[]
			{
				"javascript",
				"java",
				"actionscript",
				"php"
			};

			result("jsc solutions - C# to " + Targets[r.Next(0, Targets.Length)]);

			// should we add timing information if we use Thread.Sleep to the results?

		}

		public void /*ISoundCloudTracksDownload. not supported yet ? */ SoundCloudTracksDownload(Services.SoundCloudTrackFound yield)
		{
			new Services.SoundCloudTracks().Download(yield);
		}
	}
}
