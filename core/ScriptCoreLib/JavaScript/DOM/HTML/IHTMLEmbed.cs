using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript;

using ScriptCoreLib.JavaScript.DOM.HTML;

namespace ScriptCoreLib.JavaScript.DOM.HTML
{
    [Script(InternalConstructor = true)]
    public class IHTMLEmbed : IHTMLElement
    {


        #region Constructor

        public IHTMLEmbed()
        {
            // InternalConstructor
        }


        static IHTMLObject InternalConstructor()
        {
            return (IHTMLObject)IHTMLElement.InternalConstructor(HTMLElementEnum.embed);
        }

        #endregion

        public string src;
        public string autostart;
        public string volume;
        public string type;
        public string wmode;
        

    }
}
