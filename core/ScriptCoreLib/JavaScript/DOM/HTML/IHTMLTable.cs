using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript;

namespace ScriptCoreLib.JavaScript.DOM.HTML
{
    /// <summary>
    /// http://developer.mozilla.org/en/docs/Traversing_an_HTML_table_with_JavaScript_and_DOM_Interfaces
    /// </summary>
    [Script(InternalConstructor=true)]
    public class IHTMLTable : IHTMLElement
    {
        public int cellPadding;
        public int cellSpacing;
        public int border;
        public string align;

        #region ctor
        public IHTMLTable()
        {
        }


        static IHTMLTable InternalConstructor()
        {
			return (IHTMLTable)(object)new IHTMLElement(HTMLElementEnum.table);
        }


        #endregion

        [Script(DefineAsStatic = true)]
        public IHTMLTableBody AddBody()
        {
            IHTMLTableBody r = new IHTMLTableBody();

            this.appendChild(r);

            return r;
        }

    }
}
