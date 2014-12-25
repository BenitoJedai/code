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
using MultiMouse.SVGCursor.Design;
using MultiMouse.SVGCursor.HTML.Pages;
using MultiMouse.SVGCursor.HTML.Images.FromAssets;
using ScriptCoreLib.JavaScript.DOM.SVG;

namespace MultiMouse.SVGCursor.HTML.Images.FromAssets
{
    public class __svg
    {
        public IHTMLElement Element;

        public IHTMLImage HTMLImage;
        public string SVGSource;
        public ISVGSVGElement SVGElement;
    }

    public class __svg_cursor1 : __svg
    {
        public Action<int, int, int> fill;
    }

    public static class IHTMLImageExtensions
    {
        public static __svg_cursor1 ToSVG(this cursor1 c)
        {
            var n = new __svg_cursor1 { HTMLImage = c, Element = c };

            Action<int, int, int> internal_fill = null;

            n.fill = (r, g, b) => { if (internal_fill != null) internal_fill(r, g, b); };

            new IXMLHttpRequest(ScriptCoreLib.Shared.HTTPMethodEnum.GET,
                 c.src,
                 rrr =>
                 {
                     n.SVGSource = rrr.responseText;

                     n.SVGElement = (ISVGSVGElement)rrr.responseXML.documentElement.AsXElement().AttachTo(c.parentNode);

                     // copy id, style?
                     n.Element = n.SVGElement;

                     n.HTMLImage.ReplaceWith(n.SVGElement);

                     internal_fill = (r, g, b) =>
                     {
                         n.SVGElement.AsXElement().Elements("g").Elements("path").WithEach(
                                path =>
                                    path.Attribute("style").With(
                                        style =>
                                        {
                                            var rr = ((byte)(r)).ToString("x2");
                                            var gg = ((byte)(g)).ToString("x2");
                                            var bb = ((byte)(b)).ToString("x2");


                                            style.Value = "fill:#" + rr + gg + bb + ";" + style.Value.SkipUntilIfAny(";");
                                        }
                                    )
                            );
                     };
                 }
             );

            return n;
        }

        public static __svg ToSVG(this IHTMLImage c)
        {
            var n = new __svg { HTMLImage = c };

            new IXMLHttpRequest(ScriptCoreLib.Shared.HTTPMethodEnum.GET,
                 c.src,
                 r =>
                 {
                     n.SVGSource = r.responseText;

                     n.SVGElement = (ISVGSVGElement)r.responseXML.documentElement.AsXElement().AttachTo(c.parentNode);

                     n.HTMLImage.ReplaceWith(n.SVGElement);

                 }
             );

            return n;
        }

    }

}

namespace MultiMouse.SVGCursor
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
            page.cursor1.ToSVG().With(
                svg =>
                {
                    Native.document.onmousemove +=
                        e =>
                        {
                            svg.SVGElement.AsXElement().Elements("g").Elements("path").WithEach(
                                path =>
                                    path.Attribute("style").With(
                                        style =>
                                        {
                                            var rr = ((byte)(e.CursorX * 255 / Native.window.Width)).ToString("x2");

                                            Console.WriteLine(rr);

                                            style.Value = "fill:#00" + rr + "00;" + style.Value.SkipUntilIfAny(";");
                                        }
                                    )
                            );


                            Native.document.documentElement.style.cursorImage = svg.SVGElement;

                        };
                }
            );

            new cursor1().AttachToDocument().ToSVG().With(
                svg =>
                {
                    Native.Document.onmousemove +=
                        e =>
                        {
                            var rr = ((byte)(e.CursorX * 255 / Native.window.Width));

                            svg.fill(rr, rr, rr);
                        };
                }
            );

            new IXMLHttpRequest(ScriptCoreLib.Shared.HTTPMethodEnum.GET,
                new cursor1().src,
                r =>
                {
                    //Console.WriteLine(r.responseText);

                    r.responseXML.documentElement.AsXElement().Clone().With(
                        source =>
                            source.Elements("g").Elements("path").WithEach(
                                path =>
                                     path.Attribute("style").With(
                                        style =>
                                        {
                                            style.Value = style.Value.Replace("fill:#0000ff;", "fill:#007f00;");
                                        }
                                    )
                            )
                    ).AttachTo();

                    var cursor1 = r.responseXML.documentElement.AsXElement().Clone().With(
                         source =>
                             source.Elements("g").Elements("path").WithEach(
                                 path =>
                                      path.Attribute("style").With(
                                         style =>
                                         {
                                             style.Value = style.Value.Replace("fill:#0000ff;", "fill:#7f0000;");
                                         }
                                     )
                             )
                     ).AttachToDocument();

                    cursor1.With(
                        red =>
                        {
                            red.style.position = IStyle.PositionEnum.absolute;
                            red.style.right = "0px";

                            new IHTMLButton { innerText = "dynamic color!" }.AttachToDocument().onclick +=
                                delegate
                                {
                                    Native.Document.onmousemove +=
                                        e =>
                                        {
                                            red.AsXElement().Elements("g").Elements("path").WithEach(
                                                path =>
                                                    path.Attribute("style").With(
                                                        style =>
                                                        {
                                                            var rr = ((byte)(e.CursorX * 255 / Native.window.Width)).ToString("x2");

                                                            Console.WriteLine(rr);

                                                            style.Value = "fill:#" + rr + "0000;" + style.Value.SkipUntilIfAny(";");
                                                        }
                                                    )
                                            );
                                        };
                                };
                        }
                     );
                }
            );
        }

    }

    public static class X
    {
        public static IHTMLElement AttachTo(this XElement value, INode e = null)
        {
            if (e == null)
                e = Native.Document.body;

            var c = default(IHTMLDiv);

            if (e.ownerDocument != null)
            {
                c = (IHTMLDiv)e.ownerDocument.createElement("div");
            }
            else
            {
                c = new IHTMLDiv();
            }

            c.innerHTML = value.ToString();

            var x = c.firstChild;

            //Console.WriteLine("attach xml");

            e.appendChild(x);

            return (IHTMLElement)x;
        }

        public static IHTMLElement AttachToDocument(this  XElement e)
        {
            return e.AttachTo(Native.Document.body);
        }
    }
}
