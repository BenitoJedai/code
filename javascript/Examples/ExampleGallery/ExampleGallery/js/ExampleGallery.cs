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

    [Script, ScriptApplicationEntryPoint(ScriptedLoading = true, Format = SerializedDataFormat.none)]
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

            var ApplicationsWithLoadingImagesQuery =
                from t in Applications
                let assembly = t.Assembly.GetName().Name
                let preview = "assets/" + assembly + "/Preview.png"
                let image = new IHTMLImage(preview)
                select new { t, assembly, preview, image };

            var ApplicationsWithLoadingImages = ApplicationsWithLoadingImagesQuery.ToArray();

            var LoadingMessage = new IHTMLDiv().AttachTo(Menu);

            var DoneLoading = 500.Until(
                t =>
                {
                    var count = ApplicationsWithLoadingImages.Count(i => !i.image.complete);

                    LoadingMessage.innerText = count + " are images still loading...";

                    return (count == 0 || t.Counter == 6);
                }
            );

            DoneLoading +=
                delegate
                {
                    LoadingMessage.Dispose();

                    var query =
                        from i in ApplicationsWithLoadingImages
                        let hasimage = i.image.complete && i.image.width > 0
                        where hasimage
                        select new { i.image, i.t };

                    foreach (var v in query)
                    {
                        var reflection = 15;
                        var slot = new IHTMLDiv();

                        v.image.style.SetLocation(0, 0, 120, 90);
                        v.image.AttachTo(slot);

                        slot.style.position = IStyle.PositionEnum.relative;
                        slot.style.border = "1px solid blue";
                        slot.style.SetSize(120, 90 + reflection);

                        slot.AttachTo(Menu);

                        for (int y = 0; y < reflection; y++)
                        {
                            var r = (IHTMLImage) v.image.cloneNode(true);

                            r.style.SetLocation(0, 90 - y, 120, 90);
                            r.AttachTo(slot);
                            r.style.Opacity = (y + 1) / (reflection - 1);

                            // rect (top, right, bottom, left)

                            r.style.clip = "rect(" + y + ", 0, " + ", 0)";
                        }
                    }


                };

            //foreach (var v in Applications.OrderBy(i => i.Name))
            //{
            //    var p = v;

            //    new IHTMLAnchor(p.Name).Aggregate(
            //        a =>
            //        {
            //            a.style.textDecoration = "none";
            //            a.style.color = Color.Red;
            //            a.style.display = IStyle.DisplayEnum.block;

            //            a.onclick +=
            //                ev =>
            //                {
            //                    ev.PreventDefault();

            //                    Menu.Dispose();

            //                    Activator.CreateInstance(p);
            //                };


            //        }
            //    ).AttachTo(new IHTMLElement(IHTMLElement.HTMLElementEnum.li).AttachTo(List));
            //}
        }

        static ExampleGallery()
        {
            typeof(ExampleGallery).Spawn();
        }
    }
}
