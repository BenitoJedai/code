using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript;

using ScriptCoreLib.JavaScript.DOM.HTML;

namespace ScriptCoreLib.JavaScript.DOM.HTML
{
    // chrome calls this HTMLHeadingElement
    [Script(InternalConstructor = true)]
    public class IHTMLHeader1 : IHTMLElement
    {
        public static implicit operator IHTMLHeader1(string innerText)
        {
            return new IHTMLHeader1 { innerText = innerText };
        }


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
