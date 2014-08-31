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
using TestWebCryptoEncryption;
using TestWebCryptoEncryption.Design;
using TestWebCryptoEncryption.HTML.Pages;
using System.Diagnostics;
using ScriptCoreLib.JavaScript.WebGL;

namespace TestWebCryptoEncryption
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
            // X:\jsc.svn\examples\javascript\Test\TestWebCrypto\TestWebCrypto\Application.cs
            // X:\jsc.svn\examples\javascript\async\Test\TestWebCryptoAsync\TestWebCryptoAsync\Application.cs
            // X:\jsc.svn\examples\javascript\Test\TestWebCryptoEncryption\TestWebCryptoEncryption\Application.cs

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



            new IHTMLButton { "generateKey in UI" }.AttachToDocument().onclick +=
                async delegate
            {
                new IHTMLElement(IHTMLElement.HTMLElementEnum.hr).AttachToDocument();
                var sw = Stopwatch.StartNew();
                var publicExponent = new Uint8Array(new byte[] { 0x01, 0x00, 0x01 });

                new IHTMLPre { "before generateKey" + new { sw.ElapsedMilliseconds } }.AttachToDocument();

                // http://blog.engelke.com/2014/08/23/public-key-cryptography-in-the-browser/

                var algorithm = new
                {
                    name = "RSA-OAEP",
                    hash = new { name = "SHA-256" },

                    modulusLength = 2048,
                    publicExponent,
                };

                var pgenerateKeyAsync = Native.crypto.subtle.generateKeyAsync(algorithm, false, new[] { "encrypt", "decrypt" });


                // https://developer.mozilla.org/en-US/docs/Mozilla/JavaScript_code_modules/Promise.jsm/Promise
                new IHTMLPre { "after generateKey " + new { pgenerateKeyAsync, sw.ElapsedMilliseconds } }.AttachToDocument();

                var key = await pgenerateKeyAsync;

                // continue generateKey {{ privateKey = [object CryptoKey], publicKey = [object CryptoKey], ElapsedMilliseconds = 5021 }}
                new IHTMLPre { "continue generateKey " + new { key.privateKey, key.publicKey, sw.ElapsedMilliseconds } }.AttachToDocument();

                var ybytes = Encoding.UTF8.GetBytes("hello world");

                new IHTMLPre { "before encrypt " + new { sw.ElapsedMilliseconds } }.AttachToDocument();

                var pencrypt = Native.crypto.subtle.encryptAsync(
                    algorithm, key.publicKey, ybytes
                );
                new IHTMLPre { "after encrypt " + new { pencrypt, sw.ElapsedMilliseconds } }.AttachToDocument();

                var xbytes = await pencrypt;

                new IHTMLPre { "continue encrypt " + new { xbytes.Length, sw.ElapsedMilliseconds } }.AttachToDocument();

                // continue encrypt {{ Length = 256, ElapsedMilliseconds = 5021 }}


                new IHTMLElement(IHTMLElement.HTMLElementEnum.hr).AttachToDocument();

                foreach (var item in xbytes)
                {
                    new IHTMLCode { " 0x" + item.ToString("x2") }.AttachToDocument();
                }

                new IHTMLElement(IHTMLElement.HTMLElementEnum.hr).AttachToDocument();



                var decrypt = new IHTMLButton { "decrypt" }.AttachToDocument();

                await decrypt.async.onclick;

                decrypt.Orphanize();

                var zbytes = await Native.crypto.subtle.decryptAsync(algorithm,
                    key.privateKey, xbytes
                );



                new IHTMLElement(IHTMLElement.HTMLElementEnum.hr).AttachToDocument();

                foreach (var item in zbytes)
                {
                    new IHTMLCode { " 0x" + item.ToString("x2") }.AttachToDocument();
                }

                new IHTMLElement(IHTMLElement.HTMLElementEnum.hr).AttachToDocument();


                var zstring = Encoding.UTF8.GetString(zbytes);

                // which is it?
                new IHTMLPre { new { zstring } }.AttachToDocument();


            };
        }

    }
}
