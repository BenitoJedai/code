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
using ServerSideContent;
using ServerSideContent.Design;
using ServerSideContent.HTML.Pages;
using System.Diagnostics;

namespace ServerSideContent
{
	using ServerSideContent.HTML.Images.FromAssets;
	using static FireBoxRoom;
	using static JanusVRExperiment.Application;


	/// <summary>
	/// Your client side code running inside a web browser as JavaScript.
	/// </summary>
	public sealed class Application : ApplicationWebService
	{
		/// <summary>
		/// This is a javascript application.
		/// </summary>
		/// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
		public Application(JanusVRExperiment.HTML.Pages.IApp page)
		{
			// jsc, look this s empty. 
			// starting an edit and continue session should be a snap

			// process restarts should use the same port?
			// perhaps even signal the old application of a new session?

			// the ssl keygen could be started a head of time in another thread..


			// forward
			new JanusVRExperiment.Application(page);

			new { }.With(
				async delegate
				{

					await JanusVRExperiment.Application.async_room;

					// http://beta.vrsites.com/Getting%20started
					// we are in janusVR!

					// click to add pyramids!
					// 
					onclick += delegate
					{
						new Pyramid
						{
							pos = new Vector(4, Pyramid.Elements.Count % 2, 4 + Pyramid.Elements.Count),

							fwd = new Vector(-1, 0, 0),
							scale = new Vector(2, 2, 2),
							//col = new Vector(.5, 1, .5),

							image_id = nameof(thumb),

							collision_radius = 1
						};

						new Cube
						{
							pos = new Vector(-1, Pyramid.Elements.Count % 2, 4 + Pyramid.Elements.Count),

							fwd = new Vector(-1, 0, 0),
							scale = new Vector(2, 2, 2),
							//col = new Vector(.5, 1, .5),

							// why cannot i see the image as texture?
							image_id = nameof(thumb),

							collision_radius = 1
						};

						// sound?
						//room.createObject(


						// where is the audio? was it optimized out?
						room.playSound(nameof(HTML.Audio.FromAssets.information_center));

						// http://www.dgp.toronto.edu/~mccrae/projects/firebox/js.html
						// -- "room.playSound(id)": Plays the sound with the given asset id.


						// next we should know what to do with the shaders..
					};


				}
			);

			if (Native.document == null)
				return;

			{ Audio ref0; }
			{ TexturesImages ref0; }

			page.hellow.style.fontSize = "xx-large";

			page.hellow.innerText = "slowly click, to send room.cookies to WebMethod2";
			page.hellow.onclick += async delegate
			{
				await this.WebMethod2(
					Native.document.cookie
				);
			};
		}
	}

	/// <summary>
	/// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
	/// </summary>
	public class ApplicationWebService
	{



		// sendng dynamic xml, will not help with caching, will it?
		// yet what if we want to list our assets/ for example for janusVR?

		// can we get jsc light speeding fast via ENC?

		public void Handler(ScriptCoreLib.Ultra.WebService.WebServiceHandler h)
		{
			// http://crockford.com/javascript/jsmin
			// The CompressorRater is currently limited to JavaScript files of 300000 bytes or less!
			// The file view-source.js could not be saved because it exceeds 2 MB, the maximum allowed size for uploads.
			//Error ENC0280 Modifying 'method' which contains an anonymous type will prevent the debug session from continuing.ServerSideContent X:\jsc.svn\examples\javascript\xml\ServerSideContent\ServerSideContent\Application.cs   97

			// what about async, chrome, android, appengine
			// how does this relate to thread jumping, and service worker?

			if (!h.IsDefaultPath)
				return;

			h.Context.Response.Write(@"<!-- 

hello world! will prerender janusVR scene, as API wont enable all of the features just yet at runtime 

-->
				");

			var x = XElement.Parse(JanusVRExperiment.HTML.Pages.AppSource.Text);

			//x.Element("body").Element("FireBoxRoom").Element("Room").Element("Text").Value = "hello from1 ServerSideContent " + new { Debugger.IsAttached };
			x.Element("body").Element("FireBoxRoom").Element("Room").Element("Text").Value = "hello from1 ServerSideContent ";


			// Additional information: An XObject cannot be used as a value.
			x.Element("body").Element("FireBoxRoom").Element("Assets").Add(
				from img in XElement.Parse(TexturesImagesSource.Text).Element("body").Elements("img")
				let src = img.Attribute("src").Value
				let id = img.Attribute("id").Value
				select new XElement("AssetImage",
					new XAttribute("id", id),
					new XAttribute("src", src)
				)
			);

			// http://www.dgp.toronto.edu/~mccrae/projects/firebox/notes.html#assetsound
			// <AssetSound id="localmap_sound" src="localmap.mp3" />
			x.Element("body").Element("FireBoxRoom").Element("Assets").Add(
				from img in XElement.Parse(AudioSource.Text).Element("body").Elements("audio")
				let src = img.Attribute("src").Value
				let id = img.Attribute("id").Value
				select new XElement("AssetSound",
					new XAttribute("id", id),
					new XAttribute("src", src)
				)
			);

			// interface of XElement?

			h.Context.Response.Write(x.ToString());

			h.CompleteRequest();
			return;
		}

		// useful from in WebSurface?

		public XElement Header = new XElement(@"h1", @"JSC - The .NET crosscompiler for web platforms. ready.");

		public async Task WebMethod2(string cookies)
		{
			// cookies = "InternalFields=field_Header=PGgxPkpTQyAtIFRoZSAuTkVUIGNyb3NzY29tcGlsZXIgZm9yIHdlYiBwbGF0Zm9ybXMuIHJlYWR5LjwvaDE+; cookie1=cookie1 {{ c = 4298 }}"

			Console.WriteLine($"cookies {cookies}");
		}
	}
}
