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
using TestWebCryptoSHA1;
using TestWebCryptoSHA1.Design;
using TestWebCryptoSHA1.HTML.Pages;
using ScriptCoreLib.JavaScript.WebGL;
using System.Diagnostics;

namespace TestWebCryptoSHA1
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

            // Uncaught TypeError: Failed to execute 'digest' on 'SubtleCrypto': No function was found that matched the signature provided.

            // {{ err = NotSupportedError: Only secure origins are allowed. http://goo.gl/lq4gCo }}

            // X:\jsc.svn\examples\javascript\Test\TestWebCrypto\TestWebCrypto\Application.cs
            // X:\jsc.svn\examples\javascript\async\Test\TestWebCryptoAsync\TestWebCryptoAsync\Application.cs
            // X:\jsc.svn\examples\javascript\Test\TestWebCryptoEncryption\TestWebCryptoEncryption\Application.cs
            // X:\jsc.svn\examples\javascript\Test\TestWebCryptoKeyExport\TestWebCryptoKeyExport\Application.cs
            // X:\jsc.svn\examples\javascript\Test\TestWebCryptoKeyImport\TestWebCryptoKeyImport\Application.cs

            #region secure origin
            new IHTMLPre { new { Native.document.location.host } }.AttachToDocument();

            if (Native.document.location.host.TakeUntilOrEmpty(":") != "127.0.0.1")
            {
                // https://code.google.com/p/chromium/issues/detail?id=412681

                new IHTMLAnchor
                {
                    href = "http://127.0.0.1:" + Native.document.location.host.SkipUntilOrEmpty(":"),
                    innerText = "open as secure origin!"
                }.AttachToDocument();

                return;
            }
            #endregion


            new IHTMLButton { "digest" }.AttachToDocument().onclick +=
                delegate
            {
                // shall we change jsc to do byte arrays in Uint8Array ?
                var data = new Uint8Array(7, 8);


                var p = Native.crypto.subtle.digest(
                     new { name = "SHA-1" },
                     //new byte[] { 7, 8 }
                     data
                 );

                p.then(
                    onSuccess: x =>
                    {
                        // {{ x = [object ArrayBuffer] }}

                        new IHTMLPre { new { x } }.AttachToDocument();
                    },
                    onError: err =>
                {
                    new IHTMLPre { new { err } }.AttachToDocument();
                }
            );
            };


            new IHTMLButton { "digestAsync" }.AttachToDocument().onclick +=

                async delegate
            {
                var sw = Stopwatch.StartNew();

                // shall we change jsc to do byte arrays in Uint8Array ?
                var data = new Uint8Array(7, 8);

                var a = new { name = "SHA-1" };
                //var a = new { name = "SHA-512" };
                var x = await Native.crypto.subtle.digestAsync(a, data);

                // {{ Length = 20 }}
                // {{ a = {{ name = SHA-512 }}, Length = 64 }}
                //{{ a = {{ name = SHA-512 }}, Length = 64, ElapsedMilliseconds = 3164 }}
                //{{ a = {{ name = SHA-512 }}, Length = 64, ElapsedMilliseconds = 3 }}
                //{{ a = {{ name = SHA-512 }}, Length = 64, ElapsedMilliseconds = 1 }}

                new IHTMLPre { new { a, x.Length, sw.ElapsedMilliseconds } }.AttachToDocument();
            };



        }

    }
}
