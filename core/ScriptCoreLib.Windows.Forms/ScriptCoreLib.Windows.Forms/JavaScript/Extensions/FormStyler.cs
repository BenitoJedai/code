using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ScriptCoreLib.JavaScript.Windows.Forms;

namespace ScriptCoreLib.JavaScript.Extensions
{
    [Script]
    public class FormStyler
    {
        // X:\jsc.svn\examples\javascript\chrome\apps\ChromeNexus7\ChromeNexus7\Application.cs
        // nuget plus shadow dom to do tab flip in effect for alpha chrome AppWindow

        public Form Context;

        public static Action<FormStyler> AtFormCreated;

        public IHTMLDiv CloseButtonContent;
        public IHTMLDiv CloseButton;

        public IHTMLDiv Caption;

        public IHTMLElement CaptionContent;

        public IHTMLDiv CaptionShadow;

        public IHTMLDiv TargetOuterBorder;
        public IHTMLDiv TargetInnerBorder;

        public IHTMLDiv ContentContainerPadding;

        public IHTMLDiv TargetResizerPadding;

        public static void RaiseAtFormCreated(FormStyler s)
        {
            if (AtFormCreated != null)
                AtFormCreated(s);
        }

        static FormStyler()
        {
            AtFormCreated = LikeWindows98;
        }

        public static void LikeWindowsClassic(FormStyler s)
        {

        }

        public static void LikeWindows98(FormStyler s)
        {
            // tested by X:\jsc.svn\examples\javascript\forms\MSVSFormStyle\MSVSFormStyle\Application.cs

            // http://css-tricks.com/examples/CSS3Gradient/
            // http://www.codeguru.com/cpp/misc/misc/titlebar/article.php/c387/Win98-like-Gradient-Caption-Bar.htm
            //= RGB(16, 132, 208)


            s.Caption.style.background = "-webkit-linear-gradient(left, rgb(0, 0, 127), rgb(16, 132, 208))";
        }

        public static void LikeWindows3(FormStyler s)
        {

            s.TargetOuterBorder.style.boxShadow = "";
            s.TargetOuterBorder.style.borderColor = JSColor.Black;
            s.TargetOuterBorder.style.backgroundColor = JSColor.FromGray(0xc0);

            s.Caption.style.backgroundColor = JSColor.FromRGB(0, 0, 127);
            s.Caption.style.borderBottom = "1px solid black";

            s.CloseButton.style.right = "0px";
            s.CloseButton.style.top = "0px";
            s.CloseButton.style.backgroundColor = JSColor.FromGray(0xc0);

            s.CloseButton.style.lineHeight = "24px";
            s.CloseButton.style.height = "24px";
            s.CloseButton.style.width = "24px";

            s.CloseButton.style.borderLeftColor = JSColor.White;
            s.CloseButton.style.borderTopColor = JSColor.White;
            s.CloseButton.style.borderRightColor = JSColor.FromGray(0x80);
            s.CloseButton.style.borderBottomColor = JSColor.FromGray(0x80);


            s.CloseButtonContent.style.borderLeft = "0px";
            s.CloseButtonContent.style.borderTop = "0px";
            s.CloseButtonContent.style.borderRightColor = JSColor.FromGray(0x80);
            s.CloseButtonContent.style.borderBottomColor = JSColor.FromGray(0x80);

            //s.CloseButton.style.color = JSColor.White;
            //s.CloseButton.style.backgroundColor = JSColor.None;
            //s.CloseButton.style.borderWidth = "0px";
            //s.CloseButtonContent.style.borderWidth = "0px";

            s.TargetInnerBorder.style.borderColor = JSColor.Black;
            s.TargetInnerBorder.style.left = "2px";
            s.TargetInnerBorder.style.top = "2px";
            s.TargetInnerBorder.style.right = "2px";
            s.TargetInnerBorder.style.bottom = "2px";

            s.TargetResizerPadding.style.left = "0px";
            s.TargetResizerPadding.style.top = "0px";
            s.TargetResizerPadding.style.right = "0px";
            s.TargetResizerPadding.style.bottom = "0px";


            //dynamic style = s.CaptionContent.style;

            //style.fontFamily = "System, sans-serif;";

            s.CaptionContent.style.textAlign = DOM.IStyle.TextAlignEnum.center;
            //s.CaptionContent.style.fontFamily = DOM.IStyle.FontFamilyEnum.s
            //            font-family: System, sans-serif;
            //font-size: 20px;
        }

        [Obsolete("this is the first style to demonstrate sub component restyling")]
        public static void LikeVisualStudioMetro(FormStyler s)
        {
            s.TargetOuterBorder.style.boxShadow = "rgba(0, 122, 204, 0.3) 0px 0px 6px 3px";
            s.TargetOuterBorder.style.borderColor = JSColor.FromRGB(0, 122, 204);

            s.TargetInnerBorder.style.borderWidth = "0px";

            s.CloseButton.style.color = JSColor.White;
            s.CloseButton.style.backgroundColor = JSColor.None;
            s.CloseButton.style.borderWidth = "0px";
            s.CloseButtonContent.style.borderWidth = "0px";

            s.TargetResizerPadding.style.left = "0px";
            s.TargetResizerPadding.style.top = "0px";
            s.TargetResizerPadding.style.right = "0px";
            s.TargetResizerPadding.style.bottom = "0px";

            s.ContentContainerPadding.style.top = "26px";


            s.Caption.style.backgroundColor = JSColor.FromRGB(0, 122, 204);


            // this is the first forms styler
            // that restyles the decendnt controls

            //X:\jsc.svn\examples\javascript\forms\css\CSSPartialHoverStyleForButton\CSSPartialHoverStyleForButton\Application.cs
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201404/20140409

            //new IStyle(Native.css[s.Context].descendant[typeof(Button)] + IHTMLElement.HTMLElementEnum.button)
            // 4:72ms path not found! 
            // why are we trying to find the path from element to descendants?
            // can we work around to set this style for all forms then?

            //new IStyle(Native.css[s.Context.GetHTMLTarget()].descendant[typeof(Button)] + IHTMLElement.HTMLElementEnum.button)
            //new IStyle(Native.css[s.Context.GetHTMLTarget().css].descendant[typeof(Button)] + IHTMLElement.HTMLElementEnum.button)

            new IStyle(Native.css[typeof(Form)].descendant[typeof(Button)] + IHTMLElement.HTMLElementEnum.button)
            {
                transition = "background-color 200ms linear",
                //backgroundColor = "rgba(0, 122, 204, 0.3)",
                backgroundColor = "rgba(0, 122, 204, 0.0)",

                //textDecoration = "uppercase",
                border = "0px",

                cursor = IStyle.CursorEnum.pointer
            };

            new IStyle(Native.css[typeof(Form)].hover.descendant[typeof(Button)] + IHTMLElement.HTMLElementEnum.button)
            {
                backgroundColor = "rgba(0, 122, 204, 0.7)",

            };

            new IStyle((Native.css[typeof(Form)].hover.descendant[typeof(Button)] + IHTMLElement.HTMLElementEnum.button).hover)
            {
                backgroundColor = "rgba(0, 122, 204, 1.0)",
                color = "white",
            };
        }


    }
}
