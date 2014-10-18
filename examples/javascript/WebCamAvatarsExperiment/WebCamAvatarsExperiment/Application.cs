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
using WebCamAvatarsExperiment;
using WebCamAvatarsExperiment.Design;
using WebCamAvatarsExperiment.HTML.Pages;

namespace WebCamAvatarsExperiment
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
           

            // test it
            //base.Insert(
            //    new Abstractatech.JavaScript.Avatar.Design.WebCamAvatarsSheet1Row()
            //    );

            page.Reset.WhenClicked(
                delegate
            {
                this.Reset();
            }
            );

            page.NewImage.WhenClicked(
                //async 
                button =>
                {
                    var overlay = new IHTMLDiv { }.AttachTo(Native.document.documentElement);
                    overlay.style.position = IStyle.PositionEnum.@fixed;
                    overlay.style.left = "0";
                    overlay.style.top = "0";
                    overlay.style.right = "0";
                    overlay.style.bottom = "0";
                    overlay.style.backgroundColor = "black";

                    var div = new IHTMLDiv { }.AttachTo(overlay);

                    var css = Native.document.body.css.children;

                    css.style.display = IStyle.DisplayEnum.none;

                    Abstractatech.JavaScript.Avatar.ApplicationImplementation.MakeCamGrabber(
                        div,
                        sizeToWindow: true,
                        yield:
                        y =>
                        {
                            overlay.Orphanize();
                            css.style.display = IStyle.DisplayEnum.empty;

                            // napta ocr?
                            // http://projectnaptha.com/

                            new IHTMLImage { y.Avatar640x480 }.AttachToDocument();



                            new IHTMLImage { y.Avatar96gif }.AttachToDocument();

                            //  method:Void <.ctor>b__3(Abstractatech.JavaScript.Avatar.Design.WebCamAvatarsSheet1Row), ex = System.InvalidOperationException: Unable to change after type has been created.
                            //base.Insert(y.Avatar96gif);
                            base.Insert(y);
                        }

                   );
                }
            );

        }



    }
}
