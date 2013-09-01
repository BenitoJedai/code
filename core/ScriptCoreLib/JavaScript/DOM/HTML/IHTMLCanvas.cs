using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript;

using ScriptCoreLib.JavaScript.DOM.HTML;

namespace ScriptCoreLib.JavaScript.DOM.HTML
{
    // could a post build extend a type via IDL ? :)
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
        /// <summary>
        /// http://my.jsc-solutions.net/toDataURL
        /// or
        /// <see cref="http://my.jsc-solutions.net/toDataURL" />
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public string toDataURL(string type = "image/png")
        {
            return default(string);
        }


        public object getContext(string contextId, object args)
        {
            // a call on this to toDataURL shall trigger args:

            // {
            //    public bool alpha = false;
            //    public bool preserveDrawingBuffer = true;
            //}

            return default(object);
        }

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


        public byte[] bytes
        {
            [Script(DefineAsStatic = true)]
            get
            {
                var c = new CanvasRenderingContext2D(this.clientWidth, this.clientHeight);

                c.drawImage(
                    this, 0, 0, c.canvas.width, c.canvas.height
                );

                return c.bytes;
            }

        }
    }


}
