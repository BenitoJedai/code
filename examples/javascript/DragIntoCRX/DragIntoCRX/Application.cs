using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.Shared.Lambda;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using System;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using DragIntoCRX.Design;
using DragIntoCRX.HTML.Pages;
using ScriptCoreLib.JavaScript.Runtime;

namespace DragIntoCRX
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
            // https://developer.chrome.com/extensions/crx.html

            #region DownloadURL

            page.DragCRX.ondragstart +=
                e =>
                {
                    e.dataTransfer.setData("text/plain", "DragIntoCRX");
                    //e.dataTransfer.setData("text/uri-list", Native.Document.location + "");

                    // http://codebits.glennjones.net/downloadurl/virtualdownloadurl.htm

                    var data = "Cr24";

                    var bytes = Encoding.ASCII.GetBytes(data);

                    var data64 = System.Convert.ToBase64String(bytes);


                    e.dataTransfer.setData("DownloadURL", "application/octet-stream:DragIntoCRX.crx:data:application/octet-stream;base64," + data64);
                };



            page.DragEXE.ondragstart +=
                e =>
                {
                    e.dataTransfer.setData("text/plain", "DragIntoCRX");
                    //e.dataTransfer.setData("text/uri-list", Native.Document.location + "");

                    // http://codebits.glennjones.net/downloadurl/virtualdownloadurl.htm

                    var data = "MZ";

                    var bytes = Encoding.ASCII.GetBytes(data);

                    var data64 = System.Convert.ToBase64String(bytes);


                    e.dataTransfer.setData("DownloadURL", "application/octet-stream:DragIntoCRX.exe:data:application/octet-stream;base64," + data64);
                };


            page.DragApp.ondragstart +=
                e =>
                {
                    e.dataTransfer.setData("text/plain", "DragIntoCRX");
                    //e.dataTransfer.setData("text/uri-list", Native.Document.location + "");

                    // http://codebits.glennjones.net/downloadurl/virtualdownloadurl.htm

                    var data = "<app></app>";

                    var bytes = Encoding.ASCII.GetBytes(data);

                    var data64 = System.Convert.ToBase64String(bytes);


                    e.dataTransfer.setData("DownloadURL", "application/octet-stream:DragIntoCRX.app:data:application/octet-stream;base64," + data64);
                };

            #endregion


            page.DragHTM.style.color = JSColor.Red;
            page.DragHtmlSource.style.color = JSColor.Red;

            var source = Native.Document
                .getElementsByTagName("script")
                .Select(k => (IHTMLScript)k)
                .FirstOrDefault(k => k.src.EndsWith("/view-source"));

            if (source == null)
            {
                new IHTMLPre
                {
                    innerText =
                        @"Probably the drag already happened. running as a clone.
We could look what script and style elements we have to package again.
WebService avilability is unknown. Should ask the server if it is there.
Could post message to see if we are being hosted in an iframe.
"
                }.AttachToDocument();

                page.DragHTM.Orphanize();
                page.DragHtmlSource.Orphanize();
            }
            else
            {
                #region PackageAsApplication
                Action<IHTMLScript, XElement, Action<string>> PackageAsApplication =
                    (source0, xml, yield) =>
                    {
                        new IXMLHttpRequest(
                            ScriptCoreLib.Shared.HTTPMethodEnum.GET, source0.src,
                            r =>
                            {
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

                                        page.DragHTM.innerText += " " + placeholder;


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

                        #region ondragstart
                        page.DragHTM.style.color = JSColor.Blue;
                        page.DragHTM.ondragstart +=
                                e =>
                                {

                                    e.dataTransfer.setData("text/plain", "DragIntoCRX");
                                    //e.dataTransfer.setData("text/uri-list", Native.Document.location + "");

                                    // http://codebits.glennjones.net/downloadurl/virtualdownloadurl.htm




                                    e.dataTransfer.setData("DownloadURL", "application/octet-stream:DragIntoCRX.htm:data:application/octet-stream;base64," + data64);
                                };

                        page.DragHtmlSource.style.color = JSColor.Blue;
                        page.DragHtmlSource.ondragstart +=
                                e =>
                                {
                                    e.dataTransfer.setData("text/html", data);
                                };
                        #endregion


                    }
                );
            }
        }

    }
}
