using System;
using System.Linq;

using ScriptCoreLib;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.Shared.Drawing;
using ScriptCoreLib.Shared.Lambda;


namespace ExampleGallery.js
{
    [Script, ScriptApplicationEntryPoint(ScriptedLoading = true, IsClickOnce = true)]
    public partial class ExampleGallery
    {

        public ExampleGallery()
        {
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
                        a.style.textDecoration = "none";
                        a.style.color = Color.Red;
                        a.style.display = IStyle.DisplayEnum.block;

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
            typeof(ExampleGallery).Spawn();
        }
    }
}
