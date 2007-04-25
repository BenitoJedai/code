using ScriptCoreLib.Shared;

using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript;

using ScriptCoreLib.JavaScript.DOM.HTML;

namespace ScriptCoreLib.JavaScript.DOM
{
    [Script(HasNoPrototype=true)]
    public class IWindow : ISink
    {
        public IFunction Array;

        public string defaultStatus;

        public IHTMLDocument document;
        public IWindow opener;

        public void alert<T>(T a0) { }

        public bool confirm(string e) { return default(bool);  }

        public string prompt(string text, string value, string title)
        {
            return default(string);
        }

        public void print() { }

        /// <summary>
        /// brings window to foreground, if currently another browser window is active
        /// </summary>
        public void focus() {}
        public void blur() {}

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

        [Script(DefineAsStatic=true)]
        public IWindow open(string URL, string target, 
            int width, 
            int height,
            bool scrollbars)
        {
            IArray<string> f = new IArray<string>();

            f.push("width=" + width);
            f.push("height=" + height);
            f.push("scrollbars=" + (scrollbars ? "yes" : "no"));
           


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

        [Script(DefineAsStatic=true)]
        internal int setTimeout(EventHandler code, int time)
        {
            return setTimeout(((System.DelegateImpl)((object)code)).InvokePointer, time);
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
        internal int setInterval(EventHandler code, int time)
        {
            return setInterval(((System.DelegateImpl)((object)code)).InvokePointer, time);
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
        public event EventHandler<IEvent> onfocus
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
        public event EventHandler<IEvent> onblur
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
        public event EventHandler<IEvent> onload
        {
            [Script(DefineAsStatic = true)]
            add
            {
                base.InternalEvent(true, value, "load");
            }
            [Script(DefineAsStatic = true)]
            remove
            {
                base.InternalEvent(false, value, "load");
            }
        }
        #endregion

        #region event onunload
        public event EventHandler<IEvent> onunload
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

        #region event onunload
        public event Action<Confirmation> onbeforeunload
        {
            [Script(DefineAsStatic = true)]
            add
            {
                Func<IEvent, object> w =
                    delegate (IEvent e)
                    {
                        var c = new Confirmation();

                        value(c);

                        e.returnValue = c.Text;

                        return c.Text;
                    };

                base.InternalEvent(true, w, "beforeunload");
            }
            [Script(DefineAsStatic = true)]
            remove
            {
                throw new System.ScriptException("Not implemented");

                // TODO: via proxy

                //base.InternalEvent(false, value, "beforeunload");
            }
        }
        #endregion

        #region event onresize
        public event EventHandler<IEvent> onresize
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
        public event EventHandler<IEvent> onscroll
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

        [Script(UseCompilerConstants=true, OptimizedCode= @"
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
            [Script(DefineAsStatic=true)]
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
    }
}
