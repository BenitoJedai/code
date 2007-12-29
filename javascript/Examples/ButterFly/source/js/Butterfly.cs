using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib;
using ScriptCoreLib.Shared;
using ScriptCoreLib.Shared.Drawing;
using System;

namespace ButterFly.source.js
{


    [Script, ScriptApplicationEntryPoint]
    public class Butterfly
    {
        IHTMLDiv Control = new IHTMLDiv();

        public Butterfly()
        {
            Control.AttachToDocument();
            Control.appendChild("There is a buttrfly under your mouse. Can you see it? :)");

            Butterfly.Spawn(Control);
        }

        public Butterfly(IHTMLElement e)
        {


            e.insertNextSibling(Control);

            Control.appendChild("There is a buttrfly under your mouse. Can you see it? :)");

            Butterfly.Spawn(Control);


        }

        public static void Spawn(IHTMLElement e)
        {


            Native.Document.body.style.margin = "0px";
            Native.Document.body.style.padding = "0px";
            Native.Document.body.style.overflow = IStyle.OverflowEnum.hidden;

            e.style.width = "100%";
            e.style.height = "800px";

            e.style.backgroundColor = Color.FromRGB(209, 245, 245);

            IHTMLElement loading = new IHTMLElement(IHTMLElement.HTMLElementEnum.code, "loading...");

            loading.style.SetLocation(64, 64, 200, 64);

            e.appendChild(loading);
            new IHTMLImage("fx/gfx/buttryfly.gif").InvokeOnComplete(
                delegate
                {
                    loading.FadeOut();

                    try
                    {
                        IStyleSheet.Default.AddRule("*", "cursor: url('fx/gfx/nocursor.cur'), auto;", 0);
                    }
                    catch (Exception exc)
                    {
                        new IHTMLElement(IHTMLElement.HTMLElementEnum.pre, exc.Message).AttachToDocument();
                    }

                    e.style.backgroundImage = "url(fx/gfx/buttryfly.gif)";
                    e.style.backgroundRepeat = "no-repeat";



                    e.DisableContextMenu();

                    e.onmousemove +=
                        delegate(IEvent i)
                        {

                            Console.WriteLine("pos: " + i.CursorPosition);

                            e.style.backgroundPosition = i.CursorX + "px " + i.CursorY + "px";


                        };
                });

        }

        static Butterfly()
        {
            typeof(Butterfly).SpawnTo(i => new Butterfly(i));
        }
    }
}
