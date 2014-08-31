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
using TestWebCryptoKeyExport;
using TestWebCryptoKeyExport.Design;
using TestWebCryptoKeyExport.HTML.Pages;
using System.Diagnostics;
using ScriptCoreLib.JavaScript.WebGL;

namespace TestWebCryptoKeyExport
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
            // X:\jsc.svn\examples\javascript\Test\TestWebCryptoKeyExport\TestWebCryptoKeyExport\Application.cs

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

                new IHTMLPre { "before generateKey " + new { sw.ElapsedMilliseconds } }.AttachToDocument();

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
                new IHTMLPre { "continue generateKey "
                    + new {
                    key.privateKey,
                    key.publicKey,
                              key.publicKey.extractable,
                              sw.ElapsedMilliseconds } }.AttachToDocument();

                // continue generateKey {{ privateKey = [object CryptoKey], publicKey = [object CryptoKey], extractable = true, ElapsedMilliseconds = 671 }}

                var pexport = Native.crypto.subtle.exportKey("jwk", key.publicKey);

                pexport.then(
                    JSONWebKey =>
                    {
                        // continue exportKey {{ JSONWebKey = [object Object], ElapsedMilliseconds = 835 }}
                        // continue exportKey {{ p = null, q = null, qi = null, ElapsedMilliseconds = 3055 }}


                        new IHTMLPre { "continue exportKey " + new {



                                                                       // exponent
                                                                       JSONWebKey.e,
                                                                       // modolo
                                                                       JSONWebKey.n,

                                                                       //JSONWebKey.alg,

                                                                       //JSONWebKey.ext,
                                                                       //JSONWebKey.kty,

                                                                       sw.ElapsedMilliseconds } }.AttachToDocument();

                        // continue exportKey {{ alg = RSA-OAEP-256, e = AQAB, ext = true, kty = RSA, n = 8tGdxZBpFAIQN3Pzc-7NC_vDF26dleCMGDY7egB8Q136YlqfB7tRpYMU9k88MXGDIleUyEPoDT03yopH8B3Cuio61Wzk-6uXTl6WGjK-FvpxiJWMxa6rXdng7cCyzsG5rah3wI8B3ko4NhHO7NrdKoWG4-y1qxWi2JdAv1g8DLKFUqTuu4siLXPEXvHdWcV4booyeVzCsIf-xq2Zrh7hLbhN83_6bCG0KdkQCIYUgqbI2kHOI4acqTKcXE5_W2cqbw0GStQOyoqClNb0k7VIyufiYpKCRv5176NOmTjFeVBVRhnHkRn96n4Fc4EwLL-KBAj9sfJ1dVrQ2pS-IHIe3w, ElapsedMilliseconds = 6027 }}


                    }
                );



            };

        }

    }
}
