using ScriptCoreLib.JavaScript;

namespace ScriptCoreLib.JavaScript.DOM.HTML
{
    [Script(InternalConstructor=true)]
    public class IHTMLTextArea:IHTMLElement
    {

        public IArray<string> Lines
        {
            [Script(DefineAsStatic=true)]
            get
            {
                return IArray<string>.SplitLines(value);
            }
        }

        public string value;
        public bool disabled;
        public bool readOnly;
        public int rows;
        public int cols;
        public string wrap;

        #region constructors
        public IHTMLTextArea() { }
        public IHTMLTextArea(string value) { }

        internal static IHTMLTextArea InternalConstructor()
        {
            return (IHTMLTextArea)((object)new IHTMLElement(HTMLElementEnum.textarea));
        }

        internal static IHTMLTextArea InternalConstructor(string value)
        {
            IHTMLTextArea n = new IHTMLTextArea();

            n.value = value;

            return n;
        }


        #endregion


    }
}
