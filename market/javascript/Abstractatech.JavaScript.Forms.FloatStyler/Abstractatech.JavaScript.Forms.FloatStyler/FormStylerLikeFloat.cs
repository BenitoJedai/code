using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace System.Windows.Forms
{
    public static class FormStylerLikeFloat
    {
        // Message	3	The designer has fixed inconsistent type names in a partial class.  
        // The partial class name 'ScriptCoreLib.JavaScript.Windows.Forms' has been changed to 'Abstractatech.JavaScript.Forms.FloatStyler'.		0	0	

        public static void LikeFloat(FormStyler s)
        {
            // border-radius:25px;

            dynamic TargetOuterBorder_style = s.TargetOuterBorder.style;
            dynamic TargetInnerBorder_style = s.TargetInnerBorder.style;

            var TargetBackground = new IHTMLDiv();

            dynamic TargetBackground_style = TargetBackground.style;


            TargetBackground.style.position = IStyle.PositionEnum.absolute;
            TargetBackground.style.left = "0px";
            TargetBackground.style.top = "0px";
            TargetBackground.style.right = "0px";
            TargetBackground.style.bottom = "0px";
            TargetBackground.style.Opacity = 0.7;


            s.TargetOuterBorder.parentNode.insertBefore(TargetBackground, s.TargetOuterBorder.parentNode.firstChild);



            s.TargetOuterBorder.style.boxShadow = "rgba(0, 0, 0, 0.2) 4px 4px 4px 0px";
            s.TargetOuterBorder.style.borderColor = "black";
            s.TargetOuterBorder.style.borderWidth = "0px";


            s.CaptionContent.style.paddingLeft = "12px";
            s.CaptionContent.style.paddingRight = "30px";
            s.CaptionContent.style.left = "";
            s.CaptionContent.style.right = "0px";
            s.CaptionContent.style.height = "27px";
            s.CaptionContent.style.backgroundColor = "  rgba(0,0,0,0.7)";

            s.TargetInnerBorder.style.borderWidth = "0px";

            s.CloseButton.style.color = JSColor.Red;
            s.CloseButton.style.backgroundColor = JSColor.None;
            s.CloseButton.style.borderWidth = "0px";
            s.CloseButtonContent.style.borderWidth = "0px";


            s.TargetResizerPadding.style.left = "0px";
            s.TargetResizerPadding.style.top = "0px";
            s.TargetResizerPadding.style.right = "0px";
            s.TargetResizerPadding.style.bottom = "0px";


            s.TargetInnerBorder.style.backgroundColor = "";


            s.Context.LocationChanged +=
               delegate
               {
                   if (s.Context.WindowState == FormWindowState.Normal)
                       s.TargetOuterBorder.style.boxShadow = "rgba(0, 0, 0, 0.2) 4px 4px 4px 0px";
                   else
                   {
                       s.TargetOuterBorder.style.boxShadow = "";
                   }
               };

            s.ContentContainerPadding.style.backgroundColor = "  rgba(0,0,0,0.7)";

            //s.Caption.style.left = "";
            s.Caption.style.backgroundColor = "";
        }
    }
}
