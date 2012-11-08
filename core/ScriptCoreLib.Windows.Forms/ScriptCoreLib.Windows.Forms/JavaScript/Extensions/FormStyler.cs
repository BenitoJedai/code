using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ScriptCoreLib.JavaScript.Extensions
{
    [Script]
    public class FormStyler
    {
        public Form Context;

        public static Action<FormStyler> AtFormCreated;

        public IHTMLDiv CloseButtonContent;
        public IHTMLDiv CloseButton;

        public IHTMLDiv Caption;

        public IHTMLDiv TargetOuterBorder;
        public IHTMLDiv TargetInnerBorder;


        public IHTMLDiv TargetResizerPadding;

        public static void RaiseAtFormCreated(FormStyler s)
        {
            if (AtFormCreated != null)
                AtFormCreated(s);
        }

        static FormStyler()
        {
            AtFormCreated = LikeWindowsClassic;
        }

        public static void LikeWindowsClassic(FormStyler s)
        {

        }

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

            s.Caption.style.backgroundColor = JSColor.FromRGB(0, 122, 204);
        }


    }
}
