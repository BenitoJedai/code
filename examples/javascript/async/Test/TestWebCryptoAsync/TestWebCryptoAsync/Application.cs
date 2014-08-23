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



            new IHTMLButton { "generateKey in UI" }.AttachToDocument().onclick +=
                async delegate
            {
                new IHTMLElement(IHTMLElement.HTMLElementEnum.hr).AttachToDocument();
                var sw = Stopwatch.StartNew();
                var publicExponent = new Uint8Array(new byte[] { 0x01, 0x00, 0x01 });

                new IHTMLPre { "before generateKey" + new { sw.ElapsedMilliseconds } }.AttachToDocument();

                var value = Native.crypto.subtle.generateKeyAsync(
                new
                {
                    name = "RSASSA-PKCS1-v1_5",
                    hash = new { name = "SHA-256" },

                    modulusLength = 2048,
                    publicExponent,
                },
                    false,
            //new[] { "encrypt", "decrypt" }
            new[] { "sign", "verify" }
                );

                // https://developer.mozilla.org/en-US/docs/Mozilla/JavaScript_code_modules/Promise.jsm/Promise
                new IHTMLPre { "after generateKey " + new { value, sw.ElapsedMilliseconds } }.AttachToDocument();

                var key = await value;

                // continue generateKey {{ key = [object Object], ElapsedMilliseconds = 313 }}
                new IHTMLPre { "continue generateKey " + new { key.privateKey, key.publicKey, sw.ElapsedMilliseconds } }.AttachToDocument();
            };

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

        }

    }
}
