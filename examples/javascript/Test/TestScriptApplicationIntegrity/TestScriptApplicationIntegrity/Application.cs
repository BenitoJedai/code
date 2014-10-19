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
using TestScriptApplicationIntegrity;
using TestScriptApplicationIntegrity.Design;
using TestScriptApplicationIntegrity.HTML.Pages;
using System.Net;
using System.Diagnostics;

namespace TestScriptApplicationIntegrity
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
            // how do we know
            // we have not been modified?

            // X:\jsc.svn\examples\javascript\Test\TestWebCrypto\TestWebCrypto\Application.cs
            // X:\jsc.svn\examples\javascript\async\Test\TestWebCryptoAsync\TestWebCryptoAsync\Application.cs
            // X:\jsc.svn\examples\javascript\Test\TestWebCryptoEncryption\TestWebCryptoEncryption\Application.cs
            // X:\jsc.svn\examples\javascript\Test\TestWebCryptoKeyExport\TestWebCryptoKeyExport\Application.cs
            // X:\jsc.svn\examples\javascript\Test\TestWebCryptoKeyImport\TestWebCryptoKeyImport\Application.cs

            #region secure origin
            //new IHTMLPre { new { Native.document.location.host } }.AttachToDocument();

            //if (Native.document.location.host.TakeUntilOrEmpty(":") != "127.0.0.1")
            //{
            //    // https://code.google.com/p/chromium/issues/detail?id=412681

            //    new IHTMLAnchor
            //    {
            //        href = "http://127.0.0.1:" + Native.document.location.host.SkipUntilOrEmpty(":"),
            //        innerText = "open as secure origin!"
            //    }.AttachToDocument();

            //    return;
            //}
            #endregion

            // what about net neutrality?
            // can we tracert back to servers?
            // https://www.youtube.com/watch?v=5kF5VEpO9Xs
            // how fast is our crypto connection?


            // https://developer.mozilla.org/en/docs/Web/API/document.currentScript

            new IHTMLButton { "sha1(view-source)" }.AttachToDocument().onclick += async delegate
            {
                // should jsc do automatic sha1 checks?
                var c = new WebClient();
                var sw = Stopwatch.StartNew();

                // this is special
                // for loading workers
                // injecting extension into web apps


                // script: error JSC1000: No implementation found for this native method, please implement [System.Net.WebClient.DownloadDataTaskAsync(System.String)]
                // X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Net\WebClient.cs
                //var bytes = await c.DownloadDataTaskAsync("view-source");
                var xstring = await c.DownloadStringTaskAsync("view-source");
                var bytes = Encoding.UTF8.GetBytes(xstring);


                new IHTMLPre { new { bytes.Length, sw.ElapsedMilliseconds } }.AttachToDocument();

                // shall we change jsc to do byte arrays in Uint8Array ?
                //var data = new Uint8Array(7, 8);
                var sw2 = Stopwatch.StartNew();

                var a = new { name = "SHA-1" };
                //var a = new { name = "SHA-512" };
                var x = await Native.crypto.subtle.digestAsync(a, bytes);



                new IHTMLPre { new { a, x.Length, sw.ElapsedMilliseconds } }.AttachToDocument();

                //{{ Length = 2192348, ElapsedMilliseconds = 722 }}
                //{{ a = {{ name = SHA-1 }}, Length = 20, ElapsedMilliseconds = 10610 }}
                //{{ Length = 2192348, ElapsedMilliseconds = 853 }}
                //{{ a = {{ name = SHA-1 }}, Length = 20, ElapsedMilliseconds = 863 }}
                //{{ Length = 2192348, ElapsedMilliseconds = 735 }}
                //{{ a = {{ name = SHA-1 }}, Length = 20, ElapsedMilliseconds = 749 }}
            };


        }

    }
}
