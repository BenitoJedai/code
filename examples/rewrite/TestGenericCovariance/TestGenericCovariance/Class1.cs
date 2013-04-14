using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptCoreLib.JavaScript.DOM
{
    // http://msdn.microsoft.com/en-us/library/dd469487.aspx
    // tested for X:\jsc.svn\examples\javascript\android\com.abstractatech.scholar\com.abstractatech.scholar\Application.cs

    public class IElement
    {}

    public class IHTMLElement : IElement
    {}

    // For generic type parameters, the out keyword specifies that the type parameter is covariant. 
    // You can use the out keyword in generic interfaces and delegates.

    // not helpful for us
    public class IDocument<out DOMElement> where DOMElement : global::ScriptCoreLib.JavaScript.DOM.IElement
    {
    }

    class foo
    {
        public foo()
        {
            IDocument<IHTMLElement> x0;

            // Error	1	Cannot implicitly convert type 'ScriptCoreLib.JavaScript.DOM.IDocument<ScriptCoreLib.JavaScript.DOM.IHTMLElement>' to 'ScriptCoreLib.JavaScript.DOM.IDocument<ScriptCoreLib.JavaScript.DOM.IElement>'	X:\jsc.svn\examples\rewrite\TestGenericCovariance\TestGenericCovariance\Class1.cs	27	38	TestGenericCovariance
            IDocument<IElement> x1 = x0;


        }
    }

}
