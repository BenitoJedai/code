using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript;

using ScriptCoreLib.JavaScript.DOM.HTML;

namespace ScriptCoreLib.JavaScript.DOM.HTML
{
    // http://www.w3schools.com/tags/tag_fieldset.asp
    [Script(InternalConstructor = true)]
    public class IHTMLFieldset : IHTMLElement
    {


        #region Constructor

        public IHTMLFieldset()
        {
            // InternalConstructor
        }

        static IHTMLFieldset InternalConstructor()
        {
            return (IHTMLFieldset)IHTMLElement.InternalConstructor(HTMLElementEnum.fieldset);
        }

        #endregion


    }
}
