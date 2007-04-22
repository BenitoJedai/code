using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript;



namespace ScriptCoreLib.JavaScript.DOM.HTML
{
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
