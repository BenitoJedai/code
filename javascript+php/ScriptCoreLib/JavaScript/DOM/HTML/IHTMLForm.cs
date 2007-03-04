using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript;

using ScriptCoreLib.JavaScript.DOM.HTML;

namespace ScriptCoreLib.JavaScript.DOM.HTML
{
    [Script(InternalConstructor = true)]
    public class IHTMLForm : IHTMLElement
    {
        public string target;
        public string action;
        public string method;
        public string enctype;
        //public string size;

        #region Constructor

        public IHTMLForm()
        {
            // InternalConstructor
        }

        static IHTMLForm InternalConstructor()
        {
            return (IHTMLForm)(object)new IHTMLElement(HTMLElementEnum.form);
        }

        #endregion



        public void submit()
        {
        }
    }
}
