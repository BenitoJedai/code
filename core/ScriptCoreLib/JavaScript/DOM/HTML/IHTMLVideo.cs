using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript;

using ScriptCoreLib.JavaScript.DOM.HTML;

namespace ScriptCoreLib.JavaScript.DOM.HTML
{
    [Script(InternalConstructor = true)]
    public class IHTMLVideo : IHTMLMedia
    {
	
        #region Constructor

        public IHTMLVideo()
        {
            // InternalConstructor
        }

        static IHTMLVideo InternalConstructor()
        {
            return (IHTMLVideo)IHTMLElement.InternalConstructor(HTMLElementEnum.video);
        }

        #endregion


    }
}
