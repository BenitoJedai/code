using ScriptCoreLib.Shared;

using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript;

using ScriptCoreLib.JavaScript.DOM.HTML;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Diagnostics;

namespace ScriptCoreLib.JavaScript.DOM
{
    // http://mxr.mozilla.org/mozilla-central/source/dom/webidl/Window.webidl
    // http://mxr.mozilla.org/mozilla-central/source/dom/interfaces/base/nsIBrowserDOMWindow.idl
    // X:\opensource\github\WootzJs\WootzJs.Web\Window.cs
    // http://sharpkit.net/help/SharpKit.Html/SharpKit.Html/Window/
    // https://src.chromium.org/viewvc/blink/trunk/Source/core/frame/Window.idl
    // https://github.com/mono/mono/blob/a31c107f59298053e4ff17fd09b2fa617b75c1ba/mcs/class/Mono.WebBrowser/Mono.Mozilla/DOM/Window.cs
    // https://github.com/Reactive-Extensions/IL2JS/blob/master/Html/Microsoft/LiveLabs/Html/Window.cs



    [Script(InternalConstructor = true)]
    public partial class IWindow : IEventTarget
    {
        // https://slightlyoff.github.io/ServiceWorker/spec/service_worker/index.html#cache-objects
        // X:\jsc.svn\examples\javascript\test\TestCacheStorage\TestCacheStorage\Application.cs
        // 41 shows null? not implemented yet?
        [System.Obsolete("not available yet?")]
        public readonly CacheStorage caches;

        // tested by? what about web workers?
        // X:\jsc.svn\core\ScriptCoreLib\JavaScript\DOM\WorkerGlobalScope.cs
        public readonly Crypto crypto;

        public Storage sessionStorage;
        public Storage localStorage;

        public readonly History history;

        //public readonly URL URL;

        // http://www.whatwg.org/specs/web-apps/current-work/multipage/browsers.html#the-window-object
        public IWindow parent;
        public IWindow opener;
        public IWindow top;
        public IWindow self;
        public IWindow window;




        #region event onmessage
        public event System.Action<MessageEvent> onmessage
        {
            [Script(DefineAsStatic = true)]
            add
            {
                base.InternalEvent(true, value, "message");
            }
            [Script(DefineAsStatic = true)]
            remove
            {
                base.InternalEvent(false, value, "message");
            }
        }
        #endregion


        #region InternalConstructor
        public IWindow()
        {
        }

        private static IWindow InternalConstructor()
        {
            // what if we are a worker?
            // shall we talk to the oper?

            IWindow a = Native.window.open("about:blank", "_blank", 400, 400, false);

            return a;
        }
        #endregion

        //public IFunction Array;


        public string defaultStatus;

        public IHTMLDocument document;

        public void alert<T>(T a0) { }

        public bool confirm(string e) { return default(bool); }

        public string prompt(string text, string value, string title)
        {
            return default(string);
        }

        public void print() { }

        /// <summary>
        /// brings window to foreground, if currently another browser window is active
        /// </summary>
        public void focus() { }
        public void blur() { }


        public NavigatorInfo navigator;

        public void moveTo(int x, int y)
        {
        }

        // http://stackoverflow.com/questions/4479260/change-height-of-popup-window
        public void resizeTo(int x, int y)
        {
        }



        public string encodeURIComponent(string e)
        {
            return default(string);
        }

        public string decodeURIComponent(string e)
        {
            return default(string);
        }



        // not available in workers?
        public string escape(string e)
        {
            return default(string);
        }

        public string unescape(string e)
        {
            return default(string);
        }

        public IScreen screen;

        public string status;


        // available in global namespace for web workers?
        public bool isNaN(int i)
        { return default(bool); }

        #region open
        // http://www.devguru.com/Technologies/ecmascript/quickref/win_open.html
        public IWindow open(string URL, string target = "_blank") { return default(IWindow); }
        public IWindow open(string URL, string target, string features) { return default(IWindow); }

        [Script(DefineAsStatic = true)]
        public IWindow open(string URL, string target,
            int width,
            int height,
            bool scrollbars = false)
        {
            IArray<string> f = new IArray<string>();

            f.push("width=" + width);
            f.push("height=" + height);

            if (scrollbars)
                f.push("scrollbars=" + "yes");
            else
                f.push("scrollbars=" + "no");



            return open(URL, target, f.join(","));
        }
        #endregion








        #region event onfocus
        public event System.Action<IEvent> onfocus
        {
            [Script(DefineAsStatic = true)]
            add
            {
                base.InternalEvent(true, value, "focus");
            }
            [Script(DefineAsStatic = true)]
            remove
            {
                base.InternalEvent(false, value, "focus");
            }
        }
        #endregion

        #region event onblur
        public event System.Action<IEvent> onblur
        {
            [Script(DefineAsStatic = true)]
            add
            {
                base.InternalEvent(true, value, "blur");
            }
            [Script(DefineAsStatic = true)]
            remove
            {
                base.InternalEvent(false, value, "blur");
            }
        }
        #endregion


        #region event onerror
        public event System.Action<IErrorEvent> onerror
        {
            [Script(DefineAsStatic = true)]
            add
            {
                base.InternalEvent(true, value, "error");
            }
            [Script(DefineAsStatic = true)]
            remove
            {
                base.InternalEvent(false, value, "error");
            }
        }
        #endregion

        #region event onload
        public event System.Action<IEvent> onload
        {
            [Script(DefineAsStatic = true)]
            add
            {
                if (this.document == null)
                {
                    // Uncaught TypeError: Cannot read property 'readyState' of undefined 
                    // X:\jsc.svn\examples\javascript\WebWorkerExperiment\WebWorkerExperiment\Application.cs



                    // there is no document. nothing to load?

                    // raise the event
                    value(null);
                    return;
                }

                base.InternalEvent(true, value, "load");



                if (this.document.readyState == "complete")
                {
                    // raise the event
                    value(null);
                    return;
                }
            }
            [Script(DefineAsStatic = true)]
            remove
            {
                base.InternalEvent(false, value, "load");
            }
        }
        #endregion

        #region event onunload
        public event System.Action<IEvent> onunload
        {
            [Script(DefineAsStatic = true)]
            add
            {
                base.InternalEvent(true, value, "unload");
            }
            [Script(DefineAsStatic = true)]
            remove
            {
                base.InternalEvent(false, value, "unload");
            }
        }
        #endregion




        [Script]
        public class Confirmation
        {
            public string Text;
        }

        #region event onbeforeunload
        public event System.Action<Confirmation> onbeforeunload
        {
            [Script(DefineAsStatic = true)]
            add
            {
                System.Func<IEvent, object> w =
                    delegate(IEvent e)
                    {
                        var c = new Confirmation();

                        value(c);

                        // http://stackoverflow.com/questions/276660/how-can-i-override-the-onbeforeunload-dialog-and-replace-it-with-my-own
                        // http://www.coderanch.com/t/419881/HTML-JavaScript/Stopping-onbeforeunload-event

                        if (c.Text == null)
                        {
                            //e.PreventDefault();
                            return new IFunction("return void(0);").apply(0);
                        }

                        e.returnValue = c.Text;

                        return c.Text;
                    };

                base.InternalEvent(true, w, "beforeunload");
            }
            [Script(DefineAsStatic = true)]
            remove
            {
                throw new global::System.Exception("Not implemented");

                // TODO: via proxy

                //base.InternalEvent(false, value, "beforeunload");
            }
        }
        #endregion

        #region event onresize
        public event System.Action<IEvent> onresize
        {
            [Script(DefineAsStatic = true)]
            add
            {
                base.InternalEvent(true, value, "resize");
            }
            [Script(DefineAsStatic = true)]
            remove
            {
                base.InternalEvent(false, value, "resize");
            }
        }
        #endregion

        #region event onscroll
        public event System.Action<IEvent> onscroll
        {
            [Script(DefineAsStatic = true)]
            add
            {
                base.InternalEvent(true, value, "scroll");
            }
            [Script(DefineAsStatic = true)]
            remove
            {
                base.InternalEvent(false, value, "scroll");
            }
        }
        #endregion


        // https://developer.mozilla.org/en-US/docs/Web/API/Window.scrollTo
        public void scrollTo(int x, int y)
        {
        }

        public void close()
        {
        }

        public object eval(string e)
        {
            return null;
        }

        [Script(UseCompilerConstants = true, OptimizedCode = @"
    var s = {arg0}.self;

    if (s && s.innerHeight)
    {
        return s.innerHeight;
    }

    var d = {arg0}.document.documentElement;

    if (d && d.clientHeight)
    {
        return d.clientHeight;
    }
    return 0;
")]
        internal static int InternalHeight(IWindow w)
        {
            return 0;
        }

        [Script(UseCompilerConstants = true, OptimizedCode = @"
    var s = {arg0}.self;

    if (s && s.innerWidth)
    {
        return s.innerWidth;
    }

    var d = {arg0}.document.documentElement;

    if (d && d.clientWidth)
    {
        return d.clientWidth;
    }
    return 0;
")]
        internal static int InternalWidth(IWindow w)
        {
            return 0;
        }


        public int Height
        {
            [Script(DefineAsStatic = true)]
            get
            {
                return InternalHeight(this);
            }
        }

        public int Width
        {
            [Script(DefineAsStatic = true)]
            get
            {
                return InternalWidth(this);
            }
        }


        //window.Width / window.innerHeight;


        public double aspect
        {
            // X:\jsc.svn\examples\javascript\test\TestIScreen\TestIScreen\Application.cs

            // used by THREE.PerspectiveCamera
            [Script(DefineAsStatic = true)]
            get
            {
                // X:\jsc.svn\examples\javascript\WebGL\WebGLOBJExperiment\WebGLOBJExperiment\Application.cs

                return window.Width / (double)window.Height;
            }
        }





        [Script(OptimizedCode = @"return window.requestAnimationFrame ||
         window.webkitRequestAnimationFrame ||
         window.mozRequestAnimationFrame ||
         window.oRequestAnimationFrame ||
         window.msRequestAnimationFrame ||
         function(/* function FrameRequestCallback */ callback, /* DOMElement Element */ element) {
           window.setTimeout(callback, 1000/60);
         };"
            )]
        static IFunction __requestAnimationFrame()
        {
            return null;
        }


        // http://social.msdn.microsoft.com/Forums/en-US/30f3339c-5e04-4aa8-9a09-9be72d9d9a1b/how-can-you-use-await-with-existing-events
        [System.Obsolete("async.onframe")]
        public Task requestAnimationFrameAsync
        {
            [Script(DefineAsStatic = true)]
            get
            {
                var x = new TaskCompletionSource<object>();

                // tested by
                // X:\jsc.svn\examples\javascript\android\TextToSpeechExperiment\TextToSpeechExperiment\Application.cs
                this.requestAnimationFrame +=
                    delegate
                    {
                        x.SetResult(null);
                    };

                return x.Task;
            }
        }

        public event System.Action requestAnimationFrame
        {
            [Script(DefineAsStatic = true)]
            add
            {
                // https://developer.mozilla.org/en/DOM/window.requestAnimationFrame
                // tested by X:\jsc.svn\examples\javascript\My.Solutions.Pages.Templates\My.Solutions.Pages.Templates\Application.cs

                __requestAnimationFrame().apply(null, IFunction.OfDelegate(value));
            }
            [Script(DefineAsStatic = true)]
            remove
            {
                throw new System.NotSupportedException();
            }
        }



        // what do name every frame yield? as uses enterFrame, onframe seems decent
        // should it be part of extensions?
        // there are no extension events yet







    }
}
