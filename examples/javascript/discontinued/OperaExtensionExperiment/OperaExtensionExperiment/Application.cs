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
//using OperaExtensionExperiment.Design;
using OperaExtensionExperiment.HTML.Pages;
using ScriptCoreLib.JavaScript.Runtime;

namespace OperaExtensionExperiment
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application
    {
        //public readonly ApplicationWebService service = new ApplicationWebService();

        sealed class OperaToolbarPopupArguments
        {
            public string href;
            public int width = 400;
            public int height = 400;
        }


        sealed class Opera_UIItemProperties
        {
            public string title;
            public string icon;
            public OperaToolbarPopupArguments popup;
            public IFunction onclick;
        }

        sealed class Opera_BrowserWindowProperties
        {
            public int width;
        }

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            var toolbar = new
            {
                createItem = new IFunction("y", "return opera.contexts.toolbar.createItem(y);"),
                addItem = new IFunction("y", "opera.contexts.toolbar.addItem(y);")
            };

            var windows = new
            {
                // http://dev.opera.com/articles/view/extensions-api-windows-create/
                create = new IFunction("", "return opera.extension.windows.create();"),

                // http://dev.opera.com/articles/view/extensions-api-window-update/
                update = new IFunction("y", "this.update(y);"),
            };

            try
            {
                // can only create one button?
                toolbar.createItem.apply(null,
                       new Opera_UIItemProperties
                       {
                           title = "OperaExtensionExperiment",
                           popup = new OperaToolbarPopupArguments { href = "index.html" }
                       }
                   ).With(
                       theButton =>
                       {

                           toolbar.addItem.apply(null, theButton);
                       }
                   );

            }
            catch
            {
                // not running as extension?
            }

            // will only work on toolbar
            //page.NewBrowser.style.color = JSColor.Green;
            //page.NewBrowser.onclick +=
            //    delegate
            //    {
            //        var w = windows.create.apply(null);

            //        windows.update.apply(w,
            //            new Opera_BrowserWindowProperties
            //            {

            //                width = 400
            //            }
            //        );
            //    };

            page.IAmAPopup.style.color = JSColor.Green;

            page.IAmAPopup.onclick +=
                delegate
                {
                    page.IAmAPopup.style.color = JSColor.Red;

                    var getScreenshot = new IFunction("y", "opera.extension.getScreenshot(y);");

                    Action<ImageData> applyScreenshot =
                        imageData =>
                        {
                            // Create a blank canvas
                            var canvas = new IHTMLCanvas();
                            canvas.width = (int)imageData.width;
                            canvas.height = (int)imageData.height;

                            // Write the screenshot image data to the canvas context
                            var ctx = (CanvasRenderingContext2D)canvas.getContext("2d");
                            ctx.putImageData(imageData, 0, 0, 0, 0, imageData.width, imageData.height);

                            canvas.style.border = "1px solid blue";
                            canvas.AttachToDocument();
                            page.IAmAPopup.style.color = JSColor.Blue;
                        };

                    // http://dev.opera.com/articles/view/extensions-api-screenshot/

                    getScreenshot.apply(null, IFunction.OfDelegate(applyScreenshot));

                };

            page.foooex.ondragstart +=

                e =>
                {
                    // http://ajaxian.com/archives/how-to-drag-out-files-like-gmail

                    e.dataTransfer.setData("DownloadURL",
                         "application/octet-stream:foo.oex:" + page.foooex.href);

                };
        }

    }
}
