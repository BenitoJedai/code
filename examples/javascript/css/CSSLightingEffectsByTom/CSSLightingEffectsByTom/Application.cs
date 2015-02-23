using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using System;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using CSSLightingEffectsByTom.Design;
using CSSLightingEffectsByTom.HTML.Pages;

namespace CSSLightingEffectsByTom
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application
    {
        public readonly ApplicationWebService service = new ApplicationWebService();

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            Native.Document.body.onmousemove +=
                e =>
                {
                    var horizontal = e.CursorX / (double)Native.window.Width;
                    var vertical = e.CursorY / (double)Native.window.Height;


                    //$('.ipad').css({
                    //    '-webkit-transform': 'rotateX(' + (7 - (vertical * 14)) + 'deg) rotateY(' + (-10 + (horizontal * 20)) + 'deg)'
                    //});


                    page.ipad.style.transform = "rotateX(" + (7 - (vertical * 14)) + "deg) rotateY(" + (-10 + (horizontal * 20)) + "deg)";


                    //$('.specular').css({
                    //    'background-position': (-200 + (horizontal * -500)) + 'px ' + (-vertical * 600) + 'px',
                    //    'opacity': 1 - (horizontal * .45) - (vertical * .45)
                    //});

                    page.specular.style.backgroundPosition = (-200 + (horizontal * -500)) + "px " + (-vertical * 600) + "px";
                    page.specular.style.Opacity = 1 - (horizontal * .45) - (vertical * .45);

                    //$('.home-btn').css({
                    //    'background': '-webkit-linear-gradient(-' + (80 - horizontal * 20) + 'deg, transparent 50%, rgba(255, 255, 255, .05) 50.1%, rgba(255, 255, 255, ' + (.4 - (horizontal * .1) - (vertical * .1)) + '))'
                    //});

                    //page.home.style.background = "-webkit-linear-gradient(-" + (80 - horizontal * 20) + "deg, transparent 50%, rgba(255, 255, 255, .05) 50.1%, rgba(255, 255, 255, " + (.4 - (horizontal * .1) - (vertical * .1)) + "))";



                    //$('.ipad.white .home-btn').css({
                    //    'background': '-webkit-linear-gradient(-' + (80 - horizontal * 20) + 'deg, rgba(0, 0, 0, ' + (.05 + (horizontal * .05) + (vertical * .05)) + '), rgba(0, 0, 0, 0) 50%, transparent 50.1%)',
                    //    '-webkit-box-shadow': '0 0 1px rgba(0, 0, 0, .1)'
                    //});

                    //$('.shadow').css({
                    //    '-webkit-transform': 'rotateX(' + (65 + (vertical * 20)) + 'deg) rotateY(' + (10 - (horizontal * 20)) + 'deg) skewX(-15deg)'
                    //});

                    page.shadow.style.transform = "rotateX(" + (65 + (vertical * 20)) + "deg) rotateY(" + (10 - (horizontal * 20)) + "deg) skewX(-15deg)";

                };

            Action onclick = delegate
            {
                if (page.home.disabled)
                    return;

                var discover = new IHTMLIFrame { src = "http://discover.xavalon.net", frameBorder = "0" }.AttachTo(page.ipad);

                var scale = 2;
                var zoom = 1;

                discover.style.transform = "scale(" + (1 / scale) + ")";
                discover.style.transformOrigin = "0% 0%";

                discover.style.SetSize(
                    (int)(page.ipad.clientWidth * zoom * scale),
                     (int)(page.ipad.clientHeight * zoom * scale)
                );

                //dynamic ds = discover.style;

                //ds.zoom = (1.0 / zoom) + "";

                page.specular.Hide();
                page.homescreen.Hide();
                page.home.disabled = true;
            };

            page.home.onclick +=
             delegate
             {

                 onclick();
             };

            new ScriptCoreLib.JavaScript.Runtime.Timer(
                delegate
                {
                    onclick();
                }
            ).StartTimeout(5000);

            @"Hello world".ToDocumentTitle();
            // Send data from JavaScript to the server tier
            service.WebMethod2(
                @"A string from JavaScript.",
                value => value.ToDocumentTitle()
            );
        }

    }
}
