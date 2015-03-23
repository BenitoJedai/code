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
using TestAndroidWebCryptoKeyImport;
using TestAndroidWebCryptoKeyImport.Design;
using TestAndroidWebCryptoKeyImport.HTML.Pages;
using System.Diagnostics;

namespace TestAndroidWebCryptoKeyImport
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
			// https://developer.chrome.com/multidevice/webview/overview

			if (Native.crypto.subtle == null)
			{
				// android webview for example?
				new IHTMLPre {
					"crypto API missing. open in another browser?"
				}.AttachToDocument();

				return;
			}

			#region secure origin
			new IHTMLPre { new { Native.document.location.host } }.AttachToDocument();

			if (Native.document.location.host.TakeUntilOrEmpty(":") != "127.0.0.1")
			{
				new IHTMLAnchor
				{
					href = "http://127.0.0.1:" + Native.document.location.host.SkipUntilOrEmpty(":"),
					innerText = "open as secure origin!"
				}.AttachToDocument();

				return;
			}
			#endregion

			var sw = Stopwatch.StartNew();


			new IHTMLCode {
				// I/WebViewFactory(  500): Loading com.google.android.webview version 37 (1602158-arm64) (code 111202)

				new {

					m = this.m.Length,
					sw.ElapsedMilliseconds,

					// https://twitter.com/paul_irish/status/523168798551986176
					// how to upgrade the webview?
					// As new versions of Chromium become available, users can update from Google Play to ensure they get the latest enhancements and bug fixes for WebView, providing the latest web APIs and bug fixes for apps using WebView on Android 5.0 and higher.
					Native.window.navigator.userAgent
				}
			}.AttachToDocument();

			// {{ m = 256 }}

			var algorithm = new
			{
				name = "RSA-OAEP",
				//hash = new { name = "SHA-256" },
				hash = new { name = "SHA-1" },

				//modulusLength = 2048,
				//publicExponent,
			};

			// E/Web Console(18679): Uncaught TypeError: Cannot call method 'importKey' of undefined at http://127.0.0.1:25931/view-source:60645
			// https://code.google.com/p/chromium/issues/detail?id=113088

			new IHTMLPre { "calling crypto.subtle.importKey..." }.AttachToDocument();

			var p = Native.crypto.subtle.importKey(
				format: "jwk",
				keyData: new
				{
					alg = "RSA-OAEP",
					e = Convert.ToBase64String(this.e),
					ext = false,
					kty = "RSA",
					n = Convert.ToBase64String(this.m)
				},
				algorithm: algorithm,
				extractable: false,
				keyUsages: new[] { "encrypt" }
			);

			p.then(
				onSuccess: z =>
			{
				// onSuccess {{ z = [object CryptoKey], ElapsedMilliseconds = 9278 }}


				new IHTMLPre { "onSuccess " + new { z, sw.ElapsedMilliseconds } }.AttachToDocument();

				new IHTMLButton { "encrypt for server" }.AttachToDocument().onclick +=
				async delegate
				{
					// Man in the middle?
					// layered security
					var data = Encoding.UTF8.GetBytes("hello from client");
					var esw = Stopwatch.StartNew();

					var ebytes = await Native.crypto.subtle.encryptAsync(algorithm, z, data);
					new IHTMLPre { "encryptAsync " + new { esw.ElapsedMilliseconds } }.AttachToDocument();

					await UploadEncryptedString(
						ebytes
					);
				};


			},
				onError: z =>
			{
				// Chrome for Android does not yet support it?
				// need to wait until Android L gets released?

				// nexus9?
				// http://jimbergman.net/webkit-version-in-android-version/

				new IHTMLPre {
					"onError " +
						new {
						  z}
					}.AttachToDocument();
			}

			);
		}

	}

}
