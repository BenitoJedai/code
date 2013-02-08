using ScriptCoreLib.Shared;

using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript;

using ScriptCoreLib.JavaScript.DOM.HTML;

namespace ScriptCoreLib.JavaScript.DOM
{


    [Script(InternalConstructor = true)]
    public class IWindow : ISink
    {
        #region InternalConstructor
        public IWindow()
		{
		}

        private static IWindow InternalConstructor()
		{
            IWindow a = Native.Window.open("about:blank", "_blank", 400, 400, false);

			return a;
		}
        #endregion

        public IFunction Array;

 
        public string defaultStatus;

        public IHTMLDocument document;
        public IWindow opener;

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

        [Script]
        public class NavigatorInfo
        {
            public string userAgent;
            public string appVersion;

            [Script]
            public class PluginInfo
            {
                public string description;
            }

            // http://www.comptechdoc.org/independent/web/cgi/javamanual/javamimetype.html
            // http://www.irt.org/xref/MimeType.htm
            [Script]
            public class MimeTypeInfo
            {
                public string description;
                public string type;
            }


            public IArray<MimeTypeInfo> mimeTypes;
            public IArray<PluginInfo> plugins;
        }

        public NavigatorInfo navigator;

        public void moveTo(int x, int y)
        {
        }

        public string escape(string e)
        {
            return default(string);
        }

        public string unescape(string e)
        {
            return default(string);
        }

        public string status;

        public bool isNaN(int i)
        { return default(bool); }

        #region open
        // http://www.devguru.com/Technologies/ecmascript/quickref/win_open.html
        public IWindow open(string URL, string target) { return default(IWindow); }
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
                f.push("scrollbars=" +  "no");



            return open(URL, target, f.join(","));
        }
        #endregion



        #region async
        #region setTimeout
        public int setTimeout(string code, int time)
        {
            return default(int);
        }

        public int setTimeout(IFunction code, int time)
        {
            return default(int);
        }

        [Script(DefineAsStatic = true)]
        internal int setTimeout(System.Action code, int time)
        {
            return setTimeout(((BCLImplementation.System.__Delegate)((object)code)).InvokePointer, time);
        }
        #endregion

        #region setInterval
        public int setInterval(string code, int time)
        {
            return default(int);
        }

        public int setInterval(IFunction code, int time)
        {
            return default(int);
        }

        [Script(DefineAsStatic = true)]
        internal int setInterval(System.Action code, int time)
        {
            return setInterval(((BCLImplementation.System.__Delegate)((object)code)).InvokePointer, time);
        }
        #endregion


        public void clearTimeout(int i)
        {

        }

        public void clearInterval(int i)
        {

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

        #region event onload
        public event System.Action<IEvent> onload
        {
            [Script(DefineAsStatic = true)]
            add
            {
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

		/// <summary>
		/// DatabaseName, DatabaseVersion, DisplayName, EstimatedSize
		/// 
		/// see: http://creativepark.net/blog/entry/id/1191
		/// </summary>
		public IFunction openDatabase;




        public event System.Action requestAnimationFrame
		{
			[Script(DefineAsStatic = true)]
			add
			{
                // https://developer.mozilla.org/en/DOM/window.requestAnimationFrame

                #region requestAnimFrame
                var requestAnimFrame = (IFunction)new IFunction(
                    @"return window.requestAnimationFrame ||
         window.webkitRequestAnimationFrame ||
         window.mozRequestAnimationFrame ||
         window.oRequestAnimationFrame ||
         window.msRequestAnimationFrame ||
         function(/* function FrameRequestCallback */ callback, /* DOMElement Element */ element) {
           window.setTimeout(callback, 1000/60);
         };"
                ).apply(null);
                #endregion

                requestAnimFrame.apply(null, IFunction.OfDelegate(value));
			}
			[Script(DefineAsStatic = true)]
			remove
			{
				throw new System.NotSupportedException();
			}
		}
    }
}
