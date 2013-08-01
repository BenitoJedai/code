using MSVSFormStyle.Design;
using MSVSFormStyle.HTML.Pages;
using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Shared.Lambda;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.Windows.Forms;
using System;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Abstractatech.JavaScript.FormAsPopup;

namespace MSVSFormStyle
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application
    {
        public readonly ApplicationWebService service = new ApplicationWebService();

        public readonly ApplicationControl content = new ApplicationControl();

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {

            content.button1.Click +=
                delegate
                {
                    FormStyler.AtFormCreated = FormStyler.LikeWindowsClassic;
                    new Form1().PopupInsteadOfClosing(HandleFormClosing: false).Show();
                };

            content.button2.Click +=
               delegate
               {
                   FormStyler.AtFormCreated = FormStyler.LikeVisualStudioMetro;
                   new Form1().PopupInsteadOfClosing(HandleFormClosing: false).Show();
               };

            content.button3.Click +=
                 delegate
                 {
                     FormStyler.AtFormCreated = FormStyler.LikeWindows3;
                     new Form1().PopupInsteadOfClosing(HandleFormClosing: false).Show();
                 };


            content.button4.Click +=
                 delegate
                 {
                     FormStyler.AtFormCreated = s =>
                        {


                            s.TargetOuterBorder.style.boxShadow = "rgba(255, 122, 204, 0.3) 0px 0px 6px 3px";
                            s.TargetOuterBorder.style.borderColor = JSColor.FromRGB(255, 122, 204);

                            s.TargetInnerBorder.style.borderWidth = "0px";

                            s.CloseButton.style.color = JSColor.White;
                            s.CloseButton.style.backgroundColor = JSColor.None;
                            s.CloseButton.style.borderWidth = "0px";
                            s.CloseButtonContent.style.borderWidth = "0px";

                            s.TargetResizerPadding.style.left = "0px";
                            s.TargetResizerPadding.style.top = "0px";
                            s.TargetResizerPadding.style.right = "0px";
                            s.TargetResizerPadding.style.bottom = "0px";

                            s.Caption.style.backgroundColor = JSColor.FromRGB(255, 122, 204);
                        };

                     new Form1().PopupInsteadOfClosing(HandleFormClosing: false).Show();
                 };

            content.button6.Click +=
               delegate
               {
                   FormStyler.AtFormCreated = FormStyler.LikeWindows98;
                   new Form1().PopupInsteadOfClosing(HandleFormClosing: false).Show();
               };


            content.button7.Click +=
             delegate
             {
                 FormStyler.AtFormCreated = FormStylerLikeAero.LikeAero;

                 new Form1().PopupInsteadOfClosing(HandleFormClosing: false).Show();
             };


            content.AttachControlTo(Native.document.body);

            //content.AttachControlTo(page.Content);
            //content.AutoSizeControlTo(page.ContentSize);

            @"Style".ToDocumentTitle();




            Native.document.getElementsByTagName("script")
                .Select(k => (IHTMLScript)k)
                .FirstOrDefault(k => k.src.EndsWith("/view-source"))
                .With(
                    source =>
                    {
                        #region PackageAsApplication
                        Action<IHTMLScript, XElement, Action<string>> PackageAsApplication =
                            (source0, xml, yield) =>
                            {
                                new IXMLHttpRequest(
                                    ScriptCoreLib.Shared.HTTPMethodEnum.GET, source0.src,
                                    (IXMLHttpRequest r) =>
                                    {
                                        // store hash
                                        xml.Add(new XElement("link", new XAttribute("rel", "location"), new XAttribute("href", Native.document.location.hash)));


                                        #region script
                                        xml.Add(
                                            new XElement("script",
                                                "/* source */"
                                           )
                                        );

                                        var data = "";


                                        Action later = delegate
                                        {

                                            data = data.Replace("/* source */", r.responseText);

                                        };
                                        #endregion


                                        //Native.Document.getElementsByTagName("link").AsEnumerable().ToList().ForEach(

                                        xml.Elements("link").ToList().ForEach(
                                            (XElement link, Action next) =>
                                            {
                                                #region style
                                                var rel = link.Attribute("rel");
                                                if (rel.Value != "stylesheet")
                                                {
                                                    next();
                                                    return;
                                                }

                                                var href = link.Attribute("href");

                                                var placeholder = "/* " + href.Value + " */";

                                                //page.DragHTM.innerText += " " + placeholder;


                                                xml.Add(new XElement("style", placeholder));

                                                new IXMLHttpRequest(ScriptCoreLib.Shared.HTTPMethodEnum.GET, href.Value,
                                                    rr =>
                                                    {

                                                        later += delegate
                                                        {


                                                            data = data.Replace(placeholder, rr.responseText);

                                                        };

                                                        Console.WriteLine("link Remove");
                                                        link.Remove();

                                                        next();
                                                    }
                                                );

                                                #endregion
                                            }
                                        )(
                                            delegate
                                            {


                                                data = xml.ToString();
                                                Console.WriteLine("data: " + data);
                                                later();

                                                yield(data);
                                            }
                                        );
                                    }
                                );

                            };
                        #endregion


                        PackageAsApplication(
                             source,
                             XElement.Parse(new App.XMLSourceSource().Text),
                             data =>
                             {
                                 var bytes = Encoding.ASCII.GetBytes(data);
                                 var data64 = System.Convert.ToBase64String(bytes);


                                 Native.document.body.title = "Drag me!";

                                 Native.document.body.ondragstart +=
                                         e =>
                                         {
                                             //e.dataTransfer.setData("text/plain", "Sphere");

                                             // http://codebits.glennjones.net/downloadurl/virtualdownloadurl.htm
                                             //e.dataTransfer.setData("DownloadURL", "image/png:Sphere.png:" + icon);

                                             e.dataTransfer.setData("DownloadURL", "application/octet-stream:Spiral.htm:data:application/octet-stream;base64," + data64);
                                             e.dataTransfer.setData("text/html", data);
                                             e.dataTransfer.setData("text/uri-list", Native.document.location + "");
                                             //e.dataTransfer.setDragImage(img, img.width / 2, img.height / 2);
                                         };


                             }
                         );
                    }
            );

        }

    }
}
