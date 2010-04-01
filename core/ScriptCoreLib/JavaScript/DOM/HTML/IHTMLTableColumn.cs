using ScriptCoreLib.JavaScript;

namespace ScriptCoreLib.JavaScript.DOM.HTML
{
    [Script(InternalConstructor=true)]
    public class IHTMLTableColumn : IHTMLElement
    {
        public string align;
        public string valign;
        public bool noWrap;

        public int colSpan;
        public int rowSpan;

        #region ctor
        public IHTMLTableColumn()
        {
        }

        public IHTMLTableColumn(params INode[] e)
        {
        }

        public IHTMLTableColumn(string e)
        {
        }

        static IHTMLTableColumn InternalConstructor()
        {
			return (IHTMLTableColumn)(object)new IHTMLElement(HTMLElementEnum.td);
        }

        static IHTMLTableColumn InternalConstructor(params INode[] e)
        {
            IHTMLTableColumn n = new IHTMLTableColumn();

            n.appendChild(e);

            return n;
        }

        static IHTMLTableColumn InternalConstructor(string e)
        {
            IHTMLTableColumn n = new IHTMLTableColumn();

            n.appendChild(e);

            return n;
        }

        #endregion

    }
}
