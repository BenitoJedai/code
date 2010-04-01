using ScriptCoreLib.JavaScript;

namespace ScriptCoreLib.JavaScript.DOM.HTML
{
    [Script(InternalConstructor=true)]
    public class IHTMLTableRow : IHTMLElement
    {
        #region ctor
        public IHTMLTableRow()
        {
        }

        public IHTMLTableRow(params INode[] e)
        {
        }

        static IHTMLTableRow InternalConstructor()
        {
			return (IHTMLTableRow)(object)new IHTMLElement(HTMLElementEnum.tr);
        }

        static IHTMLTableRow InternalConstructor(params INode[] e)
        {
            IHTMLTableRow n = new IHTMLTableRow();

            n.appendChild(e);

            return n;
        }


        #endregion

        [Script(DefineAsStatic=true)]
        public IHTMLTableColumn AddColumn()
        {
            IHTMLTableColumn c = new IHTMLTableColumn();

            appendChild(c);


            return c;

        }

        [Script(DefineAsStatic = true)]
        public IHTMLTableColumn AddColumn(string e)
        {
            IHTMLTableColumn c = new IHTMLTableColumn();

            c.innerHTML = e;

            appendChild(c);
            return c;
        }

        [Script(DefineAsStatic = true)]
        public IHTMLTableColumn AddColumn(params INode[] e)
        {
            IHTMLTableColumn c = new IHTMLTableColumn(e);

            appendChild(c);
            return c;
        }
    }
}
