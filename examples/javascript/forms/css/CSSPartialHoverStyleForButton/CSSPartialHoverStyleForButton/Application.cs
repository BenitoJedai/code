using CSSPartialHoverStyleForButton;
using CSSPartialHoverStyleForButton.Design;
using CSSPartialHoverStyleForButton.HTML.Pages;
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
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace CSSPartialHoverStyleForButton
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {
        public readonly ApplicationControl content = new ApplicationControl();

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            // inspired by
            // http://www.spreaker.com/user/latenightinthemidlands/world-watchers-03-19-2014

            //Native.css[typeof(GroupBox)].descendant[typeof(Button)] > IHTMLElement.HTMLElementEnum.button
            new IStyle(Native.css[typeof(GroupBox)].descendant[typeof(Button)] + IHTMLElement.HTMLElementEnum.button)
            {
                transition = "background-color 200ms linear",
                backgroundColor = "transparent",

                textDecoration = "uppercase",
                border = "0px"
            };

            new IStyle(Native.css[typeof(GroupBox)].hover.descendant[typeof(Button)] + IHTMLElement.HTMLElementEnum.button)
            {
                backgroundColor = "gray",

            };

            new IStyle((Native.css[typeof(GroupBox)].hover.descendant[typeof(Button)] + IHTMLElement.HTMLElementEnum.button).hover)
            {
                backgroundColor = "black",
                color = "white",
            };


            content.BackColor = Color.White;
            content.AttachControlToDocument();



            new IHTMLAnchor { "drag me" }.AttachTo(Native.document.documentElement).With(
                dragme =>
                {
                    dragme.style.position = IStyle.PositionEnum.@fixed;
                    dragme.style.left = "1em";
                    dragme.style.bottom = "1em";

                    dragme.style.zIndex = 1000;

                    dragme.AllowToDragAsApplicationPackage();
                }
            );
        }

    }
}
