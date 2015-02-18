using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript;

using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.DOM.SVG;
using System.Threading.Tasks;
using System;
using ScriptCoreLib.JavaScript.Extensions;

namespace ScriptCoreLib.JavaScript.DOM.HTML
{
    // http://mxr.mozilla.org/mozilla-central/source/dom/webidl/HTMLCanvasElement.webidl
    // http://mxr.mozilla.org/mozilla-central/source/dom/interfaces/html/nsIDOMHTMLCanvasElement.idl

    // http://src.chromium.org/viewvc/blink/trunk/Source/core/html/HTMLCanvasElement.idl
    // https://src.chromium.org/viewvc/blink/trunk/Source/core/html/HTMLCanvasElement.cpp
    // http://www.scala-js.org/api/scalajs-dom/0.6/index.html#org.scalajs.dom.HTMLCanvasElement

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
        #region toDataURL
        /// <summary>
        /// http://my.jsc-solutions.net/toDataURL
        /// or
        /// <see cref="http://my.jsc-solutions.net/toDataURL" />
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public string toDataURL(string type)
        {
            return default(string);
        }
        public string toDataURL(string type = "image/jpeg", double quality = 1.0)
        {
            return default(string);
        }
        [Script(DefineAsStatic = true)]
        public string toDataURL()
        {
            //https://developer.mozilla.org/en/docs/Web/API/HTMLCanvasElement
            return toDataURL("image/png");
        }
        #endregion



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




        //// http://stackoverflow.com/questions/5905563/c-sharp-generic-operators
        //// : INodeConvertible<IHTMLElement>
        //public static explicit operator IHTMLCanvas(INodeConvertible<IHTMLDiv> l)
        //{
        //    // Error	64	'ScriptCoreLib.JavaScript.DOM.HTML.IHTMLCanvas.explicit operator ScriptCoreLib.JavaScript.DOM.HTML.IHTMLCanvas(ScriptCoreLib.JavaScript.Extensions.INodeConvertible<ScriptCoreLib.JavaScript.DOM.HTML.IHTMLDiv>)': 
        //    // user-defined conversions to or from an interface are not allowed	X:\jsc.svn\core\ScriptCoreLib\JavaScript\DOM\HTML\IHTMLCanvas.cs	114	23	ScriptCoreLib
        //    // supporting assetsLibrary elements
        //    return (IHTMLCanvas)l.AsNode();
        //}


        // implicit?
        public static explicit operator IHTMLCanvas(IHTMLDiv l)
        {
            // X:\jsc.svn\examples\javascript\WebGL\WebGLSVGSprite\WebGLSVGSprite\Application.cs
            // X:\jsc.svn\examples\javascript\canvas\AsCanvasExperiment\AsCanvasExperiment\Application.cs

            var w = l.clientWidth;
            var h = l.clientHeight;

            if (l.parentNode == null)
            {
                var hidden = new IHTMLDiv { }.AttachTo(Native.document.documentElement);
                hidden.style.visibility = IStyle.VisibilityEnum.hidden;
                l.AttachTo(hidden);

                w = l.clientWidth;
                h = l.clientHeight;

                // cleanup
                l.Orphanize();
                hidden.Orphanize();
            }

            var c = new CanvasRenderingContext2D(w, h);

            #region yield
            var yield = default(Action);

            yield = delegate
           {
               Task<ISVGSVGElement> n = l;
               n.ContinueWith(
                   svg_t =>
                   {
                       var svg = svg_t.Result;

                       IHTMLImage i = svg;

                       //c.cle
                       c.clearRect(0, 0, c.canvas.width, c.canvas.height);
                       c.drawImage(i, 0, 0, c.canvas.width, c.canvas.height);
                   }
               );

               l.async.onmutation.ContinueWith(
                   delegate
                   {
                       yield();
                   }
               );

           };

            yield();
            #endregion



            return c.canvas;
        }
    }

    [Script]
    public static class IHTMLCanvasOperators
    {
        [Obsolete("jsc assetslibrary elements should inherit this as a implicit operator?")]
        public static IHTMLCanvas AsCanvas(this INodeConvertible<IHTMLDiv> l)
        {
            // X:\jsc.svn\examples\javascript\canvas\AsCanvasExperiment\AsCanvasExperiment\Application.cs
            return (IHTMLCanvas)l.AsNode();
        }
    }
}
