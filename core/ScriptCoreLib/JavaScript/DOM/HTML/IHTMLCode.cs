using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript;

using ScriptCoreLib.JavaScript.DOM.HTML;

namespace ScriptCoreLib.JavaScript.DOM.HTML
{
    [Script(InternalConstructor = true)]
    public class IHTMLCode : IHTMLElement
    {


        #region Constructor

        public IHTMLCode()
        {
            // InternalConstructor
        }

        static IHTMLCode InternalConstructor()
        {
            return (IHTMLCode)(object)new IHTMLElement(HTMLElementEnum.code);
        }

        #endregion

        #region Constructor

        public IHTMLCode(string e)
        {
            // InternalConstructor
        }

        static IHTMLCode InternalConstructor(string e)
        {
            IHTMLCode z = new IHTMLCode();

            z.innerHTML = e;

            return z;
        }

        #endregion

    }
}
