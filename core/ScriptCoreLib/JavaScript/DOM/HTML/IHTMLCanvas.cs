using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript;

using ScriptCoreLib.JavaScript.DOM.HTML;

namespace ScriptCoreLib.JavaScript.DOM.HTML
{
    [System.ComponentModel.Description(@"
// http://dev.w3.org/html5/spec/the-canvas-element.html

interface HTMLCanvasElement : HTMLElement {
           attribute unsigned long width;
           attribute unsigned long height;

  DOMString toDataURL(in optional DOMString type, in any... args);

  object getContext(in DOMString contextId);
};
")]
    [Script(InternalConstructor = true)]
    public class IHTMLCanvas : IHTMLElement
    {
        public object getContext(string contextId)
        {
            return default(object);
        }

        #region Constructor

        public IHTMLCanvas()
        {
            // InternalConstructor
        }

        static IHTMLCanvas InternalConstructor()
        {
            return (IHTMLCanvas)new IHTMLElement(HTMLElementEnum.canvas);
        }

        #endregion

    }
}
