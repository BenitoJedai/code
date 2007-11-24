using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.Shared.Drawing;

using ScriptCoreLib.JavaScript.DOM.HTML;

namespace ScriptCoreLib.JavaScript.DOM.HTML
{
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
        [Script(DefineAsStatic=true)]
        public void ToFullscreen()
        {
            Native.Document.body.style.overflow = IStyle.OverflowEnum.hidden;

            if (this.parentNode != Native.Document.body)
                this.AttachToDocument();

            var p = new Point(Native.Window.Width, Native.Window.Height);

            Console.Log("fullscreen: " + p.X + ", " + p.Y);

            this.style.SetLocation(0, 0, p.X, p.Y);
        }
    }
}
