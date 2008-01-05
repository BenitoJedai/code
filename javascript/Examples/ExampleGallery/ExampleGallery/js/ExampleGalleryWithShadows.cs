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
    public partial class ExampleGalleryWithShadows : ExampleGalleryWithApplications
    {
        // shadow with by http://www.cycloloco.com/shadowmaker/shadowmaker.htm

        public ExampleGalleryWithShadows()
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
            var Title = typeof(ExampleGalleryWithShadows).Name;

            new IHTMLElement(IHTMLElement.HTMLElementEnum.h1,
                Title).AttachTo(Menu);

            Native.Document.title = Title;


            new IHTMLImage("assets/ExampleGallery/PreviewSelection.png").InvokeOnComplete(
                selection =>
            new IHTMLImage("assets/ExampleGallery/PreviewShadow.png").InvokeOnComplete(
                img =>
                {
                    base.Initialize(Menu,
                        (image, href, type, TypeClicked) =>
                        {
                            var div = new IHTMLDiv();

                            div.style.SetSize(120, 90);

                            var shadow = (IHTMLImage)img.cloneNode(false);

                            shadow.AttachTo(div);
                            shadow.style.SetLocation(0, 0);
                            shadow.style.zIndex = 0;


                            image.AttachTo(div);
                            image.style.SetSize(120, 90);




                            div.style.position = IStyle.PositionEnum.relative;
                            div.style.marginTop = "3em";
                            div.style.marginLeft = "1em";
                            div.style.marginRight = "1em";
                            div.style.marginBottom = "1em";
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

                            a.target = "_blank";
                            a.style.display = IStyle.DisplayEnum.block;

                            var cselection = (IHTMLImage)selection.cloneNode(false);
                            cselection.style.borderWidth = "0px";
                            cselection.style.SetLocation(-9, -9);
                            cselection.style.zIndex = 1;
                            cselection.className = "PreviewShadow";
                            cselection.AttachTo(a);


                            image.style.border = "0px solid black";
                            image.style.zIndex = 2;
                            image.AttachTo(a);

                            a.className = "Preview";
                            a.AttachTo(div);
                            a.style.zIndex = 2;
                            a.style.SetLocation(0, 0, 120, 90);


                            a.onclick +=
                                ev =>
                                {
                                    ev.PreventDefault();

                                    TypeClicked(type);
                                };

                            return div;
                        }
                   );
                }
            )
            );
        }

        static ExampleGalleryWithShadows()
        {
            typeof(ExampleGalleryWithShadows).Spawn();
        }
    }
}

