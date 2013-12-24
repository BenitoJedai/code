using ScriptCoreLib.JavaScript;
using System.Xml.Linq;
using ScriptCoreLib.JavaScript.Extensions;

namespace ScriptCoreLib.JavaScript.DOM.HTML
{
    [Script(InternalConstructor = true)]
    public class IHTMLOrderedList : IHTMLElement
    {
        public IHTMLOrderedList()
        {
        }

        internal static IHTMLOrderedList InternalConstructor()
        {
            return (IHTMLOrderedList)IHTMLElement.InternalConstructor(HTMLElementEnum.ol);
        }



        public static implicit operator IHTMLOrderedList(XElement x)
        {
            // X:\jsc.svn\examples\javascript\XElementFieldModifiedByWebService\XElementFieldModifiedByWebService\Application.cs
            // what if its not a button?
            // ScriptCoreLib.JavaScript.Extensions
            return (IHTMLOrderedList)x.AsHTMLElement();
        }

    }
}
