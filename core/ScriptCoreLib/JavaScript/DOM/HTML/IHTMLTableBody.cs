using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript;

using ScriptCoreLib;

namespace ScriptCoreLib.JavaScript.DOM.HTML
{
    [Script(InternalConstructor = true)]
    public class IHTMLTableBody : IHTMLElement
    {


        #region Constructor

        public IHTMLTableBody()
        {
            // InternalConstructor
        }

        [Script(DefineAsStatic = true)]
        public IHTMLTableRow AddRow(string e)
        {
            return AddRow(new ITextNode(e));
        }

        /// <summary>
        /// accepts either string or INode
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        [Script(DefineAsStatic = true)]
        public IHTMLTableRow AddRow(params object[] e)
        {
            IHTMLTableRow r = AddRow();

            foreach (object x in e)
            {
                IHTMLTableColumn c = new IHTMLTableColumn();
                
                Expando q = Expando.Of(x);

                if (x == null)
                {
                    // do nothing
                }
                else if (q.IsString)
                {
                    c.innerHTML = q.GetValue();
                }
                else
                {
                    c.appendChild(q.To<INode>());
                }

                r.appendChild(c);

            }

            return r;
        }


        [Script(DefineAsStatic = true)]
        public IHTMLTableColumn[] AddRowAsColumns(params string[] e)
        {
            INode[] u = new INode[e.Length];

            for (int i = 0; i < e.Length; i++)
            {
                u[i] = new ITextNode(e[i]);
            }

            return AddRowAsColumns(u);
        }

        /// <summary>
        /// accepts either string or INode
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        [Script(DefineAsStatic = true)]
        public IHTMLTableColumn[] AddRowAsColumns(params INode[] e)
        {
            IHTMLTableColumn[] n = new IHTMLTableColumn[e.Length];

            IHTMLTableRow r = AddRow();

            int i = 0;

            foreach (INode x in e)
            {
                IHTMLTableColumn c = new IHTMLTableColumn();

                n[i++] = c;

                if (x != null)
                    c.appendChild(x);

                r.appendChild(c);

               

            }

            return n;
        }

        [Script(DefineAsStatic=true)]
        public IHTMLTableRow AddRow()
        {
            IHTMLTableRow r = new IHTMLTableRow();

            this.appendChild(r);

            return r;
        }

        static IHTMLTableBody InternalConstructor()
        {
			return (IHTMLTableBody)(object)new IHTMLElement(HTMLElementEnum.tbody);
        }

        #endregion


    }
}
