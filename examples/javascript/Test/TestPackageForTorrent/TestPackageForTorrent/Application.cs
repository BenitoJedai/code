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
			// http://spexx58.blogspot.com/2013/02/following-most-recent-update-to.html

			// magnet:?xt=urn:btih:F77C51BEC9C983E576442B7450F646A811BCF95D&dn=web&tr=udp%3a%2f%2ftracker.openbittorrent.com%3a80%2fannounce&tr=udp%3a%2f%2ftracker.publicbt.com%3a80%2fannounce&tr=udp%3a%2f%2ftracker.coppersurfer.tk%3a6969&tr=udp%3a%2f%2ftracker.leechers-paradise.org%3a6969&tr=udp%3a%2f%2fopen.demonii.com%3a1337

			// X:\jsc.svn\examples\java\android\forms\InteractivePortForwarding\InteractivePortForwarding\UserControl1.cs

			//udp://tracker.coppersurfer.tk:6969

			//udp://tracker.leechers-paradise.org:6969

			// initial seeding isnt a good idea?

			Native.css.style.backgroundColor = "yellow";

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

			// "R:\torrent\web"
			// magnet:?xt=urn:btih:144B62870B3983702B82036E479A9D04860A5C96&dn=web&tr=udp%3a%2f%2ftracker.openbittorrent.com%3a80%2fannounce&tr=udp%3a%2f%2ftracker.publicbt.com%3a80%2fannounce
			// magnet:?xt=urn:btih:144B62870B3983702B82036E479A9D04860A5C96&dn=web&tr=udp%3a%2f%2ftracker.openbittorrent.com%3a80%2fannounce&tr=udp%3a%2f%2ftracker.publicbt.com%3a80%2fannounce
			// magnet:?xt=urn%3Abtih%144B62870B3983702B82036E479A9D04860A5C96

			// magnet:?xt=urn:btih:VRWKPHILELGL3GWA2523TTXWA5X6ZC3X&dn=web&tr=udp%3A%2F%2Ftracker.publicbt.com%3A80%2Fannounce&tr=udp%3A%2F%2Ftracker.openbittorrent.com%3A80%2Fannounce&xl=6463890

			// loading... loaded!{{ href = bittorrent://144b62870b3983702b82036e479a9d04860a5c96/index.htm, userAgent = Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/37.0.2062.120 Torrent/37.0.2.1 Safari/537.36 }}

		}

	}
}

//C:\Windows\system32>netsh interface ip delete destinationcache
//The requested operation requires elevation(Run as administrator).

//C:\Windows\system32>netsh interface ip delete destinationcache
//Ok.

//IPv4 Route Table
//===========================================================================
//Active Routes:
//Network Destination        Netmask Gateway       Interface Metric
//          0.0.0.0          0.0.0.0     192.168.43.1    192.168.43.10   8256
//          0.0.0.0          0.0.0.0      192.168.1.1     192.168.1.13   8491

//IPv4 Route Table
//===========================================================================
//Active Routes:
//Network Destination        Netmask Gateway       Interface Metric
//          0.0.0.0          0.0.0.0     192.168.43.1    192.168.43.10   9256
//          0.0.0.0          0.0.0.0      192.168.1.1     192.168.1.13   8491


// 1    <1 ms<1 ms<1 ms  INTENOSMB[192.168.1.1]
// 2   561 ms   399 ms   396 ms  2-16-191-90.dyn.estpak.ee[90.191.16.2]
// 3     1 ms     1 ms     1 ms  noe-bb1-ae-5-0.ee.estpak.ee[213.168.1.14]
// 4     1 ms     1 ms     1 ms  kjj-sr2-xe-3-2-0-0.ee.estpak.ee[90.190.134.197]
// 5     1 ms     2 ms     2 ms  kjj-lgw7-gi-0-25.ee.estpak.ee[194.126.96.102]
// 6     1 ms     1 ms     1 ms  neti.ee[195.50.209.246]


// 1    <1 ms     1 ms<1 ms  INTENOSMB[192.168.1.1]
// 2     1 ms     1 ms     1 ms  2-16-191-90.dyn.estpak.ee[90.191.16.2]
// 3     1 ms     1 ms     1 ms  noe-bb1-ae-5-0.ee.estpak.ee[213.168.1.14]
// 4     1 ms     1 ms     1 ms  kjj-bb3-ae-3-0.ee.estpak.ee[194.126.123.2]
// 5     1 ms*        1 ms  tln-b3-link.telia.net[62.115.34.133]
// 6    10 ms    10 ms    10 ms  s-bb4-link.telia.net[62.115.134.246]
// 7    10 ms     9 ms    10 ms  s-b5-link.telia.net[80.91.249.219]
// 8    28 ms    27 ms    27 ms  google-ic-306509-s-b5.c.telia.net[62.115.45.14]
// 9    28 ms    27 ms    27 ms  64.233.175.10
//10    27 ms    26 ms    27 ms  google-public-dns-a.google.com[8.8.8.8]



//Connect fault
//{
//	host = 192.168.43.10, port = 8080, ElapsedMilliseconds = 23, Message = /192.168.43.10:8080 - Connection refused,
//       at org.apache.harmony.luni.net.PlainSocketImpl.connect(PlainSocketImpl.java:207)
//       at org.apache.harmony.luni.net.PlainSocketImpl.connect(PlainSocketImpl.java:437)
//       at java.net.Socket.connect(Socket.java:1002)
//       at java.net.Socket.connect(Socket.java:945)
//       at ScriptCoreLibJava.BCLImplementation.System.Net.Sockets.__TcpClient___c__DisplayClass2._ConnectAsync_b__1(__TcpClient__
//   at java.lang.reflect.Method.invokeNative(Native Method)
//       at java.lang.reflect.Method.invoke(Method.java:507)
//       at ScriptCoreLibJava.BCLImplementation.System.Reflection.__MethodInfo.InternalInvoke(__MethodInfo.java:93)
//       at ScriptCoreLibJava.BCLImplementation.System.Reflection.__MethodBase.Invoke(__MethodBase.java:71)
//       at ScriptCoreLib.Shared.BCLImplementation.System.__Action.Invoke(__Action.java:28)
//       at ScriptCoreLibJava.BCLImplementation.System.Threading.Tasks.__Task___c__DisplayClassc._Run_b__b(__Task___c__DisplayClas
//   at java.lang.reflect.Method.invokeNative(Native Method)
//       at java.lang.reflect.Method.invoke(Method.java:507)
//       at ScriptCoreLibJava.BCLImplementation.System.Reflection.__MethodInfo.InternalInvoke(__MethodInfo.java:93)
//       at ScriptCoreLibJava.BCLImplementation.System.Reflection.__MethodBase.Invoke(__MethodBase.java:71)
//       at ScriptCoreLib.Shared.BCLImplementation.System.Threading.__ThreadStart.Invoke(__ThreadStart.java:28)
//       at ScriptCoreLibJava.BCLImplementation.System.Threading.__Thread___c__DisplayClass3.__ctor_b__1(__Thread___c__DisplayClas
//   at java.lang.reflect.Method.invokeNative(Native Method)
//       at java.lang.reflect.Method.invoke(Method.java:507)
//       at ScriptCoreLibJava.BCLImplementation.System.Reflection.__MethodInfo.InternalInvoke(__MethodInfo.java:93)
//       at ScriptCoreLibJava.BCLImplementation.System.Reflection.__MethodBase.Invoke(__MethodBase.java:71)
//       at ScriptCoreLib.Shared.BCLImplementation.System.__Action.Invoke(__Action.java:28)
//       at ScriptCoreLibJava.BCLImplementation.System.Threading.__Thread_RunnableHandler.run(__Thread_RunnableHandler.java:20)
//       at java.lang.Thread.run(Thread.java:1019)
// }