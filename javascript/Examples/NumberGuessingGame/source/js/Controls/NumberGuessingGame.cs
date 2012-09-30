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

namespace NumberGuessingGame.source.js.Controls
{
    [Script, ScriptApplicationEntryPoint]
    public class NumberGuessingGame //: SpawnControlBase
    {
        //public const string Alias = "fx.DemoControl";

        IHTMLDiv Control = new IHTMLDiv();

        public IStyle Style { get { return Control.style; } }

        void CreateDisposableButton(string e, Action a)
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

        public NumberGuessingGame(IHTMLElement e)
        //    : base(e)
        {
            e.insertNextSibling(Control);


            Native.Document.body.style.background = "#6591cd url(assets/NumberGuessingGame/editorBg.gif) repeat-x";
            Native.Document.body.style.overflow = IStyle.OverflowEnum.hidden;

            Control.appendChild(new IHTMLElement(IHTMLElement.HTMLElementEnum.h1,
                "This project demostrates how to make a number guessing game."));

            Control.appendChild("The computer will think of a number. Try finding out which numbers are in it. You will win if you guess the numbers before all buttons are gone.");

            CreateDisposableButton("Show The GuessingGame",
                delegate
                {
                    var div = new IHTMLDiv();

                    div.AttachToDocument();

                    new GuessingGame(div);

                }
            );
        }

        static NumberGuessingGame()
        {
            typeof(NumberGuessingGame).Spawn();
        }

    }


}
