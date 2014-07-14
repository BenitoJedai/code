using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript;

using ScriptCoreLib.JavaScript.DOM.HTML;

namespace ScriptCoreLib.JavaScript.DOM.HTML
{
    // http://src.chromium.org/viewvc/blink/trunk/Source/core/html/HTMLLegendElement.idl
    // http://www.w3schools.com/tags/tag_fieldset.asp
    [Script(InternalConstructor = true)]
    public class IHTMLLegend : IHTMLElement
    {


        #region Constructor

        public IHTMLLegend()
        {
            // InternalConstructor
        }

        static IHTMLLegend InternalConstructor()
        {
            return (IHTMLLegend)IHTMLElement.InternalConstructor(HTMLElementEnum.legend);
        }

        #endregion


    }
}
