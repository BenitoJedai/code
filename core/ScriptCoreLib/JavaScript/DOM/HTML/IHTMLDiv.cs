using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.Shared.Drawing;

using ScriptCoreLib.JavaScript.DOM.HTML;
using System;
using System.Linq;
using ScriptCoreLib.JavaScript.BCLImplementation.System.Threading.Tasks;
using System.Threading.Tasks;
using System.Collections.Generic;
using ScriptCoreLib.JavaScript.DOM.SVG;

namespace ScriptCoreLib.JavaScript.DOM.HTML
{
    // http://mxr.mozilla.org/mozilla-central/source/dom/interfaces/html/nsIDOMHTMLDivElement.idl
    // http://src.chromium.org/viewvc/blink/trunk/Source/core/html/HTMLDivElement.idl
    // https://github.com/Reactive-Extensions/IL2JS/blob/master/Html/Microsoft/LiveLabs/Html/Div.cs

    // like span, only operators?

    [Script(InternalConstructor = true)]
    public class IHTMLDiv : IHTMLElement
    {


        #region Constructor

        public IHTMLDiv()
        {
            // InternalConstructor
        }

        public IHTMLDiv(string html)
        {
            // InternalConstructor
        }

        public IHTMLDiv(params INode[] e)
        {
            // InternalConstructor
        }


        static IHTMLDiv InternalConstructor()
        {
            return (IHTMLDiv)(object)new IHTMLElement(HTMLElementEnum.div);
        }

        static IHTMLDiv InternalConstructor(string html)
        {
            IHTMLDiv u = new IHTMLDiv();

            u.innerHTML = html;

            return u;

        }

        static IHTMLDiv InternalConstructor(params INode[] e)
        {
            IHTMLDiv u = new IHTMLDiv();

            u.appendChild(e);

            return u;
        }
        #endregion


        /// <summary>
        /// will hide scrollbars, attach this element to the body and resize it 
        /// as fullscreen
        /// </summary>
        [Script(DefineAsStatic = true)]
        [Obsolete("ScriptCoreLib.Extensions")]
        public void ToFullscreen()
        {
            Native.document.body.style.overflow = IStyle.OverflowEnum.hidden;

            if (this.parentNode != Native.document.body)
                this.AttachToDocument();

            var p = new Point(Native.window.Width, Native.window.Height);

            System.Console.WriteLine("fullscreen: " + p.X + ", " + p.Y);

            this.style.SetLocation(0, 0, p.X, p.Y);
        }



        [Obsolete("prefer Task<ISVGSVGElement>")]
        public static explicit operator ISVGSVGElement(IHTMLDiv div)
        {
            // tested by?

            System.Console.WriteLine("ISVGSVGElement <- IHTMLDiv");
            Task<ISVGSVGElement> x = div;

            // are we ready?
            return x.Result;
        }

        public static implicit operator Task<ISVGSVGElement>(IHTMLDiv ref_div)
        {
            // X:\jsc.svn\examples\javascript\WebGL\WebGLSVGSprite\WebGLSVGSprite\Application.cs

            //System.Console.WriteLine("Task<ISVGSVGElement> <- IHTMLDiv");

            var div = (IHTMLDiv)ref_div.cloneNode(true);
            // keep monitoring?

            // Error	101	'ScriptCoreLib.JavaScript.DOM.SVG.ISVGSVGElement.explicit operator ScriptCoreLib.JavaScript.DOM.SVG.ISVGSVGElement(ScriptCoreLib.JavaScript.DOM.HTML.IHTMLElement)': user-defined conversions to or from a base class are not allowed	X:\jsc.svn\core\ScriptCoreLib\JavaScript\DOM\SVG\ISVGSVGElement.cs	40	23	ScriptCoreLib
            // Error	101	'ScriptCoreLib.JavaScript.DOM.SVG.ISVGSVGElement.explicit operator ScriptCoreLib.JavaScript.DOM.SVG.ISVGSVGElement(ScriptCoreLib.JavaScript.Extensions.INodeConvertible<ScriptCoreLib.JavaScript.DOM.HTML.IHTMLElement>)': user-defined conversions to or from an interface are not allowed	X:\jsc.svn\core\ScriptCoreLib\JavaScript\DOM\SVG\ISVGSVGElement.cs	40	23	ScriptCoreLib


            // X:\jsc.svn\examples\javascript\svg\SVGHTMLElement\SVGHTMLElement\Application.cs

            var s = new ISVGSVGElement
            {

            };

            var f = new ISVGForeignObject().AttachTo(s);

            var fdiv = new IHTMLDiv().AttachTo(f);

            fdiv.style.fontFamily = IStyle.FontFamilyEnum.Verdana;

            // we need to serialize styles now
            // svg wont have any default html css styles at all
            fdiv.style.fontSize = "12px";


            var hidden = new IHTMLDiv { }.AttachTo(Native.document.documentElement);
            hidden.style.position = IStyle.PositionEnum.@fixed;
            hidden.style.visibility = IStyle.VisibilityEnum.hidden;
            //hidden.style.display = IStyle.DisplayEnum.none;


            div.style.display = IStyle.DisplayEnum.inline_block;
            div.AttachTo(hidden);

            var t = new TaskCompletionSource<ISVGSVGElement>();

            var i = new List<Task<IHTMLImage>>();

            foreach (var q in div.ImageElements())
            {
                var ix = new TaskCompletionSource<IHTMLImage>();
                //Console.WriteLine("await " + new { q.src, q.complete });

                q.InvokeOnComplete(
                    qq =>
                    {
                        //Console.WriteLine("await done " + new { q.src });
                        q.src = q.toDataURL();
                        ix.SetResult(q);
                    }
                );

                if (!ix.Task.IsCompleted)
                    i.Add(ix.Task);
            }

            Action yield = delegate
            {
                //Console.WriteLine(new { div.clientWidth, div.clientHeight });

                s.setAttribute("width", div.clientWidth + 0);
                s.setAttribute("height", div.clientHeight + 0);
                div.AttachTo(fdiv);
                hidden.Orphanize();


                t.SetResult(s);
            };

            if (i.Count == 0)
            {
                yield();
            }
            else
            {
                // .net 4.0 does not yet even define what we want to use
                __Task.WhenAll(i.AsEnumerable()).ContinueWith(
                    delegate
                    {
                        yield();
                    }
                );
            }

            return t.Task;
        }




        // X:\jsc.svn\core\ScriptCoreLib.Windows.Forms\ScriptCoreLib.Windows.Forms\JavaScript\BCLImplementation\System\Windows\Forms\ToolStrip\ToolStripContainer.cs
        public static implicit operator IHTMLDiv(Type x)
        {
            // shall we use shadow dom instead?
            return new IHTMLDiv { className = x.Name };
        }
    }
}
