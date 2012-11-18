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
using RuntimeHTMLDesignMode.Design;
using RuntimeHTMLDesignMode.HTML.Pages;
using System.Collections.Generic;
using System.Diagnostics;

namespace RuntimeHTMLDesignMode
{
    public static class X
    {
        public static XElement ToXElementClone(this IHTMLElement e)
        {
            var oldxhtml = new XElement("xhtml");

            oldxhtml.Add(
                ((IHTMLElement)Native.Document.body.cloneNode(true)).AsXElement()
            );

            #region if chrome added xhtml ns lets remove it to keep things simple.
            // how do you remove the damn xmlns?
            oldxhtml =
                XElement.Parse(
                oldxhtml.ToString().Replace(" xmlns=\"http://www.w3.org/1999/xhtml\"", " ")
            );
            #endregion

            return oldxhtml.Elements().Single();
        }






    }

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
            @"Hello world".ToDocumentTitle();
            // Send data from JavaScript to the server tier
            service.WebMethod2(
                @"A string from JavaScript.",
                value => value.ToDocumentTitle()
            );

            #region AppContentAsXElement
            Func<XElement> AppContentAsXElement =
                delegate
                {
                    var x = Native.Document.body.ToXElementClone();

                    var nodes = x.Nodes().ToArray();

                    //Console.WriteLine("nodes: " + new { nodes.Length });


                    var toclean = x.Nodes().SkipWhile(
                         k =>
                         {
                             //Console.WriteLine("looking at: " + new { k });

                             var script = k as XElement;
                             if (script != null)
                             {
                                 if (script.Name.LocalName == "script")
                                 {
                                     //Console.WriteLine("done looking, found: " + new { script });
                                     return false;
                                 }
                             }

                             return true;
                         }
                     ).ToArray();

                    //Console.WriteLine("toclean: " + new { toclean.Length });

                    toclean.WithEach(
                        k => k.Remove()
                    );


                    return x;
                };
            #endregion

            var oldxhtml = AppContentAsXElement();



            new ScriptCoreLib.JavaScript.Runtime.Timer(
                t =>
                {
                    var newxhtml = AppContentAsXElement();


                    if (oldxhtml.ToString() != newxhtml.ToString())
                    {
                        Console.WriteLine("modified! right?");
                        Console.WriteLine("oldxhtml: \n\n" + oldxhtml);
                        Console.WriteLine("newxhtml: \n\n" + newxhtml);

                        var now = DateTime.Now;

                        // the document was updated. now lets also update time code
                        Native.Document.body.AsXElement().Add(
                            new XAttribute(
                                "data-modified",
                                "" + now.Ticks
                              )
                        );

                        oldxhtml = AppContentAsXElement();
                        //Console.WriteLine(oldxhtml);
                    }

                    Console.WriteLine("stopping timer");
                    t.Stop();
                    service.Update(
                        oldxhtml,
                        xml =>
                        {
                            if (xml.Name.LocalName == "body")
                            {
                                // update!
                                // this will remove all attached events.

                                Console.WriteLine(xml);
                                //Debugger.Break();

                                Native.Document.body.AsXElement().ReplaceAll(xml);
                                oldxhtml = AppContentAsXElement();
                            }

                            Console.WriteLine("restarting timer");
                            t.StartInterval(5000);
                        }
                    );
                }
            )
                .StartInterval(5000)
            ;

            var xxml = "<xbody>foo<button>x<button></xbody>";
            // body is special
            //var xxml = "<body>foo</body>";

            var xx = new IHTMLDiv().AttachToDocument();


            xx.AsXElement().ReplaceWith(XElement.Parse(xxml));


            Native.Document.DesignMode = true;
        }

    }
}
