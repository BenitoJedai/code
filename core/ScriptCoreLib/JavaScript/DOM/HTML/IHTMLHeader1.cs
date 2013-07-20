using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript;

using ScriptCoreLib.JavaScript.DOM.HTML;

namespace ScriptCoreLib.JavaScript.DOM.HTML
{
    [Script(InternalConstructor = true)]
    public class IHTMLHeader1 : IHTMLElement
    {


        #region Constructor

        public IHTMLHeader1()
        {
            // InternalConstructor
        }

        static IHTMLHeader1 InternalConstructor()
        {
            return (IHTMLHeader1)(object)new IHTMLElement(HTMLElementEnum.h1);
        }

        #endregion

    }
}
