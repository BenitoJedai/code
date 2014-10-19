using ScriptCoreLib.Shared;
using System;

using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;

namespace ScriptCoreLib.JavaScript
{
    // http://www.devguru.com/Technologies/ecmascript/quickref/js_property.html
    using SpawnItem = Pair<string, System.Action<IHTMLElement>>;
    using System.Text;
    using System.IO;
    using ScriptCoreLib.JavaScript.BCLImplementation.System;
    using ScriptCoreLib.Shared.BCLImplementation.System;
    using System.Threading.Tasks;
    using ScriptCoreLib.JavaScript.WebGL;




    [Script]
    // C# 6 shall import this static type and make members available!
    public static partial class Native
    {
        // http://jim.com/security/replacing_TCP.html
        //According google research 400 milliseconds reduces usage by 0.76%, or roughly two percent per second of delay

        //#region window
        //[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        //[Obsolete("window", true)]
        //[Script(ExternalTarget = "window")]
        //static public IWindow Window;

        //// web worker will not have this. this is the global this object
        //#endregion

        // 60 issues to deal with before we can recompile
        const bool error = false;

        #region document
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        [Script(ExternalTarget = "document")]
        [Obsolete("document", error)]
        // 22 matches
        static public IHTMLDocument Document;


        #endregion


        // window.Math ? Math module
        [Script(ExternalTarget = "Math"), System.Obsolete("Use global::System.Math instead!", false)]
        static internal IMath Math;


        #region screen
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        [Obsolete("screen", error)]
        [Script(ExternalTarget = "screen")]
        static public IScreen Screen;

        #endregion



        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Obsolete("To be moved out of CoreLib or removed")]
        public static System.Action<IEvent> DisabledEventHandler
        {
            get
            {
                return delegate(IEvent e)
                {
                    e.PreventDefault();
                    e.StopPropagation();
                };
            }
        }

        // dynamic ?
        #region self
        static object __self;

        public static object self
        {
            get
            {
                if (__self == null)
                {
                    // x:\jsc.svn\examples\javascript\OmniWebWorkerExperiment\OmniWebWorkerExperiment\Application.cs
                    __self = new IFunction("return this;").apply(null);
                }

                return __self;
            }
        }
        #endregion


        static public IWindow window;


        [Obsolete("experimental")]
        public static ShadowRoot shadow
        {
            get
            {
                // to be used in chrome extensions?
                // X:\jsc.svn\examples\javascript\Test\TestShadowSelectByClass\TestShadowSelectByClass\Application.cs
                // X:\jsc.svn\examples\javascript\chrome\extensions\ChromeExtensionShadowExperiment\ChromeExtensionShadowExperiment\Application.cs

                return Native.document.documentElement.shadow;
            }
        }
        //public static class async
        //{ 
        //    onframe   
        //}


        // alias for window.document. not available for web workers
        static public IHTMLDocument document;


        [Obsolete("experimental")]
        public static IHTMLBody body
        {
            // X:\jsc.svn\examples\javascript\future\HistoricSnapshotMashup\HistoricSnapshotMashup\Application.cs
            get { return document.body; }
        }

        static public IScreen screen;
        static public DedicatedWorkerGlobalScope worker;
        static public SharedWorkerGlobalScope sharedworker;

        // tested by ?
        static public ServiceWorkerGlobalScope serviceworker;


        // implemented at?
        static partial void __Uint8ClampedArray();


        public static Crypto crypto
        {
            get
            {
                // tested by
                // X:\jsc.svn\examples\javascript\async\Test\TestWebCryptoAsync\TestWebCryptoAsync\Application.cs

                if (worker != null)
                    return worker.crypto;

                return Native.window.crypto;
            }
        }


        // http://jim.com/security/replacing_TCP.html
        // You will notice that the server only allocates memory and does heavy computation *after*
        // the client has successfully performed proof of work and shown that it is indeed capable 
        // of receiving data sent to the advertised network address.
        // x:\jsc.svn\examples\javascript\async\asyncworkersourcesha1\asyncworkersourcesha1\application.cs
        [Obsolete("experimental. allows us to sign/encrypt our data uploads for our session.")]
        public static readonly Task<KeyPair> identity;



        public static void __ToBase64String()
        {
            // X:\jsc.svn\examples\javascript\test\TestPackageAsApplication\TestPackageAsApplication\Application.cs

            // use .self instead for workers?
            __Convert.InternalToBase64String =
                x => (string)new IFunction("return window.btoa(this);").apply(
                        __String.__fromCharCode(x)
                    );

        }



        static Native()
        {


            __Uint8ClampedArray();


            // what is it?
            if (Expando.InternalIsMember(self, "document")
                && Expando.InternalIsMember(self, "screen"))
            {
                // should be a window with a document
                window = (IWindow)self;

                document = window.document;
                screen = window.screen;

                // were ApplicationWebserviceFields decrypted befoe this point?
                __ToBase64String();

                // https:// !!!

                #region identity
                // X:\jsc.svn\examples\javascript\android\Test\TestAndroidCryptoKeyGenerate\TestAndroidCryptoKeyGenerate\Application.cs

                if (Native.crypto != null)
                    if (Native.crypto.subtle != null)
                    {
                        // do we even have crypto capability?
                        // X:\jsc.svn\examples\javascript\async\Test\TestWebCryptoAsync\TestWebCryptoAsync\Application.cs
                        // X:\jsc.svn\examples\javascript\Test\TestCryptoUIThreadIdentityKeyPair\TestCryptoUIThreadIdentityKeyPair\Application.cs
                        // X:\jsc.svn\examples\javascript\Test\TestEncryptedPrivateFields\TestEncryptedPrivateFields\ApplicationWebService.cs

                        // I/chromium( 6625): [INFO:CONSOLE(1751)] "Uncaught TypeError: Cannot call method 'generateKey' of undefined", source: http://127.0.0.1:14272/view-source (1751)

                        try
                        {
                            var publicExponent = new Uint8Array(new byte[] { 0x01, 0x00, 0x01 });

                            // http://social.msdn.microsoft.com/Forums/en-US/d12a2c2e-22b0-44ab-bab5-8202a0c8edcc/rsa-signature-with-rsassapkcs1v15?forum=csharpgeneral
                            // Asymmetric private keys should never be stored verbatim or in plain text on the local computer. If you need to store a private key, you should use a key container. 
                            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201408/20140831

                            Native.identity = Native.crypto.subtle.generateKeyAsync(
                                    new
                                {
                                    name = "RSASSA-PKCS1-v1_5",

                                    // X:\jsc.svn\core\ScriptCoreLib.Ultra.Library\ScriptCoreLib.Ultra.Library\Extensions\TcpListenerExtensions.cs
                                    // for SSL we seem to need to use SHA1 tho?
                                    hash = new { name = "SHA-256" },


                                    modulusLength = 2048,
                                    publicExponent,

                                    //  RsaHashedKeyGenParams: hash: Algorithm: Not an object
                                },
                                    false,
                                //new[] { "encrypt", "decrypt" }
                                    new[] { "sign", "verify" }
                                );
                        }
                        catch
                        {
                            // no crypto?
                        }
                    }
                #endregion


                return;
            }



            // are all browsers reporting it as the same type?
            // what if it could have multiple names?
            Native.serviceworker = self as ServiceWorkerGlobalScope;
            // X:\jsc.svn\examples\javascript\Test\TestServiceWorkerRegistrations\TestServiceWorkerRegistrations\Application.cs

            if (Native.serviceworker == null)
            {
                // ok detect other modes...


                if (Expando.InternalIsMember(self, "importScripts"))
                {

                    if (Expando.InternalIsMember(self, "postMessage"))
                    {
                        // now what. are we running as a web worker?
                        // WorkerGlobalScope
                        // DedicatedWorkerGlobalScope
                        // DedicatedWorkerContext

                        worker = (DedicatedWorkerGlobalScope)self;
                        return;
                    }

                    // now what. are we running as a web worker?
                    // WorkerGlobalScope
                    // DedicatedWorkerGlobalScope
                    // DedicatedWorkerContext

                    sharedworker = (SharedWorkerGlobalScope)self;
                }
            }


        }

        public static CSSStyleRuleMonkier css
        {
            get
            {
                // tested by
                // x:\jsc.svn\examples\javascript\webgl\heatzeekerrts\heatzeekerrts\application.cs

                if (Native.document == null)
                {
                    // X:\jsc.svn\core\ScriptCoreLib.Windows.Forms\ScriptCoreLib.Windows.Forms\JavaScript\BCLImplementation\System\Windows\Forms\ToolStrip\ToolStripButton.cs

                    return new CSSStyleRuleMonkier { };

                }


                // http://www.w3schools.com/cssref/sel_root.asp
                var x = Native.document.documentElement.css;

                //Console.WriteLine("Native.css");
                // X:\jsc.svn\examples\javascript\Forms\Test\CSSFormsButtonCursor\CSSFormsButtonCursor\Application.cs

                // root is special
                //x.__isroot = true;

                //x.descendantMode = true;

                return x;
            }
        }

        [System.Obsolete]
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public static void Spawn(params SpawnItem[] e)
        {
            foreach (var x in e)
            {
                Native.Spawn(x.A, x.B);
            }
        }

        /// <summary>
        /// Searches all tags by className, and spawns a control in that element at window load
        /// </summary>
        /// <param name="e">className</param>
        /// <param name="Spawn">delegate with owner element</param>
        [System.Obsolete]
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public static void Spawn(string id, System.Action<IHTMLElement> Spawn)
        {
            try
            {
                System.Console.WriteLine("spawn on load: " + id);

                //if (Native.Window == null)


                Native.window.onload +=
                    delegate
                    {
                        Native.document.getElementsByClassName(id).ForEach(
                            delegate(IHTMLElement e)
                            {
                                System.Console.WriteLine("spawn: {" + id + "}");

                                Spawn(e);
                            });
                    };
            }
            catch
            {
                // wont work within web worker
            }
        }


        [System.Obsolete]
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public static void Spawn(string id, System.Action<IHTMLElement, string> s)
        {
            System.Console.WriteLine("spawn on load: " + id);

            Native.window.onload +=
                delegate
                {


                    Native.document.getElementsByClassName(id).ForEach(
                        delegate(IHTMLElement v)
                        {
                            System.Console.WriteLine("spawn: {" + id + "}");


                            s(v, id);
                        }

                    );



                };
        }

        [System.Obsolete]
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        internal static void SpawnInline(string classname, System.Action<IHTMLElement> h)
        {
            Native.Document.getElementsByClassName(classname + ":inline").ForEach(h);
        }

        public static int setTimeout(Action yield, int ms)
        {
            if (Native.window != null)
                return Native.window.setTimeout(yield, ms);

            return Native.worker.setTimeout(yield, ms);
        }

        public static int setInterval(Action yield, int ms)
        {
            if (Native.window != null)
                return Native.window.setInterval(yield, ms);

            return Native.worker.setInterval(yield, ms);
        }

        public static void clearTimeout(int i)
        {
            if (Native.window != null)
            {
                Native.window.clearTimeout(i);
                return;
            }

            Native.worker.clearTimeout(i);
        }

        public static void clearInterval(int i)
        {
            if (Native.window != null)
            {
                Native.window.clearInterval(i);
                return;
            }

            Native.worker.clearInterval(i);
        }

        public static string encodeURIComponent(string i)
        {
            if (Native.window != null)
            {
                return Native.window.encodeURIComponent(i);
            }

            return Native.worker.encodeURIComponent(i);
        }

        public static string decodeURIComponent(string i)
        {
            if (Native.window != null)
            {
                return Native.window.decodeURIComponent(i);
            }

            return Native.worker.decodeURIComponent(i);
        }

        public static string escape(string i)
        {
            if (Native.window != null)
            {
                return Native.window.escape(i);
            }

            return Native.worker.escape(i);
        }

        public static string unescape(string i)
        {
            if (Native.window != null)
            {
                return Native.window.unescape(i);
            }

            return Native.worker.unescape(i);
        }

        public static Database openDatabase(
            string name = "database.sqlite",
            string version = "1.0",
            //string version = "",
            string displayName = "Web SQL",

            // AppCache allows 5MB, how much for db?
            ulong estimatedSize = 2 * 1024 * 1024,

            Action<Database> creationCallback = null
        )
        {
            // X:\jsc.svn\examples\javascript\Test\TestWebSQLDatabase\TestWebSQLDatabase\Application.cs

            if (Native.window != null)
            {
                return Native.window.openDatabase(name, version, displayName, estimatedSize, creationCallback);
            }

            return Native.worker.openDatabase(name, version, displayName, estimatedSize, creationCallback);
        }
    }




}
