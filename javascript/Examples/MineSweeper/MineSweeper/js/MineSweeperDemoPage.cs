using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.DOM.HTML;

namespace MineSweeper.js
{
    [Script, ScriptApplicationEntryPoint]
    class MineSweeperDemoPage
    {
        public readonly IHTMLDiv Control = new IHTMLDiv();

        public MineSweeperDemoPage()
        {
            Native.Document.body.style.background = "#6591cd url(" + Assets.Default.g_6591cd + ") repeat-x";

            Func<string, IHTMLElement> Paragraph =
                text => new IHTMLElement(IHTMLElement.HTMLElementEnum.p, text);

            Func<string, IHTMLElement> AddParagraph =
                text => Paragraph(text).AttachTo(Control);

            Native.Document.title = "Minesweeper";


            new IHTMLElement(IHTMLElement.HTMLElementEnum.h1, "Minesweeper").AttachTo(Control);



            var game = new MineSweeperPanel(8, 8, 0.12, Assets.Default);

            game.Control.AttachTo(Control);
            game.Control.style.Float = ScriptCoreLib.JavaScript.DOM.IStyle.FloatEnum.right;

            AddParagraph("The object of Minesweeper is to locate all the mines as quickly as possible without uncovering any of them. If you uncover a mine, you lose the game.");

            Action<string, Action> AddButton =
                (text, h) => new IHTMLButton(text).AttachTo(Control).onclick += ev => h();

            AddButton("Beginner", delegate
            {
                var n = new MineSweeperPanel(8, 8, 0.12, Assets.Default);

                n.Control.style.Float = ScriptCoreLib.JavaScript.DOM.IStyle.FloatEnum.right;
                game.Control.replaceWith(n.Control);

                game = n;
            });
            AddButton("Intermediate", delegate
            {
                var n = new MineSweeperPanel(16, 8, 0.15, Assets.Default);

                n.Control.style.Float = ScriptCoreLib.JavaScript.DOM.IStyle.FloatEnum.right;
                game.Control.replaceWith(n.Control);

                game = n;
            });
            AddButton("Expert", delegate
            {
                var n = new MineSweeperPanel(32, 8, 0.2, Assets.Default);

                n.Control.style.Float = ScriptCoreLib.JavaScript.DOM.IStyle.FloatEnum.right;
                game.Control.replaceWith(n.Control);

                game = n;
            });

            new IHTMLElement(IHTMLElement.HTMLElementEnum.h3, "Notes").AttachTo(Control);

            AddParagraph("You can uncover a square by clicking it. If you uncover a mine, you lose the game.");
            AddParagraph("If a number appears on a square, it indicates how many mines are in the eight squares that surround the numbered one. ");
            AddParagraph("To mark a square you suspect contains a mine, ctrl-click it. ");
            AddParagraph("The game area consists of the playing field, a mine counter, and a timer. ");

            new IHTMLElement(IHTMLElement.HTMLElementEnum.h3, "Strategies and tips").AttachTo(Control);

            AddParagraph("If you are uncertain about a square, ctrl-click it twice to mark it with a question mark (?). Later, you can either mark the square as a mine or remove the markings by ctrl-clicking the square again once or twice. ");
            AddParagraph("If you have marked all the mines around a numbered square, you can uncover the remaining squares around it by clicking the numbered square with the left and right mouse buttons simultaneously. If not all mines surrounding the numbered square have been marked, the remaining covered or unmarked squares will appear to be depressed (or flash) when the numbered square is ctrl-clicked. ");
            AddParagraph("Look for common patterns in numbers, which often indicate a corresponding pattern of mines. For example, the pattern 2-3-2 at the edge of a group of uncovered squares indicates a row of three mines next to the three numbers. ");

            new IHTMLElement(IHTMLElement.HTMLElementEnum.h3, "About").AttachTo(Control);

            AddParagraph("").innerHTML = "This is a javascript version of <a href='http://en.wikipedia.org/wiki/Minesweeper_(computer_game)'>the Minesweeper game</a> <a href='http://jsc.sourceforge.net/'>powered by jsc</a>.";

            var Preview = new IHTMLImage(Assets.Default.Preview).AttachTo(Control);

        }

        static MineSweeperDemoPage()
        {
            typeof(MineSweeperDemoPage).SpawnTo(i => i.replaceWith(new MineSweeperDemoPage().Control));
        }

    }
}
