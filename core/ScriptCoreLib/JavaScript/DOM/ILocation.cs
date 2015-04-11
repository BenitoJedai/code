using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.DOM;

namespace ScriptCoreLib.JavaScript.DOM
{
	using StringArray = IArray<string>;


	// http://mxr.mozilla.org/mozilla-central/source/dom/webidl/Location.webidl
	// http://developer.mozilla.org/en/docs/DOM:window.location
	// https://src.chromium.org/viewvc/blink/trunk/Source/core/frame/Location.idl

	[Script(HasNoPrototype = true)]
	public class ILocation
	{
		//Magnet link (trackerless):   magnet:?xt=urn%3Abtih%3A<SOMEHASH>
		//Browser link (trackerless):  bittorrent://<SOMEHASH>
		//Output torrent: /current/path/directory_name.torrent

		// chrome://torrent-settings/
		// bittorrent://8e65684d700ecc41a09a60ee58991845ea56f734/index.html
		// magnet:?xt=urn%3Abtih%3A8e65684d700ecc41a09a60ee58991845ea56f734

		// http://blog.bittorrent.com/2015/04/10/project-maelstrom-developer-tools/

		// http://stackoverflow.com/questions/4505798/difference-between-window-location-assign-and-window-location-replace

		// https://www.google.ee/policies/faq/
		// your web browser also may send the Internet address, or URL, of the search results page 
		// to the destination webpage as the HTTP Referrer. 


		public string protocol;
		public string host;
		public string hash;
		public string href;
		public string search;
		public string pathname;


		[System.Obsolete]
		public bool IsHTTP
		{
			[Script(DefineAsStatic = true)]
			get
			{ return protocol == "http:"; }
		}

		public void reload()
		{

		}

		public string this[string e]
		{
			[Script(DefineAsStatic = true)]
			get
			{
				string z = null;

				string k = StringArray.Split(this.search, "?")[1];

				if (k != null)
				{
					StringArray u = StringArray.Split(k, "&");


					foreach (string x in u.ToArray())
					{
						StringArray n = StringArray.Split(x, "=");

						if (n.length > 1)
						{
							if (Native.window.unescape(n[0]) == e)
							{
								z = Native.window.unescape(n[1]);

								break;
							}
						}
					}

				}
				return z;
			}
		}

		public void replace(string e)
		{
		}

		public void assign(string e)
		{
		}
	}
}
