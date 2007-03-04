using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript;

using ScriptCoreLib.JavaScript.DOM.HTML;

namespace ScriptCoreLib.JavaScript.DOM.HTML
{
    [Script(InternalConstructor = true)]
    public class IHTMLObject : IHTMLElement
    {


        #region Constructor

        public IHTMLObject()
        {
            // InternalConstructor
        }


        static IHTMLObject InternalConstructor()
        {
            return (IHTMLObject)IHTMLElement.InternalConstructor(HTMLElementEnum.@object);
        }

        #endregion

        public string classid;
        public string type;
        public string data;
        public string autostart;
        public string loop;
        //public string height;
        //public string width;

        public void Play()
        {

        }
    }
}
