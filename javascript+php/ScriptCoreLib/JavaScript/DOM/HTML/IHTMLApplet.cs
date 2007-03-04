using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript;

using ScriptCoreLib.JavaScript.DOM.HTML;

namespace ScriptCoreLib.JavaScript.DOM.HTML
{
    [Script(InternalConstructor = true)]
    public class IHTMLApplet : IHTMLElement
    {
        public string code;
        public string codebase;
        public string archive;
        public bool mayscript;


        #region Constructor

        public IHTMLApplet()
        {
            // InternalConstructor
        }

        static IHTMLApplet InternalConstructor()
        {
            return (IHTMLApplet)InternalConstructor(HTMLElementEnum.applet);
        }

        #endregion


    }
}
