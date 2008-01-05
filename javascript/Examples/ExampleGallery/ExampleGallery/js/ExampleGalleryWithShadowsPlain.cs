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
    public partial class ExampleGalleryWithShadowsPlain : ExampleGalleryWithApplications
    {
        // shadow with by http://www.cycloloco.com/shadowmaker/shadowmaker.htm

        public ExampleGalleryWithShadowsPlain()
        {

            var Menu = new IHTMLDiv().AttachToDocument();
            var Title = typeof(ExampleGalleryWithShadows).Name;

            new IHTMLElement(IHTMLElement.HTMLElementEnum.h1,
                Title).AttachTo(Menu);

            new IHTMLDiv("This is a collection of projects developed with jsc. They were coded in c# and translated to javascript with jsc. Click on the pictures to load the applications or on the names to open them in a new window. You might need to press refresh to come back to this index. Enoy! :)").AttachTo(Menu);


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
                            div.style.marginTop = "1em";
                            div.style.marginLeft = "1em";
                            div.style.marginRight = "12em";
                            div.style.marginBottom = "1em";
                            div.style.Float = IStyle.FloatEnum.left;
                            //div.style.clear =  "both";


                            #region name
                            var name = new IHTMLAnchor(href, type.Name);

                            name.style.position = IStyle.PositionEnum.absolute;
                            name.className = "PreviewName";
                            name.style.color = Color.Black;
                            name.style.textDecoration = "none";

                            name.target = "_blank";
                            name.style.left = "130px";
                            name.AttachTo(div);
                            #endregion

                            var a = new IHTMLAnchor(href, "");

                            a.target = "_blank";
                            a.style.display = IStyle.DisplayEnum.block;

               
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

                                    Native.Document.body.style.backgroundImage = "";

                                    TypeClicked(type);
                                };

                            return div;
                        }
                   );
                });
        }

        static ExampleGalleryWithShadowsPlain()
        {
            typeof(ExampleGalleryWithShadowsPlain).Spawn();
        }
    }
}

