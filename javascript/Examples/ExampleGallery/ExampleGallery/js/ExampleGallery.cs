using System;
using System.Linq;

using ScriptCoreLib;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.Shared.Drawing;
using ScriptCoreLib.Shared.Lambda;


namespace ExampleGallery.js
{
    [Script, ScriptApplicationEntryPoint]
    public class ExampleGallery
    {

        public ExampleGallery()
        {
            var Applications = new[]
            {
                typeof(ThreeDStuff.js.ThreeDStuff),
                typeof(ConsoleWorm.js.ConsoleWorm),
                typeof(ButterFly.source.js.Butterfly),
                typeof(SpaceInvaders.source.js.Controls.SpaceInvadersGame),
                typeof(LightsOut.js.LightsOut2FullScreen)
            };

            Native.Document.body.style.Aggregate(
                style =>
                {
                    style.backgroundColor = Color.Black;
                    style.color = Color.White;
                }
            );


            var Menu = new IHTMLDiv().AttachToDocument();

            new IHTMLElement(IHTMLElement.HTMLElementEnum.h1,
                typeof(ExampleGallery).Name).AttachTo(Menu);



            var List = new IHTMLElement(IHTMLElement.HTMLElementEnum.ol).AttachTo(Menu);

            foreach (var v in Applications.OrderBy(i => i.Name))
            {
                var p = v;

                new IHTMLAnchor(p.Name).Aggregate(
                    a =>
                    {
                        a.onclick +=
                            ev =>
                            {
                                ev.PreventDefault();

                                Menu.Dispose();

                                Activator.CreateInstance(p);
                            };
                    }
                ).AttachTo(new IHTMLElement(IHTMLElement.HTMLElementEnum.li).AttachTo(List));
            }
        }

        static ExampleGallery()
        {
            typeof(ExampleGallery).SpawnTo(i => new ExampleGallery());
        }


    }

}
