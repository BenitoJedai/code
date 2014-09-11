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
        // tested by
        // X:\jsc.svn\examples\javascript\chrome\apps\ChromeGalaxyS\ChromeGalaxyS\Application.cs

        // Message	3	The designer has fixed inconsistent type names in a partial class.  
        // The partial class name 'ScriptCoreLib.JavaScript.Windows.Forms' has been changed to 'Abstractatech.JavaScript.Forms.FloatStyler'.		0	0	

        public static void LikeFloat(FormStyler s)
        {
            // border-radius:25px;

            //dynamic TargetOuterBorder_style = s.TargetOuterBorder.style;
            //dynamic TargetInnerBorder_style = s.TargetInnerBorder.style;

            var TargetBackground = new IHTMLDiv();

            //dynamic TargetBackground_style = TargetBackground.style;

            new IStyle(TargetBackground)
            {
                position = IStyle.PositionEnum.absolute,
                left = "0px",
                top = "0px",
                right = "0px",
                bottom = "0px",
                Opacity = 0.7
            };


            s.TargetOuterBorder.parentNode.insertBefore(TargetBackground, s.TargetOuterBorder.parentNode.firstChild);



            s.TargetOuterBorder.style.boxShadow = "rgba(0, 0, 0, 0.2) 4px 4px 4px 0px";
            s.TargetOuterBorder.style.borderColor = "black";
            s.TargetOuterBorder.style.borderWidth = "0px";


            new IStyle(s.CaptionContent)
            {
                paddingLeft = "12px",
                paddingRight = "30px",
                left = "",
                right = "0px",
                height = "27px",
                backgroundColor = "  rgba(0,0,0,0.7)",
            };

            s.TargetInnerBorder.style.borderWidth = "0px";

            s.CloseButton.style.color = JSColor.Red;
            s.CloseButton.style.backgroundColor = JSColor.None;
            s.CloseButton.style.borderWidth = "0px";
            s.CloseButtonContent.style.borderWidth = "0px";

            new IStyle(s.TargetResizerPadding)
            {
                left = "0px",
                top = "0px",
                right = "0px",
                bottom = "0px"
            };



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
