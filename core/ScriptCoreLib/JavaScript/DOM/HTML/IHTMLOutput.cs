using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript;

using ScriptCoreLib.JavaScript.DOM.HTML;

namespace ScriptCoreLib.JavaScript.DOM.HTML
{
    [Script(InternalConstructor = true)]
    public class IHTMLOutput : IHTMLElement
    {


        #region Constructor

        public IHTMLOutput()
        {
            // InternalConstructor
        }

        static IHTMLOutput InternalConstructor()
        {
            return (IHTMLOutput)(object)new IHTMLElement(HTMLElementEnum.output);
        }

        #endregion


        public static implicit operator IHTMLOutput(string innerText)
        {
            return new IHTMLOutput { innerText = innerText };
        }
    }
}
