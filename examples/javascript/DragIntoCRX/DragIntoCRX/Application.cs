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
using DragIntoCRX.Design;
using DragIntoCRX.HTML.Pages;

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


            //page.DragCRX.draggable = true;

            page.DragCRX.ondragstart +=
                e =>
                {
                    e.dataTransfer.setData("text/plain", "DragIntoCRX");
                    //e.dataTransfer.setData("text/uri-list", Native.Document.location + "");

                    // http://codebits.glennjones.net/downloadurl/virtualdownloadurl.htm

                    var data = "Cr24";

                    var bytes = Encoding.ASCII.GetBytes(data);

                    var data64 = Convert.ToBase64String(bytes);


                    e.dataTransfer.setData("DownloadURL", "application/octet-stream:DragIntoCRX.crx:data:application/octet-stream;base64," + data64);
                };


            //page.DragEXE.draggable = true;

            page.DragEXE.ondragstart +=
                e =>
                {
                    e.dataTransfer.setData("text/plain", "DragIntoCRX");
                    //e.dataTransfer.setData("text/uri-list", Native.Document.location + "");

                    // http://codebits.glennjones.net/downloadurl/virtualdownloadurl.htm

                    var data = "MZ";

                    var bytes = Encoding.ASCII.GetBytes(data);

                    var data64 = Convert.ToBase64String(bytes);


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

                    var data64 = Convert.ToBase64String(bytes);


                    e.dataTransfer.setData("DownloadURL", "application/octet-stream:DragIntoCRX.app:data:application/octet-stream;base64," + data64);
                };

            page.DragHTM.ondragstart +=
                 e =>
                 {
                     e.dataTransfer.setData("text/plain", "DragIntoCRX");
                     //e.dataTransfer.setData("text/uri-list", Native.Document.location + "");

                     // http://codebits.glennjones.net/downloadurl/virtualdownloadurl.htm

                     var xml = new App();

                     var data = xml.Container.AsXElement().ToString();

                     var bytes = Encoding.ASCII.GetBytes(data);

                     var data64 = Convert.ToBase64String(bytes);


                     e.dataTransfer.setData("DownloadURL", "application/octet-stream:DragIntoCRX.htm:data:application/octet-stream;base64," + data64);
                 };
        }

    }
}
