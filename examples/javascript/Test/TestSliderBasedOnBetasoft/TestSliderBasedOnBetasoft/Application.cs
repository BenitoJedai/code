using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TestSliderBasedOnBetasoft;
using TestSliderBasedOnBetasoft.Design;
using TestSliderBasedOnBetasoft.HTML.Pages;
using ScriptCoreLib.Lambda;
using TestSliderBasedOnBetasoft.HTML.Images.FromAssets;


namespace TestSliderBasedOnBetasoft
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {
        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            var slide = 0;

            var gallery = new Gallery();
            var fontAwsome = new TTF.fontawesome_webfont();
            gallery.GalleryContainer.style.fontFamily = fontAwsome;
            gallery.Lch.innerText = "\xf060";
            gallery.Rch.innerText = "\xf061";

            new bg().ToBackground(gallery.GalleryContainer.style);

            var first = new TextFloatDiv();
            var snd = new TextFloatDiv();
            var trd = new BigTextDiv();
            var pic = new NewPictureDiv();
            var slide0 = new Slide0();

            new IStyle(first.TextDiv)
            {
                left = "-40em",
                transition = "all 2s ease",
            };
            first.TextDiv.AttachTo(gallery.GalleryContainer);

            new IStyle(snd.TextDiv)
            {
                right = "-40em",
                transition = "all 2s ease",
            };
            snd.AttachTo(gallery.GalleryContainer);

            new IStyle(trd.TextDiv)
            {
                top = "-40em",
                transition = "all 2s ease",
            };
            trd.AttachTo(gallery.GalleryContainer);

            new IStyle(pic.TextDiv)
            {
                left = "-40em",
                transition = "all 2s ease",
            };
            pic.TextDiv.AttachTo(gallery.GalleryContainer);

            new IStyle(slide0.TextDiv)
            {
                left = "20%",
               // transition = "all 2s ease",
                transition = "opacity 2s ease-out",
                Opacity = 0
            };
            slide0.TextDiv.AttachTo(gallery.GalleryContainer);

            gallery.AttachToDocument();

            gallery.Lch.css.hover.style.color = "#606060";
            gallery.Rch.css.hover.style.color = "#606060";

           
            gallery.LeftContainer.onclick += async delegate
            {
                if (slide == 0)
                {
                    Native.window.requestAnimationFrame += delegate
                    {
                        slide0.TextDiv.style.Opacity = 0;
                    };
                    await 1000;

                    slide = 2;
                    Native.window.requestAnimationFrame += delegate
                    {
                        pic.TextDiv.style.left = "40%";
                    };
                    await 1000;
                  
                }
                else if(slide == 1)
                {
                    slide--;
                    Native.window.requestAnimationFrame += delegate
                    {
                        first.TextDiv.style.left = "-40em";
                        snd.TextDiv.style.right = "0em";
                        snd.TextDiv.style.Opacity = 0;
                        trd.TextDiv.style.top = "-40em";
                    };
                    await 1000;
                    Native.window.requestAnimationFrame += delegate
                    {
                        slide0.TextDiv.style.Opacity = 1;
                    };
                    await 1000;
                }
                else if (slide == 2)
                {
                    slide--;
                    Native.window.requestAnimationFrame += delegate
                    {
                        pic.TextDiv.style.left = "-40em";
                    };
                    await 1000;

                    Native.window.requestAnimationFrame += delegate
                    {
                        first.TextDiv.style.left = "40%";
                        snd.TextDiv.style.right = "40%";
                        snd.TextDiv.style.Opacity = 1;
                        trd.TextDiv.style.top = "5%";
                    };
                    await 1000;
                }
            };

            gallery.RightContainer.onclick += async delegate
            {
                if (slide == 0)
                {
                    Native.window.requestAnimationFrame += delegate
                    {
                        slide0.TextDiv.style.Opacity = 0;
                    };
                    await 1000;
                    Native.window.requestAnimationFrame += delegate
                    {
                        first.TextDiv.style.left = "40%";
                        snd.TextDiv.style.right = "40%";
                        snd.TextDiv.style.Opacity = 1;
                        trd.TextDiv.style.top = "5%";
                    };
                    await 1000;
                    slide++;
                }
                else if (slide == 1)
                {
                    Native.window.requestAnimationFrame += delegate
                    {
                        first.TextDiv.style.left = "-40em";
                        snd.TextDiv.style.right = "0em";
                        snd.TextDiv.style.Opacity = 0;
                        trd.TextDiv.style.top = "-40em";
                    };
                    await 1000;
                    Native.window.requestAnimationFrame += delegate
                    {
                        pic.TextDiv.style.left = "40%";
                    };
                    await 1000;
                    slide++;
                }
                else if (slide == 2)
                {
                    Native.window.requestAnimationFrame += delegate
                    {
                        pic.TextDiv.style.left = "-40em";
                    };
                    await 1000;
                    Native.window.requestAnimationFrame += delegate
                    {
                        slide0.TextDiv.style.Opacity = 1;
                    };
                    await 1000;
                    slide = 0;
                }
            };
        }

    }
}
