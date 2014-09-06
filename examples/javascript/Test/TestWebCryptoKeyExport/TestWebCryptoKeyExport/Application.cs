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


                // The JWA alg “RSA-OAEP” requires this hash function to be SHA-1 (the default
                //> from RFC 3447). 

                var algorithm = new
                {
                    name = "RSA-OAEP",
                    //hash = new { name = "SHA-256" },
                    hash = new { name = "SHA-1" },

                    modulusLength = 2048,
                    publicExponent,
                };

                var pgenerateKeyAsync = Native.crypto.subtle.generateKeyAsync(
                    algorithm,
                    extractable: false,
                    keyUsages: new[] { "encrypt", "decrypt" }
                    );


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

                var JSONWebKey = await Native.crypto.subtle.exportJSONWebKeyAsync(key.publicKey);

                // continue exportKey {{ JSONWebKey = [object Object], ElapsedMilliseconds = 835 }}
                // continue exportKey {{ p = null, q = null, qi = null, ElapsedMilliseconds = 3055 }}


                // X:\jsc.svn\examples\javascript\appengine\test\TestAppEngineWebCryptoKeyImport\TestAppEngineWebCryptoKeyImport\Application.cs

                new IHTMLPre { "continue exportKey " +
                            new {
                                    JSONWebKey.alg,

                                    // exponent
                                    JSONWebKey.e,
                                    // modolo
                                    JSONWebKey.n,


                                    //JSONWebKey.ext,
                                    //JSONWebKey.kty,

                                    sw.ElapsedMilliseconds } }.AttachToDocument();

                var Exponent = Convert.FromBase64String(JSONWebKey.e);

                new IHTMLPre { "Exponent " + new { Exponent.Length } }.AttachToDocument();

                var Modulus = Convert.FromBase64String(JSONWebKey.n);

                new IHTMLPre { "Modulus " + new { Modulus.Length } }.AttachToDocument();

                //Exponent { { Length = 3 } }
                //Modulus { { Length = 256 } }

                // continue exportKey {{ alg = RSA-OAEP-256, e = AQAB, ext = true, kty = RSA, n = 8tGdxZBpFAIQN3Pzc-7NC_vDF26dleCMGDY7egB8Q136YlqfB7tRpYMU9k88MXGDIleUyEPoDT03yopH8B3Cuio61Wzk-6uXTl6WGjK-FvpxiJWMxa6rXdng7cCyzsG5rah3wI8B3ko4NhHO7NrdKoWG4-y1qxWi2JdAv1g8DLKFUqTuu4siLXPEXvHdWcV4booyeVzCsIf-xq2Zrh7hLbhN83_6bCG0KdkQCIYUgqbI2kHOI4acqTKcXE5_W2cqbw0GStQOyoqClNb0k7VIyufiYpKCRv5176NOmTjFeVBVRhnHkRn96n4Fc4EwLL-KBAj9sfJ1dVrQ2pS-IHIe3w, ElapsedMilliseconds = 6027 }}

                // view-source:42599 0:36694ms decryptAsync { err = OperationError:  }
                new IHTMLButton { "ask server to encrypt for client " }.AttachToDocument().onclick +=
                    async delegate
                {
                    new IHTMLPre { "before Encrypt" }.AttachToDocument();

                    var xbytes = await this.Encrypt(Exponent, Modulus);

                    // are the bytes correct?

                    var xxbytes = new Uint8Array(xbytes);


                    new IHTMLPre { "before decryptAsync. will it work??? " + new { xxbytes, xbytes.Length } }.AttachToDocument();

                    // https://code.google.com/p/chromium/issues/detail?id=390475
                    // RSA-OAEP public keys do not support decrypt/unwrapKey. 
                    // ?

                    // RSA/ECB/OAEPWithSHA-1AndMGF1Padding
                    // RSA using Optimal Asymmetric Encryption Padding (OAEP), as defined in RFC 3447 [RFC3447]
                    // http://www.w3.org/2001/04/xmlenc#rsa-oaep-mgf1p	RSA/ECB/OAEPWithSHA-1AndMGF1Padding
                    // http://openid.net/specs/draft-jones-json-web-encryption-02.html

                    // why aint it working??
                    // view-source:42612 0:33308ms decryptAsync { err = OperationError:  }
                    // either wait, test imprt next?
                    var zbytes = await Native.crypto.subtle.decryptAsync(
                         algorithm,
                         key.privateKey,

                         //key.publicKey,

                         xxbytes
                     );
                    //  OperationError
                    // ?????



                    new IHTMLElement(IHTMLElement.HTMLElementEnum.hr).AttachToDocument();

                    foreach (var item in zbytes)
                    {
                        new IHTMLCode { " 0x" + item.ToString("x2") }.AttachToDocument();
                    }

                    new IHTMLElement(IHTMLElement.HTMLElementEnum.hr).AttachToDocument();


                    var zstring = Encoding.UTF8.GetString(zbytes);

                    // which is it?
                    new IHTMLPre { new { zstring } }.AttachToDocument();
                }
                ;






            };

        }

    }
}
