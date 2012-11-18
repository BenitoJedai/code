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

        public static XElement WithModifiedAttribute(this XElement e)
        {
            var now = DateTime.Now;

            e.Add(

                new XAttribute(
                    "data-modified",
                    "" + now.Ticks
                  )
                  );

            return e;
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

                    x.Nodes().SkipWhile(
                         k =>
                         {
                             var script = k as XElement;
                             if (script != null)
                                 if (script.Name.LocalName == "script")
                                     return false;

                             return true;
                         }
                     ).ToArray().WithEach(
                        k => k.Remove()
                    );


                    return x;
                };
            #endregion

            var oldxhtml = AppContentAsXElement();



            Console.WriteLine(oldxhtml);


            var oldsource = oldxhtml.ToString();

            new ScriptCoreLib.JavaScript.Runtime.Timer(
                delegate
                {
                    var newxhtml = AppContentAsXElement();

                    var newsource = newxhtml.ToString();

                    if (oldsource != newsource)
                    {
                        Console.WriteLine("modified! right?");
                        Console.WriteLine("oldxhtml: \n\n" + oldxhtml);
                        Console.WriteLine("newxhtml: \n\n" + newxhtml);

                        oldxhtml = newxhtml.WithModifiedAttribute();
                        //Console.WriteLine(oldxhtml);
                    }


                    service.Update(
                        oldxhtml,
                        delegate
                        {

                        }
                    );
                }
            ).StartInterval(5000);

            Native.Document.DesignMode = true;
        }

    }
}
