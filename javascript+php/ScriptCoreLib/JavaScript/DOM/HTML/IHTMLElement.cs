using ScriptCoreLib.Shared;

using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript;

using ScriptCoreLib.Shared.Drawing;


namespace ScriptCoreLib.JavaScript.DOM.HTML
{
    [Script(InternalConstructor=true)]
    public partial class IHTMLElement : IElement
    {
        public string id;
        public string name;

        public int tabIndex;


        [Script(IsStringEnum=true)]
        public enum HTMLElementEnum
        {
            applet,
            b,
            button,
            code,
            center,
            h1,
            h2,
            h3,
            h4,
            h5,
            
            div,
            embed,
            span,
            script,
            form,
            p,
            pre,
            label,
            link,
            li,
            ul,
            ol,
            textarea,
            input,
            iframe,
            a,
            hr,
            br,
            style,
            table,
            tbody,
            tr,
            td,
            select,
            option,
            @object,
            param
        }

        #region constructors
        /// <summary>
        /// creates new DIV tag
        /// </summary>
        public IHTMLElement() { }
        public IHTMLElement(HTMLElementEnum tag) { }
        public IHTMLElement(HTMLElementEnum tag, string text) { }
        public IHTMLElement(params INode[] children) { }
        public IHTMLElement(HTMLElementEnum tag, params INode[] children) { }

        private static IHTMLElement InternalConstructor()
        {
            return InternalConstructor(null, null, null);
        }

        internal static IHTMLElement InternalConstructor(HTMLElementEnum tag)
        {
            return InternalConstructor(tag, null, null);
        }

        internal static IHTMLElement InternalConstructor(HTMLElementEnum tag, string text)
        {
            return InternalConstructor(tag, text, null);
        }



        private static IHTMLElement InternalConstructor(HTMLElementEnum tag, string text, IHTMLDocument doc)
        {
            IHTMLDocument d = (doc == null ? Native.Document : doc);
           

            IHTMLElement c = d.createElement(tag);

            if (text != null)
                c.appendChild(new ITextNode(text));

            return c;
        }

        private static IHTMLElement InternalConstructor(params INode[] children)
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

        public string innerHTML;


        public string title;

        public readonly IStyle style;

        [Script(DefineAsStatic=true)]
        public static implicit operator IStyle(IHTMLElement e)
        {
            return e.style;
        }

        public int height;
        public int width;


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

        public void blur()
        {

        }

        public void focus()
        {

        }

        /// <summary>
        /// detaches the node from dom
        /// </summary>
        [Script(DefineAsStatic = true)]
        public void Dispose()
        {
            INode n = this.parentNode;

            if (n != null)
                n.removeChild(this);
        }

   

        /// <summary>
        /// shows element and sets opacity to 1
        /// </summary>
        [Script(DefineAsStatic = true)]
        public void Show()
        {
            this.style.display = IStyle.DisplayEnum.empty;
            this.style.Opacity = 1;
        }

        public static void Show(bool bVisible, params IHTMLElement[] e)
        {
            foreach (IHTMLElement v in e)
            {
                v.Show(bVisible);
            }
        }

        [Script(DefineAsStatic = true)]
        public void Show(bool bVisible)
        {
            if (bVisible)
                Show();
            else
                Hide();
        }

        [Script(DefineAsStatic = true)]
        public void Hide()
        {
            this.style.display = IStyle.DisplayEnum.none;
        }



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
        public event EventHandler<IEvent> onclick
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
        public event EventHandler<IEvent> ondblclick
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
        public event EventHandler<IEvent> onmouseover
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
        public event EventHandler<IEvent> onmouseout
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
        public event EventHandler<IEvent> onmousedown
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
        public event EventHandler<IEvent> onmouseup
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
        public event EventHandler<IEvent> onmousemove
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
        public event EventHandler<IEvent> onmousewheel
        {
            [Script(DefineAsStatic = true)]
            add
            {
                base.InternalEvent(true, value, "DOMMouseScroll", "onmousewheel");
            }
            [Script(DefineAsStatic = true)]
            remove
            {
                base.InternalEvent(false, value, "DOMMouseScroll", "onmousewheel");
            }
        }
        #endregion

        #region event oncontextmenu
        public event EventHandler<IEvent> oncontextmenu
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
        public event EventHandler<IEvent> onselectstart
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


        #region event ondragdrop
        public event EventHandler<IEvent> ondragdrop
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
        public event EventHandler<IEvent> onchange
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


        #region event onkeypress
        public event EventHandler<IEvent> onkeypress
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
        public event EventHandler<IEvent> onkeyup
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
        public event EventHandler<IEvent> onkeydown
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

        public static int NextID = 0;

        [Script(DefineAsStatic=true)]
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

        [Script(DefineAsStatic = true)]
        public bool ToggleVisible()
        {
            IStyle.DisplayEnum v = IStyle.DisplayEnum.empty;

            if (this.style.display == v)
            {
                this.Hide();

                return false;
            }
            else
            {
                this.Show();

                return true;
            }
        }

        /// <summary>
        /// attaches this element to the current document body
        /// </summary>
        [Script(DefineAsStatic = true)]
        public void attachToDocument()
        {
            if (Native.Document.body == null)
            {
                Console.LogError("document has no body");

                return;
            }

            Native.Document.body.appendChild(this);
        }
    }
}
