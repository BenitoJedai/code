using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.Shared;

using ScriptCoreLib.JavaScript.DOM.HTML;

namespace ScriptCoreLib.JavaScript.DOM.HTML
{
    [Script(InternalConstructor=true)]
    public class IHTMLIFrame : IHTMLElement
    {
        public string src;

        public string frameborder;
        public string border;

        public bool allowTransparency;
        public string scrolling;

        #region Constructor

            public IHTMLIFrame()
            {
                // InternalConstructor
            }
            
            
            static IHTMLElementTemplate InternalConstructor()
            {
                return (IHTMLElementTemplate)new IHTMLElement(IHTMLElement.HTMLElementEnum.iframe);
            }
        
        #endregion


        public IWindow contentWindow;



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

    }
}
