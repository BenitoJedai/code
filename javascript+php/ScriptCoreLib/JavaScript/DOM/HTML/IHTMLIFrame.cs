using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript;

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
    }
}
