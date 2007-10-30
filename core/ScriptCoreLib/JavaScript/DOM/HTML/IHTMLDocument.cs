using ScriptCoreLib.Shared;

using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.DOM;



// http://www.w3.org/TR/DOM-Level-2-HTML/html.html#ID-5353782642

namespace ScriptCoreLib.JavaScript.DOM.HTML
{
    [Script(HasNoPrototype = true)]
    public class IHTMLDocument : IDocument<IHTMLElement>
    {

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
        public string title;
        public readonly string referrer;
        public readonly string domain;
        public readonly string URL;
        public IHTMLElement body;
        //  readonly attribute HTMLCollection  images;
        //  readonly attribute HTMLCollection  applets;
        //  readonly attribute HTMLCollection  links;
        //  readonly attribute HTMLCollection  forms;
        //  readonly attribute HTMLCollection  anchors;
        public string cookie;

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
        public void ForEachClassName(string className, EventHandler<IHTMLElement> handler)
        {
            getElementsByClassName(className).ForEach(handler);
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
            return open("text/html", replace ? "replace" : "");
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


        #region event onkeypress
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

        [Script(DefineAsStatic = true)]
        public IArray<T> getElementsByClassName<T>(string className) where T : IHTMLElement
        {
            return (IArray<T>)(object)getElementsByClassName(className);
        }

        internal string designMode;

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
    }
}
