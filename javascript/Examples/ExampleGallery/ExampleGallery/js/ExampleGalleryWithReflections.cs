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
    public partial class ExampleGalleryWithReflections : ExampleGalleryWithApplications
    {

        public ExampleGalleryWithReflections()
        {

            Native.Document.body.style.Aggregate(
               style =>
               {
                   style.backgroundColor = Color.Gray;
                   style.backgroundImage = "url(assets/ExampleGallery/bg.png)";

                   style.color = Color.White;
               }
           );

            try
            {
                IStyleSheet.Default.AddRule("a.Preview img.PreviewShadow").style.display = IStyle.DisplayEnum.none;
                IStyleSheet.Default.AddRule("a.Preview:hover img.PreviewShadow").style.display = IStyle.DisplayEnum.block;
            }
            catch
            {
                // no css support
            }

            var Menu = new IHTMLDiv().AttachToDocument();
            var Title = typeof(ExampleGalleryWithReflections).Name;

            new IHTMLElement(IHTMLElement.HTMLElementEnum.h1,
                Title).AttachTo(Menu);

            Native.Document.title = Title;

            new IHTMLImage("assets/ExampleGallery/PreviewSelection.png").InvokeOnComplete(
            selection =>
                {
                    base.Initialize(Menu,
                        (image, href, type, TypeClicked) =>
                        {
                            var div =
                                new ReflectionSetup
                                {
                                    Image = image,
                                    Position = new Point(0, 0),
                                    Size = new Point(120, 90),
                                    ReflectionZoom = 0.5,
                                    Drag = false,
                                    Bottom = 2
                                }.ConvertToImageReflection();

                            div.style.position = IStyle.PositionEnum.relative;
                            div.style.marginTop = "3em";
                            div.style.marginLeft = "1em";
                            div.style.marginRight = "1em";
                            div.style.marginBottom = "3em";
                            div.style.Float = IStyle.FloatEnum.left;

                            #region name
                            var name = new IHTMLAnchor(href, type.Name);

                            name.style.position = IStyle.PositionEnum.absolute;
                            name.style.textDecoration = "none";
                            name.style.color = Color.White;

                            name.target = "_blank";
                            name.style.top = "-1.5em";
                            name.AttachTo(div);
                            #endregion

                            var a = new IHTMLAnchor(href, "");

                            var cselection = (IHTMLImage)selection.cloneNode(false);
                            cselection.style.borderWidth = "0px";
                            cselection.style.SetLocation(-9, -9);
                            cselection.style.zIndex = 1;
                            cselection.className = "PreviewShadow";
                            cselection.AttachTo(a);

                            a.className = "Preview";
                            a.target = "_blank";
                            image.style.border = "0px solid black";
                            image.AttachTo(a);
                            image.style.zIndex = 2;
                            a.AttachTo(div);
                            a.style.zIndex = 2;
                            a.style.SetLocation(0, 0);

                            a.onclick +=
                                ev =>
                                {
                                    ev.PreventDefault();

                                    TypeClicked(type);
                                };

                            return div;
                        });
                    }
             );
        }


        static ExampleGalleryWithReflections()
        {
            typeof(ExampleGalleryWithReflections).Spawn();
        }
    }
}
