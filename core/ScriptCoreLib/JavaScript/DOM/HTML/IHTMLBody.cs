using ScriptCoreLib.Shared;

using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript;

using ScriptCoreLib.Shared.Drawing;


namespace ScriptCoreLib.JavaScript.DOM.HTML
{
    // http://mxr.mozilla.org/mozilla-central/source/dom/webidl/HTMLBodyElement.webidl
    // http://src.chromium.org/viewvc/blink/trunk/Source/core/html/HTMLBodyElement.idl
    // http://mxr.mozilla.org/mozilla-central/source/dom/interfaces/html/nsIDOMHTMLBodyElement.idl

    [Script(InternalConstructor = true)]
    public partial class IHTMLBody : IHTMLElement
    {
        #region Constructor

        public IHTMLBody()
        {
            // InternalConstructor
        }

        static IHTMLBody InternalConstructor()
        {
            return (IHTMLBody)new IHTMLElement(HTMLElementEnum.body);
        }

        #endregion



        //public static T operator +(T x, THTMLElement c)
        public static IHTMLBody operator +(IHTMLBody x, IHTMLElement c)
        {
            // X:\jsc.svn\examples\javascript\Test\TestRoslynYieldReturn\TestRoslynYieldReturn\Application.cs

            c.AttachTo(x);
            return x;
        }
    }
}
