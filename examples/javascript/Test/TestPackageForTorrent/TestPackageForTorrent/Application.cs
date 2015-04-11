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
using TestPackageForTorrent;
using TestPackageForTorrent.Design;
using TestPackageForTorrent.HTML.Pages;

namespace TestPackageForTorrent
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
			new IHTMLSpan {
				"loaded!", new { Native.document.location.href, Native.window.navigator.userAgent}
			}.AttachToDocument();

			// loading... loaded!{{ href = file:///X:/jsc.svn/examples/javascript/test/TestPackageForTorrent/TestPackageForTorrent/bin/Debug/staging/TestPackageForTorrent.Application/web/index.htm, userAgent = Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/43.0.2357.10 Safari/537.36 }}
			// loading... loaded!{{ href = file:///X:/jsc.svn/examples/javascript/test/TestPackageForTorrent/TestPackageForTorrent/bin/Debug/staging/TestPackageForTorrent.Application/web/index.htm, userAgent = Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/37.0.2062.120 Torrent/37.0.1.7 Safari/537.36 }}

			// http://pairing:2BE1445EABF26683C1F187AF376EFDF60E0E105C@127.0.0.1:25531/proxy?sid=2&file=8&token=2BE1445EABF26683C1F187AF376EFDF60E0E105C&pairing=2BE1445EABF26683C1F187AF376EFDF60E0E105C&service=STREAMING
			// "X:\jsc.svn\examples\javascript\test\TestPackageForTorrent\TestPackageForTorrent\bin\Debug\staging\TestPackageForTorrent.Application\web\web.torrent"
			// 2BE1445EABF26683C1F187AF376EFDF60E0E105C

			// bittorrent://2BE1445EABF26683C1F187AF376EFDF60E0E105C/index.html
			// magnet:?xt=urn%3Abtih%2BE1445EABF26683C1F187AF376EFDF60E0E105C
			// magnet:?xt=urn:btih:2BE1445EABF26683C1F187AF376EFDF60E0E105C&dn=web&tr=udp%3a%2f%2ftracker.openbittorrent.com%3a80%2fannounce&tr=udp%3a%2f%2ftracker.publicbt.com%3a80%2fannounce&tr=udp%3a%2f%2ftracker.ccc.de%3a80%2fannounce

			// LAN uPnP not available. no seed
			// hotspot not forwarding ports.

			// http://forum.bittorrent.com/topic/16628-wich-ports-should-be-oppened/
			//  6881-6889 TCP


		}

	}
}
