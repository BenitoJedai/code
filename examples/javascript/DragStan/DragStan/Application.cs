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
using DragStan.Design;
using DragStan.HTML.Pages;
using ScriptCoreLib.Shared.Drawing;
using ScriptCoreLib.JavaScript.Runtime;

namespace DragStan
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
        public Application(IDefaultPage page)
        {
            HoverManager m = new HoverManager();

            StreamLoremIpsum(Native.Document.body);

            new HoverElement("this is a hover text 1", m).AttachTo(Native.Document.body);

            StreamLoremIpsum(Native.Document.body);

            new HoverElement("this is a hover text 2", m).AttachTo(Native.Document.body);

            StreamLoremIpsum(Native.Document.body);

            new HoverElement("this is a hover text 3", m).AttachTo(Native.Document.body);

            StreamLoremIpsum(Native.Document.body);




            @"Hello world".ToDocumentTitle();
            // Send data from JavaScript to the server tier
            service.WebMethod2(
                @"A string from JavaScript.",
                value => value.ToDocumentTitle()
            );
        }

        static string LoremIpsum = @"Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.";

        public static void StreamLoremIpsum(INode e)
        {
            e.appendChild(new IHTMLSpan(LoremIpsum));
        }


    }

    [Script]
    public class HoverElement
    {
        public IHTMLImage Image = new HTML.Images.FromAssets.help_blue();
        public IHTMLImage ImageStan = new HTML.Images.FromAssets.stan();

        public ITextNode TextContent = new ITextNode();
        public IHTMLElement Element = new IHTMLSpan();
        public IHTMLElement ElementHeader = new IHTMLSpan();

        bool bISDragging;

        bool bDead;

        bool isClone;

        public HoverElement Clone()
        {
            HoverElement n = new HoverElement(TextContent.nodeValue, Manager);

            isClone = true;

            Element.style.padding = "4px";
            Element.style.border = "1px dotted blue";

            Element.insertPreviousSibling(n.Element);
            Element.appendChild(new IHTMLElement(IHTMLElement.HTMLElementEnum.hr));
            Element.appendChild(ImageStan);

            ImageStan.style.Float = IStyle.FloatEnum.left;

            Element.style.position = IStyle.PositionEnum.absolute;

            Element.parentNode.appendChild(Element);


            return n;
        }

        int cTTL = 3;

        HoverManager Manager;

        static bool IsNodeOrChildOfNode(INode owner, INode e)
        {
            INode p = e;

            while (p != null)
            {
                if (p == owner)
                    return true;

                p = p.parentNode;
            }

            return false;
        }


        public HoverElement(string text, HoverManager m)
        {



            TextContent.nodeValue = text;

            Manager = m;

            ElementHeader.appendChild(Image, TextContent);
            ElementHeader.style.cursor = IStyle.CursorEnum.pointer;

            Element.appendChild(ElementHeader);

            Element.style.position = IStyle.PositionEnum.relative;
            ElementHeader.style.fontWeight = "bold";
            Element.style.padding = "2px";

            ImageStan.style.cursor = IStyle.CursorEnum.help;

            ImageStan.onclick +=
                delegate
                {
                    Fader.FadeAndRemove(ImageStan, 9000, 400);

                    Application.StreamLoremIpsum(Element);

                    Element.style.width = "400px";
                    Element.style.overflow = IStyle.OverflowEnum.hidden;
                };

            Native.Document.body.onmousedown
                += e =>
                {
                    bool b = IsNodeOrChildOfNode(ElementHeader, e.Element);

                    if (b && !bDead)
                    {
                        if (!isClone)
                        {
                            Clone();
                        }

                        Element.style.backgroundColor = Color.Black;
                        Element.style.color = Color.White;
                        Element.style.Opacity = 0.5;

                        bISDragging = true;

                        Element.SetCenteredLocation(e.CursorX, e.CursorY);

                        e.CaptureMouse();
                        //e.PreventDefault(); 
                        //e.StopPropagation();

                        Element.parentNode.appendChild(Element);

                    }
                };

            Native.Document.onmouseup
                += delegate(IEvent e)
                {
                    if (bISDragging)
                    {
                        Element.style.backgroundColor = Color.White;
                        Element.style.color = Color.Black;


                        bISDragging = false;
                        Element.style.Opacity = 1;


                        bDead = !(--cTTL > 0);

                        if (bDead)
                            Fader.FadeAndRemove(Element);

                        e.PreventDefault();
                        e.StopPropagation();
                    }
                };

            Native.Document.onmousemove +=
                delegate(IEvent e)
                {
                    if (bISDragging)
                    {
                        Element.SetCenteredLocation(e.CursorX, e.CursorY);

                        e.PreventDefault();
                        e.StopPropagation();


                    }
                };

            Element.oncontextmenu +=
                delegate(IEvent e)
                {
                    e.PreventDefault();
                };

            Element.onmouseover +=
                delegate
                {
                    if (!bDead && !bISDragging)
                        ElementHeader.style.color = Color.Red;
                };

            Element.onmouseout +=
                delegate
                {
                    if (!bDead && !bISDragging)
                        ElementHeader.style.color = "";
                };
        }

        public void AttachTo(INode e)
        {
            e.appendChild(Element);
        }


    }

    [Script]
    public class HoverManager
    {

    }

}
