using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript;

using ScriptCoreLib.JavaScript.DOM.HTML;

namespace ScriptCoreLib.JavaScript.DOM.HTML
{
    [Script(InternalConstructor = true)]
    public class IHTMLPre : IHTMLElement
    {


        #region Constructor

		public IHTMLPre()
        {
            // InternalConstructor
        }

		static IHTMLPre InternalConstructor()
        {
			return (IHTMLPre)(object)new IHTMLElement(HTMLElementEnum.pre);
        }

        #endregion

    }
}
