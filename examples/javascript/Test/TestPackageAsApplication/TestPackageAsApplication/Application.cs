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
using TestPackageAsApplication;
using TestPackageAsApplication.Design;
using TestPackageAsApplication.HTML.Pages;
using ScriptCoreLib.Shared.Lambda;
using System.IO;
using System.Diagnostics;
using ScriptCoreLib.Shared.BCLImplementation.System;

//namespace ScriptCoreLib.HTML.DOM
namespace ScriptCoreLib.JavaScript.DOM.HTML
{
    public static class HTMLAnchorDragToPackageExtensions
    {
        public static void AllowToDragAsApplicationPackage(this IHTMLAnchor dragme)
        {
            //img.width = Native.Window.Width / 2;
            //img.height = Native.Window.Height / 2;


            #region PackageAsApplication
            Action<IHTMLScript, XElement, Action<string>> PackageAsApplication =
                (source0, xml, yield) =>
                {
                    new IXMLHttpRequest(
                        ScriptCoreLib.Shared.HTTPMethodEnum.GET,
                        source0.src,
                        handler: (IXMLHttpRequest r) =>
                        {
                            // store hash
                            xml.Add(new XElement("link", new XAttribute("rel", "location"), new XAttribute("href", Native.Document.location.hash)));


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

            // XMLHttpRequest cannot load file:///D:/view-source. Cross origin requests are only supported for HTTP. 



            Console.WriteLine("before PackageAsApplication");
            PackageAsApplication(

                // how would we know our current source location?
                new IHTMLScript { src = "view-source" },
                XElement.Parse(AppSource.Text),
                data =>
                {
                    Console.WriteLine("enter PackageAsApplication");
                    var bytes = Encoding.ASCII.GetBytes(data);
                    //var bytes_string = 
                    // https://developer.mozilla.org/en-US/docs/Web/API/WorkerLocation
                    // https://developer.mozilla.org/en-US/docs/Web/Reference/Functions_and_classes_available_to_workers
                    // https://bugs.webkit.org/show_bug.cgi?id=55663
                    // http://www.w3schools.com/jsref/met_win_btoa.asp
                    // https://developer.mozilla.org/en-US/docs/Web/API/Window.atob
                    // http://stackoverflow.com/questions/15386585/standard-functions-not-working-in-web-worker

                    // http://www.whatwg.org/specs/web-apps/current-work/multipage/webappapis.html#atob
                    // [object Uint8ClampedArray]

                    //  both the input and output of these functions are Unicode strings.
                    //Func<byte[], string> ToBase64String =

                    // X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\IO\BinaryReader.cs
                    // Uncaught RangeError: Maximum call stack size exceeded 
                    //Func<byte[], string> ToBase64String =
                    //    x => (string)
                    //        new IFunction("return window.btoa(this);").apply(__fromCharCode(x));

                    //  String.fromCharCode.apply(null, new Uint8ClampedArray([40, 41]))


                    //var data64 = System.Convert.ToBase64String(bytes);
                    //var data64 = ToBase64String(bytes);
                    var s = Stopwatch.StartNew();
                    Console.WriteLine("before ToBase64String");
                    //var data64 = ToBase64String(bytes);
                    // 0:4173ms after ToBase64String { ElapsedMilliseconds = 181 } 
                    //__Convert.InternalToBase64String = ToBase64String;
                    var data64 = System.Convert.ToBase64String(bytes);
                    Console.WriteLine("after ToBase64String " + new { s.ElapsedMilliseconds });


                    var datastream =
                        "application/octet-stream:App.htm:data:application/octet-stream;base64," + data64;

                    //Console.WriteLine(new { data64 });


                    // X:\jsc.svn\examples\javascript\DragDataTableIntoCSVFile\DragDataTableIntoCSVFile\Application.cs
                    // X:\jsc.svn\examples\javascript\DragIntoCRX\DragIntoCRX\Application.cs
                    //new IHTMLButton 

                    dragme.css.style.textDecoration = "underline";

                    dragme.css.hover.style.color = "blue";
                    dragme.css.active.style.color = "red";

                    Console.WriteLine("before ondragstart");

                    //dragme.draggable = true;

                    dragme.ondragstart +=
                            e =>
                            {
                                Console.WriteLine("enter ondragstart");

                                e.dataTransfer.effectAllowed = "copy";

                                //e.preventDefault();
                                //e.stopPropagation();

                                //e.dataTransfer.setData("text/plain", "Sphere");

                                // http://codebits.glennjones.net/downloadurl/virtualdownloadurl.htm
                                //e.dataTransfer.setData("DownloadURL", "image/png:Sphere.png:" + icon);


                                // X:\jsc.svn\market\synergy\javascript\chrome\chrome\DOM\DataTransferExtensions.cs


                                e.dataTransfer.setData(
                                    "DownloadURL", datastream
                                    );

                                ////e.dataTransfer.setDownloadURL(

                                //e.dataTransfer.setData("text/html", data);
                                //e.dataTransfer.setData("text/uri-list", Native.document.location.href);

                                //e.dataTransfer.setDragImage(img, img.width / 2, img.height / 2);
                            };



                }
            );
        }
    }
}
namespace TestPackageAsApplication
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
            // this is very similar what we have to do to convert html to svg to img

            // X:\jsc.svn\examples\javascript\DragDataTableIntoCSVFile\DragDataTableIntoCSVFile\Application.cs

            try
            {
                throw new InvalidOperationException("where am i");
            }
            catch (Exception ex)
            {
                //{ Message = InvalidOperationException: where am i, StackTrace = Error: InvalidOperationException: where am i
                //    at lwkABmfn8j_aa_bqCu59PrPg (http://192.168.43.252:20872/snapshot+1:23026:57)

                //{ Message = InvalidOperationException: where am i, StackTrace = Error: InvalidOperationException: where am i
                //    at MxYABmfn8j_aa_bqCu59PrPg (http://192.168.43.252:4889/:23025:57)

                //{ Message = InvalidOperationException: where am i, StackTrace = Error: InvalidOperationException: where am i
                //    at MxYABmfn8j_aa_bqCu59PrPg (http://192.168.43.252:20595/view-source:23025:57)

                //{ Message = InvalidOperationException: where am i, StackTrace = Error: InvalidOperationException: where am i
                //    at MxYABmfn8j_aa_bqCu59PrPg (file:///D:/App.htm:23037:57)
                //

                new IHTMLPre { new { ex.Message, ex.StackTrace } }.AttachToDocument();

            }

            new IHTMLButton { "make me the link " + new { Native.document.location.protocol } }.AttachToDocument().WhenClicked(
               makelink =>
               {
                   makelink.disabled = true;

                   new IHTMLAnchor { "drag me" }.AttachToDocument().AllowToDragAsApplicationPackage();



               }
               );


        }

    }
}
