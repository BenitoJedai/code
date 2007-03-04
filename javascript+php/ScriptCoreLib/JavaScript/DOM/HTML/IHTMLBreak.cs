using ScriptCoreLib;

namespace ScriptCoreLib.JavaScript.DOM.HTML
{
    [Script(InternalConstructor = true)]
    public class IHTMLBreak : IHTMLElement
    {


        #region Constructor

        public IHTMLBreak()
        {
            // InternalConstructor
        }

        static IHTMLBreak InternalConstructor()
        {
            return (IHTMLBreak)((object)new IHTMLElement(HTMLElementEnum.br));
        }

        #endregion
    }
}
