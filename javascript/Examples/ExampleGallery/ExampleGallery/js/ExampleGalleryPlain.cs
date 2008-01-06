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
    public partial class ExampleGalleryPlain : ExampleGalleryWithApplications
    {
        // shadow with by http://www.cycloloco.com/shadowmaker/shadowmaker.htm

        public ExampleGalleryPlain()
        {

            var Menu = new IHTMLDiv().AttachToDocument();
            var Title = typeof(ExampleGalleryPlain).Name;

            new IHTMLElement(IHTMLElement.HTMLElementEnum.h1,
                Title).AttachTo(Menu);

            new IHTMLDiv("This is a collection of projects developed with jsc. They were coded in c# and translated to javascript with jsc. Click on the pictures to load the applications or on the names to open them in a new window. You might need to press refresh to come back to this index. Enoy! :)").AttachTo(Menu);

            base.Initialize(Menu,
                (image, href, type, TypeClicked) =>
                {
                    var div = new IHTMLDiv();

                    var a = new IHTMLAnchor(href, "");

                    a.target = "_blank";
                    a.style.textDecoration = "none";
                    a.style.whiteSpace = IStyle.WhiteSpaceEnum.nowrap;

                    image.style.border = "0px solid black";
                    image.style.SetSize(120, 90);
                    image.AttachTo(a);
                    image.style.margin = "0.2em";
                    image.style.marginRight = "1em";
                    image.style.verticalAlign = "middle";

                    a.appendChild(type.Name);

                    a.onclick +=
                        ev =>
                        {
                            ev.PreventDefault();

                            TypeClicked(type);
                        };

                    a.AttachTo(div);

                    return div;
                }
           );
        }

        static ExampleGalleryPlain()
        {
            typeof(ExampleGalleryPlain).Spawn();
        }
    }
}

