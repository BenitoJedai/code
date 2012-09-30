using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.Shared;

using ScriptCoreLib.JavaScript.DOM.HTML;

namespace ScriptCoreLib.JavaScript.DOM.HTML
{

    // http://www.w3.org/TR/html4/present/frames.html#h-16.5
    [Script(InternalConstructor = true)]
    public class IHTMLIFrame : IHTMLElement
    {
        #region
        [Script(HasNoPrototype = true)]
        class __IHTMLIFrame
        {
            public string src;
        }

        public string src
        {
            [Script(DefineAsStatic = true)]
            get
            {
                return ((__IHTMLIFrame)(object)this).src;
            }
            [Script(DefineAsStatic = true)]
            set
            {
                // do we need this workaround?
                //contentWindow.document.location.replace(value);

                ((__IHTMLIFrame)(object)this).src = value;
            }
        }
        #endregion


        // http://stackoverflow.com/questions/65034/remove-border-from-iframe
        public string frameBorder;
        public string border;

        public bool allowFullScreen
        {
            [Script(DefineAsStatic = true)]
            set
            {
                this.setAttribute("mozallowFullScreen", "");
                this.setAttribute("webkitAllowFullScreen", "");
                this.setAttribute("allowFullScreen", "");
            }
        }

        public bool allowTransparency;
        public string scrolling;

        #region Constructor

        public IHTMLIFrame()
        {
            // InternalConstructor
        }


        static IHTMLIFrame InternalConstructor()
        {
            return (IHTMLIFrame)new IHTMLElement(IHTMLElement.HTMLElementEnum.iframe);
        }

        #endregion


        public IWindow contentWindow;



        #region event onload
        public event System.Action<IEvent> onload
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

    }
}
