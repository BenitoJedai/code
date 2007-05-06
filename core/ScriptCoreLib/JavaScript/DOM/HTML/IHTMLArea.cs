using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript;

using ScriptCoreLib.JavaScript.DOM.HTML;

namespace ScriptCoreLib.JavaScript.DOM.HTML
{
    [Script(InternalConstructor = true)]
    public class IHTMLArea : IHTMLElement
    {


        #region Constructor

        public IHTMLArea()
        {
            // InternalConstructor
        }

        static IHTMLArea InternalConstructor()
        {
            return (IHTMLArea) new IHTMLElement(HTMLElementEnum.map);
        }

        #endregion


    }
}
