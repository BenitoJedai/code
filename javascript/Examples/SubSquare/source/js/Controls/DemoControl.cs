using ScriptCoreLib;

using ScriptCoreLib.JavaScript.Controls;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.Serialized;
using ScriptCoreLib.JavaScript.DOM.HTML;
//using ScriptCoreLib.JavaScript.DOM.XML;
using ScriptCoreLib.JavaScript.DOM;

using ScriptCoreLib.Shared;
using ScriptCoreLib.Shared.Drawing;

namespace SubSquare.source.js.Controls
{
    [Script]
    public class DemoControl : SpawnControlBase
    {
        public const string Alias = "fx.DemoControl";

        IHTMLDiv Control = new IHTMLDiv();

        public IStyle Style { get { return Control.style; } }


        void CreateDisposableButton(string e, EventHandler a)
        {


            var btn = new IHTMLButton(e);

            this.Control.appendChild(btn);

            btn.onclick +=
                delegate
                {
                    a();

                    Control.Dispose();
                };
        }

        public DemoControl(IHTMLElement e)
            : base(e)
        {
            e.insertNextSibling(Control);

            Native.Document.body.style.background = "#6591cd url(fx/gfx/editorBg.gif) repeat-x";
            Native.Document.body.style.overflow = IStyle.OverflowEnum.hidden;

            Control.appendChild(new IHTMLElement(IHTMLElement.HTMLElementEnum.h1, 
                "This project demostrates interactive division of squares into subsquares."));

            Control.appendChild("Hold your mouse over a section. It will take some time to be divided.");

            CreateDisposableButton("Show SubSquares",
                delegate {

                    var r = Rectangle.Of(
                            32,
                            32,
                            700,
                            500
                        );

                    Console.WriteLine("bounds: " + r);

                    new SubSquare(
                        r, "fx/gfx/Follow.jpg"
                    );

                }
            );


        }



    }


}
