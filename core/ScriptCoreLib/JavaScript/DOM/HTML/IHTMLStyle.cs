using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript;

using ScriptCoreLib.JavaScript.DOM.HTML;

namespace ScriptCoreLib.JavaScript.DOM.HTML
{

    [Script(InternalConstructor = true)]
    public class IHTMLStyle : IHTMLElement
    {
        internal IStyleSheet sheet;

        internal IStyleSheet styleSheet;


        public IStyleSheet StyleSheet
        {
            [Script(DefineAsStatic = true)]
            get
            {
                if (Expando.InternalIsMember(this, "sheet"))
                    return this.sheet;

                if (Expando.InternalIsMember(this, "styleSheet"))
                    return this.styleSheet;

                return null;
            }
        }


        #region Constructor

        public IHTMLStyle()
        {
            // InternalConstructor
        }

        static IHTMLStyle InternalConstructor()
        {
            return (IHTMLStyle)new IHTMLElement(HTMLElementEnum.style);
        }

        #endregion

    }
}
