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
using TestWebCryptoAsync;
using TestWebCryptoAsync.Design;
using TestWebCryptoAsync.HTML.Pages;
using System.Diagnostics;
using ScriptCoreLib.JavaScript.WebGL;
using System.Threading;

namespace TestWebCryptoAsync
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


            // https://developer.chrome.com/extensions/enterprise_platformKeys
            // Only non-extractable RSASSA-PKCS1-V1_5 keys with modulusLength up to 2048 can be generated. Each key can be used for signing data at most once.
            // ?

            // https://src.chromium.org/viewvc/chrome/trunk/src/content/child/webcrypto/jwk.cc?pathrev=257759
            // // RSA private key import is not currently supported
            // ImportRsaPublicKey
            // https://code.google.com/p/chromium/issues/detail?id=373550&q=ImportKey&colspec=ID%20Pri%20M%20Iteration%20ReleaseBlock%20Cr%20Status%20Owner%20Summary%20OS%20Modified
            // http://www.ibiblio.org/hhalpin/homepage/presentations/webcrypto/Overview.html
            // thevulnerability of RSA PKCS#1 v1.5 to variants of Bleichenbacher's attack and unauthenticated block ciphers, to be precise.

            // Update August 30, 2014: The Web Cryptography API has dropped support of the RSAES-PKCS1-v1_5 algorithm that was 
            // used here originally, so this post has been changed to use RSA-OAEP instead. 
            // There’s a new post with more information, including how to update Ubuntu 14.04 so that this algorithm will work in browsers.
            // haha.

            // http://blog.engelke.com/2014/08/29/changes-to-the-web-cryptography-api/
            // Chrome 37 made it to Stable a few days ago, and now supports the Web Cryptography API without needing to set a special flag. YAY!
            // The only public-key encryption and decryption algorithm in the spec now is RSA-OAEP. 
            // http://msdn.microsoft.com/en-us/library/ie/dn302338(v=vs.85).aspx

            new IHTMLButton { "generateKey in UI" }.AttachToDocument().onclick +=
                async delegate
            {
                new IHTMLElement(IHTMLElement.HTMLElementEnum.hr).AttachToDocument();
                var sw = Stopwatch.StartNew();
                var publicExponent = new Uint8Array(new byte[] { 0x01, 0x00, 0x01 });

                new IHTMLPre { "before generateKey" + new { sw.ElapsedMilliseconds } }.AttachToDocument();

                // http://blog.engelke.com/2014/08/23/public-key-cryptography-in-the-browser/

                var value = Native.crypto.subtle.generateKeyAsync(
                new
                {
                    //name = "RSASSA-PKCS1-v1_5",
                    name = "RSA-OAEP",
                    hash = new { name = "SHA-256" },

                    modulusLength = 2048,
                    publicExponent,
                },
                    false,
            new[] { "encrypt", "decrypt" }
                //new[] { "sign", "verify" }
                );

                // https://developer.mozilla.org/en-US/docs/Mozilla/JavaScript_code_modules/Promise.jsm/Promise
                new IHTMLPre { "after generateKey " + new { value, sw.ElapsedMilliseconds } }.AttachToDocument();

                var key = await value;

                // continue generateKey {{ privateKey = [object CryptoKey], publicKey = [object CryptoKey], ElapsedMilliseconds = 5021 }}
                new IHTMLPre { "continue generateKey " + new { key.privateKey, key.publicKey, sw.ElapsedMilliseconds } }.AttachToDocument();
            };

            #region generateKey in Worker
            new IHTMLButton { "generateKey in Worker" }.AttachToDocument().onclick +=
            async delegate
            {
                new IHTMLElement(IHTMLElement.HTMLElementEnum.hr).AttachToDocument();

                var sw = Stopwatch.StartNew();

                new IHTMLPre { "before generateKey" + new { sw.ElapsedMilliseconds, Thread.CurrentThread.ManagedThreadId } }.AttachToDocument();

                var __value = Task.Run(
                    async delegate
                {
                    var publicExponent = new Uint8Array(new byte[] { 0x01, 0x00, 0x01 });
                    var sw2 = Stopwatch.StartNew();

                    Console.WriteLine("worker before generateKeyAsync " + new { sw2.ElapsedMilliseconds, Thread.CurrentThread.ManagedThreadId });

                    var value = Native.crypto.subtle.generateKeyAsync(
                        new
                    {
                        //name = "RSASSA-PKCS1-v1_5",
                        name = "RSA-OAEP",
                        hash = new { name = "SHA-256" },


                        modulusLength = 2048,
                        publicExponent,

                        //  RsaHashedKeyGenParams: hash: Algorithm: Not an object
                    },
                        false,
                        new[] { "encrypt", "decrypt" }
                    //new[] { "sign", "verify" }
                    );
                    Console.WriteLine("worker after generateKeyAsync " + new { sw2.ElapsedMilliseconds, Thread.CurrentThread.ManagedThreadId });

                    var xkey = await value;
                    Console.WriteLine("worker continue generateKeyAsync " + new { sw2.ElapsedMilliseconds, Thread.CurrentThread.ManagedThreadId });

                    //                    0:6071ms worker before generateKeyAsync { { ElapsedMilliseconds = 0, ManagedThreadId = 10 } }
                    //0:6072ms worker after generateKeyAsync { { ElapsedMilliseconds = 2, ManagedThreadId = 10 } }
                    //0:6073ms worker Task Run function has returned { value_Task = { IsCompleted = false, Result =  }, value_TaskOfT = { IsCompleted = false, Result =  } }
                    //0:7090ms worker continue generateKeyAsync { { ElapsedMilliseconds = 1020, ManagedThreadId = 10 } }
                    return xkey;

                }
                );

                new IHTMLPre { " after generateKeyAsync " + new { sw.ElapsedMilliseconds, Thread.CurrentThread.ManagedThreadId } }.AttachToDocument();


                var __key = await __value;

                // continue generateKey {{ key = [object Object], ElapsedMilliseconds = 313 }}
                new IHTMLPre { "continue generateKey " + new { __key.privateKey, __key.publicKey, sw.ElapsedMilliseconds, Thread.CurrentThread.ManagedThreadId } }.AttachToDocument();
            };
            #endregion


        }

    }
}
