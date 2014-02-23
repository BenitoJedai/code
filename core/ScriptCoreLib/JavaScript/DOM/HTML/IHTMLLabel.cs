using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript;

namespace ScriptCoreLib.JavaScript.DOM.HTML
{

    /// <summary>
    /// http://www.w3schools.com/tags/tag_label.asp
    /// </summary>
    [Script(InternalConstructor = true)]
    public class IHTMLLabel : IHTMLElement
    {
        public string htmlFor;

        public IHTMLElement htmlForElement
        {
            [Script(DefineAsStatic = true)]
            set
            {
                // X:\jsc.svn\examples\javascript\Test\TestLabelFor\TestLabelFor\Application.cs

                value.EnsureID();
                this.htmlFor = value.id;
            }
        }

        #region constructors
        public IHTMLLabel()
        {
        }

        public IHTMLLabel(string e)
        {
        }

        public IHTMLLabel(string e, IHTMLElement f)
        {
        }

        internal static IHTMLLabel InternalConstructor()
        {
            return (IHTMLLabel)IHTMLElement.InternalConstructor(HTMLElementEnum.label);
        }

        internal static IHTMLLabel InternalConstructor(string e)
        {
            IHTMLLabel n = new IHTMLLabel();

            n.appendChild(e);

            return n;
        }

        internal static IHTMLLabel InternalConstructor(string e, IHTMLElement f)
        {
            IHTMLLabel n = new IHTMLLabel(e);

            n.htmlForElement = f;

            return n;
        }

        #endregion

    }
}
