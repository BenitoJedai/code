using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript;



namespace ScriptCoreLib.JavaScript.DOM.HTML
{
    /// <summary>
    /// http://www.htmlcodetutorial.com/forms/_SELECT.html
    /// </summary>
    [Script(InternalConstructor = true)]
    public class IHTMLSelect : IHTMLElement
    {
        public bool @readOnly;
        public int size;
        public int selectedIndex;
        public bool disabled;
        public bool multiple;


        // IE seems to return "" here, see "selectedIndex" instead
        internal string value;


        #region Constructor

        public IHTMLSelect()
        {
            // InternalConstructor
        }

        static IHTMLSelect InternalConstructor()
        {
            return (IHTMLSelect)((object)new IHTMLElement(HTMLElementEnum.select));
        }

        #endregion

        public IHTMLOption this[int i]
        {
            get
            {
                // um, IE 8 seems now not to give a value to us..
                return default(IHTMLOption);
            }
        }

        /// <summary>
        /// adds object field names to the list
        /// </summary>
        /// <param name="p"></param>
        [Script(DefineAsStatic = true)]
        public void Add(Expando p)
        {
            foreach (ExpandoMember m in p.GetFields())
                Add(m.Name);
        }


        [Script(DefineAsStatic = true)]
        public IHTMLOption Add(string p)
        {
            IHTMLOption o = new IHTMLOption();

            o.value = p;
            o.innerHTML = p;

            this.appendChild(o);

            return o;
        }

        [Script(DefineAsStatic = true)]
        public void Add(params int[] e)
        {
            foreach (int v in e)
            {
                this.Add(v + "");
            }
        }

        [Script(DefineAsStatic = true)]
        public void Add(params string[] e)
        {
            foreach (string v in e)
            {
                Add(v);
            }
        }
    }
}
