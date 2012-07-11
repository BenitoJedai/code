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
        public string src;

        // http://stackoverflow.com/questions/65034/remove-border-from-iframe
        public string frameBorder;
        public string border;

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
