using ScriptCoreLib;

using ScriptCoreLib.JavaScript.Controls;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.Serialized;
using ScriptCoreLib.JavaScript.DOM.HTML;
//using ScriptCoreLib.JavaScript.DOM.XML;
using ScriptCoreLib.JavaScript.DOM;

using ScriptCoreLib.Shared;
using ScriptCoreLib.Shared.Drawing;
using System;

namespace SubSquare.source.js.Controls
{
    [Script, ScriptApplicationEntryPoint]
    public class SubSquareControl //: SpawnControlBase
    {
        //public const string Alias = "fx.DemoControl";

        IHTMLDiv Control = new IHTMLDiv();

        public IStyle Style { get { return Control.style; } }


        void CreateDisposableButton(string e, ScriptCoreLib.Shared.EventHandler a)
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

        public SubSquareControl()
           // : base(e)
        {
            Control.AttachToDocument();


            Native.Document.body.style.background = "#6591cd url(assets/SubSquare/editorBg.gif) repeat-x";
            Native.Document.body.style.overflow = IStyle.OverflowEnum.hidden;

            Control.appendChild(new IHTMLElement(IHTMLElement.HTMLElementEnum.h1, 
                "This project demostrates interactive division of squares into subsquares."));

            Control.appendChild("Hold your mouse over a section. It will take some time to be divided.");

            CreateDisposableButton("Show SubSquares",
                delegate {

                    var r = Rectangle.Of(
                            0,
                            0,
                            Native.Window.Width,
                            Native.Window.Height
                        );

                    Console.WriteLine("bounds: " + r);

                    new SubSquare(
                        r, "assets/SubSquare/Follow.jpg"
                    );

                }
            );


        }



        static SubSquareControl()
        {
            typeof(SubSquareControl).Spawn();
        }
    }


}
