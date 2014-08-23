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
using TestWebCrypto;
using TestWebCrypto.Design;
using TestWebCrypto.HTML.Pages;
using ScriptCoreLib.JavaScript.WebGL;
using System.Diagnostics;

namespace TestWebCrypto
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
            // http://www.ibiblio.org/hhalpin/homepage/presentations/webcrypto/Overview5.html
            // https://code.google.com/p/chromium/issues/detail?id=245025
            // http://msdn.microsoft.com/en-us/library/5e9ft273(v=vs.110).aspx
            // http://blog.soat.fr/2013/11/devoxx-2013-cryptographic-operations-in-the-browser/
            // http://msdn.microsoft.com/library/ie/dn265046(v=vs.85).aspx
            // http://www.web-tris.com/
            // http://stackoverflow.com/questions/202011/encrypt-and-decrypt-a-string

            //var publicExponent = new byte[0x01, 0x00, 0x01];
            // haha. jsc, why no ranks supported? :P



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




            //[[PromiseStatus]]: "rejected"
            //[[PromiseValue]]: DOMException: Only secure origins are allowed.http://goo.gl/lq4gCo
            // jsc needs to learn SSL and fast.
            // http://www.chromium.org/Home/chromium-security/prefer-secure-origins-for-powerful-new-features
            // DOMException: RsaHashedKeyGenParams: publicExponent: Missing or not a Uint8Array
            // RsaHashedKeyGenParams: publicExponent: Missing or not a Uint8Array
            // RsaHashedKeyGenParams: hash: Missing or not a dictionary

            // perhaps jsc does not yet do uin8 arrays correctly?


            // https://mobilepki.org/jcs/webcrypto

            //var publicExponent = new Uint8ClampedArray(new byte[] { 0x01, 0x00, 0x01 });
            var publicExponent = new Uint8Array(new byte[] { 0x01, 0x00, 0x01 });

            // http://www.w3.org/TR/webcrypto-usecases/
            // https://github.com/WebKitNix/webkitnix/blob/master/LayoutTests/crypto/subtle/rsassa-pkcs1-v1_5-generate-key.html
            // https://bugzilla.mozilla.org/show_bug.cgi?id=865789
            // https://docs.google.com/spreadsheet/ccc?key=0AiAcidBZRLxndE9LWEs2R1oxZ0xidUVoU3FQbFFobkE&usp=sharing#gid=1

            // https://mobilepki.org/jcs/webcrypto

            var sw = Stopwatch.StartNew();

            new IHTMLPre { "before generateKey" + new { sw.ElapsedMilliseconds } }.AttachToDocument();
            var value = Native.window.crypto.subtle.generateKey(
                //new { name = "RSASSA-PKCS1-v1_5", modulusLength = 2048, publicExponent = new byte[0x01, 0x00, 0x01] },
                //new { name = "RSASSA-PKCS1-v1_5", modulusLength = 2048, publicExponent = new byte[] { 0x01, 0x00, 0x01 } },
                //new { name = "RSASSA-PKCS1-v1_5", modulusLength = 16, publicExponent = new byte[] { 0x01, 0x00, 0x01 } },
                new
            {
                name = "RSASSA-PKCS1-v1_5",
                hash = new { name = "SHA-256" },


                modulusLength = 2048,
                publicExponent,

                //  RsaHashedKeyGenParams: hash: Algorithm: Not an object
            },
                false,
            //new[] { "encrypt", "decrypt" }
            new[] { "sign", "verify" }
            );

            // https://developer.mozilla.org/en-US/docs/Mozilla/JavaScript_code_modules/Promise.jsm/Promise
            new IHTMLPre { "after generateKey " + new { value, sw.ElapsedMilliseconds } }.AttachToDocument();

            // after generateKey {{ value = [object Object] }}

            //after generateKey { { value = [object Object], ElapsedMilliseconds = 1 } }
            //continue generateKey { { key = [object Object], ElapsedMilliseconds = 4427 } }

            value.then(
                key =>
            {
                new IHTMLPre { "continue generateKey " + new { key, sw.ElapsedMilliseconds } }.AttachToDocument();

            }
            );


        }

    }
}
