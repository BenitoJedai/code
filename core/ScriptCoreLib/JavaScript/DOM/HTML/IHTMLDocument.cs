using ScriptCoreLib.Shared;

using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.DOM;



// http://www.w3.org/TR/DOM-Level-2-HTML/html.html#ID-5353782642
// http://www.w3.org/TR/DOM-Level-2-HTML/html.html#ID-26809268

namespace ScriptCoreLib.JavaScript.DOM.HTML
{
    // http://mxr.mozilla.org/mozilla-central/source/dom/webidl/HTMLDocument.webidl
    // http://mxr.mozilla.org/mozilla-central/source/dom/interfaces/html/nsIDOMHTMLDocument.idl
    // http://src.chromium.org/viewvc/blink/trunk/Source/core/html/HTMLDocument.idl
    // http://www.whatwg.org/specs/web-apps/current-work/multipage/dom.html#current-document-readiness
    // https://github.com/mono/mono/blob/master/mcs/class/Managed.Windows.Forms/System.Windows.Forms/HtmlDocument.cs
    // https://github.com/Reactive-Extensions/IL2JS/blob/master/Html/Microsoft/LiveLabs/Html/Document.cs

    [Script(HasNoPrototype = true)]
    public partial class IHTMLDocument : IDocument<IHTMLElement>
    {
        public IWindow defaultView;




        /// <summary>
        /// Enable multitouch in Minefield by setting this field to true.
        /// </summary>
        [System.Obsolete]
        internal bool multitouchData;

        // http://www.w3schools.com/jsref/prop_doc_readystate.asp
        // http://msdn.microsoft.com/en-us/library/ie/ms534359(v=vs.85).aspx
        // http://www.w3.org/TR/2006/WD-XMLHttpRequest-20060405/#dfn-readystate
        // https://developer.mozilla.org/en-US/docs/DOM/document.readyState
        public readonly string readyState;




        public IHTMLStyle[] styleSheets;

        #region namespaces
        // http://msdn2.microsoft.com/en-us/library/ms535854.aspx
        [Script(HasNoPrototype = true)]
        internal class IMSNamespace
        {

        }

        // http://msdn2.microsoft.com/en-us/library/ms537470.aspx#
        [Script(HasNoPrototype = true)]
        internal class IMSNamespaceCollection
        {
            public IMSNamespace item(int i)
            {
                return default(IMSNamespace);
            }

            public IMSNamespace item(string sNamespace)
            {
                return default(IMSNamespace);
            }

            public IMSNamespace add(string sNamespace, string sUrn)
            {
                return default(IMSNamespace);
            }

            public int length;
        }

        internal IMSNamespaceCollection namespaces;

        #endregion


        //// http://msdn.microsoft.com/workshop/author/dhtml/reference/methods/querycommandsupported.asp
        //public bool queryCommandSupported(string sCommand)
        //{
        //    return default(bool);
        //}

        // http://msdn.microsoft.com/workshop/author/dhtml/reference/methods/execcommand.asp
        public bool execCommand(string sCommand, bool bUserInterface, object value)
        {
            return default(bool);
        }


        public object selection;

        // http://www.webreference.com/js/column12/crossbrowser.html

        //interface HTMLDocument : Document {





        public readonly string referrer;
        public readonly string domain;
        public readonly string URL;
        public IHTMLBody body;
        //  readonly attribute HTMLCollection  images;
        //  readonly attribute HTMLCollection  applets;
        //  readonly attribute HTMLCollection  links;
        //  readonly attribute HTMLCollection  forms;
        //  readonly attribute HTMLCollection  anchors;
        public string cookie;

        public IHTMLElement head
        {
            [Script(DefineAsStatic = true)]
            get
            {
                var h = Native.document.getElementsByTagName("head");

                if (h.Length == 0)
                    return null;

                return h[0];
            }
        }


        public ILocation location;

        // http://www.devguru.com/Technologies/ecmascript/quickref/doc_open.html
        public IHTMLDocument open(string mime, string replace) { return default(IHTMLDocument); }
        public void close() { }
        public void write(string a0) { }
        public void writeln(string a0) { }

        public INode[] getElementsByName(string e) { return default(INode[]); }



        public IHTMLElement createElement(IHTMLElement.HTMLElementEnum e)
        {
            return default(IHTMLElement);
        }

        [Script(DefineAsStatic = true)]
        public IArray<IHTMLElement> getElementsByClassName(string tagName, string className)
        {
            return new IArray<IHTMLElement>(getElementsByTagName(tagName),
                delegate(IArray<IHTMLElement>.IncludeArgs p)
                {
                    bool b = false;

                    try
                    {
                        b = p.Item.className == className;
                    }
                    catch
                    {
                        b = false;
                    }

                    p.Include = b;
                }
                );
        }

        [Script(DefineAsStatic = true)]
        [System.Obsolete("To be refactored.", true)]
        public void ForEachClassName(string className, System.Action<IHTMLElement> handler)
        {
            throw new System.NotImplementedException();
            //getElementsByClassName(className).ForEach(handler);
        }

        [Script(DefineAsStatic = true)]
        public IArray<IHTMLElement> getElementsByClassName(string className)
        {
            return getElementsByClassName("*", className);
        }
        //};

        public IHTMLDocument open()
        {
            return default(IHTMLDocument);
        }

        [Script(DefineAsStatic = true)]
        public IHTMLDocument open(bool replace)
        {
            if (replace)
                return open("text/html", "replace");

            return open("text/html", "");
        }


        #region event onclick
        public event System.Action<IEvent> onclick
        {
            [Script(DefineAsStatic = true)]
            add
            {
                base.InternalEvent(true, value, "click");
            }
            [Script(DefineAsStatic = true)]
            remove
            {
                base.InternalEvent(false, value, "click");
            }
        }
        #endregion




        #region event onkeydown
        public event System.Action<IEvent> onkeydown
        {
            [Script(DefineAsStatic = true)]
            add
            {
                base.InternalEvent(true, value, "keydown");
            }
            [Script(DefineAsStatic = true)]
            remove
            {
                base.InternalEvent(false, value, "keydown");
            }
        }
        #endregion

        #region event onkeypress
        public event System.Action<IEvent> onkeypress
        {
            [Script(DefineAsStatic = true)]
            add
            {
                base.InternalEvent(true, value, "keypress");
            }
            [Script(DefineAsStatic = true)]
            remove
            {
                base.InternalEvent(false, value, "keypress");
            }
        }
        #endregion


        #region event onkeypress
        public event System.Action<IEvent> onkeyup
        {
            [Script(DefineAsStatic = true)]
            add
            {
                base.InternalEvent(true, value, "keyup");
            }
            [Script(DefineAsStatic = true)]
            remove
            {
                base.InternalEvent(false, value, "keyup");
            }
        }
        #endregion

        #region event onmousemove
        public event System.Action<IEvent> onmousemove
        {
            [Script(DefineAsStatic = true)]
            add
            {
                base.InternalEvent(true, value, "mousemove");
            }
            [Script(DefineAsStatic = true)]
            remove
            {
                base.InternalEvent(false, value, "mousemove");
            }
        }
        #endregion
        #region event onmousedown
        public event System.Action<IEvent> onmousedown
        {
            [Script(DefineAsStatic = true)]
            add
            {
                base.InternalEvent(true, value, "mousedown");
            }
            [Script(DefineAsStatic = true)]
            remove
            {
                base.InternalEvent(false, value, "mousedown");
            }
        }
        #endregion
        #region event onmouseup
        public event System.Action<IEvent> onmouseup
        {
            [Script(DefineAsStatic = true)]
            add
            {
                base.InternalEvent(true, value, "mouseup");
            }
            [Script(DefineAsStatic = true)]
            remove
            {
                base.InternalEvent(false, value, "mouseup");
            }
        }
        #endregion
        #region event onmouseover
        public event System.Action<IEvent> onmouseover
        {
            [Script(DefineAsStatic = true)]
            add
            {
                base.InternalEvent(true, value, "mouseover");
            }
            [Script(DefineAsStatic = true)]
            remove
            {
                base.InternalEvent(false, value, "mouseover");
            }
        }
        #endregion
        #region event onmouseout
        public event System.Action<IEvent> onmouseout
        {
            [Script(DefineAsStatic = true)]
            add
            {
                base.InternalEvent(true, value, "mouseout");
            }
            [Script(DefineAsStatic = true)]
            remove
            {
                base.InternalEvent(false, value, "mouseout");
            }
        }
        #endregion
        #region event oncontextmenu
        public event System.Action<IEvent> oncontextmenu
        {
            [Script(DefineAsStatic = true)]
            add
            {
                base.InternalEvent(true, value, "contextmenu");
            }
            [Script(DefineAsStatic = true)]
            remove
            {
                base.InternalEvent(false, value, "contextmenu");
            }
        }
        #endregion

        [Script(DefineAsStatic = true)]
        public IArray<T> getElementsByClassName<T>(string className) where T : IHTMLElement
        {
            return (IArray<T>)(object)getElementsByClassName(className);
        }

        internal string designMode;

        /// <summary>
        /// http://code.google.com/p/doctype/wiki/DocumentDesignModeProperty
        /// 
        /// http://stackoverflow.com/questions/443033/editable-div-element
        /// </summary>
        public bool DesignMode
        {
            [Script(DefineAsStatic = true)]
            set
            {
                if (value)
                {
                    this.designMode = "on";
                }
                else
                {
                    this.designMode = "off";
                }
            }
        }


        // ff
        internal IEvent createEvent(string p)
        {
            return null;
        }


        // https://dvcs.w3.org/hg/fullscreen/raw-file/tip/Overview.html
        //public IHTMLElement fullscreenElement;
        //public bool fullscreenEnabled;




        [Script(OptimizedCode = @"
if (that.exitFullscreen) {
    that.exitFullscreen();
}
else if (that.mozCancelFullScreen) {
    that.mozCancelFullScreen();
}
else if (that.webkitCancelFullScreen) {
    that.webkitCancelFullScreen();
}"
            )]
        static void __exitFullscreen(IHTMLDocument that)
        {
        }


        [Script(DefineAsStatic = true)]
        public void exitFullscreen()
        {
            // http://tutorialzine.com/2012/02/enhance-your-website-fullscreen-api/
            // http://johndyer.name/native-fullscreen-javascript-api-plus-jquery-plugin/

            __exitFullscreen(this);
        }




        [Script(OptimizedCode = @"
		if (that.pointerLockElement) {
		    return that.pointerLockElement;
		}
		else if (that.webkitPointerLockElement) {
		    return that.webkitPointerLockElement;
		}
                    return null;
                "
      )]
        static IHTMLElement __pointerLockElement(IHTMLDocument that)
        {
            return null;
        }


        // http://dvcs.w3.org/hg/pointerlock/raw-file/default/index.html
        public IHTMLElement pointerLockElement
        {
            [Script(DefineAsStatic = true)]
            get
            {
                return __pointerLockElement(this);
            }
        }




        public IHTMLElement elementFromPoint(int x, int y)
        {
            return default(IHTMLElement);
        }






        [Script(OptimizedCode = @"
		if (that.exitPointerLock) {
		    return that.exitPointerLock();
		}
		else if (that.webkitExitPointerLock) {
		    return that.webkitExitPointerLock();
		}
                "
)]
        static void __exitPointerLock(IHTMLDocument that)
        {
        }


        [Script(DefineAsStatic = true)]
        public void exitPointerLock()
        {
            __exitPointerLock(this);
        }





        public IHTMLElementGrouping this[IHTMLElement.HTMLElementEnum selectorByNodeName]
        {
            [Script(DefineAsStatic = true)]
            get
            {
                // allow handlers

                return new IHTMLElementGrouping
                {
                    contextElement = this.documentElement,
                    selectorByNodeName = selectorByNodeName
                };
            }
        }
    }
}
