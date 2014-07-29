using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript;

using ScriptCoreLib.JavaScript.DOM.HTML;

namespace ScriptCoreLib.JavaScript.DOM.HTML
{
    // http://src.chromium.org/viewvc/blink/trunk/Source/core/html/HTMLHeadingElement.idl

    // chrome calls this HTMLHeadingElement
    [Script(InternalConstructor = true)]
    public class IHTMLHeader1 : IHTMLElement<IHTMLHeader1>
    {
        // X:\jsc.svn\examples\javascript\Test\TestShadowRootHost\TestShadowRootHost\Application.cs

        public static implicit operator IHTMLHeader1(string innerText)
        {
            return new IHTMLHeader1 { innerText = innerText };
        }


        #region Constructor

        public IHTMLHeader1()
        {
            // InternalConstructor
        }

        static IHTMLHeader1 InternalConstructor()
        {
            return (IHTMLHeader1)(object)new IHTMLElement(HTMLElementEnum.h1);
        }

        #endregion

    }
}
