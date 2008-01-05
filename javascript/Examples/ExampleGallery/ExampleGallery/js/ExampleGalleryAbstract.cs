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

    [Script]
    public abstract class ExampleGalleryAbstract
    {
        public abstract Type[] Applications { get; }


        public void Initialize(IHTMLElement Menu, Func<IHTMLImage, string, Type, Action<Type>, IHTMLElement> ConvertImageToControl)
        {
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

                            var href = v.t.Name + ".htm";

                            var r = ConvertImageToControl(v.image, href, v.t, TypeClicked);

                            r.AttachTo(Menu);

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
                                new IHTMLDiv("image not found: " + v.preview).AttachTo(Menu);
                            }


                            "script".DisposeElementsByTagName();
                            "noscript".DisposeElementsByTagName();

                            
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

        }

   
    }
}
