using ScriptCoreLib.JavaScript;
using System.Xml.Linq;
using ScriptCoreLib.JavaScript.Extensions;

namespace ScriptCoreLib.JavaScript.DOM.HTML
{
    // http://src.chromium.org/viewvc/blink/trunk/Source/core/html/HTMLOListElement.idl

    [Script(InternalConstructor = true)]
    public class IHTMLOrderedList : IHTMLElement
    {
        //21	    [Reflect] attribute boolean compact;
        //22	    attribute long start;
        //23	    [Reflect] attribute boolean reversed;
        //24	    [Reflect] attribute DOMString type;



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
