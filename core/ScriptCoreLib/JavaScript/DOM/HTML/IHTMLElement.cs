using ScriptCoreLib.Shared;

using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript;

using ScriptCoreLib.Shared.Drawing;


namespace ScriptCoreLib.JavaScript.DOM.HTML
{
    // http://mxr.mozilla.org/mozilla-central/source/dom/interfaces/html/nsIDOMHTMLElement.idl
    [Script(InternalConstructor = true)]
    public /* abstract */ partial class IHTMLElement : IElement
    {
        // element is like exception. its a base class. not ot be created. not to be thrown.

        public string id;
        public string name;

        public int tabIndex;

        //public bool contentEditable;

        [Script(IsStringEnum = true)]
        public enum HTMLElementEnum
        {
            a,
            abbr,
            acronym,
            address,
            applet,
            area,
            audio,
            b,
            @base,
            basefont,
            bdo,
            big,
            blockquote,
            body,
            br,
            button,
            canvas,
            caption,
            center,
            cite,
            code,
            col,
            colgroup,
            dd,
            del,
            dfn,
            dir,
            div,
            dl,
            dt,
            em,
            embed,
            fieldset,
            font,
            form,
            frame,
            frameset,
            head,
            h1,
            h2,
            h3,
            h4,
            h5,
            h6,
            hr,
            html,
            i,
            iframe,
            img,
            input,
            ins,
            kbd,
            label,
            legend,
            li,
            link,
            map,
            marquee,
            menu,
            meta,
            noframes,
            noscript,
            @object,
            ol,
            optgroup,
            option,
            output,
            p,
            param,
            pre,
            q,
            s,
            samp,
            script,
            select,
            small,
            span,
            strike,
            strong,
            style,
            sub,
            sup,
            table,
            tbody,
            td,
            textarea,
            tfoot,
            th,
            thead,
            title,
            tr,
            tt,
            u,
            ul,
            @var,
            video
        }

        #region constructors
        /// <summary>
        /// creates new DIV tag
        /// </summary>
        public IHTMLElement() { }
        public IHTMLElement(string tag)
        {
            // must keep it empty?
            //throw new System.NotImplementedException();
        }
        public IHTMLElement(HTMLElementEnum tag) { }
        public IHTMLElement(HTMLElementEnum tag, IHTMLDocument doc) { }
        public IHTMLElement(HTMLElementEnum tag, string text) { }
        public IHTMLElement(params INode[] children) { }
        public IHTMLElement(HTMLElementEnum tag, params INode[] children) { }

        internal static IHTMLElement InternalConstructor()
        {
            return InternalConstructor(null, null, null);
        }

        internal static IHTMLElement InternalConstructor(string tag)
        {
            return Native.Document.createElement(tag);
        }

        internal static IHTMLElement InternalConstructor(HTMLElementEnum tag)
        {
            return InternalConstructor(tag, null, null);
        }

        internal static IHTMLElement InternalConstructor(HTMLElementEnum tag, string text)
        {
            return InternalConstructor(tag, text, null);
        }

        internal static IHTMLElement InternalConstructor(HTMLElementEnum tag, IHTMLDocument doc)
        {
            if (doc == null)
                doc = Native.Document;

            // jsc should really support enum.ToString!
            IHTMLElement c = (IHTMLElement)doc.createElement(tag);


            return c;
        }

        internal static IHTMLElement InternalConstructor(HTMLElementEnum tag, string text, IHTMLDocument doc)
        {
            IHTMLElement c = InternalConstructor(tag, doc);

            if (text != null)
                c.appendChild(new ITextNode(text));

            return c;
        }

        internal static IHTMLElement InternalConstructor(params INode[] children)
        {
            return InternalConstructor(HTMLElementEnum.div, children);
        }

        internal static IHTMLElement InternalConstructor(HTMLElementEnum tag, params INode[] children)
        {
            IHTMLElement n = InternalConstructor(tag, null, null);

            n.appendChild(children);

            return n;
        }

        #endregion



        #region innerText
        public string innerText
        {
            [Script(DefineAsStatic = true)]
            get
            {
                if (this.childNodes.Length == 1)
                {

                    if (this.childNodes[0].nodeType == NodeTypeEnum.TextNode)
                    {
                        return ((ITextNode)this.childNodes[0]).nodeValue;
                    }
                }

                return "";
            }
            [Script(DefineAsStatic = true)]
            set
            {
                ITextNode n = null;

                if (this.childNodes.Length == 0)
                {
                    n = new ITextNode(this.ownerDocument);

                    this.appendChild(n);
                }
                else if (this.childNodes.Length == 1)
                {
                    if (this.childNodes[0].nodeType == NodeTypeEnum.TextNode)
                    {
                        n = (ITextNode)this.childNodes[0];
                    }
                    else
                    {
                        this.removeChildren();

                        n = new ITextNode(this.ownerDocument);

                        this.appendChild(n);
                    }
                }
                else
                {
                    this.removeChildren();

                    n = new ITextNode(this.ownerDocument);

                    this.appendChild(n);
                }



                n.nodeValue = value;
            }
        }
        #endregion


        public string innerHTML;


        public string title;

        public readonly IStyle style;

        [Script(DefineAsStatic = true)]
        public static implicit operator IStyle(IHTMLElement e)
        {
            return e.style;
        }

        public int height;
        public int width;

        // this is special
        public string className;



        public readonly int offsetLeft;
        public readonly int offsetTop;

        public readonly int offsetWidth;
        public readonly int offsetHeight;

        public readonly int clientWidth;
        public readonly int clientHeight;

        public int scrollLeft;
        public int scrollTop;

        public readonly int scrollWidth;
        public readonly int scrollHeight;

        public void select()
        {

        }

        public void blur()
        {

        }

        public void focus()
        {

        }



        /*
        public static void Show(bool bVisible, params IHTMLElement[] e)
        {
            foreach (IHTMLElement v in e)
            {
                Extensions.Extensions.Show(v, bVisible);
            }
        }
        */


        [Script(DefineAsStatic = true)]
        public void SetCenteredLocation(Point p)
        {
            SetCenteredLocation(p.X, p.Y);
        }

        [Script(DefineAsStatic = true)]
        public void SetCenteredLocation(int x, int y)
        {
            this.style.position = IStyle.PositionEnum.absolute;
            this.style.SetLocation(x - this.clientWidth / 2, y - this.clientHeight / 2);
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
        #region event ondblclick
        public event System.Action<IEvent> ondblclick
        {
            [Script(DefineAsStatic = true)]
            add
            {
                base.InternalEvent(true, value, "dblclick");
            }
            [Script(DefineAsStatic = true)]
            remove
            {
                base.InternalEvent(false, value, "dblclick");
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
        #region event onmousedown
        public event System.Action<IMouseDownEvent> onmousedown
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

        #region event onmousemove
        public event System.Action<IEvent> onmousewheel
        {
            [Script(DefineAsStatic = true)]
            add
            {
                base.InternalEvent(true, value,
                    new IEventTarget.EventNames
                    {
                        Event = "onmousewheel",
                        EventListener = "DOMMouseScroll",
                        EventListenerAlt = "mousewheel"
                    }
                );

            }
            [Script(DefineAsStatic = true)]
            remove
            {
                base.InternalEvent(false, value,
                    new IEventTarget.EventNames
                    {
                        Event = "onmousewheel",
                        EventListener = "DOMMouseScroll",
                        EventListenerAlt = "mousewheel"
                    }
                );
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


        #region event onselectstart
        public event System.Action<IEvent> onselectstart
        {
            [Script(DefineAsStatic = true)]
            add
            {
                base.InternalEvent(true, value, "selectstart");
            }
            [Script(DefineAsStatic = true)]
            remove
            {
                base.InternalEvent(false, value, "selectstart");
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

        // http://dev.w3.org/html5/spec/dnd.html#event-dragstart
        public bool draggable;

        #region event ondragstart
        public event System.Action<DragEvent> ondragstart
        {
            [Script(DefineAsStatic = true)]
            add
            {
                this.draggable = true;
                base.InternalEvent(true, value, "dragstart");
            }
            [Script(DefineAsStatic = true)]
            remove
            {
                base.InternalEvent(false, value, "dragstart");
            }
        }
        #endregion

        #region event ondragover
        public event System.Action<DragEvent> ondragover
        {
            [Script(DefineAsStatic = true)]
            add
            {
                base.InternalEvent(true, value, "dragover");
            }
            [Script(DefineAsStatic = true)]
            remove
            {
                base.InternalEvent(false, value, "dragover");
            }
        }
        #endregion

        #region event ondragleave
        public event System.Action<DragEvent> ondragleave
        {
            [Script(DefineAsStatic = true)]
            add
            {
                base.InternalEvent(true, value, "dragleave");
            }
            [Script(DefineAsStatic = true)]
            remove
            {
                base.InternalEvent(false, value, "dragleave");
            }
        }
        #endregion

        #region event ondrop
        public event System.Action<DragEvent> ondrop
        {
            [Script(DefineAsStatic = true)]
            add
            {
                base.InternalEvent(true, value, "drop");
            }
            [Script(DefineAsStatic = true)]
            remove
            {
                base.InternalEvent(false, value, "drop");
            }
        }
        #endregion
        #region event ondragdrop
        public event System.Action<IEvent> ondragdrop
        {
            [Script(DefineAsStatic = true)]
            add
            {
                base.InternalEvent(true, value, "dragdrop");
            }
            [Script(DefineAsStatic = true)]
            remove
            {
                base.InternalEvent(false, value, "dragdrop");
            }
        }
        #endregion



        #region event onchange
        public event System.Action<IEvent> onchange
        {
            [Script(DefineAsStatic = true)]
            add
            {
                base.InternalEvent(true, value, "change");
            }
            [Script(DefineAsStatic = true)]
            remove
            {
                base.InternalEvent(false, value, "change");
            }
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



        #region event onkeyup
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

        #region event ontouchstart
        public event System.Action<TouchEvent> ontouchstart
        {
            [Script(DefineAsStatic = true)]
            add
            {
                InternalEnableMultitouch();
                this.addEventListener("MozTouchDown", value, false);
                this.addEventListener("touchstart", value, false);
            }
            [Script(DefineAsStatic = true)]
            remove
            {
                this.removeEventListener("touchstart", value, false);
            }
        }
        #endregion

        #region event ontouchmove
        public event System.Action<TouchEvent> ontouchmove
        {
            [Script(DefineAsStatic = true)]
            add
            {
                InternalEnableMultitouch();
                // http://support.mozilla.org/en-US/questions/810808
                // https://developer.mozilla.org/en-US/docs/DOM/Touch_events_(Mozilla_experimental)
                this.addEventListener("MozTouchMove", value, false);
                this.addEventListener("touchmove", value, false);

            }
            [Script(DefineAsStatic = true)]
            remove
            {
                this.removeEventListener("touchmove", value, false);
            }
        }

        private static void InternalEnableMultitouch()
        {
            Native.Document.multitouchData = true;
        }
        #endregion

        #region event ontouchend
        public event System.Action<TouchEvent> ontouchend
        {
            [Script(DefineAsStatic = true)]
            add
            {
                InternalEnableMultitouch();
                this.addEventListener("MozTouchUp", value, false);
                this.addEventListener("touchend", value, false);
            }
            [Script(DefineAsStatic = true)]
            remove
            {
                this.removeEventListener("touchend", value, false);
            }
        }
        #endregion


        #region event onpointerlockchange
        public event System.Action<TouchEvent> onpointerlockchange
        {
            [Script(DefineAsStatic = true)]
            add
            {
                this.addEventListener("pointerlockchange", value, false);
            }
            [Script(DefineAsStatic = true)]
            remove
            {
                this.removeEventListener("pointerlockchange", value, false);
            }
        }
        #endregion

        public static int NextID = 0;

        [Script(DefineAsStatic = true)]
        public void EnsureID()
        {
            if (this.id == "")
                this.id += "$" + NextID++;
        }


        [Script(DefineAsStatic = true)]
        public void ScrollToBottom()
        {
            this.scrollTop = this.scrollHeight - this.clientHeight;

        }

        [Script(DefineAsStatic = true)]
        public void removeChildren()
        {
            while (this.firstChild != null)
                this.removeChild(this.firstChild);
        }

        [Script(DefineAsStatic = true)]
        public void FadeOut()
        {
            Fader.FadeOut(this);
        }

        [Script(DefineAsStatic = true)]
        public void replaceChildrenWith(string e)
        {
            removeChildren();
            appendChild(e);
        }

        /// <summary>
        /// user cannot select text, does not work with img in IE
        /// </summary>
        [Script(DefineAsStatic = true)]
        public void DisableSelection()
        {
            this.onmousedown += Native.DisabledEventHandler;
            this.onselectstart += Native.DisabledEventHandler;

        }

        [Script(DefineAsStatic = true)]
        public void EnableSelection()
        {
            this.onmousedown -= Native.DisabledEventHandler;
            this.onselectstart -= Native.DisabledEventHandler;
        }

        public Rectangle Bounds
        {
            [Script(DefineAsStatic = true)]
            get
            {
                Rectangle b = new Rectangle();

                b.Left = this.offsetLeft;
                b.Top = this.offsetTop;

                b.Width = this.scrollWidth;
                b.Height = this.scrollHeight;

                return b;
            }
        }

        [Script(DefineAsStatic = true)]
        public void DisableContextMenu()
        {
            oncontextmenu += Native.DisabledEventHandler;
        }





        static string[] InternalCaptureMouseEvents = new string[] { "click", "mousedown", "mouseup", "mousemove", "mouseover", "mouseout" };

        static System.Action InternalCaptureMouse(IHTMLElement self)
        {
            // view-source:http://help.dottoro.com/external/examples/ljrtxexf/setCapture_3.htm
            // http://www.activewidgets.com/javascript.forum.8933.28/problems-with-version-1-0.html

            if (Expando.Of(self).Contains("setCapture"))
            {
                self.setCapture();

                return
                        delegate
                        {
                            self.releaseCapture();
                        }
                    ;


            }

            bool flag = false;

            System.Action<IEvent> _capture =
                delegate(IEvent e)
                {
                    if (flag)
                        return;

                    flag = true;

                    e.StopPropagation();

                    IEvent _event = Native.Document.createEvent("MouseEvents");

                    _event.initMouseEvent(
                        e.type,
                        e.bubbles, e.cancelable, e.view, e.detail,
                        e.screenX, e.screenY, e.clientX, e.clientY,
                        e.ctrlKey, e.altKey, e.shiftKey, e.metaKey,
                        e.button, e.relatedTarget);

                    self.dispatchEvent(_event);
                    flag = false;
                };


            foreach (string v in InternalCaptureMouseEvents)
                Native.Window.addEventListener(v, _capture, true);

            return delegate
                    {
                        foreach (string v in InternalCaptureMouseEvents)
                            Native.Window.removeEventListener(v, _capture, true);
                    }
                ;
        }

        [Script(DefineAsStatic = true)]
        public System.Action CaptureMouse()
        {
            // who is using this?
            return InternalCaptureMouse(this);
        }

        // ff
        private void dispatchEvent(IEvent _event)
        {

        }




        [Script(OptimizedCode = @"
		if (that.requestFullscreen) {
		    that.requestFullscreen();
		}
		else if (that.mozRequestFullScreen) {
		    that.mozRequestFullScreen(Element.ALLOW_KEYBOARD_INPUT);
		}
		else if (that.webkitRequestFullScreen) {
		    that.webkitRequestFullScreen(Element.ALLOW_KEYBOARD_INPUT);
		}
                    
                    ")]
        static void __requestFullscreen(object that)
        {
        }


        [Script(DefineAsStatic = true)]
        public void requestFullscreen()
        {
            // http://tutorialzine.com/2012/02/enhance-your-website-fullscreen-api/
            // tested by X:\jsc.svn\examples\javascript\My.Solutions.Pages.Templates\My.Solutions.Pages.Templates\Application.cs
            __requestFullscreen(this);
        }

        internal void setCapture()
        {
        }

        internal void releaseCapture()
        {
        }


        [Script(OptimizedCode = @"
		if (that.requestPointerLock) {
		    that.requestPointerLock();
		}
		else if (that.mozRequestPointerLock) {
		    that.mozRequestPointerLock();
		}
		else if (this.webkitRequestPointerLock) {
		    that.webkitRequestPointerLock();
		}
                    
                    ")]
        static void __requestPointerLock(object that)
        {
        }

        // http://dvcs.w3.org/hg/pointerlock/raw-file/default/index.html
        [Script(DefineAsStatic = true)]
        public void requestPointerLock()
        {

            // tested by X:\jsc.svn\examples\javascript\My.Solutions.Pages.Templates\My.Solutions.Pages.Templates\Application.cs
            __requestPointerLock(this);
        }
    }
}
