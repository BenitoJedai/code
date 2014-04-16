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
using TestGallerySlider;
using TestGallerySlider.Design;
using TestGallerySlider.HTML.Pages;
using ScriptCoreLib.Lambda;

namespace TestGallerySlider
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
            var imgArray = new List<GalleryImage>();

            var gallery = new Gallery();
            var fontAwsome = new TTF.fontawesome_webfont();
            gallery.GalleryContainer.style.fontFamily = fontAwsome;
            gallery.LeftContainer.innerText = "\xf060";
            gallery.RightContainer.innerText = "\xf061";

            var img1 = new GalleryImage();
            var img2 = new GalleryImage();
            var img3 = new GalleryImage();
            var img4 = new GalleryImage();
            var img5 = new GalleryImage();
            var img6 = new GalleryImage();

            img1.GalleryImage.innerText = "Hello 1";
            img2.GalleryImage.innerText = "Hello 2";
            img3.GalleryImage.innerText = "Hello 3";
            img4.GalleryImage.innerText = "Hello 4";
            img5.GalleryImage.innerText = "Hello 5";
            img6.GalleryImage.innerText = "Hello 6";



            imgArray.Add(img1);
            imgArray.Add(img2);
            imgArray.Add(img3);
            imgArray.Add(img4);
            imgArray.Add(img5);
            imgArray.Add(img6);

            img1.AttachTo(gallery.Holder);
            img2.AttachTo(gallery.Holder);
            img3.AttachTo(gallery.Holder);
            img4.AttachTo(gallery.Holder);
            img5.AttachTo(gallery.Holder);
            img6.AttachTo(gallery.Holder);

            gallery.AttachToDocument();

            gallery.RightContainer.css.hover.style.color = "#606060";
            gallery.LeftContainer.css.hover.style.color = "#606060";

            foreach (var i in imgArray)
            {
                new IStyle(i.GalleryImage)
                {
                    width = "10em",
                    transition = "width linear 300ms",
                    Opacity = 1
                };
            }


            var counterL = 0;
            var counterR = imgArray.Count - 1;


            gallery.LeftContainer.onclick += async delegate
            {
                Console.WriteLine("Counter L" + counterL);
                Console.WriteLine("Counter R" + counterL);
                var temp = imgArray[counterR];
                var newTemp = new GalleryImage();
                newTemp.GalleryImage.innerText = "Helloo" + counterL;

                new IStyle(newTemp.GalleryImage)
                {
                    width = "0em",
                    transition = "width linear 300ms",
                    Opacity = 1
                };


                gallery.Holder.insertBefore(newTemp.AsNode(), imgArray[counterL].AsNode());

                Native.window.requestAnimationFrame += delegate
                {
                    temp.GalleryImage.style.width = "0px";
                    newTemp.GalleryImage.style.width = "10em";
                };
                imgArray[counterR] = newTemp;
                await 305;

                temp.Orphanize();
                counterR--;
                counterL--;
                if (counterL > imgArray.Count-1)
                {
                    counterL = 0;
                }
                if (counterR > imgArray.Count - 1)
                {
                    counterR = 0;
                }
                if (counterR < 0)
                {
                    counterR = imgArray.Count - 1;
                }
                if (counterL < 0)
                {
                    counterL = imgArray.Count - 1;
                }
            };

            gallery.RightContainer.onclick += async delegate
            {
                Console.WriteLine("Right click" + counterR);
                var temp = imgArray[counterL];
                var newTemp = new GalleryImage();
                newTemp.GalleryImage.innerText = "Helloo" + counterR;

                new IStyle(newTemp.GalleryImage)
                {
                    width = "0em",
                    transition = "width linear 300ms",
                    Opacity = 1
                };

                newTemp.AttachTo(gallery.Holder);
                

                Native.window.requestAnimationFrame += delegate
                {
                    temp.GalleryImage.style.width = "0px";
                    newTemp.GalleryImage.style.width = "10em";
                };
                await 305;

                temp.Orphanize();
                imgArray[counterL] = newTemp;
                counterL++;
                counterR++;
                if (counterL > imgArray.Count - 1)
                {
                    counterL = 0;
                }
                if (counterR > imgArray.Count - 1)
                {
                    counterR = 0;
                }
                if (counterR < 0)
                {
                    counterR = imgArray.Count - 1;
                }
                if (counterL < 0)
                {
                    counterL = imgArray.Count - 1;
                }
            };

        }

    }
}
