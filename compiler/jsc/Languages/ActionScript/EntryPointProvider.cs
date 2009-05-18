using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;
using ScriptCoreLib;
using System.Xml.Linq;
using System.Xml;

namespace jsc.Languages.ActionScript
{
	static class EntryPointProvider
	{
		static public void WriteEntryPoints(this Assembly a, DirectoryInfo dir, Type[] KnownTypes)
		{
			var Entries = from _Type in KnownTypes
						  let _Script = _Type.GetCustomAttributes<ScriptAttribute>().SingleOrDefault()
						  where _Script != null
						  let _ScriptApplicationEntryPoint = _Type.GetCustomAttributes<ScriptApplicationEntryPointAttribute>().SingleOrDefault()
						  where _ScriptApplicationEntryPoint != null
						  let _GoogleGadget = _Type.GetCustomAttributes<GoogleGadgetAttribute>().SingleOrDefault()
						  select new { _Type, _Script, _ScriptApplicationEntryPoint, _GoogleGadget };

			/*
<object classid="clsid:D27CDB6E-AE6D-11cf-96B8-444553540000"
width="160" height="160"
codebase="http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab">
<param name="movie" value="http://hosting.gmodules.com/ig/gadgets/file/112091410969506928037/FlashMinesweeper.swf" />
<param name="quality" value="high" />
<param name="bgcolor" value="#869ca7" />

<embed src="http://hosting.gmodules.com/ig/gadgets/file/112091410969506928037/FlashMinesweeper.swf" quality="high" bgcolor="#869ca7"
width="160" height="160" align="middle"
play="true" loop="false"
type="application/x-shockwave-flash"
pluginspage="http://www.macromedia.com/go/getflashplayer">
</embed>
</object>              
			 */

			// http://kb.adobe.com/selfservice/viewContent.do?externalId=tn_4150
			// http://www.joostdevalk.nl/code/valid-flash-embedding.html

			/*
<object type="application/x-shockwave-flash" 
	data="music/sound.swf" 
	width="0" height="0">
	<param name="movie" value="music/sound.swf" />
	<param name="quality" value="high"/>
</object>             
			 */

			//<embed 
			//    src="MovieAgentGadget.swf" 
			//    quality="high"
			//    bgcolor="#869ca7"
			//    width="500" height="375" name="MovieAgentGadget" align="middle"
			//    play="true" loop="false" allowScriptAccess="always"
			//    type="application/x-shockwave-flash"
			//    pluginspage="http://www.macromedia.com/go/getflashplayer">
			//</embed>


			//Func<string, int, int, XElement> FlashTag =
			//    (href, width, height) =>
			//    {
			//        var id = "id" + DateTime.Now.Ticks;

			//        return new XElement("embed",
			//            new
			//            {
			//                type = "application/x-shockwave-flash",
			//                src = href,
			//                id,
			//                name = id,
			//                width,
			//                height,
			//                allowFullScreen = true,
			//                allowNetworking = "all",
			//                allowScriptAccess = "always",
			//            }.GetPropertiesAsXAttributes()
			//            //new XElement("param", 
			//            //    new {
			//            //        name = "movie",
			//            //        value = href
			//            //    }.GetPropertiesAsXAttributes()
			//            //)
			//        );
			//    };


			Func<string, int, int, XElement> FlashTag =
				(href, width, height) =>
				{
					var id = "id" + DateTime.Now.Ticks;

					return new XElement("object",
						new
						{
							type = "application/x-shockwave-flash",
							data = href,
							id,
							name = id,
							width,
							height,
							allowFullScreen = true,
							allowNetworking = "all",
							allowScriptAccess = "always",
						}.GetPropertiesAsXAttributes(),
						new XElement("param",
							new
							{
								name = "movie",
								value = href
							}.GetPropertiesAsXAttributes()
						)
					);
				};

			//<object width="800" height="500"><param name="movie" value="http://nonoba.com/zproxy/mahjong-multiplayer/embed"></param><param name="allowScriptAccess" value="always" ></param><param name="allowNetworking" value="all" ></param><embed src="http://nonoba.com/zproxy/mahjong-multiplayer/embed" allowNetworking="all" allowScriptAccess="always" type="application/x-shockwave-flash" width="800" height="500"></embed></object>

			foreach (var v in Entries)
			{
				Func<bool, StringWriter> ToWriter =
					InsideGoogleGadget =>
					{
						var ww = new StringWriter();

						ww.WriteLine("<!-- created at " + System.DateTime.Now.ToString() + " -->");

						if (!InsideGoogleGadget)
						{
							ww.WriteLine("<head>");
							ww.WriteLine("<title>" + v._Type.Name + "</title>");

							if (!string.IsNullOrEmpty(v._ScriptApplicationEntryPoint.Feed))
								ww.WriteLine(
									"<link rel='alternate' href='" + v._ScriptApplicationEntryPoint.Feed + "' type='application/rss+xml' title='feed' id='' />"
								);

							ww.WriteLine("</head>");
						}
						// this allows to include this htm directly into iframe

						if (!InsideGoogleGadget)
						{
							if (v._ScriptApplicationEntryPoint.Background)
								ww.Write("<body style='margin: 0; overflow: hidden; background: #" + v._ScriptApplicationEntryPoint.BackgroundColor.ToString("X6") + ";'>");
							else
								ww.Write("<body style='margin: 0; overflow: hidden;'>");
						}

						#region WriteObject
						Action WriteObject =
							delegate
							{
								ww.Write(
									FlashTag(
									(InsideGoogleGadget && v._GoogleGadget != null && !string.IsNullOrEmpty(v._GoogleGadget.src)) ? v._GoogleGadget.src :
									v._Type.Name + ".swf"
									, v._ScriptApplicationEntryPoint.Width, v._ScriptApplicationEntryPoint.Height)
								);
							};
						#endregion

						if (v._ScriptApplicationEntryPoint.AlignToCenter)
						{
							if (InsideGoogleGadget)
							{
								if (v._ScriptApplicationEntryPoint.Background)
									ww.WriteLine("<div style='width: 100%; height: 100%; overflow: hidden; background: #" + v._ScriptApplicationEntryPoint.BackgroundColor.ToString("X6") + ";'>");
								else
									ww.WriteLine("<div style='width: 100%; height: 100%; overflow: hidden;'>");
							}
							else
							{
								ww.WriteLine("<div style='width: 100%; height: 100%; overflow: hidden;'>");

							}

							ww.WriteLine("<div style='position:absolute; top: 50%; left: 50%; width: " + v._ScriptApplicationEntryPoint.Width + "px; height: " + v._ScriptApplicationEntryPoint.Height + "px; margin-left: -" + (v._ScriptApplicationEntryPoint.Width / 2) + "px; margin-top: -" + (v._ScriptApplicationEntryPoint.Height / 2) + "px;'>");
							WriteObject();

							ww.WriteLine("</div>");
							ww.WriteLine("</div>");

						}
						else
						{
							WriteObject();
						}

						if (!InsideGoogleGadget)
						{
							ww.Write("</body>");
						}

						return ww;
					};

				using (var w = dir.CreateFile(v._Type.Name + ".htm"))
				{
					w.Write(ToWriter(false).ToString());
				}

				if (v._GoogleGadget != null)
				{

					using (var w = dir.CreateFile(v._Type.Name + ".GoogleGadget.xml"))
					{
						var xw = XmlWriter.Create(w);


						//xw.Settings.NewLineOnAttributes = true;
						//xw.Settings.NewLineHandling = NewLineHandling.Replace;

						new XDocument(
							new XElement("Module",
								new XElement("ModulePrefs", v._GoogleGadget.GetPropertiesAsXAttributes().Where(p => p.Name != "src")),
								new XElement("Content",
									new XAttribute("type", "html"),
									new XCData(
										ToWriter(true).ToString()
									)
								)
							)
						).WriteTo(xw);

						xw.Flush();
					}
				}
			}



		}
	}
}
