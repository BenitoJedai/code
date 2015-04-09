using ScriptCoreLib.Shared;
using System;

using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using System.Text;
using System.IO;
using ScriptCoreLib.JavaScript.BCLImplementation.System;
using ScriptCoreLib.Shared.BCLImplementation.System;
using System.Threading.Tasks;
using ScriptCoreLib.JavaScript.WebGL;



namespace ScriptCoreLib.JavaScript
{
    // http://www.devguru.com/Technologies/ecmascript/quickref/js_property.html
    using SpawnItem = Pair<string, System.Action<IHTMLElement>>;





    [Script]
    // C# 6 shall import this static type and make members available!
    public static partial class Native
    {
		// http://blogs.msdn.com/b/ie/archive/2015/02/18/bringing-asm-js-to-the-chakra-javascript-engine-in-windows-10.aspx

		// http://www.zdnet.com/article/microsoft-is-building-a-new-browser-as-part-of-its-windows-10-push/
		// http://www.zdnet.com/article/microsoft-ready-to-show-off-windows-10-mobile-sku-on-january-21/

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
        // http://msdn.microsoft.com/en-us/library/ms235408(v=vs.90).aspx
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201501/20150115
        //Error BC31429	'Document' is ambiguous because multiple kinds of members with this name exist in class 'Native'.	TestXElementLiteral Application.vb  32
        // visible in vb?
        [CLSCompliant(false)]
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
                return delegate (IEvent e)
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
                // X:\jsc.svn\examples\javascript\canvas\FormsSVGTreeView\FormsSVGTreeView\Application.cs

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
        // X:\jsc.svn\examples\javascript\test\TestServiceWorker\TestServiceWorker\Application.cs
        static public ServiceWorkerGlobalScope serviceworker;


		// implemented at?
		// X:\jsc.svn\core\ScriptCoreLib\JavaScript\WebGL\Uint8ClampedArray.cs
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




        public static void __ToBase64String()
        {
            // X:\jsc.svn\examples\javascript\WebGL\WebGLVRCreativeLeadership\WebGLVRCreativeLeadership\Application.cs
            //return;

            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201502/20150226
            //Console.WriteLine("__ToBase64String");

            // X:\jsc.svn\examples\javascript\test\TestPackageAsApplication\TestPackageAsApplication\Application.cs
            // x:\jsc.svn\examples\javascript\test\testutf8frombase64stringordefault\testutf8frombase64stringordefault\application.cs

            // use .self instead for workers?
            // is our version broken?
            __Convert.InternalToBase64String =
                x => (string)new IFunction("return window.btoa(this);").apply(
                        __String.__fromCharCode(x)
                    );

        }

        static void __RTCPeerConnection()
        {
            // http://src.chromium.org/viewvc/blink/trunk/Source/modules/mediastream/WindowMediaStream.idl
            // X:\jsc.svn\examples\javascript\p2p\RTCPeerIPAddress\RTCPeerIPAddress\Application.cs

            // fix RTCPeerConnection
            var w = self as dynamic;

            // is it available for service workers?

            if ((object)w.RTCPeerConnection == null)
            {
                w.RTCPeerConnection = w.webkitRTCPeerConnection;

            }

        }

        static Native()
        {

            //at ScriptCoreLib.JavaScript.Native.__Uint8ClampedArray() in x:\jsc.svn\core\ScriptCoreLib\JavaScript\WebGL\Uint8ClampedArray.cs:line 19
            //at ScriptCoreLib.JavaScript.Native..cctor() in x:\jsc.svn\core\ScriptCoreLib\JavaScript\Native.cs:line 185

            __Uint8ClampedArray();


            __RTCPeerConnection();


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

                __initialize_identity();


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
                            delegate (IHTMLElement e)
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
                        delegate (IHTMLElement v)
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


        //<exception>: URIError: URI malformed
        //b: "%1B%19%13%CA%D1hJ%E9%14%1B-%F3%DA%D1%AC%5E%09%B2%1EX%B0%1BI%B2%5E%E1%E5%5B%98%EF%0F"

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
