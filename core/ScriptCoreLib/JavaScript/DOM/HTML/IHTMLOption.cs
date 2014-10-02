using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript;



namespace ScriptCoreLib.JavaScript.DOM.HTML
{
    // http://src.chromium.org/viewvc/blink/trunk/Source/core/html/HTMLOptionElement.idl


    [Script(InternalConstructor = true)]
    public class IHTMLOption : IHTMLElement
    {
        public string value;
        public bool selected;


        #region Constructor

        public IHTMLOption()
        {
            // InternalConstructor
        }

        static IHTMLOption InternalConstructor()
        {
            return (IHTMLOption)((object)new IHTMLElement(HTMLElementEnum.option));
        }

        #endregion


    }
}
