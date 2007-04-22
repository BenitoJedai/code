using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript;

using ScriptCoreLib.JavaScript.DOM.HTML;

namespace ScriptCoreLib.JavaScript.DOM.HTML
{
    [Script(InternalConstructor = true)]
    public class IHTMLParam : IHTMLElement
    {


        #region Constructor

        public IHTMLParam()
        {
            // InternalConstructor
        }


        static IHTMLObject InternalConstructor()
        {
            return (IHTMLObject)IHTMLElement.InternalConstructor(HTMLElementEnum.param);
        }

        #endregion

        //public string name;
        public string value;
        

    }
}
