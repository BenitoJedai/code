using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript;

using ScriptCoreLib.JavaScript.DOM.HTML;
using System;

namespace ScriptCoreLib.JavaScript.DOM.HTML
{
    // http://src.chromium.org/viewvc/blink/trunk/Source/core/html/HTMLFormElement.idl

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

        public event Action<IEvent> onreset;
        public event Action<IEvent> onsubmit;

        public void submit()
        {
        }
    }
}
