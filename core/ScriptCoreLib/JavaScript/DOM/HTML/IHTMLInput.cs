using ScriptCoreLib.JavaScript;
using ScriptCoreLib.Shared;

namespace ScriptCoreLib.JavaScript.DOM.HTML
{

    /// <summary>
    /// http://www.w3.org/TR/REC-html40/interact/forms.html#adef-type-INPUT
    /// </summary>
    [Script(InternalConstructor=true)]
    public class IHTMLInput : IHTMLElement
    {


        public int maxLength;
        public int size;

        public HTMLInputTypeEnum type;
        public string value;

        public bool disabled;
        public bool @checked;
        public bool @readOnly;

        [Script(DefineAsStatic = true)]
        public int GetInteger()
        {
            return int.Parse(value);
        }

        [Script(DefineAsStatic = true)]
        public double GetDouble()
        {
            return double.Parse(value);
        }

        public bool IsInteger
        {
            [Script(DefineAsStatic=true)]
            get
            {
                return IRegExp.Integer.exec(value) != null;
            }
        }

        public bool IsCurrency
        {
            [Script(DefineAsStatic = true)]
            get
            {
                return IRegExp.Currency.exec(value) != null;
            }
        }

        #region constructors
        public IHTMLInput() { }
        public IHTMLInput(HTMLInputTypeEnum type) { }
        public IHTMLInput(HTMLInputTypeEnum type, string value) { }

        internal static IHTMLInput InternalConstructor()
        {
            return (IHTMLInput)Native.Document.createElement(HTMLElementEnum.input);
        }


        internal static IHTMLInput InternalConstructor(HTMLInputTypeEnum type)
        {
            IHTMLInput n = null;

            var _radio = HTMLInputTypeEnum.radio;

            if (type == _radio)
            {
                n = (IHTMLInput) new IFunction("e", "/*@cc_on return this.createElement(e); @*/ return null;").apply(Native.Document, "<input type='radio' name='' value='' />");
            }
            
            if (n == null)
            {
                n = new IHTMLInput();
                n.type = type;
            }

            return n;
        }

        internal static IHTMLInput InternalConstructor(HTMLInputTypeEnum type, string value)
        {
            IHTMLInput n = new IHTMLInput(type);

            n.value = value;

            return n;
        }
        #endregion






        public static IHTMLInput CreateCheckbox(string title)
        {
            IHTMLInput i = new IHTMLInput(HTMLInputTypeEnum.checkbox);

            i.title = title;

            return i;
        }
    }
}
