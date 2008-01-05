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
            var Title = typeof(ExampleGallery).Name;

            new IHTMLElement(IHTMLElement.HTMLElementEnum.h1,
                Title).AttachTo(Menu);

            Native.Document.title = Title;


            var List = new IHTMLElement(IHTMLElement.HTMLElementEnum.ol).AttachTo(Menu);

            var ApplicationsWithLoadingImagesQuery =
                from t in Applications
                let assembly = t.Assembly.GetName().Name
                let preview = "assets/" + assembly + "/Preview.png"
                let image = new IHTMLImage(preview)
                orderby t.Name
                select new { t, assembly, preview, image };

            var ApplicationsWithLoadingImages = ApplicationsWithLoadingImagesQuery.ToArray();

            var LoadingMessage = new IHTMLDiv().AttachTo(Menu);

            var DoneLoading = 500.Until(
                t =>
                {
                    var count = ApplicationsWithLoadingImages.Count(i => !i.image.complete);

                    LoadingMessage.innerText = count + " images are still loading...";

                    return (count == 0 || t.Counter == 6);
                }
            );

            Func<Point> GetCenter =
                () => new Point(Native.Window.Width / 2, Native.Window.Height / 2);

            Action<Type> TypeClicked = t => { };

            DoneLoading +=
                delegate
                {
                    var query = from i in ApplicationsWithLoadingImages
                                let hasimage = i.image.complete && i.image.width > 0
                                select new { i.image, i.t, i.assembly, hasimage, i.preview }; ;

                    var WithImages =
                        from i in query
                        where i.hasimage
                        select i;


                    var WithoutImages =
                        from i in query
                        where !i.hasimage
                        select i;

                    #region WithImages
                    DoneLoading = WithImages.ForEachAtInterval(50,
                        v =>
                        {
                            LoadingMessage.innerText = v.t.Name;

                            var r =
                             new ReflectionSetup
                             {
                                 Image = v.image,
                                 Position = new Point(0, 0),
                                 Size = new Point(120, 90),
                                 ReflectionZoom = 0.5,
                                 Drag = false,
                                 Bottom = 2
                             }.ConvertToImageReflection();

                            r.style.position = IStyle.PositionEnum.relative;
                            r.style.margin = "3em";
                            r.style.marginLeft = "1em";
                            r.style.marginRight = "1em";
                            r.style.Float = IStyle.FloatEnum.left;

                            var href = v.t.Name + ".htm";

                            var a = new IHTMLAnchor(href, "");

                            a.target = "_blank";
                            v.image.style.border = "0px solid black";
                            v.image.AttachTo(a);
                            a.AttachTo(r);

                            a.onclick +=
                                ev =>
                                {
                                    ev.PreventDefault();

                                    TypeClicked(v.t);
                                };

                            r.AttachTo(Menu);


                            #region name
                            var name = new IHTMLAnchor(href, v.t.Name);

                            name.style.position = IStyle.PositionEnum.absolute;
                            name.style.textDecoration = "none";
                            name.style.color = Color.White;

                            name.target = "_blank";
                            name.style.top = "-1.5em";
                            name.AttachTo(r);
                            #endregion


                        }
                    );
                    #endregion

                    DoneLoading +=
                        delegate
                        {
                            LoadingMessage.Dispose();

                            var clr = new IHTMLBreak();

                            clr.style.clear = "both";
                            clr.AttachTo(Menu);

                            foreach (var v in WithoutImages)
                            {
                                new IHTMLDiv(v.assembly + " - " + v.t.Name + " - " + v.preview).AttachTo(Menu);
                            }
                        };

                };

            TypeClicked +=
                t =>
                {
                    Menu.Dispose();

                    try
                    {
                        Activator.CreateInstance(t);
                    }
                    catch (Exception exc)
                    {
                        Native.Window.alert("Error: " + exc.Message);

                        Menu.AttachToDocument();
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
