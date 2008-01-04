using System;
using System.Linq;

using ScriptCoreLib;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.Shared.Drawing;
using ScriptCoreLib.Shared.Lambda;
using ScriptCoreLib.JavaScript.Controls.ImageReflection;


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
                        var r =
                         new ReflectionSetup
                         {
                             Image = v.image,
                             Position = new Point(0, 0),
                             Size = new Point(120, 90),
                             ReflectionZoom = 0.5,

                             Bottom = 2
                         }.ConvertToImageReflection();

                        r.style.position = IStyle.PositionEnum.relative;
                        r.AttachToDocument();
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
